using System.ComponentModel.DataAnnotations;

namespace Slot_bot.Entities
{
    public class User
    {
        [Key]
        public string Id { get; set; }  
        public string Username { get; set; }
        public int Balance { get; set; } = 500;
    }
}
