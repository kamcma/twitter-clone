using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Data.Models
{
    [Table("users")]
    public class User
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Column("first_name")]
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Column("email_address")]
        [EmailAddress]
        [Required]
        [Display(Name = "Email Address")]
        public string EmailAddress { get; set; }

        [Column("password_hash")]
        public string PasswordHash { get; set; }

        public ICollection<Tweet> Tweets { get; set; }
    }
}