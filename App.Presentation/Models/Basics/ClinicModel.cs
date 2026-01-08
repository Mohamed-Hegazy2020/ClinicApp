using System.ComponentModel.DataAnnotations;

namespace App.Presentation.Models.Basics
{
    public class ClinicModel:BaseModel
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
