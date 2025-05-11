using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Queries;

namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Services;

public interface IBatchQueryService
{
    Task<IEnumerable<Batch>> Handle(GetAllBatchByProfileIdQuery query);
    Task<Batch?> Handle(GetBatchByIdQuery query);
    
    Task<IEnumerable<Batch>> Handle(GetAllBatchesQuery query);
    
    Task<Fermentation?> Handle(GetFermentationByBatchIdQuery query);

    Task<Clarification?> Handle(GetClarificationByBatchIdQuery query);
    
    Task<Pressing?> Handle(GetPressingByBatchIdQuery query);
    
    Task<Aging?> Handle(GetAgingByBatchIdQuery query);
    
    Task<Bottling?> Handle(GetBottlingByBatchIdQuery query);
}