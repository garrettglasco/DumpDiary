using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AspSandbox.Pages.Models;
using System.Security.Cryptography.X509Certificates;
using Npgsql;
using System.Data;

namespace AspSandbox.Pages.Models
{

    public class PostgresHelper : IDisposable
    {
        private string connectionString = "Host=localhost;Port=5432;Username=postgres;Password=Garr2012;Database=DumpDiary";
        private NpgsqlConnection connection;

		private ConnectionState connectionState;
		public ConnectionState ConnectionState
		{
			get { return connectionState; }
			private set { connectionState = value; }
		}

		public PostgresHelper(string connectionString)
        {
            this.connectionString = connectionString;
            connection = new NpgsqlConnection(connectionString);
        }

        public void OpenConnection()
        {
            if (connection.State != ConnectionState.Open)
            {
                connection.Open();
            }
        }

        public void CloseConnection()
        {
            if (connection.State == ConnectionState.Open)
            {
                connection.Close();
            }
        }

        public NpgsqlDataReader ExecuteQuery(string query)
        {
            OpenConnection();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            return cmd.ExecuteReader();
        }

        public void ExecuteNonQuery(string query)
        {
            OpenConnection();
            NpgsqlCommand cmd = new NpgsqlCommand(query, connection);
            cmd.ExecuteNonQuery();
        }

        public void Dispose()
        {
            CloseConnection();
            connection.Dispose();
        }
    }
}