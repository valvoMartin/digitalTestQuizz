using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace digital.Shared.Entities.Test
{
    public class AnswerUser
    {
        public int Id { get; set; }



        public int UserId { get; set; }
        public User User { get; set; } = null!;



        public int QuestionId { get; set; }
        public Question Question { get; set; } = null!;



        public int AnswerId { get; set; }
        public Answer Answer { get; set; } = null!;




        public DateTime DateFinished { get; set; } = DateTime.UtcNow;
    }
}
