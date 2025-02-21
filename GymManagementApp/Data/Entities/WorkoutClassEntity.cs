using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GymManagementApp.Data.Entities
{
    [Table("workout_classes")]
    public class WorkoutClassEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime Schedule { get; set; }

        public int TrainerId { get; set; }

        [ForeignKey("TrainerId")]
        public TrainerEntity Trainer { get; set; }
    }
}
