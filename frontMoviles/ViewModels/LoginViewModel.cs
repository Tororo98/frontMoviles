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
    public class LoginViewModel : ViewModelBase
    {
        public ValidatableObject<string> CorreoUsuario { get; set; }  //Campo de Busqueda
        public ValidatableObject<string> PasswordUsuario { get; set; }

        public MessageViewPop PopUp { get; set; }

        private User usuario;

        public ChooseRequest<User> GetUser { get; set; }

        public ICommand LoginCommand { get; set; }

        public ICommand RegistrarCommand { get; set; }

        public User Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged();
            }
        }

        public LoginViewModel()
        {
            PopUp = new MessageViewPop();
            Usuario = new User();
            InitializeRequest();
            InitializeCommands();
            InitializeFields();
        }

        public void InitializeRequest()
        {
            string urlBuscarUsuario = EndPoint.URL_SERVIDOR + EndPoint.CONSULTAR_USER;

            GetUser = new ChooseRequest<User>();
            GetUser.ElegirEstrategia("POST", urlBuscarUsuario);


        }

        public void InitializeCommands()
        {
            LoginCommand = new Command(async () => await ConsultarUsuario(), () => true);
            RegistrarCommand = new Command(async () => await IrARegistro(), () => true);

        }

        public void InitializeFields()
        {
            CorreoUsuario = new ValidatableObject<string>();
            PasswordUsuario = new ValidatableObject<string>();

            CorreoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El correo es Obligatorio" });
            PasswordUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "La contraseña es Obligatoria" });
        }

        public async Task ConsultarUsuario()
        {
            try
            {
                User usuario = new User()
                {
                    Correo = CorreoUsuario.Value,
                    Password = PasswordUsuario.Value

                };
                APIResponse response = await GetUser.EjecutarEstrategia(usuario);
                if (response.IsSuccess)
                {
                    await NavigationService.PushPage(new PlatoView());
                }
                else
                {
                    ((MessageViewModel)PopUp.BindingContext).Message = "Error al acceder";
                    await PopupNavigation.Instance.PushAsync(PopUp);
                }
            }
            catch (Exception e)
            {

            }
        }
        public async Task IrARegistro()
        {
            await NavigationService.PushPage(new RegistroView());
        }


    }
}
