﻿using frontMoviles.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace frontMoviles.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlatoView : ContentPage
    {
        PlatosViewModel context = new PlatosViewModel();
        public PlatoView()
        {
            InitializeComponent();
            BindingContext = context;
        }
    }
}