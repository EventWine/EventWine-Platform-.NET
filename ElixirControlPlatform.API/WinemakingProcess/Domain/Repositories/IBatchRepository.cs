using ElixirControlPlatform.API.Shared.Domain.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.ValueObjects;

namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Repositories;

public interface IBatchRepository : IBaseRepository<Batch>
{
    Task<IEnumerable<Batch>> GetAllBatchByProfileIdAsync(Guid profileId);
    Task<Fermentation?> GetFermentationByBatchAsync(int batchId);
    
    Task<Clarification?> GetClarificationByBatchAsync(int batchId);
    
    Task<Pressing?> GetPressingByBatchAsync(int batchId);
    
    Task<Aging?> GetAgingByBatchAsync(int batchId);
    
    Task<Bottling?> GetBottlingByBatchAsync(int batchId);
    
}
