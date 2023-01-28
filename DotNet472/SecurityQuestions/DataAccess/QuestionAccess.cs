using Dapper;
using SecurityQuestions.DataClasses;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace SecurityQuestions.DataAccess
{
    public class QuestionAccess : DataAccessBase
    {
        public static Question GetQuestion(int questionId)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"SELECT QuestionId, QuestionString FROM SecurityQuestions.dbo.Questions WHERE QuestionId = {questionId}";
                var question = sqlConn.QuerySingle<Question>(sqlText);
                return question;
            }
        }

        public static List<Question> GetAllQuestions()
        {
            using(var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"SELECT QuestionId, QuestionString FROM SecurityQuestions.dbo.Questions ORDER BY QuestionId";
                return sqlConn.Query<Question>(sqlText).AsList();
            }
        }
    }
}
