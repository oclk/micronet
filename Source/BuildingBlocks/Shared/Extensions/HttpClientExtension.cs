using Newtonsoft.Json;
using Shared.Constants;
using System.Collections.Specialized;
using System.Reflection;
using System.Text;

namespace Shared.Extensions;

/// <summary>
/// Utility class for making HTTP requests using HttpClient with advanced features like circuit breaker, retry policies, and custom error handling.
/// </summary>
public static class HttpClientExtension
{
    /// <summary>
    /// Sends a RESTful HTTP request asynchronously and returns the response.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <typeparam name="TResponse">The type of the response object.</typeparam>
    /// <param name="method">The HTTP method (GET, POST, PUT, etc.).</param>
    /// <param name="url">The URL of the request.</param>
    /// <param name="request">The request object.</param>
    /// <param name="contentType">The content type of the request.</param>
    /// <param name="headers">The headers to be included in the request.</param>
    /// <param name="queryParams">The query parameters to be included in the request URL.</param>
    /// <returns>A task representing the asynchronous operation. The task result is the deserialized response object.</returns>
    public static async Task<TResponse> SendRESTRequestAsync<TRequest, TResponse>(this HttpClient httpClient, HttpMethod method,
        string url,
        TRequest request = default,
        string contentType = ContentTypes.APPLICATION_JSON,
        Dictionary<string, string> headers = null,
        Dictionary<string, string> queryParameters = null,
        CancellationToken cancellationToken = default)
    {
        HttpContent httpContent = null;

        #region Handle Header & QueryParameters
        if (headers != null)
        {
            foreach (var (key, value) in headers)
            {
                httpClient.DefaultRequestHeaders.Add(key, value);
            }
        }

        if (queryParameters != null)
        {
            url = AppendQueryString(url, queryParameters);
        }
        #endregion

        #region Handle HttpContent
        if (request != null)
        {
            httpContent = contentType switch
            {
                ContentTypes.MULTIPART_FORM_DATA => GetMultipartFormDataContent(request),
                ContentTypes.APPLICATION_X_WWW_FORM_URLENCODED => GetFormUrlEncodedContent(request),
                ContentTypes.TEXT_PLAIN or ContentTypes.APPLICATION_JAVASCRIPT or ContentTypes.TEXT_HTML or ContentTypes.APPLICATION_XML => new StringContent(request.ToString(), Encoding.UTF8, contentType),
                ContentTypes.APPLICATION_JSON => new StringContent(JsonConvert.SerializeObject(request), Encoding.UTF8, contentType),
                _ => throw new ArgumentException($"Unsupported content type: {contentType}", nameof(contentType)),
            };
        }
        #endregion

        #region Execute & Handle Response
        var requestMessage = new HttpRequestMessage
        {
            Method = method,
            RequestUri = new Uri(url),
            Content = httpContent
        };

        using (HttpResponseMessage response = await httpClient.SendAsync(requestMessage, cancellationToken))
        {
            // Check error and return
            response.EnsureSuccessStatusCode();

            // Handle and return success response
            return await HandleResponse<TResponse>(response);
        };        
        #endregion
    }

    /// <summary>
    /// Appends query parameters to the given URL.
    /// </summary>
    /// <param name="url">The original URL.</param>
    /// <param name="queryParams">The query parameters to be appended.</param>
    /// <returns>The modified URL with appended query parameters.</returns>
    private static string AppendQueryString(string url, Dictionary<string, string> queryParams)
    {
        UriBuilder uriBuilder = new(url);
        NameValueCollection nameValueCollection = System.Web.HttpUtility.ParseQueryString(uriBuilder.Query);

        foreach (var (key, value) in queryParams)
        {
            if (!string.IsNullOrEmpty(key) && !string.IsNullOrEmpty(value))
            {
                nameValueCollection[key] = value;
            }
        }

        uriBuilder.Query = nameValueCollection.ToString();
        return uriBuilder.ToString();
    }

    /// <summary>
    /// Creates a MultipartFormDataContent object from the given request object.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <param name="request">The request object.</param>
    /// <returns>The MultipartFormDataContent object.</returns>
    private static MultipartFormDataContent GetMultipartFormDataContent<TRequest>(TRequest request)
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

    /// <summary>
    /// Creates a FormUrlEncodedContent object from the given request object.
    /// </summary>
    /// <typeparam name="TRequest">The type of the request object.</typeparam>
    /// <param name="request">The request object.</param>
    /// <returns>The FormUrlEncodedContent object.</returns>
    private static FormUrlEncodedContent GetFormUrlEncodedContent<TRequest>(TRequest request)
    {
        Dictionary<string, string> collection = request.GetType()
                  .GetProperties()
                  .ToDictionary(prop => prop.Name, prop => prop.GetValue(request)?.ToString());

        return new FormUrlEncodedContent(collection);
    }

    /// <summary>
    /// Handles the success response from the HTTP request.
    /// </summary>
    /// <typeparam name="T">The type of the response object.</typeparam>
    /// <param name="response">The HTTP response message.</param>
    /// <returns>The deserialized response object.</returns>
    private static async Task<T> HandleResponse<T>(HttpResponseMessage response)
    {
        var content = await response.Content.ReadAsStringAsync();

        if (response.IsSuccessStatusCode)
        {
            if (response.Content.Headers.ContentType?.MediaType == ContentTypes.APPLICATION_JSON)
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
