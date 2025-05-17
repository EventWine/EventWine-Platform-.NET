using System.Net.Mime;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Queries;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Services;
using ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Resources;
using ElixirControlPlatform.API.WinemakingProcess.Interfaces.REST.Transform;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace ElixirControlPlatform.API.WinemakingProcess.Interfaces;


[ApiController]
[Route("api/v1/winemakingProcess")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Available Winemaking Process by batch Endpoints")]
public class WinemakingProcessByBatchController(IBatchQueryService batchQueryService, IBatchCommandService batchCommandService): ControllerBase
{
    
    //================================================= BATCH - FERMENTATION ==========================================
    
    [HttpGet("batch/{batchId:int}/fermentation")]
    [SwaggerOperation(
        Summary = "Get a Fermentation by Batch",
        Description = "Get a Fermentation by Batch",
        OperationId = "GetFermentationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Fermentation was successfully retrieved", typeof(FermentationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Fermentation was not found")]
    public async Task<IActionResult> GetFermentationByBatch(int batchId)
    {
        var fermentation = await batchQueryService.Handle(new GetFermentationByBatchIdQuery(batchId));
        if (fermentation is null) return NotFound();
        var fermentationResource = FermentationResourceFromEntityAssembler.ToResourceFromEntity(fermentation);
        return Ok(fermentationResource);
    }
   
    //POST --------------------------------------------------------------------------------------------------------

    [HttpPost("{batchId:int}/fermentation")]
    [SwaggerOperation(
        Summary = "Add a Fermentation to a Batch",
        Description = "Add a Fermentation to a Batch",
        OperationId = "AddFermentationToBatch"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The Fermentation was successfully added to the Batch", typeof(FermentationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Fermentation was not added to the Batch")]
    public async Task<IActionResult> AddFermentationToBatch([FromBody] AddFermentationToBatchResource resource, int batchId)
    {
        var addFermentationToBatchCommand = AddFermentationToBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var batch = await batchCommandService.Handle(addFermentationToBatchCommand, batchId);
        
        if (batch is null) return BadRequest();
        
        var fermentationResource = FermentationResourceFromEntityAssembler.ToResourceFromEntity(batch.Fermentation);
       
        return CreatedAtAction(nameof(GetFermentationByBatch), new { batchId = batch.Id }, fermentationResource);    
    }
    
    // DELETE ------------------------------------------------------------------------------------------------------
    
    [HttpDelete("{batchId:int}/fermentation")]
    [SwaggerOperation(
        Summary = "Delete a Fermentation by Batch",
        Description = "Delete a Fermentation by Batch",
        OperationId = "DeleteFermentationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Fermentation was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Fermentation was not found")]
    public async Task<IActionResult> DeleteFermentationByBatch(int batchId)
    {
        var deleteFermentationByBatchCommand = new DeleteFermentationByBatchCommand(batchId);
        var batch = await batchCommandService.Handle(deleteFermentationByBatchCommand);
        if (batch is null) return NotFound();
        return Ok(new { title = "Delete Fermentation", Message = $"Fermentation for batch {batchId} was successfully deleted" });
    }
    
    // UPDATE ------------------------------------------------------------------------------------------------------
    
    [HttpPut("{batchId:int}/fermentation")]
    [SwaggerOperation(
        Summary = "Update a Fermentation by Batch",
        Description = "Update a Fermentation by Batch",
        OperationId = "UpdateFermentationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Fermentation was successfully updated", typeof(FermentationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Fermentation was not found")]
    public async Task<IActionResult> UpdateFermentationByBatch([FromBody] UpdateFermentationByBatchResource resource, int batchId)
    {
        var updateFermentationByBatchCommand = UpdateFermentationByBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var updatedBatch = await batchCommandService.Handle(updateFermentationByBatchCommand, batchId);
        
        if (updatedBatch is null) return NotFound();
        
        return Ok(resource);
    }
  
    //============================================== END BATCH - FERMENTATION =========================================
        
    
    
    
    //================================================= BATCH - CLARIFICATION ==========================================
    
    // GET -----------------------------------------------------------------------------------------------------------
    [HttpGet("batch/{batchId:int}/clarification")]
    [SwaggerOperation(
        Summary = "Get a Clarification by Batch",
        Description = "Get a Clarification by Batch",
        OperationId = "GetClarificationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Clarification was successfully retrieved", typeof(ClarificationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Clarification was not found")]
    public async Task<IActionResult> GetClarificationByBatch(int batchId)
    {
        var clarification = await batchQueryService.Handle(new GetClarificationByBatchIdQuery(batchId));
        if (clarification is null) return NotFound();
        var clarificationResource = ClarificationResourceFromEntityAssembler.ToResourceFromEntity(clarification);
        return Ok(clarificationResource);
    }
    
    //POST ----------------------------------------------------------------------------------------------------------

    [HttpPost("{batchId:int}/clarification")]
    [SwaggerOperation(
        Summary = "Add a Clarification to a Batch",
        Description = "Add a Clarification to a Batch",
        OperationId = "AddClarificationToBatch"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The Clarification was successfully added to the Batch", typeof(ClarificationResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Clarification was not added to the Batch")]
    public async Task<IActionResult> AddClarificationToBatch([FromBody] AddClarificationToBatchResource resource, int batchId)
    {
        var addClarificationToBatchCommand = AddClarificationToBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var batch = await batchCommandService.Handle(addClarificationToBatchCommand, batchId);
        
        if (batch is null) return BadRequest();
        
        var clarificationResource = ClarificationResourceFromEntityAssembler.ToResourceFromEntity(batch.Clarification);
        
        return CreatedAtAction(nameof(GetClarificationByBatch), new { batchId = batch.Id }, clarificationResource);
    }
    
    // DELETE --------------------------------------------------------------------------------------------------------
    
    [HttpDelete("{batchId:int}/clarification")]
    [SwaggerOperation(
        Summary = "Delete a Clarification by Batch",
        Description = "Delete a Clarification by Batch",
        OperationId = "DeleteClarificationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Clarification was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Clarification was not found")]
    public async Task<IActionResult> DeleteClarificationByBatch(int batchId)
    {
        var deleteClarificationByBatchCommand = new DeleteClarificationByBatchCommand(batchId);
        var batch = await batchCommandService.Handle(deleteClarificationByBatchCommand);
        if (batch is null) return NotFound();
        return Ok(new { title = "Delete Clarification", Message = $"Clarification for batch {batchId} was successfully deleted" });
    }
    
    // UPDATE --------------------------------------------------------------------------------------------------------
    
    [HttpPut("{batchId:int}/clarification")]
    [SwaggerOperation(
        Summary = "Update a Clarification by Batch",
        Description = "Update a Clarification by Batch",
        OperationId = "UpdateClarificationByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Clarification was successfully updated", typeof(ClarificationResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Clarification was not found")]
    public async Task<IActionResult> UpdateClarificationByBatch([FromBody] UpdateClarificationByBatchResource resource, int batchId)
    {
        var updateClarificationByBatchCommand = UpdateClarificationByBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var updatedBatch = await batchCommandService.Handle(updateClarificationByBatchCommand, batchId);
        
        if (updatedBatch is null) return NotFound();
        
        return Ok(resource);
    }
    
    
    //============================================== END BATCH - CLARIFICATION ====================================
    
    //================================================= BATCH - PRESSING ==========================================
    
    // GET -----------------------------------------------------------------------------------------------------------
    
    [HttpGet("batch/{batchId:int}/pressing")]
    [SwaggerOperation(
        Summary = "Get a Pressing by Batch",
        Description = "Get a Pressing by Batch",
        OperationId = "GetPressingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Pressing was successfully retrieved", typeof(PressingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Pressing was not found")]
    public async Task<IActionResult> GetPressingByBatch(int batchId)
    {
        var pressing = await batchQueryService.Handle(new GetPressingByBatchIdQuery(batchId));
        if (pressing is null) return NotFound();
        var pressingResource = PressingResourceFromEntityAssembler.ToResourceFromEntity(pressing);
        return Ok(pressingResource);
    }
    
    //POST -----------------------------------------------------------------------------------------------------------
    
    [HttpPost("{batchId:int}/pressing")]
    [SwaggerOperation(
        Summary = "Add a Pressing to a Batch",
        Description = "Add a Pressing to a Batch",
        OperationId = "AddPressingToBatch"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The Pressing was successfully added to the Batch", typeof(PressingResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Pressing was not added to the Batch")]
    public async Task<IActionResult> AddPressingToBatch([FromBody] AddPressingToBatchResource resource, int batchId)
    {
        var addPressingToBatchCommand = AddPressingToBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        var batch = await batchCommandService.Handle(addPressingToBatchCommand, batchId);
        if (batch is null) return BadRequest();
        
        var pressingResource = PressingResourceFromEntityAssembler.ToResourceFromEntity(batch.Pressing);
        
        return CreatedAtAction(nameof(GetPressingByBatch), new { batchId = batch.Id }, pressingResource);
    }
    
    // DELETE --------------------------------------------------------------------------------------------------------
    
    [HttpDelete("{batchId:int}/pressing")]
    [SwaggerOperation(
        Summary = "Delete a Pressing by Batch",
        Description = "Delete a Pressing by Batch",
        OperationId = "DeletePressingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Pressing was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Pressing was not found")]
    public async Task<IActionResult> DeletePressingByBatch(int batchId)
    {
        var deletePressingByBatchCommand = new DeletePressingByBatchCommand(batchId);
        var batch = await batchCommandService.Handle(deletePressingByBatchCommand);
        if (batch is null) return NotFound();
        return Ok(new { title = "Delete Pressing", Message = $"Pressing for batch {batchId} was successfully deleted" });
    }
    
    // UPDATE --------------------------------------------------------------------------------------------------------
    
    [HttpPut("{batchId:int}/pressing")]
    [SwaggerOperation(
        Summary = "Update a Pressing by Batch",
        Description = "Update a Pressing by Batch",
        OperationId = "UpdatePressingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Pressing was successfully updated", typeof(PressingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Pressing was not found")]
    public async Task<IActionResult> UpdatePressingByBatch([FromBody] UpdatePressingByBatchResource resource, int batchId)
    {
        var updatePressingByBatchCommand = UpdatePressingByBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var updatedBatch = await batchCommandService.Handle(updatePressingByBatchCommand, batchId);
        
        if (updatedBatch is null) return NotFound();
        
        return Ok(resource);
    }
    
    
    //============================================== END BATCH - PRESSING ======================================
    
    
    //================================================= BATCH - AGING ==========================================
    
    // GET -----------------------------------------------------------------------------------------------------------
    [HttpGet("batch/{batchId:int}/aging")]
    [SwaggerOperation(
        Summary = "Get a Aging by Batch",
        Description = "Get a Aging by Batch",
        OperationId = "GetAgingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Aging was successfully retrieved", typeof(AgingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Aging was not found")]
    public async Task<IActionResult> GetAgingByBatch(int batchId)
    {
        var aging = await batchQueryService.Handle(new GetAgingByBatchIdQuery(batchId));
        if (aging is null) return NotFound();
        var agingResource = AgingResourceFromEntityAssembler.ToResourceFromEntity(aging);
        return Ok(agingResource);
    }
    
    // POST ---------------------------------------------------------------------------------------------------------
    
    [HttpPost("{batchId:int}/aging")]
    [SwaggerOperation(
        Summary = "Add a Aging to a Batch",
        Description = "Add a Aging to a Batch",
        OperationId = "AddAgingToBatch"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The Aging was successfully added to the Batch", typeof(AgingResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Aging was not added to the Batch")]
    public async Task<IActionResult> AddAgingToBatch([FromBody] AddAgingToBatchResource resource, int batchId)
    {
        var addAgingToBatchCommand = AddAgingToBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        var batch = await batchCommandService.Handle(addAgingToBatchCommand,batchId);
        if (batch is null) return BadRequest();
        
        var agingResource = AgingResourceFromEntityAssembler.ToResourceFromEntity(batch.Aging);
        
        return CreatedAtAction(nameof(GetAgingByBatch), new { batchId = batch.Id }, agingResource);
    }
    
    // DELETE -------------------------------------------------------------------------------------------------------
    
    [HttpDelete("{batchId:int}/aging")]
    [SwaggerOperation(
        Summary = "Delete a Aging by Batch",
        Description = "Delete a Aging by Batch",
        OperationId = "DeleteAgingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Aging was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Aging was not found")]
    public async Task<IActionResult> DeleteAgingByBatch(int batchId)
    {
        var deleteAgingByBatchCommand = new DeleteAgingByBatchCommand(batchId);
        var batch = await batchCommandService.Handle(deleteAgingByBatchCommand);
        if (batch is null) return NotFound();
        return Ok(new { title = "Delete Aging", Message = $"Aging for batch {batchId} was successfully deleted" });
    }
    
    // UPDATE -------------------------------------------------------------------------------------------------------
    
    [HttpPut("{batchId:int}/aging")]
    [SwaggerOperation(
        Summary = "Update a Aging by Batch",
        Description = "Update a Aging by Batch",
        OperationId = "UpdateAgingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Aging was successfully updated", typeof(AgingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Aging was not found")]
    public async Task<IActionResult> UpdateAgingByBatch([FromBody] UpdateAgingByBatchResource resource, int batchId)
    {
        var updateAgingByBatchCommand = UpdateAgingByBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        
        var updatedBatch = await batchCommandService.Handle(updateAgingByBatchCommand, batchId);
        
        if (updatedBatch is null) return NotFound();
        
        return Ok(resource);
    }
    
    //============================================== END BATCH - AGING ======================================
    
    //================================================= BATCH - BOTTLING ==========================================
    // GET -----------------------------------------------------------------------------------------------------------
    [HttpGet("batch/{batchId:int}/bottling")]
    [SwaggerOperation(
        Summary = "Get a Bottling by Batch",
        Description = "Get a Bottling by Batch",
        OperationId = "GetBottlingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status200OK, "The Bottling was successfully retrieved", typeof(BottlingResource))]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Bottling was not found")]
    public async Task<IActionResult> GetBottlingByBatch(int batchId)
    {
        var bottling = await batchQueryService.Handle(new GetBottlingByBatchIdQuery(batchId));
        if (bottling is null) return NotFound();
        var bottlingResource = BottlingResourceFromEntityAssembler.ToResourceFromEntity(bottling);
        return Ok(bottlingResource);
    }
    
    // POST ---------------------------------------------------------------------------------------------------------
    [HttpPost("{batchId:int}/bottling")]
    [SwaggerOperation(
        Summary = "Add a Bottling to a Batch",
        Description = "Add a Bottling to a Batch",
        OperationId = "AddBottlingToBatch"
    )]
    [SwaggerResponse(StatusCodes.Status201Created, "The Bottling was successfully added to the Batch", typeof(BottlingResource))]
    [SwaggerResponse(StatusCodes.Status400BadRequest, "The Bottling was not added to the Batch")]
    public async Task<IActionResult> AddBottlingToBatch([FromBody] AddBottlingToBatchResource resource, int batchId)
    {
        var addBottlingToBatchCommand = AddBottlingToBatchCommandFromResourceAssembler.ToCommandFromResource(resource);
        var batch = await batchCommandService.Handle(addBottlingToBatchCommand, batchId);
        if (batch is null) return BadRequest();
        
        var bottlingResource = BottlingResourceFromEntityAssembler.ToResourceFromEntity(batch.Bottling);
        
        return CreatedAtAction(nameof(GetBottlingByBatch), new { batchId = batch.Id }, bottlingResource);
    }
    
    // DELETE -------------------------------------------------------------------------------------------------------
    [HttpDelete("{batchId:int}/bottling")]
    [SwaggerOperation(
        Summary = "Delete a Bottling by Batch",
        Description = "Delete a Bottling by Batch",
        OperationId = "DeleteBottlingByBatch"
    )]
    [SwaggerResponse(StatusCodes.Status204NoContent, "The Bottling was successfully deleted")]
    [SwaggerResponse(StatusCodes.Status404NotFound, "The Bottling was not found")]
    public async Task<IActionResult> DeleteBottlingByBatch(int batchId)
    {
        var deleteBottlingByBatchCommand = new DeleteBottlingByBatchCommand(batchId);
        var batch = await batchCommandService.Handle(deleteBottlingByBatchCommand);
        if (batch is null) return NotFound();
        return Ok(new { title = "Delete Bottling", Message = $"Bottling for batch {batchId} was successfully deleted" });
    }
    
}

