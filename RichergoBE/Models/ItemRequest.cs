namespace RichergoBE.Entities
{
    public class ItemRequest
    {
        public string Value { get; set; } = string.Empty;
        public string Code { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
        public Guid CreatedById { get; set; } 
    }
}
