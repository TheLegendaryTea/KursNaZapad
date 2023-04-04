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
using System.Windows.Shapes;

namespace KursNaZapad
{
    /// <summary>
    /// Логика взаимодействия для Name.xaml
    /// </summary>
    public partial class Name : Window
    {
        List<string> WordsDB = new List<string>();
        string lastWord = string.Empty;
        public Name()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            InitializeComponent();
            using (StreamReader sr = new StreamReader("3.txt", Encoding.GetEncoding(1251)))
            {
                while (sr.Peek() != -1)
                {
                    WordsDB.Add(sr.ReadLine());
                }

            }
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow window = new MainWindow();
            window.Show();
            this.Close();
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter)
            {
                try
                {
                    if (Result.Text.EndsWith("ъ") || Result.Text.EndsWith("ь") || Result.Text.EndsWith("ы")|| Result.Text.EndsWith("й"))
                    {
                        lastWord = lastWord.Substring(0, lastWord.Length - 2);
                        return;
                    }
                    if (textBox1.Text[0] != lastWord[lastWord.Length - 1])
                    {
                        MessageBox.Show("Введите слово по правилам игры!");
                        return;
                    }
                }
                catch { }

                if (listBox1.Items.Contains("<-- " + textBox1.Text) || listBox1.Items.Contains("--> " + textBox1.Text))
                {
                    MessageBox.Show("Это слово уже было!");
                    textBox1.Text = "";
                }
                else
                {

                    listBox1.Items.Add("--> " + textBox1.Text);

                    if (!WordsDB.Contains(textBox1.Text))
                    {
                        if (MessageBox.Show("Данного слова нет в базе данных\r\nДобавить его в базу?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                        {
                            using (StreamWriter sw = new StreamWriter("3.txt", true, Encoding.GetEncoding(1251)))
                            {
                                sw.WriteLine(textBox1.Text);
                            }
                        }
                    }
                    bool found = false;
                    foreach (string str in WordsDB)
                    {
                        int count = 0;
                        if (textBox1.Text.EndsWith("ъ") || textBox1.Text.EndsWith("ь") || textBox1.Text.EndsWith("ы"))
                        {
                            if (textBox1.Text[textBox1.Text.Length - 2] == str[0])
                            {
                                if (listBox1.Items.Contains("<-- " + str) || listBox1.Items.Contains("--> " + str))
                                    continue;

                                listBox1.Items.Add("<-- " + str);

                                lastWord = str;
                                Result.Text = str;

                                textBox1.Text = string.Empty;
                                textBox1.Focus();
                                found = true;
                                break;
                            }
                        }

                        if (textBox1.Text[textBox1.Text.Length - 1] == str[0])
                        {
                            if (listBox1.Items.Contains("<-- " + str) || listBox1.Items.Contains("--> " + str))
                                continue;

                            listBox1.Items.Add("<-- " + str);

                            lastWord = str;
                            Result.Text = str;

                            textBox1.Text = string.Empty;
                            textBox1.Focus();
                            found = true;
                            break;
                        }
                    }
                    if (!found)
                    {
                        if (MessageBox.Show("Компьютер не нашел ответа, ты выиграл!\r\nPlay again?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                            using (StreamReader sr = new StreamReader("3.txt", Encoding.GetEncoding(1251)))
                            {
                                Result.Text = "";
                                lastWord = "";
                                WordsDB.Clear();
                                listBox1.Items.Clear();
                                textBox1.Text = "";
                                while (sr.Peek() != -1)
                                    WordsDB.Add(sr.ReadLine());
                            }
                    }
                }
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            using (StreamReader sr = new StreamReader("3.txt", Encoding.GetEncoding(1251)))
            {
                if (MessageBox.Show("Вы уверены что хотите сдаться?", "", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    Result.Text = "";
                    lastWord = "";
                    WordsDB.Clear();
                    listBox1.Items.Clear();
                    textBox1.Text = "";
                    while (sr.Peek() != -1)
                        WordsDB.Add(sr.ReadLine());
                }
            }
        }

        private void Button_Click_2(object sender, RoutedEventArgs e)
        {
            Rules rules = new Rules();
            rules.Show();
        }

       
    }
}
