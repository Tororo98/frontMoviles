using frontMoviles.Configuration;
using frontMoviles.Models;
using frontMoviles.Models.AuxiliarModels;
using frontMoviles.Services.ApiRest;
using frontMoviles.Validations.Base;
using frontMoviles.Validations.Rules;
using frontMoviles.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace frontMoviles.ViewModels
{
    public class RegistroViewModel : ViewModelBase
    {
        #region Properties
        private User usuario;

        public ValidatableObject<string> NombreUsuario { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> ApellidoUsuario { get; set; }

        private bool isGuardarEnable;

        private bool isGuardarEditar;

        public MessageViewPop PopUp { get; set; }

        #region Requests
        public ChooseRequest<User> CreateUser { get; set; }

        #endregion Requests

        #region Commands
        public ICommand CrearUserCommand { get; set; }
        public ICommand ValidateNombreUsuarioCommand { get; set; }
        public ICommand ValidateApellidoUsuarioCommand { get; set; }
        #endregion Commands

        #endregion Properties

        #region Getters & Setters

        public User Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
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
        public bool IsGuardarEditar
        {
            get { return isGuardarEditar; }
            set
            {
                isGuardarEditar = value;
                OnPropertyChanged();
            }
        }

        #endregion Getters & Setters

        #region Initialize

        public RegistroViewModel()
        {
            PopUp = new MessageViewPop();
            Usuario = new User();
            IsGuardarEnable = true;
            IsGuardarEditar = true;
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }
        #endregion Initialize

        public void InitializeRequest()
        {
            string urlCrearUsuario = EndPoint.URL_SERVIDOR + EndPoint.CREAR_USER;

            CreateUser = new ChooseRequest<User>();
            CreateUser.ElegirEstrategia("POST", urlCrearUsuario);

        }
        public void InitializeCommands()
        {
            CrearUserCommand = new Command(async () => await GuardarUser(), () => IsGuardarEnable);
            ValidateNombreUsuarioCommand = new Command(() => ValidateNombreUsuarioForm(), () => true);
            ValidateApellidoUsuarioCommand = new Command(() => ValidateApellidoUsuarioForm(), () => true);
        }

        public void InitializeFields()
        {
            NombreUsuario = new ValidatableObject<string>();
            ApellidoUsuario = new ValidatableObject<string>();

            NombreUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Id es Obligatorio" });
            ApellidoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El nombre de la categoria es Obligatorio" });
        }

        #region Methods
        private async Task GuardarUser()
        {
            await CrearUsuario();
            IsGuardarEnable = false;
            ((Command)CrearUserCommand).ChangeCanExecute();
  
        }

        public async Task CrearUsuario()
        {
            try
            {
                User usuario = new User()
                {
                    //Nombre = NombreUsuario.Value,
                    //Apellido = ApellidoUsuario.Value
                    Nombre = "PruebaNombre",
                    Apellido = "Last",
                    Correo = "Prueba@gmail.com",
                    Password = "123445",

                };
                APIResponse response = await CreateUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Usuario creado exitosamente";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Error al crear el Usuario";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {

            }
        }
        private void ValidateNombreUsuarioForm()
        {
            IsGuardarEnable = NombreUsuario.Validate();
            ((Command)CrearUserCommand).ChangeCanExecute();
        }

        private void ValidateApellidoUsuarioForm()
        {
            isGuardarEnable = ApellidoUsuario.Validate();
            ((Command)CrearUserCommand).ChangeCanExecute();
        }
        #endregion Methods
    }
}
