namespace SecondHand.Tests.Library.Handlers.Ad;

using FluentAssertions;
using Moq;
using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Ad;
using SecondHand.Library.Handlers.Ad;

public class DeleteAdHandlerTest
{
    private Mock<IAdDataAccess>? _mockAdDataAccess;
    private DeleteAdHandler? _deleteAdHandler;

    private readonly DeleteAdCommand _deleteCommand = new(It.IsAny<int>());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAdDataAccess = new Mock<IAdDataAccess>();
        _deleteAdHandler = new DeleteAdHandler(_mockAdDataAccess.Object);
    }

    [Test]
    public void Get_Success()
    {
        // Arrange

        _mockAdDataAccess?
            .Setup(x=>x.DeleteAd(It.IsAny<int>()))
            .Returns(true);
        // Act
        var result = _deleteAdHandler?.Handle(_deleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().Be(true);

        _mockAdDataAccess?
            .Verify(x=>x.DeleteAd(It.IsAny<int>()),Times.Once);
    }

    [Test]
    public void Get_Failed()
    {
        // Arrange

        _mockAdDataAccess?
            .Setup(x=>x.DeleteAd(It.IsAny<int>()))
            .Returns(false);
        // Act
        var result = _deleteAdHandler?.Handle(_deleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().Be(false);

        _mockAdDataAccess?
            .Verify(x=>x.DeleteAd(It.IsAny<int>()),Times.Once);
    }
}