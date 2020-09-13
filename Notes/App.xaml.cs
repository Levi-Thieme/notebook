using Notes.Data;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Notes
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();
            RegisterRepositoryDependencies();
            MainPage = CreateMainPage();
        }

        private Page CreateMainPage()
        {
            var tabbedPage = new TabbedPage();
            tabbedPage.Children.Add(CreateNotesPage());
            return tabbedPage;
        }

        private Page CreateNotesPage()
        {
            var notesRepository = new NoteRepository();
            var notesPage = new NotesPage(notesRepository);
            var page = new NavigationPage(notesPage) { Title = notesPage.Title };
            return page;
        }

        private void RegisterRepositoryDependencies()
        {
            DependencyService.Register<IDatabaseConnection, DatabaseConnection>();
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
