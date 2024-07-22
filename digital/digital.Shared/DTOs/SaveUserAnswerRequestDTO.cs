namespace digital.Shared.DTOs
{
    public class SaveUserAnswerRequestDTO
    {
        public string Email { get; set; } = null!;
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public bool IsLast { get; set; }
        //public Guid TestSessionId { get; set; }

        public int TestNumber { get; set; }
    }
}
