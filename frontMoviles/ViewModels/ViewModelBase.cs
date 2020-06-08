using frontMoviles.Models;
using frontMoviles.Services.Navigation;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace frontMoviles.ViewModels
{
    public class ViewModelBase : NotificationObject
    {

        #region Properties
        public NavigationService NavigationService { get; set; }
        #endregion

        #region Getters & Setters

        #endregion Getters & Setters

        public ViewModelBase()
        {
            NavigationService = App.NavigationService;
        }

        public virtual async Task ConstructorAsync(object parameters)
        {
            await Task.FromResult(true);
        }
    }
}
