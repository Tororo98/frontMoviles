﻿using frontMoviles.Models;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        #region Properties
        private string message;

        public ICommand CloseCommand { get; set; }
        #endregion

        #region Getters y Setters
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters y Setters

        public MessageViewModel()
        {
            CloseCommand = new Command(async () => await Close(), () => true);
        }

        public async Task Close()
        {
            await PopupNavigation.Instance.PopAsync();
        }
    }
}
