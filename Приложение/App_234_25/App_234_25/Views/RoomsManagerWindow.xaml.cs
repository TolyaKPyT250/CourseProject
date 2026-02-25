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
                        dbRoom.Description = txtDescription.Text.Trim().ToUpper();
                        db.SaveChanges();

                        MessageBox.Show($"Тег для кабинета {dbRoom.RoomNumber} успешно обновлен!");
                        LoadRooms();
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите кабинет из списка!");
            }
        }
        private void AddRoom_Click(object sender, RoutedEventArgs e)
        {
            string roomNum = txtNewRoomNumber.Text.Trim();
            if (string.IsNullOrEmpty(roomNum))
            {
                MessageBox.Show("Введите номер кабинета!");
                return;
            }

            using (var db = new user25Entities())
            {
                var newRoom = new Room { RoomNumber = roomNum, Description = "ОБЩ" };
                db.Rooms.Add(newRoom);
                db.SaveChanges();

                txtNewRoomNumber.Clear();
                LoadRooms();
                lbRooms.SelectedItem = newRoom;
                MessageBox.Show("Кабинет добавлен!");
            }
        }
        private void DeleteRoom_Click(object sender, RoutedEventArgs e)
        {
            if (lbRooms.SelectedItem is Room selected)
            {
                var result = MessageBox.Show($"Удалить кабинет {selected.RoomNumber}?", "Внимание",
                                             MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new user25Entities())
                    {
                        var dbRoom = db.Rooms.Find(selected.Id);
                        if (dbRoom != null)
                        {
                            try
                            {
                                db.Rooms.Remove(dbRoom);
                                db.SaveChanges();
                                txtDescription.Clear();
                                LoadRooms();
                            }
                            catch (Exception)
                            {
                                MessageBox.Show("Нельзя удалить кабинет, в котором проводятся занятия!");
                            }
                        }
                    }
                }
            }
        }
    }
}
