using System.ComponentModel.DataAnnotations;

namespace ProjectFit.Entities
{
    public class Plans
    {
        [Key]
        public int PlanId { get; set; }
        [Required]
        public string PlanName { get; set; }
        [Required]
        public string PlanDescription { get; set; }
        [Required]
        public string PlanDuration { get; set; }
        [Required]
        public string PlanType { get; set; }
        public int PlanPrice { get; set; }

    }
}
