using System.Data;
namespace SecondHand.Tests.Library.Handlers.Ad;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Handlers.Ad;

public class UpdateAdHandlerTest
{
    private Mock<IAdDataAccess>? _mockAdDataAccess;

    private UpdateAdHandler? _updateAdHandler;

    private Models.Advertisement.Ad _ad = new();

    private readonly UpdateAdCommand _updateAdCommand = new(new Models.Advertisement.Ad());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAdDataAccess = new Mock<IAdDataAccess>();
        _updateAdHandler = new UpdateAdHandler(_mockAdDataAccess.Object);
    }

    [Test]
    public void Insert_Success()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.UpdateAd(It.IsAny<Models.Advertisement.Ad>())).Returns(_ad);

        // Act
        var result = _updateAdHandler?.Handle(_updateAdCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Advertisement.Ad>();
        _mockAdDataAccess?.Verify(x=>x.UpdateAd(It.IsAny<Models.Advertisement.Ad>()), Times.Once);
    }

    [Test]
    public void Insert_Failed()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.UpdateAd(It.IsAny<Models.Advertisement.Ad>())).Returns((Models.Advertisement.Ad)null!);

        // Act
        var result = _updateAdHandler?.Handle(_updateAdCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAdDataAccess?.Verify(x=>x.UpdateAd(It.IsAny<Models.Advertisement.Ad>()), Times.Once);
    }
}