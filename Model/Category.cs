using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Shop.Model
{
    //[Table("Categoria")] //coloca um nome personalizado na tabela
    public class Category
    {
        [Required(ErrorMessage ="Este campo é obrigatório")]
         //[Column("Cat_id")] //Coloca nome de coluna em um campo
        public int Id { get; set; }

        [Required(ErrorMessage = "Este campo é obrigatório")]
        [MinLength(3,ErrorMessage ="O valor minimo de caracteres é 3")]
        [MaxLength(60,ErrorMessage ="O valor maximo de caracteres é 60 ")]
        [DataType("varchar")]
        public string Title { get; set; }

    }
}
