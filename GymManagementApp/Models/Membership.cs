using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace GymManagementApp.Models
{
    public class Membership
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Type { get; set; } // np. "Miesięczny", "Roczny"

        public decimal Price { get; set; }
    }
}
