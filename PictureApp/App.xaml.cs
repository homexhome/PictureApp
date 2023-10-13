using PictureApp.Pages;

namespace PictureApp
{
    public partial class App : Application
    {
        public App() {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());

        }
    }
}