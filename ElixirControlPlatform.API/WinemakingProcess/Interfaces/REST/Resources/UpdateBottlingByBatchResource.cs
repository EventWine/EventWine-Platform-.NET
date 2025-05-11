namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;

public record UpdateBottlingByBatchResource(
    string BottlingDate,
    string BottleSizeMl,
    int NumberOfBottles,
    string LabelType,
    string CorkType);