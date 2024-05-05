using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Lab_4_MVC.Models
{
    public class Customer
    {
        [Key]
        public int CustomerId { get; set; }
        [Required]
        [StringLength(maximumLength:30, MinimumLength = 10)]
        public string CustomerName { get; set; }
        [Required]
        [StringLength(maximumLength: 20, MinimumLength = 9)]
        public string CustomerAdress { get; set; }
        [Required]
        [StringLength(maximumLength:50)]
        public string CustomerEmail { get; set; }
        [Required]
        [Range(1,120)]
        public int CustomerAge { get; set; }
        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public ICollection<Library>? Libraries { get; set; }
    }
}
