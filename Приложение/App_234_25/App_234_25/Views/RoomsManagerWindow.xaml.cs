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
    /// Логика взаимодействия для RoomsManagerWindow.xaml
    /// </summary>
    public partial class RoomsManagerWindow : Window
    {
        public RoomsManagerWindow()
        {
            InitializeComponent();
            LoadRooms();
        }

        private void LoadRooms()
        {
            using (var db = new user25Entities())
            {
                lbRooms.ItemsSource = db.Rooms.ToList();
            }
        }

        private void lbRooms_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbRooms.SelectedItem is Room selected)
            {
                // Загружаем текущий Description кабинета в текстовое поле
                txtDescription.Text = selected.Description;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (lbRooms.SelectedItem is Room selected)
            {
                using (var db = new user25Entities())
                {
                    var dbRoom = db.Rooms.Find(selected.Id);
                    if (dbRoom != null)
                    {
                        // Сохраняем введенный тег, переводя его в верхний регистр для порядка
                        dbRoom.Description = txtDescription.Text.Trim().ToUpper();
                        db.SaveChanges();

                        MessageBox.Show($"Тег для кабинета {dbRoom.RoomNumber} успешно обновлен!");
                        LoadRooms(); // Обновляем список
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите кабинет из списка!");
            }
        }
    }
}
