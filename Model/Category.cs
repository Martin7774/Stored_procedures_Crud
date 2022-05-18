using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Ps_7.Model
{
    public class Category
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public int id { get; set; }
        [Display(Name = "Krótka Nazwa")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public string shortName { get; set; }
        [Display(Name = "Opis")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public string longName { get; set; }
    }
}
