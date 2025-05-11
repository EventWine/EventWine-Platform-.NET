namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;

public record UpdateBottlingByBatchCommand(
    string BottlingDate,
    string BottleSizeMl,
    int NumberOfBottles,
    string LabelType,
    string CorkType);