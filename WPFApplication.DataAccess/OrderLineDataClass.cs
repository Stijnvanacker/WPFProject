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
    public class OrderLineDataClass
    {
        private readonly string connectionString;

        public OrderLineDataClass(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //retourneert een OrderLine met een bepaald id
        public OrderLine GetById(int id)
        {
            var orderLineList = new List<OrderLine>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, OrderId, ArticleId, ArticleName, Price, Amount, CreatedDate from OrderLine where Id = @Id",
                CommandType = CommandType.Text
            };

            var idParameter = new SqlParameter("Id", id);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderLineList.Add(CreateOrderLine(reader));
            }

            connection.Close();

            return orderLineList.FirstOrDefault();
        }

        //retourneert alle OrderLines van een bepaald order
        public ICollection<OrderLine> getByOrderId(int orderId)
        {
            var orderLineList = new List<OrderLine>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, OrderId, ArticleId, ArticleName, Price, Amount, CreatedDate from OrderLine where OrderId = @orderId",
                CommandType = CommandType.Text
            };

            var idParameter = new SqlParameter("orderId", orderId);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderLineList.Add(CreateOrderLine(reader));
            }

            connection.Close();

            return orderLineList;
        }

        //retourneert alle OrderLines van een bepaald artikel in een order
        public OrderLine GetByArticleId(int articleId, int orderId)
        {
            var orderLineList = new List<OrderLine>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, OrderId, ArticleId, ArticleName, Price, Amount, CreatedDate from OrderLine where ArticleId = @articleId and orderId = @orderId",
                CommandType = CommandType.Text
            };

            var articleIdParameter = new SqlParameter("articleId", articleId);
            var orderIdParameter = new SqlParameter("orderId", orderId);
            command.Parameters.Add(articleIdParameter);
            command.Parameters.Add(orderIdParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderLineList.Add(CreateOrderLine(reader));
            }

            connection.Close();

            return orderLineList.FirstOrDefault();
        }

        //verwijdert een bestaande OrderLine
        public void DeleteOrderLine(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                        command.CommandText = "DELETE OrderLine where Id = @Id";

                        command.Parameters.AddWithValue("@Id", orderLine.Id);
                        
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

        //voegt een nieuwe OrderLine toe
        public void InsertOrderLine(OrderLine orderLine)
        {
            try
            {
                var connection = new SqlConnection(connectionString);

                connection.Open();
                var command = new SqlCommand();
                {
                    command.CommandText = "INSERT INTO OrderLine (OrderId, ArticleId, ArticleName, Price, Amount, CreatedDate) VALUES (@OrderId, @ArticleId, @ArticleName, @Price, @Amount, @CreatedDate)";
                    command.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                    command.Parameters.AddWithValue("@ArticleId", orderLine.ArticleId);
                    command.Parameters.AddWithValue("@ArticleName", orderLine.ArticleName);
                    command.Parameters.AddWithValue("@Price", orderLine.Price);
                    command.Parameters.AddWithValue("@Amount", orderLine.Amount);
                    command.Parameters.AddWithValue("@CreatedDate", orderLine.CreatedDate);

                    command.Connection = connection;

                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
            catch (SqlException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //past een bestaande OrderLine aan
        public void UpdateOrderLine(OrderLine orderLine)
        {

            if (orderLine != null)
            {
                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                        command.CommandText =
                            "UPDATE OrderLine SET OrderId = @OrderId, ArticleId = @ArticleId, ArticleName = @ArticleName, Price = @Price, Amount = @Amount, CreatedDate = @CreatedDate" +
                            " WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", orderLine.Id);
                        command.Parameters.AddWithValue("@OrderId", orderLine.OrderId);
                        command.Parameters.AddWithValue("@ArticleId", orderLine.ArticleId);
                        command.Parameters.AddWithValue("@ArticleName", orderLine.ArticleName);
                        command.Parameters.AddWithValue("@Price", orderLine.Price);
                        command.Parameters.AddWithValue("@Amount", orderLine.Amount);
                        command.Parameters.AddWithValue("@CreatedDate", orderLine.CreatedDate);



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

        //retourneert alle OrderLines
        public ICollection<OrderLine> GetAll()
        {
            var orderLineList = new List<OrderLine>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, OrderId, ArticleId, ArticleName, Price, Amount, CreatedDate from OrderLine",
                CommandType = CommandType.Text
            };

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                orderLineList.Add(CreateOrderLine(reader));
            }

            connection.Close();

            return orderLineList;
        }

        //maakt een OrderLine aan
        private OrderLine CreateOrderLine(SqlDataReader reader)
        {
            return new OrderLine
            {
                Id = Convert.ToInt32(reader["Id"]),
                OrderId = Convert.ToInt32(reader["OrderId"]),
                ArticleId = Convert.ToInt32(reader["ArticleId"]),
                ArticleName = reader["ArticleName"].ToString(),
                Price = Convert.ToSingle(reader["Price"]),
                Amount = (int)reader["Amount"],
                CreatedDate = Convert.ToDateTime(reader["CreatedDate"])
            };
        }
    }
}
