namespace HABManagement.Models
{
    public class SList
    {
        public int ID { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Memo { get; set; } = string.Empty;
        public int? Priority { get; set; }
    }
}
