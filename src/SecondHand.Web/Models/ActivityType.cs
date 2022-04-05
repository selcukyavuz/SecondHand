namespace SecondHand.Web.Model;

using System.Runtime.Serialization;

public enum ActivityType
{
    [EnumMember(Value = "AlpineSki")] AlpineSki,
    [EnumMember(Value = "BackcountrySki")] BackcountrySki, 
    [EnumMember(Value = "Canoeing")] Canoeing, 
    [EnumMember(Value = "Crossfit")] Crossfit,
    [EnumMember(Value = "EBikeRide")] EBikeRide, 
    [EnumMember(Value = "Elliptical")] Elliptical, 
    [EnumMember(Value = "Golf")] Golf, 
    [EnumMember(Value = "Handcycle")] Handcycle, 
    [EnumMember(Value = "Hike")] Hike, 
    [EnumMember(Value = "IceSkate")] IceSkate, 
    [EnumMember(Value = "InlineSkate")] InlineSkate, 
    [EnumMember(Value = "Kayaking")] Kayaking, 
    [EnumMember(Value = "Kitesurf")] Kitesurf, 
    [EnumMember(Value = "NordicSki")] NordicSki, 
    [EnumMember(Value = "Ride")] Ride, 
    [EnumMember(Value = "RockClimbing")] RockClimbing, 
    [EnumMember(Value = "RollerSki")] RollerSki, 
    [EnumMember(Value = "Rowing")] Rowing, 
    [EnumMember(Value = "Run")] Run, 
    [EnumMember(Value = "Sail")] Sail, 
    [EnumMember(Value = "Skateboard")] Skateboard, 
    [EnumMember(Value = "Snowboard")] Snowboard, 
    [EnumMember(Value = "Snowshoe")] Snowshoe, 
    [EnumMember(Value = "Soccer")] Soccer, 
    [EnumMember(Value = "StairStepper")] StairStepper, 
    [EnumMember(Value = "StandUpPaddling")] StandUpPaddling, 
    [EnumMember(Value = "Surfing")] Surfing, 
    [EnumMember(Value = "Swim")] Swim, 
    [EnumMember(Value = "Velomobile")] Velomobile, 
    [EnumMember(Value = "VirtualRide")] VirtualRide, 
    [EnumMember(Value = "VirtualRun")] VirtualRun, 
    [EnumMember(Value = "Walk")] Walk,
    [EnumMember(Value = "WeightTraining")] WeightTraining, 
    [EnumMember(Value = "Wheelchair")] Wheelchair,
    [EnumMember(Value = "Windsurf")] Windsurf, 
    [EnumMember(Value = "Workout")] Workout, 
    [EnumMember(Value = "Yoga")] Yoga
}