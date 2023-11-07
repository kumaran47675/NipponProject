using System.Numerics;

namespace Nippon_Final_Project.Models
{
    public class MergedRecord
    {
        public string UserId { get; set; }
        public string Password { get; set; }
        public string UserName { get; set; }
        public string Depot { get; set; }

        public bool Admin { get; set; } 
       
    }
}
