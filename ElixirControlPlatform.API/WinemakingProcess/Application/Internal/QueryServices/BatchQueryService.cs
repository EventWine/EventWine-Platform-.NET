using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Queries;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Services;

namespace ElixirControlPlatform.API.WinemakingProcess.Application.Internal.QueryServices;

public class BatchQueryService(IBatchRepository batchRepository) : IBatchQueryService
{
    public async Task<IEnumerable<Batch>> Handle(GetAllBatchByProfileIdQuery query)
    {
        return await batchRepository.GetAllBatchByProfileIdAsync(query.ProfileId);
    }
    
    public async Task<Batch?> Handle(GetBatchByIdQuery query)
    {
        return await batchRepository.FindByIdAsync(query.Id);
    }

    public async Task<IEnumerable<Batch>> Handle(GetAllBatchesQuery query)
    {
        return await batchRepository.ListAsync();
    }
    
    public async Task<Fermentation?> Handle(GetFermentationByBatchIdQuery query)
    {
        return await batchRepository.GetFermentationByBatchAsync(query.BatchId);
    }
    
    public async Task<Clarification?> Handle(GetClarificationByBatchIdQuery query)
    {
        return await batchRepository.GetClarificationByBatchAsync(query.BatchId);
    }
    
    public async Task<Pressing?> Handle(GetPressingByBatchIdQuery query)
    {
        return await batchRepository.GetPressingByBatchAsync(query.BatchId);
    }
    
    public async Task<Aging?> Handle(GetAgingByBatchIdQuery query)
    {
        return await batchRepository.GetAgingByBatchAsync(query.BatchId);
    }
    
    public async Task<Bottling?> Handle(GetBottlingByBatchIdQuery query)
    {
        return await batchRepository.GetBottlingByBatchAsync(query.BatchId);
    }
}