namespace SecurityQuestions.DataClasses
{
    public class Question
    {
        public int QuestionId { get; set; }
        public string QuestionString { get; set; }

        public Question()
        {

        }

        public Question(int questionId, string questionString)
        {
            QuestionId = questionId;
            QuestionString = questionString;
        }
    }
}
