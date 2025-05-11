using ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Configuration;
using ElixirControlPlatform.API.Shared.Infrastructure.Persistence.EFC.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace ElixirControlPlatform.API.WinemakingProcess.Infrastructure.Persistence.EFC.Repositories;

public class BatchRepository(AppDbContext context) : BaseRepository<Batch>(context), IBatchRepository
{
    public async Task<IEnumerable<Batch>> GetAllBatchByProfileIdAsync(Guid profileId)
    {
        return await Context.Set<Batch>()
            .Where(batch => batch.ProfileId == profileId)
            .ToListAsync();
    }

    public async Task<Fermentation?> GetFermentationByBatchAsync(int batchId)
    {
        return await Context.Set<Fermentation>()
            .FirstOrDefaultAsync(fermentation => fermentation.BatchId == batchId);
    }
    
    public async Task<Clarification?> GetClarificationByBatchAsync(int batchId)
    {
        return await Context.Set<Clarification>()
            .FirstOrDefaultAsync(clarification => clarification.BatchId == batchId);
    }
    
    public async Task<Pressing?> GetPressingByBatchAsync(int batchId)
    {
        return await Context.Set<Pressing>()
            .FirstOrDefaultAsync(pressing => pressing.BatchId == batchId);
    }
    
    public async Task<Aging?> GetAgingByBatchAsync(int batchId)
    {
        return await Context.Set<Aging>()
            .FirstOrDefaultAsync(aging => aging.BatchId == batchId);
    }
    
    public async Task<Bottling?> GetBottlingByBatchAsync(int batchId)
    {
        return await Context.Set<Bottling>()
            .FirstOrDefaultAsync(bottling => bottling.BatchId == batchId);
    }
    
}