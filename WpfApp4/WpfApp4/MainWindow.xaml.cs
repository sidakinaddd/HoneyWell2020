using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=userstore;Trusted_Connection=True";
        string sqlExpression = "";
        int pageSize = 10;
        int pageNumber = 0;
        SqlDataAdapter dataAdp;
        DataTable dt;
        SqlConnection connection = new SqlConnection(connectionString);

        public MainWindow()
        {
            
            InitializeComponent();
            update_table();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            bool married = false;
            if (Married.IsChecked == true)
            {
                married = true;
            }
            if(Name.Text=="" || Name.Text.Any(char.IsDigit) || LastName.Text=="" || LastName.Text.Any(char.IsDigit) || FathersName.Text == "" || FathersName.Text.Any(char.IsDigit) || Age.Text.Any(char.IsLetter) || Age.Text == "")
            {
                MessageBox.Show("Введите корректно ваши данные!");
            }
            else
            {
                using (UserEntities db = new UserEntities())
                {
                    try
                    {
                        User nuser = new User { Name = Name.Text, LastName = LastName.Text, Age = Convert.ToInt32(Age.Text), FathersName = FathersName.Text, Position = Position.Text, Gender = Gender.Text, Married = married };
                        db.Users.Add(nuser);
                        db.SaveChanges();
                        update_table();
                        Name.Text = "";
                        LastName.Text = "";
                        FathersName.Text = "";
                        Age.Text = "";
                    }
                    catch (SqlException ex)
                    {
                        MessageBox.Show(ex.ToString());
                    }

                }
            }
            
        }

        public void update_table()
        {
            try
            {
                connection.Open();
                sqlExpression = "select * from Users";
                SqlCommand createCommand = new SqlCommand(sqlExpression, connection);
                createCommand.ExecuteNonQuery();

                dataAdp = new SqlDataAdapter(createCommand);
                dt = new DataTable("Users");
                dataAdp.Fill(dt);
                dataGrid1.ItemsSource = dt.DefaultView;
                dataAdp.Update(dt);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                connection.Close();
            }
        }
        

        private void Forward_button_Click(object sender, RoutedEventArgs e)
        {
            
            if (dt.Rows.Count < pageSize) return;

            pageNumber++;
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdp = new SqlDataAdapter(GetSql(), connection);

                dt.Rows.Clear();

                dataAdp.Fill(dt);
            }
        }

        private void Back_button1_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0) return;
            pageNumber--;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                dataAdp = new SqlDataAdapter(GetSql(), connection);

                dt.Rows.Clear();

                dataAdp.Fill(dt);
            }
        }
        private string GetSql()
        {
            return "SELECT * FROM Users ORDER BY Id OFFSET ((" + pageNumber + ") * " + pageSize + ") " +
                "ROWS FETCH NEXT " + pageSize + "ROWS ONLY";
        }

        private void Update_button_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                SqlCommandBuilder commandBuilder = new SqlCommandBuilder(dataAdp);
                dataAdp.Update(dt);
                dt.Clear();
                dataAdp.Fill(dt);
            }
            catch(SqlException ex)
            {
                MessageBox.Show(ex.ToString());
            }

        }
    }
    
}
