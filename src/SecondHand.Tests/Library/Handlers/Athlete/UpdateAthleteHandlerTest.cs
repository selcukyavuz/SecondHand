namespace SecondHand.Tests.Library.Handlers.Athlete;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Athlete;
using SecondHand.Library.Handlers.Athlete;

public class UpdateAthleteHandlerTest
{
    private Mock<IAthleteDataAccess>? _mockAthleteDataAccess;

    private UpdateAthleteHandler? _updateAthleteHandler;

    private readonly Models.Strava.Athlete _Athlete = new();

    private readonly UpdateAthleteCommand _updateAthleteCommand = new(new Models.Strava.Athlete());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAthleteDataAccess = new Mock<IAthleteDataAccess>();
        _updateAthleteHandler = new UpdateAthleteHandler(_mockAthleteDataAccess.Object);
    }

    [Test]
    public void Insert_Success()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.UpdateAthlete(It.IsAny<Models.Strava.Athlete>())).Returns(_Athlete);

        // Act
        var result = _updateAthleteHandler?.Handle(_updateAthleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Strava.Athlete>();
        _mockAthleteDataAccess?.Verify(x=>x.UpdateAthlete(It.IsAny<Models.Strava.Athlete>()), Times.Once);
    }

    [Test]
    public void Insert_Failed()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.UpdateAthlete(It.IsAny<Models.Strava.Athlete>())).Returns((Models.Strava.Athlete)null!);

        // Act
        var result = _updateAthleteHandler?.Handle(_updateAthleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAthleteDataAccess?.Verify(x=>x.UpdateAthlete(It.IsAny<Models.Strava.Athlete>()), Times.Once);
    }
}