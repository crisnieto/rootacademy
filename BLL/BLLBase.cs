using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public abstract class BLLBase
    {
        protected BLLBitacora bitacora;

        public BLLBase()
        {
            bitacora = new BLLBitacora();
        }


        public void verificarPermiso(IIdentity user ,string operacion)
        {
            try
            {
                Sesion.Instancia().verificarPermiso(user ,operacion);
            }
            catch (Exception ex)
            {
                bitacora.crearNuevaBitacora("Error de Permisos", ex.Message, Criticidad.Alta);
                throw ex;
            }

        }

        public void crearNuevaBitacora(string actividad, string descripcion, Criticidad criticidad)
        {
            bitacora.crearNuevaBitacora(actividad, descripcion, criticidad);
        }

    }
}
