using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Millionaire.Domain.Entities
{
    public class GameQuestion
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string Question { get; set; }

        [Required]
        public int Level { get; set; }



        //navigation
        public virtual Answer Answer { get; set; }
    }
}
