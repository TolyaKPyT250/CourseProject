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
using System.Windows.Shapes;

namespace App_234_25.Views
{
    /// <summary>
    /// Логика взаимодействия для GenerateWindow.xaml
    /// </summary>
    public partial class GenerateWindow : Window
    {
        public int MinLessons { get; set; }
        public int MaxLessons { get; set; }

        public GenerateWindow()
        {
            InitializeComponent();
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            MinLessons = (int)sldMin.Value;
            MaxLessons = (int)sldMax.Value;
            this.DialogResult = true;
        }
    }
}
