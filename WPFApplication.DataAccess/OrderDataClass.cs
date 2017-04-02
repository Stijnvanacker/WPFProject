using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WPFApplication.Model;

namespace WPFApplication.DataAccess
{
    public class OrderDataClass
    {
        private readonly string connectionString;

        public OrderDataClass(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //retourneert een order met een bepaald id
        public Order GetById(int id)
        {
            var orderList = new List<Order>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, TableId, TableName, CreatedDate, Staat from Orders where Id = @Id",
                CommandType = CommandType.Text
            };
            
            var idParameter = new SqlParameter("Id", id);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderList.Add(CreateOrder(reader));
            }

            connection.Close();

            return orderList.FirstOrDefault();
        }

        //retourneert de orders van een bepaalde tafel
        public Order GetByTableId(int tableId)
        {
            var orderList = new List<Order>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, TableId, TableName, CreatedDate, Staat from Orders where TableId = @tableId and Staat = 0",
                CommandType = CommandType.Text
            };

            var idParameter = new SqlParameter("tableId", tableId);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderList.Add(CreateOrder(reader));
            }

            connection.Close();

            return orderList.FirstOrDefault();
        }

        //retourneert alle orders
        public ICollection<Order> GetAll()
        {
            var orderList = new List<Order>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, TableId, TableName, CreatedDate, Staat from Orders",
                CommandType = CommandType.Text
            };

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderList.Add(CreateOrder(reader));
            }

            connection.Close();

            return orderList;
        }

        //maakt een nieuw order aan
        private Order CreateOrder(SqlDataReader reader)
        {
            return new Order
            {
                Id = Convert.ToInt32(reader["Id"]),
                TableId = Convert.ToInt32(reader["TableId"]),
                TableName = reader["TableName"].ToString(),
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"]),
                Staat  = (int)reader["Staat"]
           

            };
        }

        //voegt een order toe in de database
        public void InsertOrder(Order order)
        {
            try
            {
                var connection = new SqlConnection(connectionString);

                connection.Open();
                var command = new SqlCommand();
                {
                    command.CommandText = "INSERT INTO Orders (TableId, TableName, CreatedDate, Staat) VALUES (@TableId, @TableName, @CreatedDate, @Staat)";
                    command.Parameters.AddWithValue("@TableId", order.TableId);
                    command.Parameters.AddWithValue("@TableName", order.TableName);
                    command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                    command.Parameters.AddWithValue("@Staat", order.Staat);

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

        //verwijderd een bepaald order
        public void DeleteOrder(Order order)
        {
            if (order != null)
            {
                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                        command.CommandText = "DELETE Orders where Id = @Id";

                        command.Parameters.AddWithValue("@Id", order.Id);

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

        //past een bestaand order aan
        public void UpdateOrderLine(Order order)
        {

            if (order != null)
            {
                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                        command.CommandText =
                            "UPDATE Orders SET TableId = @TableId, TableName = @TableName, CreatedDate = @CreatedDate, Staat = @Staat" +
                            " WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", order.Id);
                        command.Parameters.AddWithValue("@TableId", order.TableId);
                        command.Parameters.AddWithValue("@TableName", order.TableName);
                        command.Parameters.AddWithValue("@CreatedDate", order.CreatedDate);
                        command.Parameters.AddWithValue("@Staat", order.Staat);



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


        //retourneert de orders van een bepaalde maand en jaar
        public ICollection<Order> GetOrdersByMonthAndCurrentYear(int month, int year)
        {
            var orderList = new List<Order>();

            var connection = new SqlConnection(connectionString);

            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "Select * FROM Orders WHERE MONTH(CreatedDate) = @monthNumber AND YEAR(CreatedDate) = @year",
                CommandType = CommandType.Text,
             
                

            };

            command.Parameters.AddWithValue("@monthNumber", month);
            command.Parameters.AddWithValue("@year", year);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderList.Add(CreateOrder(reader));
            }

            connection.Close();

            return orderList;
        }

     
    
    }
}
