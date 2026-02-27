using App_234_25.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace App_234_25.Views
{
    public partial class AddLessonWindow : Window
    {
        public AddLessonWindow()
        {
            InitializeComponent();

            // Автозаполнение дня недели при выборе даты
            dpLessonDate.SelectedDateChanged += (s, e) =>
            {
                if (dpLessonDate.SelectedDate != null)
                {
                    var culture = new System.Globalization.CultureInfo("ru-RU");
                    string day = dpLessonDate.SelectedDate.Value.ToString("dddd", culture);
                    day = char.ToUpper(day[0]) + day.Substring(1);
                    cbDay.SelectedItem = day;
                }
            };
            FillLists();
        }

        private void FillLists()
        {
            using (var db = new user25Entities())
            {
                cbLecturer.ItemsSource = db.Lecturers.ToList();
                cbGroup.ItemsSource = db.Groups.ToList();
                cbRoom.ItemsSource = db.Rooms.ToList();

                cbDay.ItemsSource = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
                cbLessonNumber.ItemsSource = new List<int> { 1, 2, 3, 4, 5 };
            }
        }

        // Событие выбора преподавателя: загружаем ЕГО предметы из текстового поля
        private void cbLecturer_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbLecturer.SelectedItem is Lecturer selectedLecturer)
            {
                // Используем поле Department для хранения предметов через ";"
                // Пример в БД: "Математика;Физика;Информатика"
                if (!string.IsNullOrWhiteSpace(selectedLecturer.Department))
                {
                    var subjects = selectedLecturer.Department.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                    cbSubject.ItemsSource = subjects;
                }
                else
                {
                    cbSubject.ItemsSource = new List<string> { "Предметы не назначены" };
                }
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            var lecturer = cbLecturer.SelectedItem as Lecturer;
            var group = cbGroup.SelectedItem as App_234_25.Model.Group;
            var room = cbRoom.SelectedItem as Room;
            var day = cbDay.SelectedItem as string;

            // Теперь предмет — это просто строка из ComboBox
            var selectedSubject = cbSubject.SelectedItem as string;

            DateTime? selectedDate = dpLessonDate.SelectedDate;

            if (lecturer == null || group == null || room == null || day == null ||
                cbLessonNumber.SelectedItem == null || selectedDate == null ||
                selectedSubject == null || selectedSubject == "Предметы не назначены")
            {
                MessageBox.Show("Заполните абсолютно все поля!");
                return;
            }

            int lessonNum = (int)cbLessonNumber.SelectedItem;
            DateTime dateValue = selectedDate.Value.Date;

            using (var db = new user25Entities())
            {
                bool lecBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.LecturerId == lecturer.Id);
                bool grpBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.GroupId == group.Id);
                bool romBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.RoomId == room.Id);

                if (lecBusy) { MessageBox.Show("Преподаватель уже занят!"); return; }
                if (grpBusy) { MessageBox.Show("У группы уже есть занятие!"); return; }
                if (romBusy) { MessageBox.Show("Аудитория уже занята другой парой!"); return; }

                var newEntry = new Schedule
                {
                    DayOfWeek = day,
                    LessonNumber = lessonNum,
                    LecturerId = lecturer.Id,
                    GroupId = group.Id,
                    RoomId = room.Id,
                    SubjectName = selectedSubject,
                    LessonDate = dateValue
                };

                try
                {
                    db.Schedules.Add(newEntry);
                    db.SaveChanges();
                    MessageBox.Show("Запись успешно добавлена!");
                    this.DialogResult = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка: " + ex.Message);
                }
            }
        }
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}