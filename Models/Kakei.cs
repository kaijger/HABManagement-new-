using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HABManagement.Models
{
    public class Kakei
    {
        public int ID { get; set; }

        public string Balance { get; set; } = "null";
		[DataType(DataType.Date)]
        public DateTime Date { get; set; }
        public int Price { get; set; }
        public string Category { get; set; } = string.Empty;
        public string Category2 { get; set; } = string.Empty;
        public string? Memo { get; set; } = string.Empty;

    }
}
