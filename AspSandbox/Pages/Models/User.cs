using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Drawing;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using AspSandbox.Pages.Models;
using Microsoft.AspNetCore.Razor.TagHelpers;
using Npgsql;

namespace AspSandbox.Pages.Models
{
    public class User
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Age { get; set; }
        public string? Gender { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public List<Dump>? Dumps = new List<Dump>();
        public List<Dump>? UserLastFiveDumps;

        public User(int id)
        {
            Id = id;
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"SELECT * FROM users WHERE id = {id}";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        UserName = reader.GetString(1);
                        Password = reader.GetString(2);
                        Age = reader.GetInt32(3);
                        Gender = reader.GetString(4);
                        FirstName = reader.GetString(5);
                        LastName = reader.GetString(6);
                    }
                }
            }
            GetDumps();
        }
        public User(int userId, string userName, string password)
        {
            Id = userId;
            UserName = userName;
            Password = password;
        }
        public User(int userId, string userName, string password, int? age, string? gender, string? firstName, string? lastName)
        {
            Id = userId;
            UserName = userName;
            Password = password;
            Age = age;
            Gender = gender;
            FirstName = firstName;
            LastName = lastName;
        }

        public void takesAPoo(Dump dump)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = "INSERT INTO dumps(id, user_id, date, shape, color, amount, comments)" +
                    $"VALUES('{dump.Id}', '{dump.User_id}', '{dump.Date}', '{dump.Shape}', '{dump.Color}', '{dump.Amount}', '{dump.Comments}')";
                postgresHelper.ExecuteNonQuery(query);
            }
        }

        public void GetDumps()
        {
			string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
			using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
			{
				string query = $"SELECT * FROM dumps WHERE user_id = {Id} ORDER BY date";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        string? comments = reader["comments"] != DBNull.Value ? reader["comments"].ToString() : null;
                        Dumps.Add(
                            new Dump(reader.GetInt32(0),
                                        reader.GetInt32(1),
                                        reader.GetDateTime(2),
                                        reader.GetString(3),
                                        reader.GetString(4),
                                        reader.GetString(5),
                                        comments)
                                );
                    }
                }
                UserLastFiveDumps = getLastFiveDumps();
			}
		}

        public void editDump(int dumpId, string dumpProb, string newDumpProb, PostgresHelper sgHelper)
        {
            string query = $"UPDATE TABLE dumps SET {dumpProb} TO '{newDumpProb}' where id = {dumpId}";
            sgHelper.ExecuteQuery(query);
        }

        public void printDumpInfo(int dumpId)
        {
            Console.WriteLine($"1.SHAPE:  {Dumps[dumpId].Shape}");
            Console.WriteLine($"2.COLOR:  {Dumps[dumpId].Color}");
            Console.WriteLine($"3.AMOUNT: {Dumps[dumpId].Amount}");
            Console.WriteLine($"4.Shape:  {Dumps[dumpId].Comments}");
        }

        public List<Dump> getLastFiveDumps()
        {
            if (Dumps.Count == 0)
            {
                return new List<Dump>();
            }
            else
            {
                return Dumps.Skip(Math.Max(0, Dumps.Count - 5)).ToList();
            }
        }
        public void updateUserName(string userName)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET username = '{userName}' where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            UserName = userName;
        }

        public void updatePassword(string password)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET password = '{password}' where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            Password = password;
        }

        public void updateAge(int age)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET age = {age} where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            Age = age;
        }

        public void updateGender(string gender)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET gender = '{gender}' where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            Gender = gender;
        }

        public void updateLastName(string lastname)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET lastname = '{lastname}' where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            LastName = lastname;
        }

        public void updateFirstName(string firstname)
        {
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"UPDATE users SET firstname = '{firstname}' where id = {Id}";
                postgresHelper.ExecuteNonQuery(query);
            }
            FirstName = firstname;
        }
    }
}
