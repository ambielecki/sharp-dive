using DiveCalculator.Services.DiveCalculator;

namespace DiveCalculatorTest;

public class ImperialServiceTest
{
    [Theory]
    [InlineData(60, 30, "L")]
    [InlineData(140, 30, null)] // depth exists, over NDL
    [InlineData(30, 60, "L")]
    [InlineData(70, 13, "D")] // exact match
    [InlineData(150, 1, null)] // out of rec diving limit
    [InlineData(90, 25, "Q")] // NDL
    public void GetPressureGroupTest(int depth, int minutes, string? expected) {
        var diveCalculator = new ImperialDiveCalculator();
        
        var pressureGroup = diveCalculator.GetPressureGroup(depth, minutes).PressureGroup;
        Assert.Equal(expected, pressureGroup);
    }

    [Theory]
    [InlineData(34, 205)]
    [InlineData(35, 205)]
    [InlineData(110, 16)]
    [InlineData(140, 8)]
    [InlineData(141, null)]
    public void GetMaxBottomTimeTest(int depth, int? expected) {
        var diveCalculator = new ImperialDiveCalculator();
        
        var maxTime = diveCalculator.GetMaxBottomTime(depth).MaxBottomTime;
        Assert.Equal(expected, maxTime);
    }
}