using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace Millionaire.Domain.Entities
{
    public class Answer
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }

        [Required]
        [StringLength(400)]
        public string First { get; set; }

        [Required]
        [StringLength(400)]
        public string Second { get; set; }

        [Required]
        [StringLength(400)]
        public string Third { get; set; }

        [Required]
        [StringLength(400)]
        public string Fourth { get; set; }

        [Required]
        public int Сorrect { get; set; }

        //navigation
        public virtual GameQuestion GameQuestion { get; set; }
    }
}
