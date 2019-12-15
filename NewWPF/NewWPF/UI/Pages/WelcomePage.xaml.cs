using NewWPF.ViewModel.App;

namespace NewWPF.UI.Pages
{
    /// <summary>
    /// Interaction logic for WelcomePage.xaml
    /// </summary>
    public partial class WelcomePage : BasePage<WelcomeViewModel>
    {
        public WelcomePage() : base()
        {
            InitializeComponent();
        }

        public WelcomePage(WelcomeViewModel specificViewModel) : base(specificViewModel)
        {
            InitializeComponent();
        }
    }
}