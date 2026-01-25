using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace App.Domain.Entities.Basics
{
    [Table("Patient", Schema = "Basic")]
    public class Patient : Entity
    {
        //Basic Information 
        [Required]
        public string? NameAr { get; set; }
        public string? NameEn { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public int? Age { get; set; }
		[Required]
        public string? Gender { get; set; }

        // Contact 
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Address { get; set; }

        // Medical Information 
        public string? BloodType { get; set; } 
        /// <summary>
        /// لديه حساسية
        /// </summary>
        public bool HasAllergies { get; set; }
        /// <summary>
        /// تفاصيل الحساسية
        /// </summary>
        public string? AllergiesDetails { get; set; } 
        /// <summary>
        /// يعاني من أمراض مزمنة
        /// </summary>
        public bool HasChronicDiseases { get; set; } 
        /// <summary>
        /// تفاصيل الأمراض المزمنة
        /// </summary>
        public string? ChronicDiseasesDetails { get; set; } 
        /// <summary>
        /// الأدوية الحالية
        /// </summary>
        public string? CurrentMedications { get; set; } 
        /// <summary>
        /// العمليات الجراحية السابقة
        /// </summary>
        public string? PastSurgeries { get; set; } 
        /// <summary>
        /// التاريخ الطبي للعائلة
        /// </summary>
        public string? FamilyMedicalHistory { get; set; } 


    }
}

