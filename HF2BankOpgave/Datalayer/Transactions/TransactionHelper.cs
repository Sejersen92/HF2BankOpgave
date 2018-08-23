using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace HF2BankOpgave.Datalayer.Transactions
{
    public class TransactionHelper
    {
        private static string ConnectionString { get; set; }

        public TransactionHelper (string connectionString)
        {
            ConnectionString = connectionString;
        }

        public const string AccountSQL = "SELECT ID FROM CUSTOMER"; //Get all ID's
        public const string RowCountSQL = "select count(*) FROM [dbo].[Customer] WHERE ID = @Id"; //Get a total of rows
        public const string TransactionByDate = "SELECT * FROM dbo.[Transaction] Where ID = @Id and Date between @FromDate and @ToDate";

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

        /// <summary>
        /// Search transactions on a specific account, for a specific customer. Insert unique account ID, a fromDate and a toDate.
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="FromDate"></param>
        /// <param name="ToDate"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public static IEnumerable<Transaction> TransactionLookUpByDate(int Id, DateTime FromDate, DateTime ToDate, string OrderBy)
        {
            var sql = TransactionByDate;

            sql += string.Format(@" ORDER BY {0} DESC", OrderBy);

            var result = new List<Transaction>();
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("Id", Id);
                    cmd.Parameters.AddWithValue("FromDate", FromDate);
                    cmd.Parameters.AddWithValue("ToDate", ToDate);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(new Transaction()
                            {
                                TransactionID = rdr.GetInt32(0),
                                AccountID = rdr.GetInt32(1),
                                CustomerID = rdr.GetInt32(2),
                                Date = rdr.GetDateTime(3),
                                Amount = rdr.GetDecimal(4),
                                Description = rdr.GetString(5)
                            });
                        }
                    }
                }
            }
            return result;
        }
    }
}