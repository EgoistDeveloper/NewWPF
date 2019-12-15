using GalaSoft.MvvmLight;
using NewWPF.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NewWPF.ViewModel.App
{
    public class WelcomeViewModel : ViewModelBase
    {
        public WelcomeViewModel()
        {
            // do somethings

            using (var db = new AppDbContext())
            {
                _ = db.Examples.ToList();
            }
        }
    }
}
