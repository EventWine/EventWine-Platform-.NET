namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;

public record BottlingResource(
    int Id,
    int BatchId,
    string BottlingDate,
    string BottleSizeMl,
    int NumberOfBottles,
    string LabelType,
    string CorkType
    );