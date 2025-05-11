using ElixirControlPlatform.API.Shared.Domain.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.ValueObjects;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Repositories;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Services;

namespace ElixirControlPlatform.API.WinemakingProcess.Application.Internal.CommandServices;

public class BatchCommandService(IBatchRepository batchRepository, IUnitOfWOrk unitOfWork) : IBatchCommandService
{

    public async Task<Batch?> Handle(CreateBatchCommand command,  Guid profileId)
    {
        var batch = new Batch(command, profileId);
        await batchRepository.AddAsync(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<bool> Handle(DeleteBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.Id);
        if (batch is null) throw new Exception("Batch not found");
        
        batchRepository.Remove(batch);
        await unitOfWork.CompleteAsync();
        return true;
    }
    
    public async Task<Batch?> Handle(UpdateBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        batch.UpdateBatch(command);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        
        return batch;
    }

    
    //========================== Fermentation ==========================
    public async Task<Batch?> Handle(AddFermentationToBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        batch.AddFermentationByBatch(
            batchId,
            command.StartDate, 
            command.EndDate, 
            command.AverageTemperature, 
            command.InitialDensity, 
            command.InitialPh, 
            command.FinalDensity, 
            command.FinalPh, 
            command.ResidualSugar);
        
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(DeleteFermentationByBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        var fermentation = await batchRepository.GetFermentationByBatchAsync(command.BatchId);
        
        if (batch is null) throw new Exception("Batch not found");
        
        if (fermentation is null) throw new Exception("The batch has no registered fermentation");
 
        batch.DeleteFermentationByBatch();
        
        batchRepository.Update(batch);
        
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(UpdateFermentationByBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        var fermentation = await batchRepository.GetFermentationByBatchAsync(batchId);
        if (fermentation is null) throw new Exception("The batch has no registered fermentation");
        
        batch.UpdateFermentationByBatch(command);
        
        batchRepository.Update(batch);
        
        await unitOfWork.CompleteAsync();
        return batch;
    } 
    
    //======================== end Fermentation ========================
    
    
    //========================== Clarification ==========================
    
    public async Task<Batch?> Handle(AddClarificationToBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        //Quiero validar que el batch haya pasado por la etapa de fermentación para poder crear un clarificado
        if (batch.Status != CurrentBatchStatus.Fermentation) throw new Exception("The batch must first go through the fermentation stage");      
        
        batch.AddClarificationByBatch(
            batchId, 
            command.ProductsUsed, 
            command.ClarificationMethod, 
            command.FiltrationDate, 
            command.ClarityLevel, 
            command.StartDate, 
            command.EndDate);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(DeleteClarificationByBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        var clarification = await batchRepository.GetClarificationByBatchAsync(command.BatchId);
        
        if (batch is null) throw new Exception("Batch not found");
        if (clarification is null) throw new Exception("The batch has no registered clarification");
        
        batch.DeleteClarificationByBatch();
        
        batchRepository.Remove(batch);
        
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(UpdateClarificationByBatchCommand command, int batchId)
    {
        
        var batch = await batchRepository.FindByIdAsync(batchId);
        
        if (batch is null) throw new Exception("Batch not found");
        
        var clarification = await batchRepository.GetClarificationByBatchAsync(batchId);
        
        if (clarification is null) throw new Exception("The batch has no registered clarification");
        
        batch.UpdateClarificationByBatch(command);
        
        batchRepository.Update(batch);
        
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    //======================== end Clarification ========================
    
    
    //========================== Pressing ==========================
    
    public async Task<Batch?> Handle(AddPressingToBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        if (batch.Status != CurrentBatchStatus.Clarification) throw new Exception("The batch must first go through the clarification stage");
        
        batch.AddPressingByBatch(
            batchId,
            command.PressingDate, 
            command.MustVolume, 
            command.PressType, 
            command.AppliedPressure);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(DeletePressingByBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        var pressing = await batchRepository.GetPressingByBatchAsync(command.BatchId);
        
        if (batch is null) throw new Exception("Batch not found");
        if (pressing is null) throw new Exception("The batch has no registered pressing");
        
        batch.DeletePressingByBatch();
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(UpdatePressingByBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        var pressing = await batchRepository.GetPressingByBatchAsync(batchId);
        if (pressing is null) throw new Exception("The batch has no registered pressing");
        
        
        batch.UpdatePressingToBatch(command);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    //======================== end Pressing ========================
    
    //========================== Aging ==========================
    
    public async Task<Batch?> Handle(AddAgingToBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        if (batch.Status != CurrentBatchStatus.Pressing) throw new Exception("The batch must first go through the pressing stage");
        
        batch.AddAgingByBatch(
            batchId,
            command.BarrelType, 
            command.StartDate, 
            command.EndDate, 
            command.AgingDurationMonths, 
            command.InspectionsPerformed, 
            command.InspectionResult);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }

    public async Task<Batch?> Handle(DeleteAgingByBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        var aging = await batchRepository.GetAgingByBatchAsync(command.BatchId);
        
        if (batch is null) throw new Exception("Batch not found");
        if (aging is null) throw new Exception("The batch has no registered aging");
        
        batch.DeleteAgingByBatch();
        
        batchRepository.Remove(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(UpdateAgingByBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        var aging = await batchRepository.GetAgingByBatchAsync(batchId);
        if (aging is null) throw new Exception("The batch has no registered aging");
        
        batch.UpdateAgingByBatch(command);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    //======================== end Aging ========================
    
    //========================== Bottling ==========================
    
    public async Task<Batch?> Handle(AddBottlingToBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        if (batch.Status != CurrentBatchStatus.Aging) throw new Exception("The batch must first go through the aging stage");
        
        batch.AddBottlingByBatch(
            batchId,
            command.BottlingDate, 
            command.BottleSizeMl, 
            command.NumberOfBottles, 
            command.LabelType, 
            command.CorkType);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(DeleteBottlingByBatchCommand command)
    {
        var batch = await batchRepository.FindByIdAsync(command.BatchId);
        var bottling = await batchRepository.GetBottlingByBatchAsync(command.BatchId);
        
        if (batch is null) throw new Exception("Batch not found");
        if (bottling is null) throw new Exception("The batch has no registered bottling");
        
        batch.DeleteBottlingByBatch();
        
        batchRepository.Remove(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
    
    public async Task<Batch?> Handle(UpdateBottlingByBatchCommand command, int batchId)
    {
        var batch = await batchRepository.FindByIdAsync(batchId);
        if (batch is null) throw new Exception("Batch not found");
        
        var bottling = await batchRepository.GetBottlingByBatchAsync(batchId);
        if (bottling is null) throw new Exception("The batch has no registered bottling");
        
        batch.UpdateBottlingByBatch(command);
        
        batchRepository.Update(batch);
        await unitOfWork.CompleteAsync();
        return batch;
    }
}