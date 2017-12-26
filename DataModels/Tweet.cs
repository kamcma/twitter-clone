using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TwitterClone.Data.Models
{
    [Table("tweets")]
    public class Tweet
    {
        [Key]
        [Column("id")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [ForeignKey("author")]
        public User Author { get; set;}

        [Column("body")]
        [StringLength(256)]
        [DataType(DataType.Text)]
        [Required]
        public string Body { get; set; }

        [Column("timestamp")]
        [DataType(DataType.DateTime)]
        [Required]
        public DateTime Timestamp { get; set; }
    }
}