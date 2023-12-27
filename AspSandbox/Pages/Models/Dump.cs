using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Npgsql;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace AspSandbox.Pages.Models
{
    public class Dump
    {
        public int Id { get; set; }
        public int User_id { get; set; }
        public DateTime Date { get; set; }
        public string Shape { get; set; }
        public string Color { get; set; }
        public string Amount { get; set; }
        public string? Comments { get; set; }


        public Dump(int id, int user_id, DateTime date, string shape, string color, string amount, string comments)
        {
            Id = id;
            User_id = user_id;
            Date = date;
            Shape = shape;
            Color = color;
            Amount = amount;
            Comments = comments;
        }
        public Dump(int user_id, string shape, string color, string amount, string? comments)
        {
            Shape = shape;
            Color = color;
            Amount = amount;
            Comments = comments;
            User_id = user_id;
            Date = DateTime.Now;
            int MaxId = getMaxID();
            Id = ++MaxId;

            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = "INSERT INTO dumps(id, user_id, date, shape, color, amount, comments)" +
                    $"VALUES({Id}, {User_id}, '{Date}', '{Shape}', '{Color}', '{Amount}', '{Comments}')";
                postgresHelper.ExecuteNonQuery(query);
            }
        }

        public Dump(int id)
        {
            Id = id;
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = $"SELECT * FROM dumps WHERE id = {id}";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        User_id = reader.GetInt32(1);
                        Date = reader.GetDateTime(2);
                        Shape = reader.GetString(3);
                        Color = reader.GetString(4);
                        Amount = reader.GetString(5);
                        Comments = reader.GetString(6);
                    }
                }
            }
        }
         
        public int getMaxID()
        {
            int maxId = 0;
            string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
            using (PostgresHelper postgresHelper = new PostgresHelper(connectionString))
            {
                string query = "SELECT MAX(id) FROM dumps";
                using (NpgsqlDataReader reader = postgresHelper.ExecuteQuery(query))
                {
                    while (reader.Read())
                    {
                        try { maxId = reader.GetInt32(0); }
                        catch (InvalidCastException ex) { maxId = 0; }
                    }
                }
            }
            return maxId;
        }

        public void printInfo()
        {
            Console.WriteLine($"1.SHAPE:     {Shape}");
            Console.WriteLine($"2.COLOR:     {Color}");
            Console.WriteLine($"3.AMOUNT:    {Amount}");
            Console.WriteLine($"4.SHAPE:     {Shape}");
            Console.WriteLine($"5.COMMENTS:  {Comments}");
        }
    }
}
