using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Capa_de_negocios_ASELEC
{
    public class Validacion_Identificacion
    {
        public static bool VerificaIdentificacion(string identificacion)
        {
            bool estado = false;
            char[] valced = new char[13];
            int provincia;
            if (identificacion.Length >= 10)
            {
                valced = identificacion.Trim().ToCharArray();
                provincia = int.Parse((valced[0].ToString() + valced[1].ToString()));
                if (provincia > 0 && provincia < 25)
                {
                    if (int.Parse(valced[2].ToString()) < 6)
                    {
                        estado = VerificaCedula(valced);
                    }
                }
            }
            return estado;
        }

        public static bool VerificaCedula(char[] validarCedula)
        {
            int aux = 0, par = 0, impar = 0, verifi;
            for (int i = 0; i < 9; i += 2)
            {
                aux = 2 * int.Parse(validarCedula[i].ToString());
                if (aux > 9)
                    aux -= 9;
                par += aux;
            }
            for (int i = 1; i < 9; i += 2)
            {
                impar += int.Parse(validarCedula[i].ToString());
            }

            aux = par + impar;
            if (aux % 10 != 0)
            {
                verifi = 10 - (aux % 10);
            }
            else
                verifi = 0;
            if (verifi == int.Parse(validarCedula[9].ToString()))
                return true;
            else
                return false;
        }

       
    }
}