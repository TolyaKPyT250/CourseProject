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
    /// Логика взаимодействия для AddLessonWindow.xaml
    /// </summary>
    public partial class AddLessonWindow : Window
    {
        public AddLessonWindow()
        {
            InitializeComponent();
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
                // Заполняем списки данными из БД
                cbLecturer.ItemsSource = db.Lecturers.ToList();
                cbGroup.ItemsSource = db.Groups.ToList();
                cbRoom.ItemsSource = db.Rooms.ToList();

                // Статические списки для дней и времени
                cbDay.ItemsSource = new List<string> { "Понедельник", "Вторник", "Среда", "Четверг", "Пятница" };
                cbLessonNumber.ItemsSource = new List<int> { 1, 2, 3, 4, 5 };
            }
        }
        private void Save_Click(object sender, RoutedEventArgs e)
        {
            // Получаем данные из интерфейса
            var lecturer = cbLecturer.SelectedItem as Lecturer;
            var group = cbGroup.SelectedItem as App_234_25.Model.Group;
            var room = cbRoom.SelectedItem as Room;
            var day = cbDay.SelectedItem as string;
            DateTime? selectedDate = dpLessonDate.SelectedDate;

            if (lecturer == null || group == null || room == null || day == null || cbLessonNumber.SelectedItem == null || selectedDate == null || string.IsNullOrWhiteSpace(txtSubject.Text))
            {
                MessageBox.Show("Заполните абсолютно все поля, включая дату и название предмета!");
                return;
            }

            int lessonNum = (int)cbLessonNumber.SelectedItem;
            DateTime dateValue = selectedDate.Value.Date;

            using (var db = new user25Entities())
            {
                // 1. Проверка: свободен ли ПРЕПОДАВАТЕЛЬ?
                bool lecBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.LecturerId == lecturer.Id);

                // 2. Проверка: свободна ли ГРУППА?
                bool grpBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.GroupId == group.Id);

                // 3. Проверка: свободна ли АУДИТОРИЯ? (Чтобы два препода не вели в одном кабинете)
                bool romBusy = db.Schedules.Any(s => s.LessonDate == dateValue && s.LessonNumber == lessonNum && s.RoomId == room.Id);

                // Вывод ошибок
                if (lecBusy) { MessageBox.Show("Преподаватель уже занят!"); return; }
                if (grpBusy) { MessageBox.Show("У группы уже есть занятие!"); return; }
                if (romBusy) { MessageBox.Show("Аудитория уже занята другой парой!"); return; }

                // Если всё ок — сохраняем
                var newEntry = new Schedule
                {
                    DayOfWeek = day,
                    LessonNumber = lessonNum,
                    LecturerId = lecturer.Id,
                    GroupId = group.Id,
                    RoomId = room.Id,
                    SubjectName = txtSubject.Text,
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
                    MessageBox.Show("Ошибка при сохранении: " + ex.Message);
                }
            }
        }
    }
}
