using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace DataDictionary.Models
{
    public class Datenbank
    {
        private DataTable dataTable = new DataTable();
        public string ConnectionString { get; set; }
        public List<string> ListTables()
        {

            List<string> tables = new List<string>();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                DataTable dt = connection.GetSchema("Tables");

                foreach (DataRow row in dt.Rows)
                {
                    string tablename = (string)row[2];
                    tables.Add(tablename);
                    dataTable = new DataTable();
                }
                connection.Close();
                return tables;
            }
            catch (Exception ex)
            {
                return tables;
                Console.WriteLine(ex.Message);
            }
        }

        public List<string> ListColumn(string tablename)
        {
            List<string> columns = new List<string>();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string query = string.Format("SELECT column_name FROM information_schema.columns WHERE table_name = '" + tablename + "'");
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);
                foreach (DataRow row in dataTable.Rows)
                {
                    var columnname = row[0];
                    columns.Add(columnname.ToString());
                    dataTable = new DataTable();
                }
                connection.Close();
                return columns;
            }
            catch (Exception ex)
            {
                return columns;
                Console.WriteLine(ex.Message);
            }

        }
        public List<string> ListData(string tablename, string columnname)
        {
            List<string> data = new List<string>();
            try
            {
                SqlConnection connection = new SqlConnection();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                string query = string.Format("select " + columnname.ToString() + " from " + tablename.ToString());
                SqlCommand cmd = new SqlCommand(query, connection);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dataTable);

                foreach (DataRow row in dataTable.Rows)
                {
                    var dataname = row[0];
                    data.Add(string.Format(dataname.ToString()));
                    dataTable = new DataTable();
                }
                connection.Close();
                return data;
            }
            catch (Exception ex)
            {
                return data;
                Console.WriteLine(ex.Message);
            }
        }
    }
}