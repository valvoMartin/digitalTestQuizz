    using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Entities.Test
{
    public class Question
    {
        public int Id { get; set; }



        [Display(Name = "Pregunta")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Text { get; set; } = null!;


        //public List<Answer>? Answers { get; set; }

        public ICollection<Answer>? Answers { get; set; }


        public ICollection<AnswerUser>? AnswerUsers { get; set; }

    }
}
