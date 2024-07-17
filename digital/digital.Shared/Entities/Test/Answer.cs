using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Entities.Test
{
    public class Answer
    {
        public int Id { get; set; }

        [Display(Name = "Respuesta")]
        [MaxLength(100, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Text { get; set; } = null!;


        [Display(Name = "Puntaje")]
        [Range(0, 10, ErrorMessage = "El campo {0} debe estar entre {1} y {2}.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Score { get; set; }


        [Display(Name = "Pregunta")]
        [Range(0, int.MaxValue, ErrorMessage = "Debes seleccionar una {0}.")]
        [Required(ErrorMessage = "Debe seleccionar un valor para {1}.")]
        public int QuestionId { get; set; }

        public Question? Question { get; set; }




        // Otros campos relevantes...

        public ICollection<AnswerUser>? AnswerUsers { get; set; }
    }
}
