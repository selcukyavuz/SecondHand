using System.Data;
namespace SecondHand.Tests.Library.Handlers.Ad;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Handlers.Ad;

public class InsertAdHandlerTest
{
    private Mock<IAdDataAccess>? _mockAdDataAccess;

    private InsertAdHandler? _insertAdHandler;

    private Models.Advertisement.Ad _ad = new();

    private readonly InsertAdCommand _insertAdCommand = new(new Models.Advertisement.Ad());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAdDataAccess = new Mock<IAdDataAccess>();
        _insertAdHandler = new InsertAdHandler(_mockAdDataAccess.Object);
    }

    [Test]
    public void Insert_Success()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.InsertAd(It.IsAny<Models.Advertisement.Ad>())).Returns(_ad);

        // Act
        var result = _insertAdHandler?.Handle(_insertAdCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Advertisement.Ad>();
        _mockAdDataAccess?.Verify(x=>x.InsertAd(It.IsAny<Models.Advertisement.Ad>()), Times.Once);
    }

    [Test]
    public void Insert_Failed()
    {
        // Arrange
        _mockAdDataAccess?.Setup(x=>x.InsertAd(It.IsAny<Models.Advertisement.Ad>())).Returns((Models.Advertisement.Ad)null!);

        // Act
        var result = _insertAdHandler?.Handle(_insertAdCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAdDataAccess?.Verify(x=>x.InsertAd(It.IsAny<Models.Advertisement.Ad>()), Times.Once);
    }
}