using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using RestAPI8.Models;

namespace RestAPI8.Models
{
    public class User
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Password { get; set; }
        public required string Role { get; set; }

        public override string ToString()
        {
            return $"User(Id = {Id}, Name={Name}, Role={Role})";
        }
    }
}
