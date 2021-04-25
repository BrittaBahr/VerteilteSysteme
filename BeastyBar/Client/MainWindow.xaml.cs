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
namespace Client
{
    using System.Windows.Media.Imaging;
    using System.Windows.Navigation;
    using System.Windows.Shapes;
    using BeastyBarGameLogic.GamePlayer;
    using Client.ClassesForView;
    using Client.ViewModel;

    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly GameManagerVM gameManagerVM;

        private double actualGridWidth;

        private double actualGridHeight;

        public MainWindow()
        {
            InitializeComponent();
            this.gameManagerVM = new GameManagerVM(new BeastyBarPlayer(0, "Svenja", null, new Random()), new AlphaRedGreenBlue(255, 20, 20, 255), new List<ConnectedPlayerVM>());
            this.DataContext = this.gameManagerVM;
        }

        private void WindowSizeChanged(object sender, SizeChangedEventArgs e)
        {
            double menuWidth = e.NewSize.Width * 0.2;

            if (menuWidth > 200)
            {
                menuWidth = 200;
            }

            double newWidth = (((e.NewSize.Width - menuWidth) / 6) - 20 * 2.2); // 20 * 2.2 because there are border arround the cards
            double newHeight = newWidth * 1.5;
            this.gameManagerVM.CardHeight = newHeight;
            this.gameManagerVM.CardWidth = newWidth;
            this.gameManagerVM.MenuWidth = menuWidth;

            List<CardVM> cs = new List<CardVM>();

            foreach (CardVM x in this.gameManagerVM.Queue)
            {
                x.Height = newHeight;
                x.Width = newWidth;
                cs.Add(x);
            }

            this.gameManagerVM.Queue = cs.ToArray();

            cs = new List<CardVM>();

            foreach (CardVM x in this.gameManagerVM.Handcards)
            {
                x.Height = newHeight;
                x.Width = newWidth;
                cs.Add(x);
            }

            this.gameManagerVM.Handcards = cs.ToArray();
        }

        private void HandcardMouseEnter(object sender, MouseEventArgs e)
        {
            if (e.Source is Rectangle r)
            {
                r.Fill = new SolidColorBrush(Color.FromArgb(150, 204, 229, 255));
            }
        }

        private void HandcardMouseLeave(object sender, MouseEventArgs e)
        {
            if (e.Source is Rectangle r)
            {
                r.Fill = new SolidColorBrush(Color.FromArgb(0, 0, 0, 0));
            }
        }
    }
}
