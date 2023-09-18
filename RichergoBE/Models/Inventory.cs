namespace RichergoBE.Models
  {
  public class Inventory
    {
    public Guid Id { get; set; }
    public Guid ItemId { get; set; } = Guid.Empty;
    public int Quantity { get; set; } = 0;
    public string Code { get; set; } = string.Empty;
    public string Memo { get; set; } = string.Empty;
    public Guid PhotoId { get; set; } = Guid.Empty;
    public int Version { get; set; } = 0;
    public Guid PositionId { get; set; } = Guid.Empty;
    public Guid OwnerId { get; set; } = Guid.Empty;
    public Guid PositionTargetId { get; set; } = Guid.Empty;
    public Guid CreatedById { get; set; } = Guid.Empty;
    public DateTime CreatedDate { get; set; } = DateTime.Now;
    }
  }
