namespace digital.Shared.Entities.Test
{
    public class AnswerUser
    {
        public int Id { get; set; }



        public string? Email { get; set; }
        public User? User { get; set; }



        public int QuestionId { get; set; }
        public Question? Question { get; set; }



        public int AnswerId { get; set; }
        public Answer? Answer { get; set; }



        //public Guid TestSessionId { get; set; }

        public int TestNumber { get; set; }

        public DateTime? DateFinished { get; set; }
    }
}
