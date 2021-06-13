using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace APISoP.CrossCutting.Types
{
    public class MessagesUsers
    {
        public const string Created = "Usuario creado correctamente";
        public const string Updated = "Datos del usuario actualziados";
        public const string FailedValidate = "Error en la validación de datos del usuario";
        public const string Deleted = "El usuario se ha eliminado";
        public const string Obtain = "Datos del usuario: ";
        public const string ObtainAll = "Usuarios registrados";
    }

    public class MessagesStores
    {
        public const string Created = "Nueva sucursal registrada";
        public const string Updated = "Datos de sucursal actualziados";
        public const string FailedValidate = "Error en la validación de datos";
        public const string Deleted = "La sucursal se ha eliminado";
        public const string Obtain = "Datos de la sucursal: ";
        public const string ObtainAll = "Sucursales registradas";
    }

    public class MessagesProfiles
    {
        public const string Created = "Nuevo perfil creado";
        public const string Updated = "Datos del perfil actualziados";
        public const string FailedValidate = "Error en la validación de datos";
        public const string Deleted = "El perfil se ha eliminado";
        public const string Obtain = "Datos del perfil: ";
        public const string ObtainAll = "Perfiles registrados";
    }
     

}
