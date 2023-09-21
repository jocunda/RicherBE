namespace RichergoBE.Models
  {
  public class InventoryRequest
    {
    public required string No { get; set; }
    public string? Memo { get; set; }
    public double InitialQuantity { get; set; }
    public Guid ItemId { get; set; }
    }
  }
