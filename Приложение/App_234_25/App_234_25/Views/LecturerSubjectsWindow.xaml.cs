using App_234_25.Model;
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
    /// Логика взаимодействия для LecturerSubjectsWindow.xaml
    /// </summary>
    public partial class LecturerSubjectsWindow : Window
    {
        public LecturerSubjectsWindow()
        {
            InitializeComponent();
            LoadLecturers();
        }
        private void LoadLecturers()
        {
            using (var db = new user25Entities())
            {
                lbLecturers.ItemsSource = db.Lecturers.ToList();
            }
        }
        private void lbLecturers_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbLecturers.SelectedItem is Lecturer selected)
            {
                txtSubjects.Text = selected.Department;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (lbLecturers.SelectedItem is Lecturer selected)
            {
                using (var db = new user25Entities())
                {
                    var dbLecturer = db.Lecturers.Find(selected.Id);
                    if (dbLecturer != null)
                    {
                        dbLecturer.Department = txtSubjects.Text;
                        db.SaveChanges();
                        MessageBox.Show("Предметы обновлены!");
                        LoadLecturers();
                    }
                }
            }
            else
            {
                MessageBox.Show("Сначала выберите преподавателя из списка слева!");
            }
        }
    }
}
