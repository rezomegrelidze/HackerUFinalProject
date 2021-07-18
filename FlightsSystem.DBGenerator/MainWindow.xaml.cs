using System;
using System.Collections.ObjectModel;
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
using FlightsSystem.DBGenerator.Models;
using FlightsSystem.DBGenerator.Services;

namespace FlightsSystem.DBGenerator
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<string> LogList;

        public MainWindow()
        {
            InitializeComponent();
            LogList = new ObservableCollection<string>();
            LoggingListBox.ItemsSource = LogList;
        }

        private void ZoomInOrOutHandle(object sender, MouseWheelEventArgs e)
        {
            if (Keyboard.IsKeyDown(Key.LeftCtrl))
            {
                var newFontSize = FontSize + (e.Delta) * 0.01;
                FontSize = newFontSize > 10 ? newFontSize : FontSize;
            }
        }

        private async void AddToDB(object sender, RoutedEventArgs e)
        {
            var randomDataGenerator = new RandomDataGenerator(MainGrid.DataContext as RandomDataSpec,
                LogList);
            randomDataGenerator.OnProgressChanged += () =>
                {
                    ProgressBar.Value = randomDataGenerator.OperationProgress;
                    if (Convert.ToInt32(ProgressBar.Value) == 100)
                    {
                        MessageBox.Show("finished!!");
                    }
                };

            await Dispatcher.InvokeAsync(
                async () => await randomDataGenerator.AddRandomDataToDatabaseAsync());
            ProgressBar.Value = 0;
        }

        private async void ReplaceDB(object sender, RoutedEventArgs e)
        {
            var randomDataGenerator = new RandomDataGenerator(MainGrid.DataContext as RandomDataSpec,
                LogList);
            randomDataGenerator.OnProgressChanged += () =>
            {
                ProgressBar.Value = randomDataGenerator.OperationProgress;
            };
            await randomDataGenerator.ReplaceDatabaseAsync();
            ProgressBar.Value = 0; // reset progress bar
        }
    }
}
