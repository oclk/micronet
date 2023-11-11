using Moq;
using Newtonsoft.Json;
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