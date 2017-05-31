using System;
using System.Data.SqlClient;
using System.IO;
using System.Runtime.CompilerServices;
using MySql.Data.MySqlClient;
using System.Text.RegularExpressions;

namespace ConsoleApplication14
{
    public class DBConnect
    {
        private MySqlConnection connection;

        public DBConnect()
        {
            Initialize();
        }

        public MySqlConnection Initialize()
        {
            const string server = "localhost";
            const string database = "chattmob";
            const string uid = "root";
            const string password = "T0mgr33n";
            const string connectionString = "SERVER=" + server + ";" + "DATABASE=" + database + ";" + "UID=" + uid + ";" +
                                            "PASSWORD=" + password + ";";
            connection = new MySqlConnection(connectionString);
            return connection;
        }

        public bool OpenConnection()
        {
            try
            {
                connection.Open();
                Console.WriteLine("Connection Success");
                return true;
            }
            catch (MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        Console.WriteLine("Cannot connect to server.  Contact Adam Parson");
                        break;

                    case 1045:
                        Console.WriteLine("Invalid username/password, please try again");
                        break;
                }
                return false;
            }
        }

        public bool CloseConnection()
        {
            try
            {
                connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                Console.WriteLine(ex.Message);
                return true;
            }
        }

        public void Insert()
        {
            for (int j = 0; j < 20; j++)
                try
                {
                    using (StreamReader file = new StreamReader(@"C:\Users\Robert\Desktop\Chatt Mobility\Download Here\testdelete.txt"))
                    {
                        string firstName = null;
                        string lastName = null;
                        string email = null;
                        string phone = null;
                        string date = null;
                        string product = null;
                        string manufacturer = null;
                        string description = null;

                        if (this.OpenConnection() == true)
                        {
                            string line;
                            while ((line = file.ReadLine()) != null)
                            {
                                char[] delimiters = new char[] { '\t' };
                                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                if (parts.Length > 0)
                                {
                                    for (int i = 0; i < parts.Length - 7; i++)
                                    {
                                        // list.Add(part);
                                        firstName = Regex.Replace(parts[i], @"[^\w\s]", "");
                                        lastName = Regex.Replace(parts[i + 1], @"[^\w\s]", ""); ;
                                        email = Regex.Replace(parts[i + 2], @"[^\w\s.@-_]", ""); ;
                                        phone = Regex.Replace(parts[i + 3], @"[^\w\s-]", "");
                                        date = parts[i + 4];
                                        product = Regex.Replace(parts[i + 5], @"[^\w\s]", "");
                                        manufacturer = Regex.Replace(parts[i + 6], @"[^\w\s]", "");
                                        description = Regex.Replace(parts[i + 7], @"[^\w\s,-.?!]", "");
                                    }

                                    var query = "INSERT INTO chattmob.customer_table (FIRST_NAME, LAST_NAME, EMAIL, PHONE, DATE_CREATED, PRODUCT_TYPE, MANUFACTURER, DESCRIPTION) VALUES('" +
                                                   firstName + "', '" + lastName + "', '" + email + "', '" + phone + "', '" + date + "', '" + product + "', '" + manufacturer + "', '" + description + "');";

                                    MySqlCommand cmd = new MySqlCommand(query, connection);

                                    cmd.ExecuteNonQuery();
                                }
                            }
                            this.CloseConnection();
                        }
                    }

                    j = 999;
                }
                catch
                {
                    System.Threading.Thread.Sleep(100);
                }
        }
    }
}