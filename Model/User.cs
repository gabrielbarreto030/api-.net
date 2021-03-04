using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Model
{
    public class User
    {   
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage ="Campo obrigatório")]
        [MinLength(3,ErrorMessage ="Campo de no minimo 3 caracteres")]
        [MaxLength(20,ErrorMessage ="Campo excedido")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [MinLength(4, ErrorMessage = "Campo de no minimo 3 caracteres")]
        [MaxLength(20, ErrorMessage = "Campo excedido")]
        public string Password { get; set; }

        public string Role { get; set; }
      
    }

}
