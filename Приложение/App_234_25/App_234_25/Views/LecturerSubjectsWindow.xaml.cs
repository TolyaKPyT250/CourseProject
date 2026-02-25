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
        private void AddLecturer_Click(object sender, RoutedEventArgs e)
        {
            string name = txtNewLecturerName.Text.Trim();
            if (string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Введите ФИО преподавателя!");
                return;
            }

            using (var db = new user25Entities())
            {
                var newLecturer = new Lecturer
                {
                    FullName = name,
                    Department = ""
                };

                db.Lecturers.Add(newLecturer);
                db.SaveChanges();

                txtNewLecturerName.Clear();
                LoadLecturers();
                lbLecturers.SelectedItem = newLecturer;
                MessageBox.Show("Преподаватель успешно добавлен!");
            }
        }
        private void DeleteLecturer_Click(object sender, RoutedEventArgs e)
        {
            if (lbLecturers.SelectedItem is Lecturer selected)
            {
                var result = MessageBox.Show($"Удалить преподавателя {selected.FullName}?\nЭто удалит все связанные записи в расписании!",
                                             "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new user25Entities())
                    {
                        var dbLecturer = db.Lecturers.Find(selected.Id);
                        if (dbLecturer != null)
                        {
                            try
                            {
                                db.Lecturers.Remove(dbLecturer);
                                db.SaveChanges();
                                txtSubjects.Clear();
                                LoadLecturers();
                                MessageBox.Show("Преподаватель удален.");
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Ошибка удаления. Возможно, преподаватель уже задействован в расписании.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите преподавателя из списка!");
            }
        }
    }
}
