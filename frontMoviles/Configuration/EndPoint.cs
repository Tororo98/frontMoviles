using System;
using System.Collections.Generic;
using System.Text;

namespace frontMoviles.Configuration
{
    public class EndPoint
    {
        //Here we put the enpoints created on NodeJS
        public static readonly string URL_SERVIDOR = "localhost:5020";
        public static readonly string CONSULTAR_ALL_USERS = "/users/getUser";
        public static readonly string CONSULTAR_ALL_BILLS = "bills/getBill";
        public static readonly string CREAR_PLATO = "/create";
        public static readonly string CREAR_USER = "/users/create";
        //public static readonly string EDITAR_CATEGORIA = "/update";
        //public static readonly string ELIMINAR_CATEGORIA = "/delete";
    }
}
