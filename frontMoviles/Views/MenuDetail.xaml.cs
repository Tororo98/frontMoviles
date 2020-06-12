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
    public partial class MenuDetail : ContentPage
    {
        public MenuDetail(string Nombre, string Descripcion, string Valor)
        {
            InitializeComponent();
            MiNombre.Text = Nombre;
            MiDesc.Text = Descripcion;
            MiValor.Text = Valor;
        }
    }
}