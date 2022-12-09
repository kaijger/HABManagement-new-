using System.ComponentModel.DataAnnotations;

namespace HABManagement.Models
{
    public class Food
    {
        public int ID { get; set; }

        public string Name { get; set; } = string.Empty;
        public int Num { get; set; }
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [DataType(DataType.Date)]
        public DateTime? ExpiryDate { get; set; }
        public string? Memo { get; set; } = string.Empty;
    }
}
