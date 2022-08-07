namespace SecondHand.Tests.Library.Handlers.Ad;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Handlers.Ad;
using SecondHand.Library.Queries.Ad;

public class GetAdByIdHandlerTest
{
    private Mock<IAdDataAccess>? _mockAdDataAccess;
    private GetAdByIdHandler? _getAdByIdHandler;

    private readonly GetAdByIdQuery _getAdByIdQuery = new(It.IsAny<int>());
    private readonly CancellationToken _cancellationToken = new();
    private readonly Models.Advertisement.Ad _ad = new();

    [SetUp]
    public void Setup()
    {
        _mockAdDataAccess = new Mock<IAdDataAccess>();
        _getAdByIdHandler = new GetAdByIdHandler(_mockAdDataAccess.Object);
    }

    [Test]
    public void Get_Success()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.GetAd(It.IsAny<int>())).Returns(_ad);

        // Act
        var result = _getAdByIdHandler?.Handle(_getAdByIdQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Advertisement.Ad>();
        _mockAdDataAccess?.Verify(x=>x.GetAd(It.IsAny<int>()),Times.Once);
    }

    [Test]
    public void Get_Failed()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.GetAd(It.IsAny<int>())).Returns((Models.Advertisement.Ad)null!);

        // Act
        var result = _getAdByIdHandler?.Handle(_getAdByIdQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAdDataAccess?.Verify(x=>x.GetAd(It.IsAny<int>()),Times.Once);
    }
}