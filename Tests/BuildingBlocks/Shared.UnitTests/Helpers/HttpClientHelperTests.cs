using Moq;
using Newtonsoft.Json;
using Polly;
using Shared.Constants;
using Shared.Helpers;
using System.Net;

namespace Shared.UnitTests.Helpers;

public class HttpClientHelperTests
{
    [Fact]
    public async Task SendRESTRequestAsync_JsonContentType_SuccessfulRequest_ReturnsDeserializedObject()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var expectedResponse = new TestResponse { Property1 = "Value1", Property2 = 42 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, ContentTypes.APPLICATION_JSON)
        };

        httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseMessage);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        var result = await httpClientHelper.SendRESTRequestAsync<TestRequest, TestResponse>(
            HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.APPLICATION_JSON, null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Property1, result.Property1);
        Assert.Equal(expectedResponse.Property2, result.Property2);
    }

    [Fact]
    public async Task SendRESTRequestAsync_MultipartFormDataContentType_SuccessfulRequest_ReturnsDeserializedObject()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var expectedResponse = new TestResponse { Property1 = "Value1", Property2 = 42 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, ContentTypes.APPLICATION_JSON)
        };

        httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseMessage);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        var result = await httpClientHelper.SendRESTRequestAsync<TestRequest, TestResponse>(
            HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.MULTIPART_FORM_DATA, null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Property1, result.Property1);
        Assert.Equal(expectedResponse.Property2, result.Property2);
    }

    [Fact]
    public async Task SendRESTRequestAsync_FormUrlEncodedContentType_SuccessfulRequest_ReturnsDeserializedObject()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var expectedResponse = new TestResponse { Property1 = "Value1", Property2 = 42 };
        var jsonResponse = JsonConvert.SerializeObject(expectedResponse);

        var responseMessage = new HttpResponseMessage(HttpStatusCode.OK)
        {
            Content = new StringContent(jsonResponse, System.Text.Encoding.UTF8, ContentTypes.APPLICATION_JSON)
        };

        httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(responseMessage);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        var result = await httpClientHelper.SendRESTRequestAsync<TestRequest, TestResponse>(
            HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.APPLICATION_X_WWW_FORM_URLENCODED, null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(expectedResponse.Property1, result.Property1);
        Assert.Equal(expectedResponse.Property2, result.Property2);
    }

    [Fact]
    public async Task SendRESTRequestAsync_WithRetryPolicy_RetriesOnTransientError()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var transientError = new HttpRequestException("Transient error");

        httpClientMock.SetupSequence(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Throws(transientError)
            .ReturnsAsync(new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new StringContent("Success", System.Text.Encoding.UTF8, ContentTypes.TEXT_PLAIN)
            });

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        var result = await httpClientHelper.SendRESTRequestAsync<TestRequest, string>(
            HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.TEXT_PLAIN, null, null);

        // Assert
        Assert.NotNull(result);
        Assert.Equal("Success", result);
    }

    [Fact]
    public async Task SendRESTRequestAsync_WithCircuitBreakerPolicy_ClosesCircuitAfterSuccess()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var circuitBreakerPolicy = Policy.Handle<Exception>()
            .CircuitBreakerAsync(2, TimeSpan.FromSeconds(10), onBreak: (ex, breakDelay) =>
            {
                Console.WriteLine($"Circuit breaker opened due to {ex.Message}. Waiting {breakDelay.TotalSeconds} seconds before trying again.");
            },
            onReset: () => Console.WriteLine("Circuit breaker closed."),
            onHalfOpen: () => Console.WriteLine("Circuit breaker half-opened.")
        );

        var transientError = new HttpRequestException("Transient error");

        httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Throws(transientError);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        await Assert.ThrowsAsync<Exception>(async () =>
        {
            await circuitBreakerPolicy.ExecuteAsync(async () =>
            {
                await httpClientHelper.SendRESTRequestAsync<TestRequest, string>(
                    HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.TEXT_PLAIN, null, null);
            });
        });

        // Circuit breaker should be open at this point

        // Reset circuit breaker
        await Task.Delay(TimeSpan.FromSeconds(11));

        await circuitBreakerPolicy.ExecuteAsync(async () =>
        {
            // This should not throw CircuitBrokenException
            await httpClientHelper.SendRESTRequestAsync<TestRequest, string>(
                HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.TEXT_PLAIN, null, null);
        });
    }
}

#region Sample Request and Response classes for testing
public class TestRequest
{
    public string PropertyA { get; set; }
    public int PropertyB { get; set; }
}

public class TestResponse
{
    public string Property1 { get; set; }
    public int Property2 { get; set; }
}
#endregion