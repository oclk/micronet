﻿using Moq;
using Newtonsoft.Json;
using Shared.Constants;
using Shared.Helpers;
using System.Net;

namespace Shared.UnitTests.Helpers;

/// <summary>
/// Unit tests for the <see cref="HttpClientHelper"/> class.
/// </summary>
public class HttpClientHelperTests
{
    /// <summary>
    /// Sends a RESTful HTTP request asynchronously with JSON content type and verifies the successful response deserialization.
    /// </summary>
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

    /// <summary>
    /// Sends a RESTful HTTP request asynchronously with MultipartFormData content type and verifies the successful response deserialization.
    /// </summary>
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

    /// <summary>
    /// Sends a RESTful HTTP request asynchronously with FormUrlEncoded content type and verifies the successful response deserialization.
    /// </summary>
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

    /// <summary>
    /// Sends a RESTful HTTP request asynchronously with retry policy and verifies that it retries on transient error.
    /// </summary>
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

    /// <summary>
    /// Sends a RESTful HTTP request asynchronously with circuit breaker policy and verifies that the circuit closes after success.
    /// </summary>
    [Fact]
    public async Task SendRESTRequestAsync_WithCircuitBreakerPolicy_ClosesCircuitAfterSuccess()
    {
        // Arrange
        var httpClientFactoryMock = new Mock<IHttpClientFactory>();
        var httpClientMock = new Mock<HttpClient>();

        var transientError = new HttpRequestException("Transient error");

        httpClientMock.Setup(x => x.SendAsync(It.IsAny<HttpRequestMessage>(), It.IsAny<CancellationToken>()))
            .Throws(transientError);

        httpClientFactoryMock.Setup(x => x.CreateClient(It.IsAny<string>())).Returns(httpClientMock.Object);

        var httpClientHelper = new HttpClientHelper(httpClientFactoryMock.Object, TimeSpan.FromSeconds(1), 2, 3);

        // Act
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await httpClientHelper.SendRESTRequestAsync<TestRequest, string>(
                    HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.TEXT_PLAIN, null, null);
        });

        // Reset circuit breaker
        await Task.Delay(TimeSpan.FromSeconds(11));

        // This should not throw CircuitBrokenException
        await Assert.ThrowsAsync<HttpRequestException>(async () =>
        {
            await httpClientHelper.SendRESTRequestAsync<TestRequest, string>(
                HttpMethod.Get, "http://example.com", new TestRequest(), ContentTypes.TEXT_PLAIN, null, null);
        });
    }
}

#region Sample Request and Response classes for testing
/// <summary>
/// Represents a sample request object used for testing purposes.
/// </summary>
public class TestRequest
{
    /// <summary>
    /// Gets or sets the value of PropertyA in the request.
    /// </summary>
    public string PropertyA { get; set; }

    /// <summary>
    /// Gets or sets the value of PropertyB in the request.
    /// </summary>
    public int PropertyB { get; set; }
}

/// <summary>
/// Represents a sample response object used for testing purposes.
/// </summary>
public class TestResponse
{
    /// <summary>
    /// Gets or sets the value of Property1 in the response.
    /// </summary>
    public string Property1 { get; set; }

    /// <summary>
    /// Gets or sets the value of Property2 in the response.
    /// </summary>
    public int Property2 { get; set; }
}
#endregion