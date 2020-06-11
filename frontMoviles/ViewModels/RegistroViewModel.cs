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
       

        public ValidatableObject<string> NombreUsuario { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> ApellidoUsuario { get; set; }
        public ValidatableObject<string> CorreoUsuario { get; set; }
        public ValidatableObject<string> Password { get; set; }

        private User usuario;

        private bool isGuardarEnable;

        public MessageViewPop PopUp { get; set; }

        #region Requests
        public ChooseRequest<User> CreateUser { get; set; }

        #endregion Requests

        #region Commands
        public ICommand CrearUserCommand { get; set; }
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

        #endregion Getters & Setters

        #region Initialize

        public RegistroViewModel()
        {
            PopUp = new MessageViewPop();
            Usuario = new User();
            IsGuardarEnable = true;
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
        }

        public void InitializeFields()
        {
            NombreUsuario = new ValidatableObject<string>();
            ApellidoUsuario = new ValidatableObject<string>();
            CorreoUsuario = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();

            NombreUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Nombre es Obligatorio" });
            ApellidoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Apellido es Obligatorio" });
            CorreoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Correo es Obligatorio" });
            Password.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Password es Obligatorio" });
        }

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
                    Nombre = NombreUsuario.Value,
                    Apellido = ApellidoUsuario.Value,
                    //Nombre = "PruebaNombre",
                    //Apellido = "Last",
                    Correo = CorreoUsuario.Value,
                    Password = Password.Value,
                    IdRol = 1

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
        
    }
}
