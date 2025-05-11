using ElixirControlPlatform.API.Profiles.Domain.Model.Aggregate;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Commands;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;
using ElixirControlPlatform.API.WinemakingProcess.Domain.Model.ValueObjects;

namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Aggregate;

public partial class Batch
{
    public int Id { get; private set; }
    
    //******************* BATCH RELATIONSHIP WITH PROFILE *******************
    public Guid ProfileId { get; set; } // Clave foránea hacia Profile
    public Profile Profile { get; private set; }
    
    //*************** END BATCH RELATIONSHIP WITH PROFILE *******************
    

    //========================== Batch Information ==========================
    public string VineyardCode { get; private set; }
    public string GrapeVariety { get; private set; }
    public string HarvestDate { get; private set; }
    public int GrapeQuantity { get; private set; }
    public string VineyardOrigin { get; private set; }
    public string ProcessStartDate { get; private set; }

    public CurrentBatchStatus Status { get; internal set; }

    //======================== end Batch Information ========================
    

    //============ Winemaking Process - Propiedad de navegación =============
    public Fermentation Fermentation { get; private set; }
    public Clarification Clarification { get; private set; }
    public Pressing Pressing { get; private set; }
    public Aging Aging { get; private set; }
    
    public Bottling Bottling { get; private set; }
    //=========== end Winemaking Process - Propiedad de navegación ==========
    
    
    public Batch()
    {
        this.Status = CurrentBatchStatus.Collected;
        this.VineyardCode = string.Empty;
        this.GrapeVariety = string.Empty;
        this.HarvestDate = string.Empty;
        this.GrapeQuantity = 0;
        this.VineyardOrigin = string.Empty;
        this.ProcessStartDate = string.Empty;
    }

    public Batch(string vineyardCode, string grapeVariety, string harvestDate, int grapeQuantity, string vineyardOrigin,
        string processStartDate, Guid profileId) : this()
    {
        VineyardCode = vineyardCode;
        GrapeVariety = grapeVariety;
        HarvestDate = harvestDate;
        GrapeQuantity = grapeQuantity;
        VineyardOrigin = vineyardOrigin;
        ProcessStartDate = processStartDate;
        ProfileId = profileId;
    }

    public Batch(CreateBatchCommand command, Guid profileId) : this()
    {
        VineyardCode = command.VineyardCode;
        GrapeVariety = command.GrapeVariety;
        HarvestDate = command.HarvestDate;
        GrapeQuantity = command.GrapeQuantity;
        VineyardOrigin = command.VineyardOrigin;
        ProcessStartDate = command.ProcessStartDate;
        
        ProfileId = profileId;
    }
    
    public void UpdateBatch(UpdateBatchCommand command)
    {
        VineyardCode = command.VineyardCode;
        GrapeVariety = command.GrapeVariety;
        HarvestDate = command.HarvestDate;
        GrapeQuantity = command.GrapeQuantity;
        VineyardOrigin = command.VineyardOrigin;
        ProcessStartDate = command.ProcessStartDate;
    }
    
    
    //========================================================================
    //========================== Winemaking Process ==========================
    //========================================================================
    
    //============================= FERMENTATION =============================
    public void AddFermentationByBatch(int batchId, string startDate, string endDate, double averageTemperature,
        double initialDensity, double initialPh, double finalDensity, double finalPh, double residualSugar)
    {
        Fermentation = new Fermentation(batchId, startDate, endDate, averageTemperature, initialDensity, initialPh,
            finalDensity, finalPh, residualSugar);
        this.Status = CurrentBatchStatus.Fermentation;
    }
    
    public void DeleteFermentationByBatch()
    {
        Fermentation = null;
        this.Status = CurrentBatchStatus.Collected;
    }

    public void UpdateFermentationByBatch(UpdateFermentationByBatchCommand command)
    {
        Fermentation = new Fermentation(Id, command.StartDate, command.EndDate, command.AverageTemperature,
            command.InitialDensity, command.InitialPh, command.FinalDensity, command.FinalPh, command.ResidualSugar);
    }
    
    //------------------------ end FERMENTATION ----------------------------

    //============================= CLARIFICATION =============================
    public void AddClarificationByBatch(int batchId, string productsUsed, string clarificationMethod,
        string filtrationDate, double clarityLevel, string startDate, string endDate)
    {
        Clarification = new Clarification(batchId, productsUsed, clarificationMethod, filtrationDate, clarityLevel,
            startDate, endDate);
        this.Status = CurrentBatchStatus.Clarification;
    }
    
    public void DeleteClarificationByBatch()
    {
        Clarification = null;
        this.Status = CurrentBatchStatus.Pressing;
    }
    
    public void UpdateClarificationByBatch(UpdateClarificationByBatchCommand command)
    {
        Clarification = new Clarification(Id, command.ProductsUsed, command.ClarificationMethod, command.FiltrationDate,
            command.ClarityLevel, command.StartDate, command.EndDate);
    }
    
    //------------------------ end Clarification ----------------------------

    //============================= PRESSING =============================
    public void AddPressingByBatch(int batchId, string pressingDate, double mustVolume, string pressType,
        double appliedPressure)
    {
        Pressing = new Pressing(batchId, pressingDate, mustVolume, pressType, appliedPressure);
        this.Status = CurrentBatchStatus.Pressing;
    }
    
    public void DeletePressingByBatch()
    {
        this.Pressing = null;
        this.Status = CurrentBatchStatus.Fermentation;
    }
    
    public void UpdatePressingToBatch(UpdatePressingByBatchCommand command)
    {
        Pressing = new Pressing(Id, command.PressingDate, command.MustVolume, command.PressType, command.AppliedPressure);
    }

    //------------------------ end Pressing ----------------------------

    
    //============================= AGING =============================
    public void AddAgingByBatch(int BatchId, string BarrelType, string StartDate, string EndDate, int AgingDurationMonths, int InspectionsPerformed, string InspectionResult)
    {
        Aging = new Aging(BatchId, BarrelType, StartDate, EndDate, AgingDurationMonths, InspectionsPerformed, InspectionResult);
        this.Status = CurrentBatchStatus.Aging;
    }
    
    public void DeleteAgingByBatch()
    {
        Aging = null;
        this.Status = CurrentBatchStatus.Clarification;
    }
    
    public void UpdateAgingByBatch(UpdateAgingByBatchCommand command)
    {
        Aging = new Aging(Id, command.BarrelType, command.StartDate, command.EndDate, command.AgingDurationMonths, command.InspectionsPerformed, command.InspectionResult);
    }
    //------------------------ end Aging ----------------------------
    
    //============================= BOTTLING =============================
    
    public void AddBottlingByBatch(int batchId, string bottlingDate, string bottleSizeMl, int numberOfBottles,
        string labelType, string corkType)
    {
        Bottling = new Bottling(batchId, bottlingDate, bottleSizeMl, numberOfBottles, labelType, corkType);
        this.Status = CurrentBatchStatus.Bottling;
    }
    
    public void DeleteBottlingByBatch()
    {
        Bottling = null;
        this.Status = CurrentBatchStatus.Aging;
    }
    
    public void UpdateBottlingByBatch(UpdateBottlingByBatchCommand command)
    {
        Bottling = new Bottling(Id, command.BottlingDate, command.BottleSizeMl, command.NumberOfBottles, command.LabelType, command.CorkType);
    }

    //------------------------ end Bottling ----------------------------
    
    //========================================================================
    //======================== end Winemaking Process ========================
    //========================================================================
}