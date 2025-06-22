namespace DiveCalculator.DTO.DiveCalculator;

public class MaxBottomTimeResponse(int? maxBottomTime, List<string> warnings)
{
    public int? MaxBottomTime { get; set; } = maxBottomTime;
    public List<string> Warnings { get; set; } = warnings;
}