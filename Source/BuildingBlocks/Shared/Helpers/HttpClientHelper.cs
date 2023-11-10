using Newtonsoft.Json;
using Polly;
using Polly.CircuitBreaker;
using Polly.Retry;
using Shared.Constants;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace Shared.Helpers;

public class HttpClientHelper
{
    private readonly IHttpClientFactory _httpClientFactory;
    private readonly AsyncCircuitBreakerPolicy _circuitBreakerPolicy;
    private readonly AsyncRetryPolicy _retryPolicy;
    private readonly TimeSpan _timeout = TimeSpan.FromSeconds(30);

    public HttpClientHelper(IHttpClientFactory httpClientFactory,
        TimeSpan circuitBreakerOpenDuration,
        int circuitBreakerThreshold,
        int retryAttempts)
    {
        _httpClientFactory = httpClientFactory;

        #region Circuit Breaker Policy
        _circuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(circuitBreakerThreshold,
                circuitBreakerOpenDuration,
                onBreak: (ex, breakDelay) =>
                {
                    Console.WriteLine($"Circuit breaker opened due to {ex.Message}. Waiting {breakDelay.TotalSeconds} seconds before trying again.");
                },
                onReset: () => Console.WriteLine("Circuit breaker closed."),
                onHalfOpen: () => Console.WriteLine("Circuit breaker half-opened.")
            );
        #endregion

        #region Retry Policy
        _retryPolicy = Policy.Handle<Exception>()
            .WaitAndRetryAsync(retryAttempts, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                onRetry: (ex, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Retrying {retryCount} after {timeSpan.TotalSeconds} seconds due to {ex.Message}.");
                });
        #endregion
    }

    public async Task<TResponse> SendAsync<TRequest, TResponse>(HttpMethod method,
        string url,
        TRequest request = default,
        string contentType = ContentTypes.JSON,
        Dictionary<string, string> headers = null,
        Dictionary<string, string> queryParams = null)
    {
        #region Definitions
        var httpClient = _httpClientFactory.CreateClient();
        HttpContent httpContent = null;
        #endregion

        #region Handle Header & QueryParams
        if (headers != null)
        {
            foreach (var (key, value) in headers)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }
        }

        if (queryParams != null)
        {
            url = AppendQueryString(url, queryParams);
        }
        #endregion

        #region Handle HttpContent
        if (request != null)
        {
            switch (contentType)
            {
                case ContentTypes.MULTIPART_FORM_DATA:
                    httpContent = GetMultipartFormDataContent(request);
                    break;

                case ContentTypes.FORM_URL_ENCODED:
                    httpContent = GetFormUrlEncodedContent(request);                    
                    break;

                case ContentTypes.TEXT:
                case ContentTypes.JAVASCRIPT:
                case ContentTypes.HTML:
                case ContentTypes.XML:
                    httpContent = new StringContent(request.ToString(), Encoding.UTF8, contentType);
                    break;

                case ContentTypes.JSON:
                    httpContent = new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, contentType);
                    break;

                default:
                    throw new ArgumentException($"Unsupported content type: {contentType}", nameof(contentType));
            }
        }
        #endregion

        #region Execute & Handle Response
        return await _circuitBreakerPolicy
            .WrapAsync(_retryPolicy)
            .ExecuteAsync(async () =>
            {
                using (CancellationTokenSource cancellationTokenSource = new(_timeout))
                {
                    var requestMessage = new HttpRequestMessage
                    {
                        Method = method,
                        RequestUri = new Uri(url),
                        Content = httpContent
                    };

                    HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationTokenSource.Token);

                    if (!response.IsSuccessStatusCode)
                    {
                        await HandleErrorResponse(response);
                    }

                    return await HandleResponse<TResponse>(response);
                }
            });
        #endregion
    }

    private string AppendQueryString(string url, Dictionary<string, string> queryParams)
    {
        UriBuilder uriBuilder = new(url);
        NameValueCollection nameValueCollection = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var (key, value) in queryParams)
        {
            nameValueCollection[key] = value;
        }

        uriBuilder.Query = nameValueCollection.ToString();
        return uriBuilder.ToString();
    }

    private MultipartFormDataContent GetMultipartFormDataContent<TRequest>(TRequest request)
    {
        MultipartFormDataContent multipartFormDataContent = new();

        // Get the properties of the model
        PropertyInfo[] properties = request.GetType().GetProperties();
        foreach (var property in properties)
        {
            // Get the property and value
            string propertyName = property.Name;
            object propertyValue = property.GetValue(request);

            // If the value is not null, add it as a form field
            if (propertyValue != null)
            {
                // If it is a file, add it as StreamContent, otherwise add it as StringContent
                if (property.PropertyType == typeof(Stream))
                {
                    multipartFormDataContent.Add(new StreamContent((Stream)propertyValue), propertyName, $"{propertyName}.png");
                }
                else
                {
                    multipartFormDataContent.Add(new StringContent(propertyValue.ToString()), propertyName);
                }
            }
        }

        return multipartFormDataContent;
    }

    private FormUrlEncodedContent GetFormUrlEncodedContent<TRequest>(TRequest request)
    {
        Dictionary<string, string> collection = request.GetType()
                  .GetProperties()
                  .ToDictionary(prop => prop.Name, prop => prop.GetValue(request)?.ToString());

        return new FormUrlEncodedContent(collection);
    }

    private async Task HandleErrorResponse(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        // Custom error handling strategy can be defined here.
        // For example, a special action can be taken based on a specific HTTP status code.

        switch (response.StatusCode)
        {
            case System.Net.HttpStatusCode.NotFound:
                // Perform a custom action
                Console.WriteLine($"Not Found: {content}");
                break;
            case System.Net.HttpStatusCode.InternalServerError:
                // Perform a custom action
                Console.WriteLine($"Internal Server Error: {content}");
                break;
            default:
                // Default action for other cases
                Console.WriteLine($"Unexpected error: {content}");
                break;
        }
    }

    private async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentType?.MediaType == ContentTypes.JSON)
            {
                return JsonConvert.DeserializeObject<T>(content);
            }
            else
            {
                // If the Content type is not JSON, an appropriate action must be taken here.
                // For example, you can return the content directly as a string.
                // Additionally, another conversion operation can be performed, it all depends on the need.
                return (T)Convert.ChangeType(content, typeof(T));
            }
        }
        else
        {
            throw new HttpRequestException($"Request failed with status code {response.StatusCode}. Content: {content}");
        }
    }
}
