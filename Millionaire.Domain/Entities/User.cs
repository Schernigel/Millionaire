using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Millionaire.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        [Required]
        [StringLength(50)]
        public string Password { get; set; }

        [Required]
        [Index(IsUnique = true)]
        [StringLength(50)]
        public string Email { get; set; }

        //navigation

        public virtual ICollection<Results> Results { get; set; }

    }
}
