using System.ComponentModel.DataAnnotations;

namespace HABManagement.Models
{
    public class Kakei
    {
        public int ID { get; set; }

        public string Balance { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Category2 { get; set; } = string.Empty;
        public string? Memo { get; set; } = string.Empty;
    }
}
