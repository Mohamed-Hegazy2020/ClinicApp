using System.ComponentModel.DataAnnotations;

namespace App.Domain.Entities
{
    public class Entity
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Code { get; set; }
        public DateTime? CreationDate { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
