using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;
using ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;

namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Transform;

public static class UpdateBottlingByBatchCommandFromResourceAssembler
{
    public static UpdateBottlingByBatchCommand ToCommandFromResource(UpdateBottlingByBatchResource resource)
    {
        return new UpdateBottlingByBatchCommand(
            BottlingDate: resource.BottlingDate,
            BottleSizeMl: resource.BottleSizeMl,
            NumberOfBottles: resource.NumberOfBottles,
            LabelType: resource.LabelType,
            CorkType: resource.CorkType
        );
    }
    
}   