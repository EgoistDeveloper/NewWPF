using GalaSoft.MvvmLight;
using Microsoft.EntityFrameworkCore;
using NewWPF.Data;
using NewWPF.Helpers;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace NewWPF.ViewModel.Example
{
    public class ExampleViewModel : ViewModelBase
    {
        public ExampleViewModel()
        {
            GoToPageCommand = new RelayParameterizedCommand(GoToPage);

            Examples = new ObservableCollection<Models.ExampleModel.Example>();
        }

        #region Commands

        public ICommand GoToPageCommand { get; set; }


        #endregion

        #region Public Properties

        public ObservableCollection<Models.ExampleModel.Example> Examples { get; set; }

        #endregion

        #region Pagination

        public Pagination Pagination { get; set; }
        public int PageLimit { get; set; } = 24;
        public int CurrentPage { get; set; } = 1;
        public string SearchTerm { get; set; }

        #endregion


        #region Methods

        public void LoadItems()
        {
            using var db = new AppDbContext();

            var totalSize = db.Examples.Where(x => EF.Functions.Like(x.Title, $"%{SearchTerm}%")).Count();
            totalSize = totalSize > 0 ? totalSize : 1;

            Pagination = new Pagination(totalSize, CurrentPage, PageLimit, 10);

            Examples = db.Examples.Where(x => EF.Functions.Like(x.Title, $"%{SearchTerm}%"))
            .OrderBy(x => x.AddedDate)
            .Skip((CurrentPage - 1) * PageLimit)
            .Take(PageLimit)
            .ToObservableCollection();
        }

        /// <summary>
        /// Go to seleceted page
        /// </summary>
        /// <param name="sender"></param>
        public void GoToPage(object sender)
        {
            var page = (Models.Common.Page)sender;

            CurrentPage = page.PageNumber;
            LoadItems();
        }

        #endregion
    }
}