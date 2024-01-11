using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;

namespace Perimetry
{
    public partial class MainWindow : Window
    {
        private const int ellipseSize = 20;
        private const int fieldRadius = 400;
        private const int amountOfDots = 50;
        private bool enterPressed = false;
        public MainWindow()
        {
            InitializeComponent();
            CenterWindowOnScreen();
            StartTest();
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
           enterPressed = true;  
        }

        private void CenterWindowOnScreen()
        {
            double screenWidth = SystemParameters.PrimaryScreenWidth;
            double screenHeight = SystemParameters.PrimaryScreenHeight;
            double windowWidth = this.Width;
            double windowHeight = this.Height;
            this.Left = (screenWidth / 2) - (windowWidth / 2);
            this.Top = (screenHeight / 2) - (windowHeight / 2);
        }

        private async void StartTest()
        {
            for (int i = 0; i < amountOfDots; i++)
            {
                var random = new Random();
                double a = random.NextDouble() * 2 * Math.PI;
                double r = fieldRadius * Math.Sqrt(random.NextDouble());
                double x = r * Math.Cos(a) + 400;
                double y = r * Math.Sin(a) + 400;

                var el = new Ellipse();

                el.Width = ellipseSize;
                el.Height = ellipseSize;
                el.Fill = new SolidColorBrush(Color.FromRgb(0, 0, 0));
                Canvas.Children.Add(el);
                Canvas.SetLeft(el, x - ellipseSize / 2);
                Canvas.SetTop(el, y - ellipseSize / 2);
                for(int j = 0; j < 300; j++)
                {
                    if(!enterPressed)
                        await Task.Delay(10);
                }
                if (enterPressed)
                    el.Fill = new SolidColorBrush(Color.FromRgb(0, 255, 0));
                else
                    el.Fill = new SolidColorBrush(Color.FromRgb(255, 0, 0));
                el.Visibility = Visibility.Hidden;
                enterPressed = false;
            }

            foreach(var el in Canvas.Children)
            {
                var ell = el as Ellipse;
                if (ell != null)
                {
                    ell.Visibility = Visibility.Visible;
                }
            }
        }
    }
}
