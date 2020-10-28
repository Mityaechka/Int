using System;
using System.Linq;

namespace Intemotion.Entities
{
    public class Voucher
    {
        public int Id { get; set; }

        public string    UserId { get; set; }
        public virtual User User { get; set; }

        public int GameId { get; set; }
        public virtual Game Game { get; set; }

        public string Code { get; set; }
        public bool IsActive { get; set; } = true;

        public Voucher()
        {
            Code = RandomString(8); 
        }
        private static string RandomString(int length)
        {
            Random random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
