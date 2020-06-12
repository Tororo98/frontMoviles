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
        
        public ValidatableObject<string> BusquedaPlato { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> NombrePlato { get; set; }
        public ValidatableObject<float> ValorPlato { get; set; }
        public ValidatableObject<string> Descripcion { get; set; }

        private List<PlatoModel> listaPlatos;

        private PlatoModel plato;

        private bool isFacturarEnable;
        private bool isBuscarEnable;

        private ObservableCollection<PlatoModel> platos;

        public MessageViewPop PopUp { get; set; }

        #region Requests
        public ChooseRequest<PlatoModel> CreatePlate { get; set; }

        // public ChooseRequest<PlatoModel> DeletePlate { get; set;}

        public ChooseRequest<BaseModel> GetPlate { get; set; }

        public ChooseRequest<BaseModel> GetPlatos { get; set; }

        public ChooseRequest<BaseModel> ElPlatos { get; set; }

        #endregion Requests

        #region Commands
        public ICommand CrearPlatoModelCommand { get; set; }

        //public ICommand EliminarPlatoModelCommand { get; set; }

        public ICommand FacturarCommand { get; set; }

        public ICommand ListaPlatosCommand { get; set; }

        public ICommand SelectPlateCommand { get; set; }

        public ICommand ValidateBusquedaCommand { get; set; }
        public ICommand ValidateNombrePlatoCommand { get; set; }
        public ICommand ValidateDescripcionCommand { get; set; } 

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

        public List<PlatoModel> ListaPlatos
        {
            get { return listaPlatos; }
            set
            {
                listaPlatos = value;
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
            isFacturarEnable = false;
            IsBuscarEnable = true;
            Platos = new ObservableCollection<PlatoModel>();
            ListaPlatos = new List<PlatoModel>();
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }
        #endregion Initialize

        public void InitializeRequest()
        {
            string urlCrearPlato = EndPoint.URL_SERVIDOR + EndPoint.CREAR_PLATO;
            string urlPlatos = EndPoint.URL_SERVIDOR + EndPoint.CONSULTAR_ALL_PLATES;
            string urlBuscarPlato = EndPoint.URL_SERVIDOR + EndPoint.CONSULTAR_PLATE;

            GetPlate = new ChooseRequest<BaseModel>();
            GetPlate.ElegirEstrategia("GET", urlBuscarPlato);

            GetPlatos = new ChooseRequest<BaseModel>();
            GetPlatos.ElegirEstrategia("GET", urlPlatos);

            CreatePlate = new ChooseRequest<PlatoModel>();
            CreatePlate.ElegirEstrategia("POST", urlCrearPlato);

        }
        public void InitializeCommands()
        {
            CrearPlatoModelCommand = new Command(async () => await GuardarPlato(), () => true);
            //ElminarPlatoModelCommand = new Command(async () => await GuardarPlato(), () => true);
            SelectPlateCommand = new Command(async () => await SelecccionarPlatoAComprar(), () => IsBuscarEnable);
            ListaPlatosCommand = new Command(async () => await ListarPlatos(), () => true);
            ValidateBusquedaCommand = new Command(() => ValidateBusquedaForm(), () => true);
            ValidateNombrePlatoCommand = new Command(() => ValidateNombrePlatoForm(), () => true);
            ValidateDescripcionCommand = new Command(() => ValidateDescripcionForm(), () => true);
            FacturarCommand = new Command(async () => await NavegarFactura(), () => isFacturarEnable);
        }

        public void InitializeFields()
        {
            BusquedaPlato = new ValidatableObject<string>();
            NombrePlato = new ValidatableObject<string>();
            ValorPlato = new ValidatableObject<float>();
            Descripcion = new ValidatableObject<string>();

            BusquedaPlato.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Id es Obligatorio" });
            NombrePlato.Validations.Add(new RequiredRule<string> { ValidationMessage = "El nombre es Obligatorio" });
            Descripcion.Validations.Add(new RequiredRule<string> { ValidationMessage = "La Descripcion es Obligatoria" });
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
                    Nombre = NombrePlato.Value,
                    Valor = ValorPlato.Value,
                    DescripcionPlato = Descripcion.Value
                    //Apellido = ApellidoUsuario.Value
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

        public async Task NavegarFactura()
        {
            await NavigationService.PushPage(new FacturaView(ListaPlatos), ListaPlatos);
        }
        public async Task SelecccionarPlatoAComprar()
        {
            try
            {
                ParametersRequest parametros = new ParametersRequest();
                //var idPlato = "1";
                parametros.Parameters.Add(BusquedaPlato.Value);
                APIResponse response = await GetPlate.EjecutarEstrategia(null, parametros);
                //Console.WriteLine(response.Response);
                if (response.IsSuccess)
                {
                    Plato = JsonConvert.DeserializeObject<PlatoModel>(response.Response);
                    NombrePlato.Value = Plato.Nombre;
                    ValorPlato.Value = Plato.Valor;
                    Descripcion.Value = Plato.DescripcionPlato;
                    isFacturarEnable = true;
                    ListaPlatos.Add(Plato);
                    ((Command)CrearPlatoModelCommand).ChangeCanExecute();
                    //((Command)EliminarPlatoModelCommand).ChangeCanExecute();
                }
                else
                {

                    ((MessageViewModel)PopUp.BindingContext).Message = "No se encuentra ese Plato";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        private void ValidateBusquedaForm()
        {
            IsBuscarEnable = BusquedaPlato.Validate();
            ((Command)SelectPlateCommand).ChangeCanExecute();
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
