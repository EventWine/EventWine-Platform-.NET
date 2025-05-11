using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;

namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Services;

public interface IBatchCommandService
{
    //=========== BATCH
    public Task<Batch?> Handle(CreateBatchCommand command, Guid profileId);
    public Task<bool> Handle(DeleteBatchCommand command);
    public Task<Batch?> Handle(UpdateBatchCommand command, int batchId);
    
    //=========== Fermentation
    public Task<Batch?> Handle(AddFermentationToBatchCommand command, int batchId);
    public Task<Batch?> Handle(DeleteFermentationByBatchCommand command);
    public Task<Batch?> Handle(UpdateFermentationByBatchCommand command, int batchId);   
    
    // =========== end Fermentation
    
    //=========== Clarification
    public Task<Batch?> Handle(AddClarificationToBatchCommand command, int batchId);
    public Task<Batch?> Handle(DeleteClarificationByBatchCommand command);
    public Task<Batch?> Handle(UpdateClarificationByBatchCommand command, int batchId);
    
    // =========== end Clarification
    
    //=========== Pressing
    public Task<Batch?> Handle(AddPressingToBatchCommand command, int batchId);
    public Task<Batch?> Handle(DeletePressingByBatchCommand command);
    public Task<Batch?> Handle(UpdatePressingByBatchCommand command, int batch);
    // =========== end Pressing
    
    
    //=========== Aging
    public Task<Batch?> Handle(AddAgingToBatchCommand command, int batchId);
    public Task<Batch?> Handle(DeleteAgingByBatchCommand command);
    public Task<Batch?> Handle(UpdateAgingByBatchCommand command, int batch);
    // =========== end Aging
    
    //=========== Bottling
    public Task<Batch?> Handle(AddBottlingToBatchCommand command, int batchId);
    public Task<Batch?> Handle(DeleteBottlingByBatchCommand command);
    public Task<Batch?> Handle(UpdateBottlingByBatchCommand command, int batch);
    // =========== end Bottling
    
    
}