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
    public class ArticleDataClass
    {
        private readonly string connectionString;

        public ArticleDataClass(string connectionString)
        {
            this.connectionString = connectionString;
        }

        //retourneert een artikel met een bepaald id
        public Article GetById(int id)
        {
            var articleList = new List<Article>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, Name, Price, MenuIndexX, MenuIndexY from Article where Id = @Id",
                CommandType = CommandType.Text
            };

            var idParameter = new SqlParameter("Id", id);
            command.Parameters.Add(idParameter);

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                articleList.Add(CreateArticle(reader));
            }

            connection.Close();

            return articleList.FirstOrDefault();
        }

        //retourneert alle artikelen
        public ICollection<Article> GetAll()
        {
            var articleList = new List<Article>();

            var connection = new SqlConnection(connectionString);
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "select Id, Name, Price, MenuIndexX, MenuIndexY from Article",
                CommandType = CommandType.Text
            };

            connection.Open();

            var reader = command.ExecuteReader();
            while (reader.Read())
            {
                articleList.Add(CreateArticle(reader));
            }

            connection.Close();

            return articleList;
        }
        
        //voegt een artikel toe
        public void InsertArticle(Article article)
        {
            try
            {
                var connection = new SqlConnection(connectionString);

                connection.Open();
                var command = new SqlCommand();
                {
                    command.CommandText = "INSERT INTO Article (Name, Price, MenuIndexX, MenuIndexY) VALUES (@Name, @Price, @MenuIndexX, @MenuIndexY)";
                
                    command.Parameters.AddWithValue("@Name", article.Name);
                    command.Parameters.AddWithValue("@Price", article.Price);
                    command.Parameters.AddWithValue("@MenuIndexX", article.MenuIndexX);
                    command.Parameters.AddWithValue("@MenuIndexY", article.MenuIndexY);

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

        //past een bestaand artikel aan
        public void UpdateArticle(Article article)
        {

            if (article != null)
            {


                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {
                        command.CommandText = "UPDATE Article SET Name = @Name, Price = @Price, MenuIndexX = @MenuIndexX, MenuIndexY = @MenuIndexY" +
                        " WHERE Id = @Id";
                        command.Parameters.AddWithValue("@Id", article.Id);
                        command.Parameters.AddWithValue("@Name", article.Name);
                        command.Parameters.AddWithValue("@Price", article.Price);
                        command.Parameters.AddWithValue("@MenuIndexX", article.MenuIndexX);
                        command.Parameters.AddWithValue("@MenuIndexY", article.MenuIndexY);
                        



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

        //verwijderd een bestaand artikel
        public void DeleteArticle(Article article)
        {

            if (article != null)
            {


                try
                {
                    var connection = new SqlConnection(connectionString);
                    var command = new SqlCommand();
                    {

                        command.CommandText = "DELETE FROM Article WHERE Id=@Id";
                        command.Parameters.AddWithValue("@Id", article.Id);

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

        //maakt een nieuw artikel aan
        private Article CreateArticle(SqlDataReader reader)
        {
            return new Article
            {
                Id = Convert.ToInt32(reader["Id"]),
                Name = reader["Name"].ToString(),
                Price = Convert.ToSingle(reader["Price"]),
                MenuIndexX = (int)reader["MenuIndexX"],
                MenuIndexY = (int)reader["MenuIndexY"]
            };
        }
    }
}
