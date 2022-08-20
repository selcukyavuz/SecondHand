namespace SecondHand.Tests.Library.Handlers.Athlete;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.SqlServer.Interface;
using SecondHand.Library.Commands.Athlete;
using SecondHand.Library.Handlers.Athlete;

public class InsertAthleteHandlerTest
{
    private Mock<IAthleteDataAccess>? _mockAthleteDataAccess;

    private InsertAthleteHandler? _insertAthleteHandler;

    private readonly Models.Strava.Athlete _Athlete = new();

    private readonly InsertAthleteCommand _insertAthleteCommand = new(new Models.Strava.Athlete());
    private readonly CancellationToken _cancellationToken = new();

    [SetUp]
    public void Setup()
    {
        _mockAthleteDataAccess = new Mock<IAthleteDataAccess>();
        _insertAthleteHandler = new InsertAthleteHandler(_mockAthleteDataAccess.Object);
    }

    [Test]
    public void Insert_Success()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.InsertAthlete(It.IsAny<Models.Strava.Athlete>())).Returns(_Athlete);

        // Act
        var result = _insertAthleteHandler?.Handle(_insertAthleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Strava.Athlete>();
        _mockAthleteDataAccess?.Verify(x=>x.InsertAthlete(It.IsAny<Models.Strava.Athlete>()), Times.Once);
    }

    [Test]
    public void Insert_Failed()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.InsertAthlete(It.IsAny<Models.Strava.Athlete>())).Returns((Models.Strava.Athlete)null!);

        // Act
        var result = _insertAthleteHandler?.Handle(_insertAthleteCommand,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAthleteDataAccess?.Verify(x=>x.InsertAthlete(It.IsAny<Models.Strava.Athlete>()), Times.Once);
    }
}