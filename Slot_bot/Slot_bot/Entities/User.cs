using System.ComponentModel.DataAnnotations;

namespace Slot_bot.Entities
{
    public class User
    {
        [Key]
        public ulong Id { get; set; }  
        public string Username { get; set; }
        public int ScoreAmount { get; set; } = 500;
    }
}
