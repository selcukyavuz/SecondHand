namespace SecondHand.Tests.Library.Handlers.Ad;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Handlers.Ad;
using SecondHand.Library.Queries.Ad;

public class GetAdListQueryHandlerTest
{
    private Mock<IAdDataAccess>? _mockAdDataAccess;

    private GetAdListQueryHandler? _getAdListQueryHandler;

    private readonly GetAdListQuery _getAdListQuery = new();
    private readonly CancellationToken _cancellationToken = new();
    private readonly List<Models.Advertisement.Ad> _ads = new();

    [SetUp]
    public void Setup()
    {
        _mockAdDataAccess = new Mock<IAdDataAccess>();
        _getAdListQueryHandler = new GetAdListQueryHandler(_mockAdDataAccess.Object);
    }

    [Test]
    public void Get_Success()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.GetAd()).Returns(_ads);

        // Act
        var result = _getAdListQueryHandler?.Handle(_getAdListQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<List<Models.Advertisement.Ad>>();
        _mockAdDataAccess?.Verify(x=>x.GetAd(),Times.Once);
    }

    [Test]
    public void Get_Failed()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.GetAd()).Returns((List<Models.Advertisement.Ad>)null!);

        // Act
        var result = _getAdListQueryHandler?.Handle(_getAdListQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAdDataAccess?.Verify(x=>x.GetAd(),Times.Once);
    }
}