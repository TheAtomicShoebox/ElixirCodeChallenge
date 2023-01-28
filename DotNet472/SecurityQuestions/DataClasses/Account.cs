namespace SecurityQuestions.DataClasses
{
    public class Account
    {
        public int AccountId { get; set; }
        public string AccountName { get; set; }

        public Account()
        {

        }

        public Account(int accountId, string accountName)
        {
            AccountId = accountId;
            AccountName = accountName;
        }
    }
}
