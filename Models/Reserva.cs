using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace GYM.Models
{
    public class Reserva
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ReserveId { get; set; }

        public string User { get; set; }

        [Required]
        public int AmountOfPeople { get; set; }

        public int FunctionId { get; set; }

    }
}
