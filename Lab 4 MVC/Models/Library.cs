using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Lab_4_MVC.Models
{
    public class Library
    {
        [Key]
        public int LibraryId { get; set; }
        [ForeignKey("Customer")]
        public int FKCustomerId { get; set; }
        public Customer Customer { get; set; }
        [ForeignKey("Book")]
        public int FKBookId { get; set; }
        public Book Book { get; set; }
        public DateOnly? BorrowDateStart { get; set; }
        public DateOnly? borrowDateEnd { get; set; }
    }
}
