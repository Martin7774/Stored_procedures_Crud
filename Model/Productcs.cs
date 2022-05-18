using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace Ps_7.Model
{
    public class Product
    {
        [Display(Name = "Id")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public int id { get; set; }
        [Display(Name = "Nazwa")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public string name { get; set; }
        [Display(Name = "Cena")]
        [Range(0, 9999999999999999999, ErrorMessage = "Wartości nie mogą być ujemne")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public decimal price { get; set; }
        [Display(Name = "Opis")]
        public string description { get; set; }
        [Display(Name = "Id category")]
        [Required(ErrorMessage = "Pole jest obowiazkowe")]
        public int categoryId { get; set; }
    }
}
