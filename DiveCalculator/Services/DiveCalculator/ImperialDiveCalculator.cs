using DiveCalculator.DTO.DiveCalculator;

namespace DiveCalculator.Services.DiveCalculator;

public class ImperialDiveCalculator : IDiveCalculator
{
    private const int Ndl = 140;
    public string ExceedsNdl { get; } = "Dive Time Exceeds NDL";
    public string ExceedsRecreationalDepth { get; } = "Depth Exceeds Recreational Limits";

    private List<string> _warnings = [];
    
    private readonly List<string> _tableGroups = [
        "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
        "W", "X", "Y", "Z"
    ];

    private readonly List<int> _tableDepths = [35, 40, 50, 60, 70, 80, 90, 100, 110, 120, 130, 140];

    private readonly Dictionary<string, List<int?>> _tableOne = new Dictionary<string, List<int?>>() {
        { "A", [10, 9, 7, 6, 5, 4, 4, 3, 3, 3, 3, null] },
        { "B", [19, 16, 13, 11, 9, 8, 7, 6, 6, 5, 5, 4] },
        { "C", [25, 22, 17, 14, 12, 10, 9, 8, 7, 6, 6, 5] },
        { "D", [29, 25, 19, 16, 13, 11, 10, 9, 8, 7, 7, 6] },
        { "E", [32, 27, 21, 17, 15, 13, 11, 10, 9, 8, null, 7] },
        { "F", [36, 31, 24, 19, 16, 14, 12, 11, 10, 9, 8, 8] },
        { "G", [40, 34, 26, 21, 18, 15, 13, 12, 11, 10, 9] },
        { "H", [44, 37, 28, 23, 19, 17, 15, 13, 12, 11, 10] },
        { "I", [48, 40, 31, 25, 21, 18, 16, 14, 13, null] },
        { "K", [57, 48, 36, 29, 24, 21, 18, 16, 14, 13] },
        { "L", [62, 51, 39, 31, 26, 22, 19, 17, 15] },
        { "M", [67, 55, 41, 33, 27, 23, 21, 18, 16] },
        { "N", [73, 60, 44, 35, 29, 25, 22, 19] },
        { "O", [79, 64, 47, 37, 31, 26, 23, 20] },
        { "P", [85, 69, 50, 39, 33, 28, 24] },
        { "Q", [92, 74, 53, 42, 35, 29, 25] },
        { "R", [100, 79, 57, 44, 36, 30] },
        { "S", [108, 85, 60, 47, 38] },
        { "T", [117, 91, 63, 49, 40] },
        { "U", [127, 97, 67, 52] },
        { "V", [139, 104, 71, 54] },
        { "W", [152, 111, 75, 55] },
        { "X", [168, 120, 80] },
        { "Y", [188, 129] },
        { "Z", [205, 140] }
    };
    
    public PressureGroupResponse GetPressureGroup(int depth, int minutes) {
        if (depth > Ndl) {
            _warnings.Add(ExceedsRecreationalDepth);
            
            return new PressureGroupResponse(null, _warnings);
        }

        var tableDepthKey = GetTableDepthKey(depth);
        
        var pressureGroup =  GetPressureGroupFromTableOne(tableDepthKey, minutes);
        if (pressureGroup == null) {
            _warnings.Add(ExceedsNdl);
        }
        
        return new PressureGroupResponse(pressureGroup, _warnings);
    }
    
    public MaxBottomTimeResponse GetMaxBottomTime(int depth) {
        if (depth > Ndl) {
            _warnings.Add(ExceedsRecreationalDepth);
            
            return new MaxBottomTimeResponse(null, _warnings);
        }
        
        int? tableDepthKey = GetTableDepthKey(depth);
        int? bottomTime = null;

        if (tableDepthKey != null)
        {
            foreach (KeyValuePair<string, List<int?>> row in _tableOne.Reverse()) {
                if (row.Value.Count > tableDepthKey.Value) {
                    if (row.Value[tableDepthKey.Value] != null) {
                        bottomTime = row.Value[tableDepthKey.Value];
                    }
                
                    break;           
                }
            }
        }
        
        return new MaxBottomTimeResponse(bottomTime, _warnings);
    }

    private int? GetTableDepthKey(int depth) {
        for (var i = 0; i < _tableDepths.Count; i++) {
            if (_tableDepths[i] >= depth) {
                return i;
            }
        }
        
        return null;
    }

    private string? GetPressureGroupFromTableOne(int? depthKey, int minutes) {
        if (depthKey == null) {
            return null;
        }
        
        foreach (KeyValuePair<string, List<int?>> row in _tableOne) {
            var length = row.Value.Count;
            if (length <= depthKey.Value) {
                continue;
            }
            
            var depthTime = row.Value[depthKey.Value];

            if (depthTime != null && depthTime >= minutes) {
                return row.Key;           
            }
        }

        return null;
    }
}