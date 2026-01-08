using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Basics
{
    [Table("Clinic", Schema = "Basic")]
    public class Clinic:Entity
    {
        [Required]
        public string NameAr { get; set; }
        public string? NameEn { get; set; }        
        public string? Email { get; set; }
        public string? Phone { get; set; }
        public string? Mobile { get; set; }
        public string? Fax { get; set; }
        public string? Notes { get; set; }
        public string? ImagePath { get; set; }
    }
}
