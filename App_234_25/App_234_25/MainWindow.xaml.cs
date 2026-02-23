using App_234_25.Model;
using App_234_25.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
using StudentGroup = App_234_25.Model.Group;
using System.Data.Entity;

namespace App_234_25
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            LoadData();
        }

        private void LoadData()
        {
            using (var context = new user25Entities())
            {
                cbGroups.ItemsSource = context.Groups.ToList();
            }
            RefreshData();
        }

        private void cbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            RefreshData();
        }
        private void btnOpenAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLessonWindow addWin = new AddLessonWindow();
            if (addWin.ShowDialog() == true)
            {
                // После добавления просто обновляем таблицу
                RefreshData();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            if (dgSchedule.SelectedItem is Schedule selectedSchedule)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить это занятие?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new user25Entities())
                    {
                        var toDelete = db.Schedules.Find(selectedSchedule.Id);
                        if (toDelete != null)
                        {
                            db.Schedules.Remove(toDelete);
                            db.SaveChanges();
                            MessageBox.Show("Запись удалена");

                            // Самое важное: просто обновляем таблицу
                            RefreshData();
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления в таблице!");
            }
        }

        // Для перетаскивания окна
        private void TitleBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == MouseButton.Left)
                this.DragMove();
        }

        // Кнопка свернуть
        private void Minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }

        // Кнопка закрыть
        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown(); // Или this.Close();
        }

        private void RefreshData()
        {
            using (var context = new user25Entities())
            {
                var query = context.Schedules
                    .Include(s => s.Lecturer)
                    .Include(s => s.Room)
                    .Include(s => s.Group)
                    .AsQueryable();

                if (cbGroups.SelectedItem is StudentGroup selectedGroup)
                {
                    query = query.Where(s => s.GroupId == selectedGroup.Id);
                }

                // УДАЛИ ОТСЮДА БЛОК if (dpFilterDate.SelectedDate != null)

                dgSchedule.ItemsSource = query
                    .OrderBy(s => s.LessonDate)
                    .ThenBy(s => s.LessonNumber)
                    .ToList();
            }
        }

        private void btnClearAll_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Вы уверены, что хотите полностью очистить расписание?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                using (var db = new user25Entities())
                {
                    // Удаляем все записи из таблицы расписания
                    db.Schedules.RemoveRange(db.Schedules);
                    db.SaveChanges();

                    MessageBox.Show("Расписание полностью очищено!");
                    RefreshData();
                }
            }
        }
        private void btnGenerate_Click(object sender, RoutedEventArgs e)
        {
            // 1. Вызываем окно настроек
            GenerateWindow settingsWin = new GenerateWindow { Owner = this };
            if (settingsWin.ShowDialog() != true) return;

            int minL = settingsWin.MinLessons;
            int maxL = settingsWin.MaxLessons;

            using (var db = new user25Entities())
            {
                var allGroups = db.Groups.ToList();
                var allLecturers = db.Lecturers.ToList();
                var allRooms = db.Rooms.ToList();

                if (!allGroups.Any() || !allLecturers.Any() || !allRooms.Any())
                {
                    MessageBox.Show("Заполните справочники!");
                    return;
                }

                Random rnd = new Random();
                DateTime startDate = DateTime.Now.Date;
                int addedCount = 0;

                // Генерация на 5 рабочих дней
                for (int dayOffset = 0; dayOffset < 5; dayOffset++)
                {
                    DateTime currentDate = startDate.AddDays(dayOffset);
                    if (currentDate.DayOfWeek == DayOfWeek.Saturday || currentDate.DayOfWeek == DayOfWeek.Sunday) continue;

                    string dayStr = currentDate.ToString("dddd", new System.Globalization.CultureInfo("ru-RU"));
                    dayStr = char.ToUpper(dayStr[0]) + dayStr.Substring(1);

                    foreach (var group in allGroups)
                    {
                        // Определяем случайное кол-во пар для ЭТОЙ группы на ЭТОТ день
                        int lessonsToCreate = rnd.Next(minL, maxL + 1);

                        for (int lessonNum = 1; lessonNum <= lessonsToCreate; lessonNum++)
                        {
                            // Пытаемся подобрать свободного препода и аудиторию (до 10 попыток)
                            bool success = false;
                            for (int retry = 0; retry < 10; retry++)
                            {
                                var lecturer = allLecturers[rnd.Next(allLecturers.Count)];
                                var room = allRooms[rnd.Next(allRooms.Count)];

                                bool busy = db.Schedules.Local.Any(s => s.LessonDate == currentDate && s.LessonNumber == lessonNum && (s.LecturerId == lecturer.Id || s.RoomId == room.Id)) ||
                                           db.Schedules.Any(s => s.LessonDate == currentDate && s.LessonNumber == lessonNum && (s.LecturerId == lecturer.Id || s.RoomId == room.Id));

                                if (!busy)
                                {
                                    db.Schedules.Add(new Schedule
                                    {
                                        LessonDate = currentDate,
                                        DayOfWeek = dayStr,
                                        LessonNumber = lessonNum,
                                        GroupId = group.Id,
                                        LecturerId = lecturer.Id,
                                        RoomId = room.Id,
                                        SubjectName = "Дисциплина " + rnd.Next(1, 15)
                                    });
                                    addedCount++;
                                    success = true;
                                    break;
                                }
                            }
                        }
                    }
                }
                db.SaveChanges();
                MessageBox.Show($"Успешно сгенерировано {addedCount} пар.");
                RefreshData();
            }
        }
    }
}
