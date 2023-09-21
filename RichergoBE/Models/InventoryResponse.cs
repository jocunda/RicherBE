namespace RichergoBE.Models
  {
  public class InventoryResponse
    {
    public Guid Id { get; set; }
    public required string No { get; set; }
    public string? Memo { get; set; }
    public double CurrentQuantity { get; set; }
    public Guid PhotoId { get; set; }
    public Guid ItemId { get; set; }
    public required string ItemValue { get; set; }
    }
  }
