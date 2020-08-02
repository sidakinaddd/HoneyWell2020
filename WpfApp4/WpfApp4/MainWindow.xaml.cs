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
using System.Text.RegularExpressions;
using System.IO;
using System.Xml.Serialization;

namespace WpfApp4
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        
        static string connectionString = @"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=userstore;Trusted_Connection=True";
 
        SqlConnection connection = new SqlConnection(connectionString);

        public MainWindow()
        {
            
            InitializeComponent();
            show_table();
         
        }

        public void show_table()
        {
            using (UserEntities db = new UserEntities())
            {
                var users = db.Users;

                dataGrid1.ItemsSource = users.ToList();

            }
        }
        private void insert_button_clicked(object sender, RoutedEventArgs e)
        {
            bool married = false;
            if (Married.IsChecked == true)
            {
                married = true;
            }
            if (!Regex.IsMatch(Name.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(LastName.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(FathersName.Text, @"^[a-zA-Z]+$") || !Regex.IsMatch(Age.Text,@"^[0-9]{2}$"))
            {
                MessageBox.Show("Введите корректно ваши данные!");
            }
            else
            {
                try
                {
                    using (UserEntities db = new UserEntities())
                    {


                        User new_user = new User();
                        new_user.Name = Name.Text;
                        new_user.LastName = LastName.Text;
                        new_user.FathersName = FathersName.Text;
                        new_user.Age = Convert.ToInt32(Age.Text);
                        new_user.Position = Position.Text;
                        new_user.Gender = Gender.Text;
                        new_user.Married = married;
                        db.Users.Add(new_user);
                        db.SaveChanges();
                        show_table();
                        Name.Text = "";
                        LastName.Text = "";
                        FathersName.Text = "";
                        Age.Text = "";
                    }
                }catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
               
            }
            
        }

        
        private void update_button_Click(object sender, RoutedEventArgs e)
        {
            using(UserEntities db = new UserEntities())
            {
                try
                {
                    User selected_user = (User)dataGrid1.SelectedItem;
                    int selected_user_id = selected_user.Id;
                    User updating_user = (from u in db.Users where u.Id == selected_user_id select u).First();
                    updating_user.Name = selected_user.Name;
                    updating_user.LastName = selected_user.LastName;
                    updating_user.FathersName = selected_user.FathersName;
                    updating_user.Age = selected_user.Age;
                    updating_user.Position = selected_user.Position;
                    updating_user.Gender = selected_user.Gender;
                    updating_user.Married = selected_user.Married;
                    db.SaveChanges();
                    MessageBox.Show($"User {selected_user.Name} was updated");
                    show_table();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
               
            }
           
        }

        private void delete_button_clicked(object sender, RoutedEventArgs e)
        {
            using (UserEntities db = new UserEntities())
            {
                try
                {
                    User selected_user = (User)dataGrid1.SelectedItem;
                    int selected_user_id = selected_user.Id;
                    User removing_user = (from u in db.Users where u.Id == selected_user_id select u).First();
                    MessageBox.Show($"User {selected_user.Name} was deleted");
                    db.Users.Remove(removing_user);
                    db.SaveChanges();
                    show_table();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }

            }
        }
        private void deserealization_button_clicked()
        {
            XmlSerializer serializer = new XmlSerializer(typeof(UserList));

            try
            {
                using (FileStream fileStream = new FileStream("users.xml", FileMode.Open))
                {
                    UserList result = (UserList)serializer.Deserialize(fileStream);
                    MessageBox.Show(result.Users.Count.ToString());
                }

            }catch(Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }



        }

        private void serialize_button_clicked(object sender, RoutedEventArgs e)
        {
            string sql = "SELECT * FROM Users";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    SqlDataAdapter adapter = new SqlDataAdapter(sql, connection);
                    DataSet ds = new DataSet("Users");
                    DataTable dt = new DataTable("User");
                    ds.Tables.Add(dt);
                    adapter.Fill(ds.Tables["User"]);
                    ds.WriteXml("users_test.xml");
                    dataGrid1.ItemsSource = "";
                    MessageBox.Show("Written");
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
                finally
                {
                    connection.Close();
                }
                
                
            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            DataSet dataSet = new DataSet();
            dataSet.ReadXml(@"C:\Users\sidak\source\repos\WpfApp4\WpfApp4\bin\Debug\users_test.xml");
            dataGrid1.ItemsSource = dataSet.Tables[0].DefaultView;
        }
    }
    
}
