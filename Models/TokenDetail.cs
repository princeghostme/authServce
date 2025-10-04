using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TokenDetail
    {
        public int id { get; set; } = 1;
        public string? code { get; set; } = "USR001";
        public string? firstName { get; set; } = "John";
        public string? lastName { get; set; } = "Doe";
        public string? email { get; set; } = "test@email.com";
        public string? phone { get; set; } = "700457707";
        public string? loginVia { get; set; } = "test";
        public DateTime requestOn { get; set; } = DateTime.Now;
        public List<int>? roles { get; set; } = new List<int>() { 1, 2, 3 };

    }
}
