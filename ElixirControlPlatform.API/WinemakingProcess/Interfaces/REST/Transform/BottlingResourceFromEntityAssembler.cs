using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;

namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Transform;

public static class BottlingResourceFromEntityAssembler
{
    public static BottlingResource ToResourceFromEntity(Bottling entity)
    {
        return new BottlingResource(
            entity.Id,
            entity.BatchId,
            entity.BottlingDate,
            entity.BottleSizeMl,
            entity.NumberOfBottles,
            entity.LabelType,
            entity.CorkType);
    }
    
}