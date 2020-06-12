﻿using frontMoviles.Configuration;
using frontMoviles.Models;
using frontMoviles.Models.AuxiliarModels;
using frontMoviles.Services.ApiRest;
using frontMoviles.Validations.Base;
using frontMoviles.Validations.Rules;
using frontMoviles.Views;
using Rg.Plugins.Popup.Services;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public ValidatableObject<string> Key { get; set; }



        private User usuario;

        private bool isGuardarEnable;

        public MessageViewPop PopUp { get; set; }

        //public List<Rol> RolList { get; set; }
        

        #region Requests
        public ChooseRequest<User> CreateUser { get; set; }

        #endregion Requests

        #region Commands
        public ICommand CrearUserCommand { get; set; }

        public ICommand VolverCommand { get; set; }
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

        //public class Rol
        //{
        //    public int Key { get; set; }
        //    public string Value { get; set; }
        //}

        //public List<Rol> GetRols()
        //{
        //    var roles = new List<Rol>()
        //    {
        //        new Rol(){Key = 1, Value= "Administrador"},
        //        new Rol(){Key = 2, Value= "Empleado"},
        //        new Rol(){Key = 3, Value= "Ceo"}
        //    };

        //    return roles;
        //}

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
            //RolList = GetRols().OrderBy(t => t.Key).ToList();
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
            VolverCommand = new Command(async () => await Volver(), () => true);
        }

        public void InitializeFields()
        {
            NombreUsuario = new ValidatableObject<string>();
            ApellidoUsuario = new ValidatableObject<string>();
            CorreoUsuario = new ValidatableObject<string>();
            Password = new ValidatableObject<string>();
            Key = new ValidatableObject<string>();

            NombreUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Nombre es Obligatorio" });
            ApellidoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Apellido es Obligatorio" });
            CorreoUsuario.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Correo es Obligatorio" });
            Password.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Password es Obligatorio" });
            Key.Validations.Add(new RequiredRule<string> { ValidationMessage = "El Rol es Obligatorio" });
        }

        private async Task GuardarUser()
        {
            await CrearUsuario();
            IsGuardarEnable = false;
            ((Command)CrearUserCommand).ChangeCanExecute();
  
        }

        public async Task Volver()
        {
            await NavigationService.PopPage();
        }
        public async Task CrearUsuario()
        {
            var rol = 0;
            if (Key.Value == "Administrador")
            {
                rol = 1;
            }
            else if (Key.Value == "Empleado")
            {
                rol = 2;
            }
            else if (Key.Value == "Ceo")
            {
                rol = 3;
            }
            Console.WriteLine(rol);
            try
            {
                User usuario = new User()
                {
                    Nombre = NombreUsuario.Value,
                    Apellido = ApellidoUsuario.Value,
                    Correo = CorreoUsuario.Value,
                    Password = "123445",
                    //Password = Password.Value,                
                    IdRol = rol,


                    //Nombre = "PruebaNombre",
                    //Apellido = "Last",
                    //Correo = "Prueba@gmail.com",
                    //Password = "123445",
                    //IdRol = 1
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
