﻿using HF2BankOpgave.Datalayer.Accounting.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

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

        public const string AccountSQL = "SELECT ID FROM CUSTOMER"; //Get all ID's
        public const string RowCountSQL = "select count(*) FROM [dbo].[Customer] WHERE ID = @Id"; //Get a total of rows

        public const string IDLookUpSQL = @"
        select * from dbo.Customer WHERE Customer.ID = @Id";

        public const string AccountLookupSQL = @"SELECT [ID]
      ,[CreateDate]
      ,[TotalAccountBalance]
      ,[AccountName]
  FROM [dbo].[Account] where _CustomerID = @Id";

        public const string OrderByLookUpSQL = @"SELECT * FROM [dbo].[Customer] Where FirstName = @OrderBy";

        public const string DeleteCustomerSQL = "DELETE FROM [dbo].[Customer] WHERE ID = @Id";

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
        /// Search for customers by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public static IEnumerable<CustomerModel> CustomerLookUpID(int Id, string OrderBy)
        {
            var sql = IDLookUpSQL;

            sql += string.Format(@" ORDER BY {0} DESC", OrderBy);

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
                            result.Add(new CustomerModel()
                            {
                                CustomerID = rdr.GetInt32(0),
                                FirstName = rdr.GetString(1),
                                LastName = rdr.GetString(2),
                                CreateDate = rdr.GetDateTime(3)
                            });
                        }
                    }
                }
            }
            return result;
        }

        /// <summary>
        /// Search for customers by name
        /// </summary>
        /// <param name="Name"></param>
        /// <returns></returns>
        [HttpGet]
        public static IEnumerable<CustomerModel> CustomerLookUpName(string Name)
        {
            var sql = OrderByLookUpSQL;

            var result = new List<CustomerModel>();
            using (var con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (var cmd = new SqlCommand(sql, con))
                {
                    cmd.Parameters.AddWithValue("OrderBy", Name);

                    using (var rdr = cmd.ExecuteReader())
                    {
                        while (rdr.Read())
                        {
                            result.Add(new CustomerModel()
                            {
                                CustomerID = rdr.GetInt32(0),
                                FirstName = rdr.GetString(1),
                                LastName = rdr.GetString(2),
                                CreateDate = rdr.GetDateTime(3)
                            });
                        }
                    }
                }

            }
            return result;
        }

        /// <summary>
        /// get account data on a specific customer by ID
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="OrderBy"></param>
        /// <returns></returns>
        [HttpGet]
        public static IEnumerable<Account> AccountLookUpID(int Id, string OrderBy)
        {
            var sql = AccountLookupSQL;

            sql += string.Format(@" ORDER BY {0} DESC", OrderBy);

            var result = new List<Account>();
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
                            result.Add(new Account()
                            {
                                AccountID = rdr.GetInt32(0),
                                AccountName = rdr.GetString(3),
                                CreateDate = rdr.GetDateTime(1),
                                TotalAccountBalance = rdr.GetDecimal(2)
                            });
                        }
                    }
                }
            }
            return result;
        }

        public static void CreateCustomer()
        {

        }

        [HttpPost]
        public static void UpdateCustomer(int CustomerID, CustomerModel customerModel)
        {

        }

        /// <summary>
        /// Delete a single user, by CustomerID.
        /// </summary>
        /// <param name="CustomerID"></param>
        [HttpPost]
        public static void DeleteCustomer(int CustomerID)
        {
            var sql = OrderByLookUpSQL;

            using (SqlConnection con = new SqlConnection(ConnectionString))
            {
                con.Open();

                using (SqlCommand command = new SqlCommand(sql, con))
                {
                    command.ExecuteNonQuery();
                }
            }
        }

        public static void UpdateAccount(int AccountID, Account accountModel)
        {

        }

        [HttpPost]
        public static void DeleteAccount(int AccountID)
        {

        }

        
    }
}