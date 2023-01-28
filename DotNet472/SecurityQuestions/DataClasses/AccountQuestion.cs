namespace SecurityQuestions.DataClasses
{
    public class AccountQuestion
    {
        public int AccountId { get; set; }
        public int QuestionId { get; set; }
        public string Answer { get; set; }

        public AccountQuestion()
        {

        }

        public AccountQuestion(int accountId, int questionId)
        {
            AccountId = accountId;
            QuestionId = questionId;
        }

        public AccountQuestion(Account account, Question question)
        {
            AccountId = account.AccountId;
            QuestionId = question.QuestionId;
        }
    }
}
