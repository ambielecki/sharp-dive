using DiveCalculator.DTO.DiveCalculator;

namespace DiveCalculator.Services.DiveCalculator;

public interface IDiveCalculator
{
    public PressureGroupResponse GetPressureGroup(int depth, int minutes);
    public MaxBottomTimeResponse GetMaxBottomTime(int depth);
}