namespace DiveCalculator.Services.DiveCalculator;

public interface IDiveCalculator
{
    public string? GetPressureGroup(int depth, int minutes);
}