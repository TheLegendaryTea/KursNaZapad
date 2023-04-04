﻿using System;
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

namespace KursNaZapad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Application.Current.MainWindow = this;
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if ((bool)rad_standart.IsChecked)
            {
                Game game = new Game();
                game.Show();
            }
            else if ((bool)rad_gorod.IsChecked)
            {
                Gorod gorod = new Gorod();
                gorod.Show();
            }
            else
            {
                Name name = new Name();
                name.Show();
            }
            this.Close();
        }
    }
}
