using System.ComponentModel.DataAnnotations.Schema;

namespace RichergoBE.Entities
{
    public class Segmentation
    {
    public Guid Id { get; set; } = Guid.NewGuid();
    public double Quantity { get; set; }
    public Guid OwnerId { get; set; }
    public Guid TargetId { get; set; }
    public Guid CreatedById { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
