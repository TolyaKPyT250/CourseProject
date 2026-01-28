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
        }

        private void cbGroups_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cbGroups.SelectedItem is App_234_25.Model.Group selectedGroup)
            {
                using (var context = new user25Entities())
                {
                    // Используем .Include, чтобы подтянуть связанные таблицы
                    var schedule = context.Schedules
                        .Include(s => s.Lecturer)
                        .Include(s => s.Room)
                        .Where(s => s.GroupId == selectedGroup.Id)
                        .ToList();

                    dgSchedule.ItemsSource = schedule;
                }
            }
        }
        private void btnOpenAdd_Click(object sender, RoutedEventArgs e)
        {
            AddLessonWindow addWin = new AddLessonWindow();
            if (addWin.ShowDialog() == true)
            {
                LoadData(); // Обновляем таблицу, чтобы увидеть новую пару
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            // Проверяем, выбрана ли строка в таблице
            if (dgSchedule.SelectedItem is Schedule selectedSchedule)
            {
                var result = MessageBox.Show("Вы уверены, что хотите удалить это занятие?", "Подтверждение", MessageBoxButton.YesNo);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new user25Entities())
                    {
                        // Находим объект в контексте по ID и удаляем
                        var toDelete = db.Schedules.Find(selectedSchedule.Id);
                        if (toDelete != null)
                        {
                            db.Schedules.Remove(toDelete);
                            db.SaveChanges();
                            MessageBox.Show("Запись удалена");
                            LoadData(); // Обновляем список групп
                                        // И принудительно обновляем таблицу, вызвав событие смены группы
                            cbGroups_SelectionChanged(null, null);
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления в таблице!");
            }
        }
    }
}
