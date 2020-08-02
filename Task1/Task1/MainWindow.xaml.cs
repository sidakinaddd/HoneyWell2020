using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;


namespace Task1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        bool hidden;
        double menuWidth;
        bool operation_clicked = false;
        string op;
        double first_number;
        double second_number;
        bool result_click = false;


        public MainWindow()
        {
            InitializeComponent();
            menuWidth = sideMenu.Width;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button b = (Button)sender;
            if (result_click)
            {
                textBlock1.Text = "";
                textBlock2.Text = "";
                result_click = false;
            }
            if(textBlock1.Text.Contains(',') && b.Content.ToString() == ",")
            {
                MessageBox.Show("You can not put , again");
            }
            else
            {
                textBlock1.Text += b.Content.ToString();
            }
            

        }
        private void Operation_Click(object sender, RoutedEventArgs e)
        {

            Button b = (Button)sender;
            
            if (!operation_clicked)
            {
                op = b.Content.ToString();
                first_number = Convert.ToDouble(textBlock1.Text);
                textBlock2.Text += textBlock1.Text;
                textBlock2.Text += op;
                textBlock1.Text = "";
                operation_clicked = true;
               
            }
           
        }
        private void squareRoot_Click(object sender, RoutedEventArgs e)
        {
            double a = Convert.ToDouble(textBlock1.Text);
            double res = Math.Sqrt(a);
            textBlock1.Text = res.ToString();
            result_click = true;
        }
        private void Result_click(object sender, RoutedEventArgs e)
        {
            operation_clicked = false;
            if (doubleRadio.IsChecked==true)
            {
                try
                {
                    textBlock2.Text += textBlock1.Text;
                    second_number = Convert.ToDouble(textBlock1.Text);
                    if (op == "+")
                    {
                        textBlock2.Text += "=" + (first_number + second_number);
                        textBlock1.Text = Convert.ToString(first_number + second_number);
                    }
                    else if (op == "-")
                    {
                        textBlock2.Text += "=" + (first_number - second_number);
                        textBlock1.Text = Convert.ToString(first_number - second_number);
                    }
                    else if (op == "*")
                    {
                        textBlock2.Text += "=" + (first_number * second_number);
                        textBlock1.Text = Convert.ToString(first_number * second_number);
                    }
                    else
                    {
                        textBlock2.Text += "=" + (first_number / second_number);
                        textBlock1.Text = Convert.ToString(first_number / second_number);
                    }
                    result_click = true;
                }
                catch (Exception exc)
                {
                    textBlock1.Text = "Error!";
                }
            }
            else
            {
                try
                {
                    textBlock2.Text += textBlock1.Text;
                    second_number = Convert.ToDouble(textBlock1.Text);
                    if (op == "+")
                    {
                        textBlock2.Text += "=" + Convert.ToInt32(first_number + second_number);
                        textBlock1.Text = Convert.ToString(Convert.ToInt32(first_number + second_number));
                    }
                    else if (op == "-")
                    {
                        textBlock2.Text += "=" + Convert.ToInt32(first_number - second_number);
                        textBlock1.Text = Convert.ToString(Convert.ToInt32(first_number - second_number));
                    }
                    else if (op == "*")
                    {
                        textBlock2.Text += "=" + Convert.ToInt32(first_number * second_number);
                        textBlock1.Text = Convert.ToString(Convert.ToInt32(first_number * second_number));
                    }
                    else
                    {
                        textBlock2.Text += "=" + Convert.ToInt32(first_number / second_number);
                        textBlock1.Text = Convert.ToString(Convert.ToInt32(first_number / second_number));
                    }
                    result_click = true;
                }
                catch (Exception exc)
                {
                    textBlock1.Text = "Error!";
                }
            }

            
        }
        
        private void Clear_Click(object sender, RoutedEventArgs e)
        {
            textBlock1.Text = "";
            textBlock2.Text = "";
            operation_clicked = false;
            
        }
        private void R_Click(object sender, RoutedEventArgs e)
        {
            int length = textBlock1.Text.Length;
            textBlock1.Text=textBlock1.Text.Remove(length-1);
        }
      


        private void menu_Clicked(object sender, MouseButtonEventArgs e)
        {
            if (hidden)
            {
                sideMenu.Width = sideMenu.Width * 3;
                hidden = false;
            }
            else
            {
                sideMenu.Width = sideMenu.Width / 3;
                hidden = true;
            }
        }

        private void item_selected(object sender, RoutedEventArgs e)
        {
            var item = sender as ListViewItem;
            if(item.Name == "Calculator")
            {
                designCalculator.Visibility = Visibility.Visible;
                desingCalendar.Visibility = Visibility.Hidden;
                designConverter.Visibility = Visibility.Hidden;
                designAboutApp.Visibility = Visibility.Hidden;
               
            }
            else if(item.Name == "Calendar")
            {
                designCalculator.Visibility = Visibility.Hidden;
                desingCalendar.Visibility = Visibility.Visible;
                designConverter.Visibility = Visibility.Hidden;
                designAboutApp.Visibility = Visibility.Hidden;
            }
            else if(item.Name == "Converter")
            {
                designCalculator.Visibility = Visibility.Hidden;
                desingCalendar.Visibility = Visibility.Hidden;
                designConverter.Visibility = Visibility.Visible;
                designAboutApp.Visibility = Visibility.Hidden;
            }
            else
            {
                designCalculator.Visibility = Visibility.Hidden;
                desingCalendar.Visibility = Visibility.Hidden;
                designConverter.Visibility = Visibility.Hidden;
                designAboutApp.Visibility = Visibility.Visible;
            }
        }
        

        private void convert_click(object sender, RoutedEventArgs e)
        {
            string val1 = firstParam.SelectedValue.ToString().Substring(38);
            string val2 = secondParam.SelectedValue.ToString().Substring(38);
            try
            {
                if (val1 == "Метр")
                {
                    if (val2 == "Метр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text);
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Километр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.001;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Фут")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 3.28084;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Ярд")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 1.09361;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                }
                if (val1 == "Километр")
                {

                    if (val2 == "Метр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 1000;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Километр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text);
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                    else if (val2 == "Фут")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 3280.84;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Ярд")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 1093.61;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                }
                if (val1 == "Фут")
                {
                    if (val2 == "Метр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.3048;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Километр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.0003048;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                    else if (val2 == "Фут")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text);
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Ярд")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.3333333;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                }
                if (val1 == "Ярд")
                {
                    if (val2 == "Метр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.9144;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Километр")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 0.0009144;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }

                    else if (val2 == "Фут")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text) * 3;
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                    else if (val2 == "Ярд")
                    {
                        double res = Convert.ToDouble(firstTextBox.Text);
                        convertedResult.Text = firstTextBox.Text + " " + val1 + " = " + res + " " + val2;
                    }
                }
            }
            catch
            {
                MessageBox.Show("Error");
            }

            

        }

        private void window_loaded(object sender, RoutedEventArgs e)
        {
            string userName = Environment.UserName;

            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            
            timer.Tick += (o, t) => {
                if(DateTime.Now.Hour>6 && DateTime.Now.Hour < 13)
                {
                    labelAbout.Content = "Доброе утро, "+userName+"!";
                }else if (DateTime.Now.Hour >=13 && DateTime.Now.Hour <18)
                {
                    labelAbout.Content = "Доброй день, "+userName+"!";
                }
                else
                {
                    labelAbout.Content = "Доброй вечер, "+userName+"!";
                }
            };
            timer.Start();

        }
    }
}
