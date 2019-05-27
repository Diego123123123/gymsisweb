using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Models
{
    public class UserReserve
    {
        public int ReserveId { get; set; }

        public string email { get; set; }

        public string function { get; set; }

        public int amount { get; set; }
    }
}
