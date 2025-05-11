namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;

public record AddBottlingToBatchCommand(
    string BottlingDate,
    string BottleSizeMl,
    int NumberOfBottles,
    string LabelType,
    string CorkType
    );