namespace DiveCalculator.DTO.DiveCalculator;

public class PressureGroupResponse(string? pressureGroup, List<string> warnings)
{
    public string? PressureGroup { get; set; } = pressureGroup;
    public List<string> Warnings { get; set; } = warnings;
}