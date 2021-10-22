using CrudApplication.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace CrudApplication.Services
{
    public class DbService
    {
        //private const string ConnectionString = "Data Source = DESKTOP - DD7UPN2\\SQLEXPRESS;Initial Catalog = db_Bookstore_v1; Integrated Security = True";

        internal const string ConnectionString = "Data Source=VMS-84;Initial Catalog=db_Bookstore_v1;Integrated Security=True";
       

        public bool InsertBook(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            try
            {
                //var command = "INSERT INTO Books VALUES('" + book.Name + "' , '"+ book.Author +"' , "+ book.Price +" , '"+ book.Publications +"')";  bad practice

                var command = $"INSERT INTO Books VALUES('{book.Name}' , '{book.Author}' , {book.Price} , '{book.Publications}')";  // good practice 

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Submit successfull");
                return true;
            }

            catch (Exception e)
            {
                MessageBox.Show("something went wrong try again" + e);
                return false;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

        public bool DeleteBook(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            try
            {
                var command = $"DELETE FROM Books WHERE Name = '{book.Name}'";

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Book deleted successfully");
                return true;
            }

            catch(Exception e)
            {
                MessageBox.Show("Something went wrong , try again after sometime");
                return false;
            }

            finally
            {
                sqlConnection.Close();
            }

        }

        public bool UpdateBookDetails(Book book)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);

            try
            {

              //   UPDATE Books
              //   SET Name = 'Veereshrt', Price = 100, Publications = 'Good seller'
              //    WHERE BookId = 2;


                var command = $"UPDATE Books SET Name = '{book.Name}' , Author = 'Veeresh' , Price = 1000 , Publications = '{book.Publications}'" +
                              $" WHERE BookId = {1} ";

                SqlCommand sqlCommand = new SqlCommand(command, sqlConnection);
                sqlConnection.Open();
                sqlCommand.ExecuteNonQuery();
                MessageBox.Show("Book updated sucessful");
                return true;
            }

            catch(Exception e)
            {
                MessageBox.Show("Book update is failed");
                return false;
            }

            finally
            {
                sqlConnection.Close();
            }
        }

    }
}
