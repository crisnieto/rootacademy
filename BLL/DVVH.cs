using BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class DVVH
    {
        string[] listaTablasIntegridad =
         {
               "Usuario",
               "Persona",
            };

        /// <summary>
        /// verificarIntegridad se encarga de realizar la validacion de integridad tanto de digitos verificador vertical como horizontal.
        /// Primero se valida los digitos verificadores verticales para las tablas registradas
        /// Luego se valida los digitos verificadores horizontales para cada registro de las tablas interesadas.
        /// En caso de que exista un error de integridad, se debera lanzar una Exception.
        /// </summary>
        /// <returns></returns>
        public bool verificarIntegridad()
        {
            //Primero debo obtener todas las tablas que tienen DVH y DVV
            DAL.DVVH dalDVVH = new DAL.DVVH();
            foreach (string tabla in listaTablasIntegridad)
            {
                List<int> listaDeDVH = dalDVVH.obtenerListaDVHdeTabla(tabla);
                int dvvObtenido = dalDVVH.conseguirDVV(tabla);
                compararCalculadoConObtenido(calcularDVV(listaDeDVH), dvvObtenido);
            }
            BLLUsuario bllUsuario = new BLLUsuario();
            foreach (Usuario usuario in bllUsuario.conseguirUsuariosValidacion())
            {
                int calculado = bllUsuario.calcularDVH(usuario);
                if (calculado != usuario.Dvh)
                {
                    Console.WriteLine(usuario.Username);
                    new BLLBitacora().crearNuevaBitacora("Calculo de DVVH", "Se detecto un error de calculo de DVH para la entidad Usuario con ID: " + usuario.Id, Criticidad.Alta);
                    lanzarErrorDeVerificacion();
                }
            }
            BLLPersona bllPersona = new BLLPersona();
            foreach (Persona persona in bllPersona.conseguirTodos())
            {
                int calculado = bllPersona.calcularDVH(persona);
                if (calculado != persona.Dvh)
                {
                    Console.WriteLine(persona.Nombre);
                    new BLLBitacora().crearNuevaBitacora("Calculo de DVVH", "Se detecto un error de calculo de DVH para la entidad Persona con ID: " + persona.Id, Criticidad.Alta);
                    lanzarErrorDeVerificacion();
                }
            }

            return true;
        }

        /// <summary>
        /// Se encarga de sumar la lista de cada DVH obtenido para una tabla y devolver dicha sumatoria.
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public int calcularDVV(List<int> i)
        {
            //Aca sumo cada uno de los DVH obtenidos para obtener el DVV calculado.
            int dvvCalculado = 0;
            foreach (int dvh in i)
            {
                dvvCalculado += dvh;
            }
            return dvvCalculado;
        }

        /// <summary>
        /// actualizarDVV se encarga de actualizar la tabla DVV ante cambios
        /// </summary>
        /// <param name="tabla"></param>
        /// <returns></returns>
        public int actualizarDVV(string tabla)
        {
            DAL.DVVH dalDVVH = new DAL.DVVH();
            return dalDVVH.actualizarDVV(tabla);
        }

        /// <summary>
        /// compararCalculadoConObtenido se encarga simplemente de validar que el parametro calculado sea igual al obtenido, y en caso contrario lanzar error.
        /// </summary>
        /// <param name="calculado"></param>
        /// <param name="obtenido"></param>
        public void compararCalculadoConObtenido(int calculado, int obtenido)
        {

            if (calculado == obtenido)
            {
                Console.WriteLine("Calculo de integridad correcto!!!");
            }
            else
            {
                lanzarErrorDeVerificacion();
            }
        }


        /// <summary>
        /// lanzarErrorDeVerificacion es responsable de hacer un throw de una excepcion y es llamado cuando existe un error de integridad
        /// </summary>
        public void lanzarErrorDeVerificacion()
        {
            throw new Exception(RootAcademyMessage.formatearMensaje("Existe un error de integridad. Por favor verificar la base de datos."));
        }
    }
}
