using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using NewWPF.Models.Common;
using static NewWPF.DI.DI;

namespace NewWPF.ViewModel.App
{
    public class NavbarViewModel : ViewModelBase
    {
        public NavbarViewModel()
        {
            NavbarItems = new ObservableCollection<NavbarItem>()
            {
                new NavbarItem()
                {
                    ApplicationPage = ApplicationPage.Home,
                    Title = "Home Page",
                    IconData = Application.Current.FindResource("Home") as Geometry,
                }
            };

            SetIsChecked(ViewModelApplication.CurrentPage);

            GoToCommand = new RelayParameterizedCommand(GoTo);
        }

        #region Commands

        public ICommand GoToCommand { get; set; }

        #endregion


        #region Public Properties

        public ObservableCollection<NavbarItem> NavbarItems { get; set; }

        #endregion


        #region Methods

        /// <summary>
        /// Go to app page
        /// </summary>
        /// <param name="sender"></param>
        public void GoTo(object sender)
        {
            var navbarItem = (NavbarItem)sender;

            if (ViewModelApplication.CurrentPage != navbarItem.ApplicationPage)
            {
                SetIsChecked(navbarItem.ApplicationPage);
                ViewModelApplication.GoToPage(navbarItem.ApplicationPage);
            }
        }

        /// <summary>
        /// Set is checked navbar item
        /// </summary>
        /// <param name="applicationPage"></param>
        public void SetIsChecked(ApplicationPage applicationPage)
        {
            foreach (var item in NavbarItems)
            {
                item.IsChecked = applicationPage == item.ApplicationPage ? true : false;
            }
        }

        #endregion
    }
}