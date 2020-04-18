using Xamarin.Forms;

namespace AviationApp
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new AppShell();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        public static Database.Database SQLiteDatabase
        {
            get
            {
                if (sqliteDatabase == null)
                {
                    sqliteDatabase = new Database.Database();
                }
                return sqliteDatabase;
            }
        }
        private static Database.Database sqliteDatabase;
    }
}
