namespace SecondHand.Tests.Library.Handlers.Athlete;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Commands.Athlete;
using SecondHand.Library.Handlers.Athlete;

public class DeleteAthleteHandlerTest
{
    private Mock<IAthleteDataAccess>? _mockAthleteDataAccess;

    private DeleteAthleteHandler? _deleteAthleteHandler;

    private readonly DeleteAthleteCommand _deleteCommand = new(It.IsAny<int>());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAthleteDataAccess = new Mock<IAthleteDataAccess>();
        _deleteAthleteHandler = new DeleteAthleteHandler(_mockAthleteDataAccess.Object);
    }

    [Test]
    public void Delete_Success()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.DeleteAthlete(It.IsAny<int>())).Returns(true);

        // Act
        var result = _deleteAthleteHandler?.Handle(_deleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().Be(true);
        _mockAthleteDataAccess?.Verify(x=>x.DeleteAthlete(It.IsAny<int>()),Times.Once);
    }

    [Test]
    public void Delete_Failed()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.DeleteAthlete(It.IsAny<int>())).Returns(false);
        // Act
        var result = _deleteAthleteHandler?.Handle(_deleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().Be(false);
        _mockAthleteDataAccess?.Verify(x=>x.DeleteAthlete(It.IsAny<int>()),Times.Once);
    }
}