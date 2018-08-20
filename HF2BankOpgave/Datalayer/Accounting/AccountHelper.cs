using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace HF2BankOpgave.Datalayer.Accounting
{
    public class AccountHelper
    {
        private static string ConnectionString { get; set; }
        private string AccountId { get; set; }

        public AccountHelper(string connectionString)
        {
            ConnectionString = connectionString;
        }
        
        public const string AccountSQL = "SELECT Id FROM CUSTOMERDB"; //Get all ID's
        public const string RowCountSQL = "select count(*) FROM [dbo].[Customer] WHERE Id = @Id"; //Get a total of rows
        public const string LookUpSQL = @"SELECT [Id]
                              ,[FirstName]
                              ,[LastName]
                              ,[CreateDate]
                               FROM [dbo].[Customer]
                               WHERE Id = @Id
                               ";

        private static IEnumerable<string> AccountCache = new List<string>();
        private static DateTime AccountTimeStamp;
        private object thislock = new object();

        public IEnumerable<string> GetAccountIds()
        {
            if (AccountCache != null && AccountCache.Any() && AccountTimeStamp > DateTime.UtcNow.Date.AddHours(-24))
            {
                return AccountCache;
            }
            lock (thislock)
            {
                var result = new List<string>();
                using (var con = new SqlConnection(ConnectionString))
                {
                    con.Open();

                    using (var cmd = new SqlCommand(AccountSQL, con))
                    {
                        using (var rdr = cmd.ExecuteReader())
                        {
                            while (rdr.Read())
                            {
                                result.Add(rdr[0] as string);
                            }
                        }
                    }
                }
                AccountCache = result;
                AccountTimeStamp = DateTime.UtcNow;
                return result;
            }
        }

        public int RowCount(string Id)
        {
            var sql = RowCountSQL;

            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("Id", Id);

                    return (int)cmd.ExecuteScalar();
                }
            }
        }

        public static IEnumerable<CustomerModel> LookUp(string Id, string OrderBy)
        {
            var sql = LookUpSQL;

            sql += string.Format(@" ORDER BY {0} DESC OFFSET @index ROWS FETCH NEXT @count ROWS ONLY", OrderBy);

            var result = new List<CustomerModel>();
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("Id", Id);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(new CustomerModel() { ID = rdr.GetInt32(0), FirstName =  rdr.GetString(1), LastName = rdr.GetString(2), CreateDate = rdr.GetDateTime(3) });
                        }
                    }
                }

            }
            return result;
        }
    }
}