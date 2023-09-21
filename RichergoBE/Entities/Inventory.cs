using System.ComponentModel.DataAnnotations.Schema;

namespace RichergoBE.Entities
{
    public class Inventory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? ItemId { get; set; }

        [ForeignKey("ItemId")]
        public Item? Item { get; set; } //navigation

        public double InitialQuantity { get; set; }
        public double CurrentQuantity { get; set; }
        public string No { get; set; } = string.Empty;
        public string? Memo { get; set; } = string.Empty;
        public Guid PhotoId { get; set; }
        public int Version { get; set; }
        public Guid? PositionId { get; set; }
        public Guid OwnerId { get; set; }
        public Guid PositionTargetId { get; set; }
        public Guid CreatedById { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;


    }

  }
