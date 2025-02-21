using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace GymManagementApp.Data.Entities
{
    [Table("trainers")]
    public class TrainerEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Specialization { get; set; }

        public ICollection<WorkoutClassEntity> WorkoutClasses { get; set; }
    }
}
