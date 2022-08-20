namespace SecondHand.Tests.Library.Handlers.Athlete;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Handlers.Athlete;
using SecondHand.Library.Queries.Athlete;

public class GetAthleteByIdHandlerTest
{
    private Mock<IAthleteDataAccess>? _mockAthleteDataAccess;
    private GetAthleteByIdHandler? _getAthleteByIdHandler;

    private readonly GetAthleteByIdQuery _getAthleteByIdQuery = new(It.IsAny<int>());
    private readonly CancellationToken _cancellationToken = new();
    private readonly Models.Strava.Athlete _Athlete = new();

    [SetUp]
    public void Setup()
    {
        _mockAthleteDataAccess = new Mock<IAthleteDataAccess>();
        _getAthleteByIdHandler = new GetAthleteByIdHandler(_mockAthleteDataAccess.Object);
    }

    [Test]
    public void Get_Success()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.GetAthlete(It.IsAny<int>())).Returns(_Athlete);

        // Act
        var result = _getAthleteByIdHandler?.Handle(_getAthleteByIdQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<Models.Strava.Athlete>();
        _mockAthleteDataAccess?.Verify(x=>x.GetAthlete(It.IsAny<int>()),Times.Once);
    }

    [Test]
    public void Get_Failed()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.GetAthlete(It.IsAny<int>())).Returns((Models.Strava.Athlete)null!);

        // Act
        var result = _getAthleteByIdHandler?.Handle(_getAthleteByIdQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAthleteDataAccess?.Verify(x=>x.GetAthlete(It.IsAny<int>()),Times.Once);
    }
}