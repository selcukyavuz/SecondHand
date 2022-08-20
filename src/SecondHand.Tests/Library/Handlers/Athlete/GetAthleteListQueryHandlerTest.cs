namespace SecondHand.Tests.Library.Handlers.Athlete;

using FluentAssertions;
using Moq;

using SecondHand.DataAccess.MongoDB.Interface;
using SecondHand.Library.Handlers.Athlete;
using SecondHand.Library.Queries.Athlete;

public class GetAthleteListQueryHandlerTest
{
    private Mock<IAthleteDataAccess>? _mockAthleteDataAccess;

    private GetAthleteListQueryHandler? _getAthleteListQueryHandler;

    private readonly GetAthleteListQuery _getAthleteListQuery = new();
    private readonly CancellationToken _cancellationToken = new();
    private readonly List<Models.Strava.Athlete> _Athletes = new();

    [SetUp]
    public void Setup()
    {
        _mockAthleteDataAccess = new Mock<IAthleteDataAccess>();
        _getAthleteListQueryHandler = new GetAthleteListQueryHandler(_mockAthleteDataAccess.Object);
    }

    [Test]
    public void Get_Success()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.GetAthlete()).Returns(_Athletes);

        // Act
        var result = _getAthleteListQueryHandler?.Handle(_getAthleteListQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeOfType<List<Models.Strava.Athlete>>();
        _mockAthleteDataAccess?.Verify(x=>x.GetAthlete(),Times.Once);
    }

    [Test]
    public void Get_Failed()
    {
        // Arrange
        _mockAthleteDataAccess?.Setup(x=>x.GetAthlete()).Returns((List<Models.Strava.Athlete>)null!);

        // Act
        var result = _getAthleteListQueryHandler?.Handle(_getAthleteListQuery,_cancellationToken).Result;

        // Assert
        result.Should().BeNull();
        _mockAthleteDataAccess?.Verify(x=>x.GetAthlete(),Times.Once);
    }
}