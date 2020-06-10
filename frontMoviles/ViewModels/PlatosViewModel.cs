using frontMoviles.Configuration;
using frontMoviles.Models;
using frontMoviles.Models.AuxiliarModels;
using frontMoviles.Services.ApiRest;
using frontMoviles.Services.Navigation;
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
using static frontMoviles.Models.PlatoModel;

namespace frontMoviles.ViewModels
{
    public class PlatosViewModel : ViewModelBase
    {
        #region Properties
        
        public ValidatableObject<string> NombrePlato { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> Valor { get; set; }

        private PlatoModel plato;

        private bool isGuardarEnable;

        private ObservableCollection<PlatoModel> platos;

        public MessageViewPop PopUp { get; set; }

        #region Requests
        public ChooseRequest<PlatoModel> CreatePlate { get; set; }

        public ChooseRequest<BaseModel> GetPlatos { get; set; }

        #endregion Requests

        #region Commands
        public ICommand CrearPlatoModelCommand { get; set; }

        public ICommand ListaPlatosCommand { get; set; }
        #endregion Commands

        #endregion Properties

        #region Getters & Setters

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
            get { return isGuardarEnable; }
            set
            {
                isGuardarEnable = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<PlatoModel> Platos
        {
            get { return platos; }
            set
            {
                platos = value;
                OnPropertyChanged();
            }
        }
        #endregion Getters & Setters

        #region Initialize

        public PlatosViewModel()
        {
            PopUp = new MessageViewPop();
            Plato = new PlatoModel();
            IsGuardarEnable = true;
            Platos = new ObservableCollection<PlatoModel>();
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }
        #endregion Initialize

        public void InitializeRequest()
        {
            string urlCrearPlato = EndPoint.URL_SERVIDOR + EndPoint.CREAR_PLATO;
            string urlPlatos = EndPoint.URL_SERVIDOR + EndPoint.CONSULTAR_ALL_PLATES;

            GetPlatos = new ChooseRequest<BaseModel>();
            GetPlatos.ElegirEstrategia("GET", urlPlatos);

            CreatePlate = new ChooseRequest<PlatoModel>();
            CreatePlate.ElegirEstrategia("POST", urlCrearPlato);

        }
        public void InitializeCommands()
        {
            CrearPlatoModelCommand = new Command(async () => await GuardarPlato(), () => IsGuardarEnable);
            ListaPlatosCommand = new Command(async () => await ListarPlatos(), () => true);
        }

        public void InitializeFields()
        {
            NombrePlato = new ValidatableObject<string>();
            Valor = new ValidatableObject<string>();

            NombrePlato.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Id es Obligatorio" });
            Valor.Validations.Add(new RequiredRule<string> { ValidationMessage = "El nombre de la categoria es Obligatorio" });
        }

        private async Task GuardarPlato()
        {
            await CrearPlato();
            ((Command)CrearPlatoModelCommand).ChangeCanExecute();

        }

        public async Task CrearPlato()
        {
            try
            {
                PlatoModel plato = new PlatoModel()
                {
                    //Nombre = NombreUsuario.Value,
                    //Apellido = ApellidoUsuario.Value
                    Nombre = "ArrozConPollo",
                    Valor = 8000,
                    DescripcionPlato = "ArrozConPollo"

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

        public async Task ListarPlatos()
        {
            var navigationStack = App.Current.MainPage.Navigation.NavigationStack;
            APIResponse response = await GetPlatos.EjecutarEstrategia(null);
            if (response.IsSuccess)
            {
                List<PlatoModel> listaPlatos = JsonConvert.DeserializeObject<List<PlatoModel>>(response.Response);
                Platos = new ObservableCollection<PlatoModel>(listaPlatos);
            }
            else
            {
                ((MessageViewModel)PopUp.BindingContext).Message = "Error al cargar las categorías";
                await PopupNavigation.Instance.PushAsync(PopUp);
            }
        }
    }
}
