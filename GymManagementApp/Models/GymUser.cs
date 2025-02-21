using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementApp.Models
{
    public class GymUser
    {
        [Key]
        public int Id { get; set; }  // ✅ Tylko jedna definicja

        [Required(ErrorMessage = "Pole Imię i nazwisko jest wymagane")]
        public string Name { get; set; } = string.Empty;  // ✅ Ustawienie domyślnej wartości

        [Required(ErrorMessage = "Pole Email jest wymagane")]
        [EmailAddress(ErrorMessage = "Podaj poprawny adres e-mail")]
        public string Email { get; set; } = string.Empty;  // ✅ Zapobieganie `null`

        [Required(ErrorMessage = "Pole Karnet jest wymagane")]
        [Display(Name = "Karnet")]
        public int MembershipId { get; set; }

        [ForeignKey("MembershipId")]
        public Membership? Membership { get; set; }  // ✅ Obsługuje `null`
    }
}
