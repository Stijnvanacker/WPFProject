using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFApplication.Model;

namespace WPFApplication.DataAccess
{
    public class TableDataClass
    {
        private readonly string connectionString;

        public TableDataClass(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //retourneert een tafel met een bepaald id
        public Table GetById(int id)
        {
            var tableList = new List<Table>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, Name, PositionX, PositionY, Width, Height from Tafel where Id = @Id",
                CommandType = CommandType.Text
            };

            var idParameter = new SqlParameter("Id", id);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tableList.Add(CreateTable(reader));
            }

            connection.Close();

            return tableList.FirstOrDefault();
        }

        //retourneert alle tafels
        public ICollection<Table> GetAll()
        {
            var tableList = new List<Table>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, Name, PositionX, PositionY, Width, Height from Tafel",
                CommandType = CommandType.Text
            };

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                tableList.Add(CreateTable(reader));
            }

            connection.Close();

            return tableList;
        }

        //maakt een tafel object aan
        private Table CreateTable(SqlDataReader reader)
        {
            return new Table
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                PositionX = (int) reader["PositionX"],
                PositionY = (int) reader["PositionY"],
                Width = (int) reader["Width"],
                Height = (int) reader["Height"]
            };
        }

        //voegt een nieuwe tafel toe
        public void InsertTable(Table table)
        {
            try
            {
                var connection = new SqlConnection(connectionString);

                connection.Open();
                var command = new SqlCommand();
                {
                    command.CommandText = "INSERT INTO Tafel (Name, PositionX, PositionY, Width, Height) VALUES (@Name, @PositionX, @PositionY, @Width, @Height)";       
                    command.Parameters.AddWithValue("@Name", table.Name);
                    command.Parameters.AddWithValue("@PositionX", table.PositionX);
                    command.Parameters.AddWithValue("@PositionY", table.PositionY);
                    command.Parameters.AddWithValue("@Width", table.Width);
                    command.Parameters.AddWithValue("@Height", table.Height);

                    command.Connection = connection;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        //past een bestaande tafel aan
        public void UpdateTable(Table table)
        {

            if (table != null)
            {
                
            
            try
            {
                var connection = new SqlConnection(connectionString);
                var command = new SqlCommand();
                {
                    command.CommandText = "UPDATE Tafel SET Name = @Name, PositionX = @PositionX, PositionY = @PositionY, Width = @Width, Height = @Height" +
                    " WHERE Id = @Id";
                    command.Parameters.AddWithValue("@Id", table.Id);
                    command.Parameters.AddWithValue("@Name", table.Name);
                    command.Parameters.AddWithValue("@PositionX", table.PositionX);
                    command.Parameters.AddWithValue("@PositionY", table.PositionY);
                    command.Parameters.AddWithValue("@Width", table.Width);
                    command.Parameters.AddWithValue("@Height", table.Height);



                    connection.Open();
                    command.Connection = connection;
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show(ex.Message);
            }
        }

        }

        //verwijderd een bestaande tafel
        public void DeleteTable(Table table)
        {

            if (table != null)
            {


                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                       
                        command.CommandText = "DELETE FROM Tafel WHERE Id=@Id";
                        command.Parameters.AddWithValue("@Id", table.Id);
               
                        connection.Open();
                        command.Connection = connection;
                        command.ExecuteNonQuery();
                        connection.Close();
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine(ex.Message);
                    MessageBox.Show(ex.Message);
                }
            }

        }





    }
}
