using LionFishWeb.Models;
using LionFishWeb.Utility;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LionFishWeb.Repositories
{
    public class ConfirmationCodeRepo : IConfirmationCodeRepo
    {
        public bool AddConfirmationCode(ConfirmationCode cc)
        {
            int conv;
            if (cc.IsPasswordReset) conv = 1;
            else conv = 0;

            string query = "insert into confirmationcode(code, email, ispasswordreset) values (@code, @email, @ispasswordreset)";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@code", cc.Code);
                command.Parameters.AddWithValue("@email", cc.Email);
                command.Parameters.AddWithValue("@ispasswordreset", conv);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public bool DeleteConfirmationCode(string email)
        {
            string query = "delete from confirmationcode where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    command.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return true;
        }

        public ConfirmationCode GetConfirmationCode(string email)
        {
            string query = "select * from confirmationcode where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();
                    ConfirmationCode cc = new ConfirmationCode();

                    read.GetOrdinal("code");
                    read.GetOrdinal("email");
                    read.GetOrdinal("ispasswordreset");
                    read.GetOrdinal("date");

                    while (read.Read())
                    {
                        cc.Code = read.GetString("code");
                        cc.Email = read.GetString("email");
                        cc.IsPasswordReset = Convert.ToBoolean(read.GetInt16("ispasswordreset"));
                        cc.Date = read.GetDateTime("date");
                    }
                    return cc;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return null;
                }
            }
        }

        public bool CheckConfirmationCodeByEmail(string email)
        {
            string query = "select * from confirmationcode where email = @email";
            using (MySqlConnection connection = new MySqlConnection(Constants.Conn))
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.Parameters.AddWithValue("@email", email);

                try
                {
                    connection.Open();
                    var read = command.ExecuteReader();

                    if (read.Read())
                    {
                        return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return false;
                }
            }
            return false;
        }
    }
}