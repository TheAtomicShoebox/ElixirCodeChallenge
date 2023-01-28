using SecurityQuestions.DataAccess;
using SecurityQuestions.DataClasses;

namespace SecurityQuestions
{
    public class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                var account = FindAccount();
                var questions = FindAccountQuestions(account.AccountId);
                if(questions.Count == 0)
                {
                    StoreQuestionsFlow(account, questions);
                    continue;
                }
                var response = ReadYesNoResponse("Do you want to answer a security question? (y/n)");
                if (response)
                {
                    AnswerFlow(account, questions);
                }
                else
                {
                    StoreQuestionsFlow(account, questions);
                }
                Pause();
            }
        }

        static void AnswerFlow(Account account, List<AccountQuestion> existingQuestions)
        {
            var questions = QuestionAccess
                .GetAllQuestions()
                .Join(existingQuestions, q => q.QuestionId, aq => aq.QuestionId, (q, aq) => (q, aq));

            bool correct = false;

            foreach(var (question, accountQuestion) in questions)
            {
                var answer = ReadInputWithPrompt(question.QuestionString);
                if(answer == accountQuestion.Answer)
                {
                    Console.WriteLine("CONGRATULATIONS!!! YOU ANSWERED CORRECTLY!!!");
                    correct = true;
                    break;
                }
            }

            if (!correct)
            {
                Console.WriteLine("You ran out of questions before answering one correctly! Try again next time!");
            }
        }

        static void Pause()
        {
            Console.Write("Press any key to continue...");
            Console.ReadKey(true);
        }

        static void StoreQuestionsFlow(Account account, List<AccountQuestion> questions)
        {
            var response = ReadYesNoResponse("Would you like to store answers to security questions? (y/n)");
            if (!response)
            {
                return;
            }
            do
            {
                StoreQuestions(account, questions);
                questions = FindAccountQuestions(account.AccountId);
            }
            while (questions.Count < 3);
        }

        static Account FindAccount()
        {
            var name = ReadInputWithPrompt("Hi, what is your name?");
            var account = AccountAccess.GetAccountByName(name);
            if (account == null)
            {
                Console.WriteLine($"No accounts under {name} found. Adding new account....");
                account = AccountAccess.InsertAccount(name);
                Console.WriteLine($"Account {name} added.");
                return account;
            }
            else
            {
                Console.WriteLine($"Account {name} found.");
                return account;
            }
        }

        static void StoreQuestions(Account account, List<AccountQuestion> existingQuestions)
        {
            var storedQuestions = existingQuestions.Count;
            IEnumerable<(Question, AccountQuestion)> questions;

            questions = existingQuestions.Count == 0 ?
                    QuestionAccess.GetAllQuestions().Select(q => (q, new AccountQuestion(account, q))) :
                    from q in QuestionAccess.GetAllQuestions()
                    join aq in existingQuestions on q.QuestionId equals aq.QuestionId into gj
                    from sub in gj.DefaultIfEmpty()
                    select (q, sub);

            foreach (var (question, accountQuestion) in questions)
            {
                if (storedQuestions >= 3 && ReadYesNoResponse("At least 3 questions have been chosen. Would you like to finish choosing questions? (y/n)"))
                {
                    break;
                }
                if (accountQuestion == null || string.IsNullOrEmpty(accountQuestion.Answer))
                {
                    Console.WriteLine($"Q: {question.QuestionString}");
                    if(ReadYesNoResponse("Would you like to use this question? (y/n)"))
                    {
                        var answer = ReadInputWithPrompt("Answer: ", false);
                        AccountQuestionAccess.AddOrUpdateAccountQuestion(account.AccountId, question.QuestionId, answer);
                        storedQuestions++;
                    }
                }
                else
                {
                    Console.WriteLine($"Q: {question.QuestionString}\nA: {accountQuestion.Answer}");
                    if(ReadYesNoResponse("Would you like to update this question? (y/n)"))
                    {
                        var answer = ReadInputWithPrompt("Answer: ", false);
                        AccountQuestionAccess.AddOrUpdateAccountQuestion(account.AccountId, question.QuestionId, answer);
                    }
                }
            }
        }

        static List<AccountQuestion> FindAccountQuestions(int accountId)
        {
            var questions = AccountQuestionAccess.GetAllAccountQuestionsByAccount(accountId);
            return questions;
        }

        static string ReadInputWithPrompt(string prompt, bool newLine = true)
        {
            if (newLine)
            {
                Console.WriteLine(prompt);
            }
            else
            {
                Console.Write(prompt);
            }

            return Console.ReadLine();
        }

        static bool ReadYesNoResponse(string prompt)
        {
            var response = ReadInputWithPrompt(prompt).ToUpper();
            if(response == "Y")
            {
                return true;
            }
            else if(response == "N")
            {
                return false;
            }
            else
            {
                Console.WriteLine("Invalid input. Please use Y|y or N|n");
                return ReadYesNoResponse(prompt);
            }
        }
    }
}
