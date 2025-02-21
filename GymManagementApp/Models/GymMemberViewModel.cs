using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace GymManagementApp.Models
{
    public class GymMemberViewModel
    {
        [HiddenInput]
        public int? Id { get; set; }

        [Required(ErrorMessage = "Imię i nazwisko jest wymagane!")]
        public string Name { get; set; }

        [RegularExpression(pattern: ".+\\@.+\\.[a-z]{2,3}", ErrorMessage = "Niepoprawny format email!")]
        [Required(ErrorMessage = "Email jest wymagany!")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Wiek jest wymagany!")]
        [Range(18, 100, ErrorMessage = "Wiek musi wynosić co najmniej 18 lat!")]
        public int Age { get; set; }

        [Required(ErrorMessage = "Wybierz status członkostwa!")]
        public string MembershipType { get; set; }
    }
}
