using System.ComponentModel.DataAnnotations.Schema;

namespace RichergoBE.Entities
{
    public class WorkPlan
    {
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid? ItemId { get; set; }

    [ForeignKey("ItemId")]
    public Item? Item { get; set; } //navigation

    public Guid StartId { get; set; }
    public Guid EndId { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
    public Guid StatusId { get; set; }
    public Guid CreatedById { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
