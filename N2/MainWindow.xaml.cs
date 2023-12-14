using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
using static System.Net.Mime.MediaTypeNames;

namespace N2
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        int readInt(TextBox textBox)
        {
            int intNum;

            while (!(int.TryParse(textBox.Text, out intNum))) // Проверка введенной строки на тип int
            {
                MessageBox.Show("Неверно задано число.", "Ошибка!");
                textBox.BorderBrush = Brushes.Red;
                return 0;
            }
            textBox.BorderBrush = Brushes.Green;
            return intNum;
        }

        private void btn_load_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            if (dlg.ShowDialog() == true)
            {
                FileStream fs = File.OpenRead(dlg.FileName);
                byte[] array = new byte[fs.Length];
                fs.Read(array, 0, array.Length); 
                string textFromFile = System.Text.Encoding.UTF8.GetString(array); 
                tb_result.Text = textFromFile;
            }
            
        }

        string encrypt(int key) // шифровка
        {
            string text = tb_result.Text;
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = ' ';              

                if (text[i] + key > char.MaxValue)
                {
                    symbol = (char)((text[i] + key) - char.MaxValue);
                } else
                    symbol = (char)(text[i] + key);

                result += char.ConvertFromUtf32(Convert.ToInt32(symbol)).ToString();
            }

            return result;
        }

        string decipher(int key) // дешифровка
        {
            string text = tb_result.Text;
            string result = "";

            for (int i = 0; i < text.Length; i++)
            {
                char symbol = ' ';


                if (text[i] - key < 0)
                {
                    symbol = (char)((text[i] - key) + char.MaxValue);
                }
                else
                    symbol = (char)(text[i] - key);

                result += char.ConvertFromUtf32(Convert.ToInt32(symbol)).ToString();
            }

            return result;
        }

        private void btn_encrypt_Click(object sender, RoutedEventArgs e)
        {
            int key = readInt(tb_key);
            tb_result.Text = encrypt(key);
        }

        private void btn_decipher_Click(object sender, RoutedEventArgs e)
        {
            int key = readInt(tb_key);
            tb_result.Text = decipher(key);
        }

        private void btn_save_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.SaveFileDialog dlg = new Microsoft.Win32.SaveFileDialog();
            string text = tb_result.Text;

            if (dlg.ShowDialog() == true)
            {
                using (FileStream fstream = new FileStream(dlg.FileName, FileMode.OpenOrCreate))
                {
                    byte[] array = System.Text.Encoding.UTF8.GetBytes(text);
                    fstream.Write(array, 0, array.Length);
                }
            }

        }
    }
}
