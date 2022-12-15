using EsoftRozhin.AppDataFile;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Task = EsoftRozhin.AppDataFile.Task;

namespace EsoftRozhin.Pages
{
    /// <summary>
    /// Логика взаимодействия для ExecutorPage.xaml
    /// </summary>
    public partial class ExecutorPage : Page
    {
        public ExecutorPage()
        {
            InitializeComponent();
            var allTypes = EsoftBaseRozhinEntities.GetContext().StatusTask.ToList();
            allTypes.Insert(0, new StatusTask
            {
                Status = "Все типы"
            });
            ComboType.ItemsSource = allTypes;

            ComboType.SelectedIndex = 0;

            var currentTask = EsoftBaseRozhinEntities.GetContext().Task.ToList();
            LViewExecutor.ItemsSource = currentTask;

        }

        private void UpdateExecutor()
        {
            var currentExecutor = EsoftBaseRozhinEntities.GetContext().StatusTask.ToList();

            //if (ComboType.SelectedIndex > 0)
            //    currentExecutor = currentExecutor.Where(p => p.Status.Contains(ComboType.SelectedItem as StatusTask)).ToList();

            currentExecutor = currentExecutor.Where(p => p.Status.ToLower().Contains(TBoxSearch.Text.ToLower())).ToList();

            LViewExecutor.ItemsSource = currentExecutor.OrderBy(p => p.Status).ToList();

        }

        private void ButtonBack_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.GoBack();
        }

        private void ComboType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            UpdateExecutor();
        }

        private void TBoxSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateExecutor();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            Manager.MainFrame.Navigate(new AddEditPageE((sender as Button).DataContext as Task));
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var agentForRemoving = LViewExecutor.SelectedItems.Cast<Task>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить следующие {agentForRemoving.Count()} элементов?", "Внимание",
                MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    EsoftBaseRozhinEntities.GetContext().Task.RemoveRange(agentForRemoving);
                    EsoftBaseRozhinEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены!");

                    LViewExecutor.ItemsSource = EsoftBaseRozhinEntities.GetContext().Task.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            //Manager.MainFrame.Navigate(new AddEditPageE(null));
            try
            {
                MessageBox.Show("Страница находится в разработке!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message.ToString());
            }
        }

        private void Page_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                EsoftBaseRozhinEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                LViewExecutor.ItemsSource = EsoftBaseRozhinEntities.GetContext().Task.ToList();
            }
        }
    }
}
