using frontMoviles.Configuration;
using frontMoviles.Models;
using frontMoviles.Models.AuxiliarModels;
using frontMoviles.Services.ApiRest;
using frontMoviles.Validations.Base;
using frontMoviles.Validations.Rules;
using frontMoviles.Views;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    class InfoViewModel : ViewModelBase
    {
        private ObservableCollection<PlatoModel> platos;

        public ValidatableObject<string> BusquedaPlato { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> NombrePlato { get; set; }

        private bool isFacturarEnable;
        private bool isBuscarEnable;
        private PlatoModel plato;

        public ValidatableObject<long> ValorPlato { get; set; }

        public ValidatableObject<string> Descripcion { get; set; }

        public ICommand CrearPlatoModelCommand { get; set; }
        public MessageViewPop PopUp { get; set; }

        public ChooseRequest<PlatoModel> CreatePlate { get; set; }

        public ICommand ValidateBusquedaCommand { get; set; }
        public ICommand ValidateNombrePlatoCommand { get; set; }

        public ICommand ValidateDescripcionCommand { get; set; }
        public ObservableCollection<PlatoModel> Platos
        {
            get { return platos; }
            set
            {
                platos = value;
                OnPropertyChanged();
            }
        }

        public PlatoModel Plato
        {
            get { return plato; }
            set
            {
                plato = value;
                OnPropertyChanged();
            }
        }
        public bool IsGuardarEnable
        {
            get { return isFacturarEnable; }
            set
            {
                isFacturarEnable = value;
                OnPropertyChanged();
            }
        }

        public bool IsBuscarEnable
        {
            get { return isBuscarEnable; }
            set
            {
                isBuscarEnable = value;
                OnPropertyChanged();
            }
        }
        public InfoViewModel(ObservableCollection<PlatoModel> listaPlatos)
        {
            platos = listaPlatos;
            PopUp = new MessageViewPop();
            Plato = new PlatoModel();
            isFacturarEnable = false;
            IsBuscarEnable = true;
            InitializeFields();
        }

        public void InitializeFields()
        {
            BusquedaPlato = new ValidatableObject<string>();
            NombrePlato = new ValidatableObject<string>();
            ValorPlato = new ValidatableObject<long>();
            Descripcion = new ValidatableObject<string>();
            string urlCrearPlato = EndPoint.URL_SERVIDOR + EndPoint.CREAR_PLATO;

            ValidateNombrePlatoCommand = new Command(() => ValidateNombrePlatoForm(), () => true);
            ValidateDescripcionCommand = new Command(() => ValidateDescripcionForm(), () => true);
            CrearPlatoModelCommand = new Command(async () => await GuardarPlato(), () => true);

            BusquedaPlato.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Id es Obligatorio" });
            NombrePlato.Validations.Add(new RequiredRule<string> { ValidationMessage = "El nombre de la categoria es Obligatorio" });
            Descripcion.Validations.Add(new RequiredRule<string> { ValidationMessage = "La Descripcion es Obligatoria" });

        }

        private async Task GuardarPlato()
        {
            await CrearPlato();
            //((Command)CrearPlatoModelCommand).ChangeCanExecute();

        }

        public async Task CrearPlato()
        {
            try
            {
                PlatoModel plato = new PlatoModel()
                {
                    Nombre = NombrePlato.Value,
                    Valor = ValorPlato.Value,
                    DescripcionPlato = Descripcion.Value
                    //Nombre = "ArrozConPollo",
                    //Valor = 8000,
                    //DescripcionPlato = "ArrozConPollo"

                };
                APIResponse response = await CreatePlate.EjecutarEstrategia(plato);
                if (response.IsSuccess)
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Plato creado exitosamente";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Error al crear el Plato";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {

            }
        }
       
        private void ValidateNombrePlatoForm()
        {
            IsGuardarEnable = NombrePlato.Validate();
            ((Command)CrearPlatoModelCommand).ChangeCanExecute();
        }

        private void ValidateDescripcionForm()
        {
            IsGuardarEnable = Descripcion.Validate();
            ((Command)CrearPlatoModelCommand).ChangeCanExecute();
        }

    }
}
