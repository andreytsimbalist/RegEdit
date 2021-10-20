using System.Windows.Controls;
using RegEdit.initializer;

namespace RegEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        public MainWindow()
        {
            InitializeComponent();

            siski.Items.Add(Initializer.Init());
        }
    }
}