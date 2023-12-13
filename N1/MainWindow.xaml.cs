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

namespace N1
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {

        Random rand = new Random();

        public MainWindow()
        {
            InitializeComponent();
        }

        int readInt(TextBox textBox)
        {
            int intNum;

            while (!(int.TryParse(textBox.Text, out intNum)))
            {
                MessageBox.Show("Неверно задано число.", "Ошибка!");
                textBox.BorderBrush = Brushes.Red;
                return 0;
            }
            textBox.BorderBrush = Brushes.Green;
            return intNum;
        }

        string genPassword(int start, int end, int len) 
        {
            string result = "";

            for (int i = 0; i < len; i++)
            {
                string randStr = rand.Next(start, end).ToString();
                result = result + char.ConvertFromUtf32(Convert.ToInt32(randStr)).ToString();
            }

            return result;
        }

        private void btn_gen_Click(object sender, RoutedEventArgs e)
        {

            int start = int.Parse(tb_start.Text, System.Globalization.NumberStyles.HexNumber);
            int end = int.Parse(tb_end.Text, System.Globalization.NumberStyles.HexNumber);
            int len = readInt(tb_len);

            tb_password.Text = genPassword(start, end, len);            
        }
    }
}
