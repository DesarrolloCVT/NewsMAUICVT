using NewsMauiCVT.Views;

namespace NewsMauiCVT
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            DevExpress.Maui.DataGrid.Initializer.Init();
            MainPage =  new NavigationPage(new LoginPage());

            Current.UserAppTheme = AppTheme.Light;
            RequestedThemeChanged += (s, e) => { Current.UserAppTheme = AppTheme.Light; };
        }

        public static int Iduser { get; set; }
        public static string UserSistema { get; set; }
        public static string NombreUsuario { get; set; }
        public static int idPerfil { get; set; }
        public static bool vali { get; set; }
        public static string Fconsoli { get; set; }
        public static string DptoConsolidado { get; set; }
        public static string lati { get; set; }
        public static string longi { get; set; }
        public static string altit { get; set; }
        protected override void OnStart()
        {
            // Handle when your app starts
            Console.WriteLine("OnStart");
        }
        protected override void OnSleep()
        {
            Console.WriteLine("OnSleep");
        }
        protected override void OnResume()
        {
            Console.WriteLine("OnResume");
        }
    }
}
