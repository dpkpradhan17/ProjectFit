using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Globalization;

namespace ProjectFit.Entities
{
    public class Admission
    {
        [Key]
        public int AdmissionId { get; set; }
        [Required]
        [ForeignKey("Coaches")]
        public int CoachId { get; set; }
        [Required]
        [ForeignKey("Plans")]
        public int PlanId { get; set; }
        [Required]
        [ForeignKey("User")]
        public string Email { get; set; }
        [Required]
        public string Status { get; set; }
        public string PlanFeedBack { get; set; }
        public string CoachFeedback { get; set; }
        public Coaches Coaches { get; set; }
        public Plans Plans { get; set; }
        public User User { get; set; }
    }
}
