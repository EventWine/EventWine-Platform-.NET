using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;
using ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;

namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Transform;

public static class AddBottlingToBatchCommandFromResourceAssembler
{
    public static AddBottlingToBatchCommand ToCommandFromResource(AddBottlingToBatchResource resource)
    {
        return new AddBottlingToBatchCommand(
            BottlingDate: resource.BottlingDate,
            BottleSizeMl: resource.BottleSizeMl,
            NumberOfBottles: resource.NumberOfBottles,
            LabelType: resource.LabelType,
            CorkType: resource.CorkType
        );
    }
    
}