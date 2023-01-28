using Dapper;
using SecurityQuestions.DataClasses;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SecurityQuestions.DataAccess
{
    public class AccountQuestionAccess : DataAccessBase
    {
        public static List<AccountQuestion> GetAllAccountQuestionsByAccount(int accountId)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"SELECT AccountId, QuestionId, Answer FROM SecurityQuestions.dbo.AccountQuestions WHERE AccountId = {accountId}";
                var accountQuestions = sqlConn.Query<AccountQuestion>(sqlText);
                return accountQuestions.AsList();
            }
        }
        
        public static void AddOrUpdateAccountQuestion(int accountId, int questionId, string answer)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $@"IF EXISTS (SELECT * FROM SecurityQuestions.dbo.AccountQuestions WHERE AccountId = {accountId} AND QuestionId = {questionId})
                                BEGIN
                                    UPDATE SecurityQuestions.dbo.AccountQuestions
                                    SET Answer = '{answer}'
                                END
                                ELSE
                                BEGIN
                                    INSERT INTO SecurityQuestions.dbo.AccountQuestions
                                    (AccountId, QuestionId, Answer)
                                    VALUES({accountId},{questionId},'{answer}')
                                END";
                sqlConn.Execute(sqlText);
            }
        }
    }
}
