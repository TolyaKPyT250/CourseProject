using App_234_25.Model;
using System.Linq;
using System.Windows;
using StudentGroup = App_234_25.Model.Group;

namespace App_234_25.Views
{
    /// <summary>
    /// Логика взаимодействия для GroupsManagerWindow.xaml
    /// </summary>
    public partial class GroupsManagerWindow : Window
    {
        public GroupsManagerWindow()
        {
            InitializeComponent();
            LoadGroups();
        }

        private void LoadGroups()
        {
            using (var db = new user25Entities())
            {
                lbGroups.ItemsSource = db.Groups.ToList();
            }
        }
        private void lbGroups_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (lbGroups.SelectedItem is StudentGroup selected)
            {
                txtGroupName.Text = selected.GroupName;
                txtGroupDesc.Text = selected.Description;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroups.SelectedItem is StudentGroup selected)
            {
                using (var db = new user25Entities())
                {
                    var dbGroup = db.Groups.Find(selected.Id);
                    if (dbGroup != null)
                    {
                        dbGroup.GroupName = txtGroupName.Text;
                        dbGroup.Description = txtGroupDesc.Text.Trim().ToUpper();
                        db.SaveChanges();
                        MessageBox.Show("Данные группы обновлены!");
                        LoadGroups();
                    }
                }
            }
        }
        private void Add_Click(object sender, RoutedEventArgs e)
        {
            using (var db = new user25Entities())
            {
                var newGroup = new StudentGroup
                {
                    GroupName = "НОВАЯ ГРУППА",
                    Description = "ДРУГОЕ"
                };

                db.Groups.Add(newGroup);
                db.SaveChanges();

                LoadGroups();
                lbGroups.SelectedItem = newGroup;
                MessageBox.Show("Группа добавлена. Теперь отредактируйте её данные справа.");
            }
        }
        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            if (lbGroups.SelectedItem is StudentGroup selected)
            {
                var result = MessageBox.Show($"Вы уверены, что хотите удалить группу {selected.GroupName}?\nЭто также может повлиять на связанные записи в расписании!",
                                             "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Warning);

                if (result == MessageBoxResult.Yes)
                {
                    using (var db = new user25Entities())
                    {
                        var dbGroup = db.Groups.Find(selected.Id);
                        if (dbGroup != null)
                        {
                            try
                            {
                                db.Groups.Remove(dbGroup);
                                db.SaveChanges();

                                txtGroupName.Clear();
                                txtGroupDesc.Clear();
                                LoadGroups();
                                MessageBox.Show("Группа удалена.");
                            }
                            catch (System.Data.Entity.Infrastructure.DbUpdateException)
                            {
                                MessageBox.Show("Нельзя удалить группу, у которой уже есть занятия в расписании! Сначала очистите расписание.");
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Выберите группу для удаления!");
            }
        }
    }
}
