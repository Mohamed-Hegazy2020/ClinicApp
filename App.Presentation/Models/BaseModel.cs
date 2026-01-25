using System.ComponentModel.DataAnnotations;

namespace App.Presentation.Models
{
    public class BaseModel
    {
        [Key]
        public int Id { get; set; }
        [Required]
		[Display(Name = "NameAr")]
		public int Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
