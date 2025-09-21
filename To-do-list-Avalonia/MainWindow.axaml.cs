using Avalonia.Controls;
using To_do_list_Avalonia.ViewModels;

namespace To_do_list_Avalonia
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DataContext = new MainViewModel();
        }
    }
}