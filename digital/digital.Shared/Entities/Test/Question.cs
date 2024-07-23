using System.ComponentModel.DataAnnotations;

namespace digital.Shared.Entities.Test
{
    public class Question
    {
        public int Id { get; set; }



        [Display(Name = "Pregunta")]
        [MaxLength(200, ErrorMessage = "El campo {0} no puede tener más de {1} caracteres.")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Text { get; set; } = null!;




        // Clave foránea para la relación con SubArea
        public int SubAreaId { get; set; }
        public SubArea? SubArea { get; set; }







        public ICollection<Answer>? Answers { get; set; }

        public ICollection<AnswerUser>? AnswerUsers { get; set; }



    }
}
