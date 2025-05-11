namespace ElixirControlPlatform.API.WinemakingProcess.Domain.Model.Entities;

public class Bottling
{
    public int Id { get; set; }
    
    //========================= Bottling Information =========================
    public int BatchId { get; private set; }
    
    public string BottlingDate { get; private set; }
    
    public string BottleSizeMl { get; private set; }
    
    public int NumberOfBottles { get; private set; }
    
    public string LabelType { get; private set; }
    
    public string CorkType { get; private set; }
    
    //======================= end Bottling Information ======================
    
    
    public Bottling()
    {
        this.BatchId = 0;
        this.BottlingDate = string.Empty;
        this.BottleSizeMl = string.Empty;
        this.NumberOfBottles = 0;
        this.LabelType = string.Empty;
        this.CorkType = string.Empty;
    }
    
    public Bottling(int batchId, string bottlingDate, string bottleSizeMl, int numberOfBottles, string labelType, string corkType) : this()
    {
        BatchId = batchId;
        BottlingDate = bottlingDate;
        BottleSizeMl = bottleSizeMl;
        NumberOfBottles = numberOfBottles;
        LabelType = labelType;
        CorkType = corkType;
    }
}