using Dapper;
using SecurityQuestions.DataClasses;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SecurityQuestions.DataAccess
{
    public class AccountAccess : DataAccessBase
    {
        public static Account GetAccount(int accountId)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"SELECT AccountId, AccountName FROM SecurityQuestions.dbo.Accounts WHERE AccountId = {accountId}";
                var account = sqlConn.QueryFirstOrDefault<Account>(sqlText);
                return account;
            }
        }

        public static Account GetAccountByName(string accountName)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"SELECT AccountId, AccountName FROM SecurityQuestions.dbo.Accounts WHERE AccountName = '{accountName}'";
                var account = sqlConn.QueryFirstOrDefault<Account>(sqlText);
                return account;
            }
        }

        public static List<Account> GetAccountsByQuestion(int questionId)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = @"SELECT a.AccountId, a.AccountName 
                                FROM SecurityQuestions.dbo.Accounts
                                JOIN SecurityQuestions.dbo.AccountQuestions aq
                                WHERE aq.QuestionId = " + questionId;
                var accounts = sqlConn.Query<Account>(sqlText);
                return accounts.AsList();
            }
        }

        public static Account InsertAccount(string accountName)
        {
            using (var sqlConn = new SqlConnection(ConnectionString))
            {
                var sqlText = $"INSERT INTO SecurityQuestions.dbo.Accounts (AccountName) VALUES ('{accountName}')";
                var rows = sqlConn.Execute(sqlText);
                return GetAccountByName(accountName);
            }
        }
    }
}
