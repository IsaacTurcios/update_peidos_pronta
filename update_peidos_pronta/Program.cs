using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace update_peidos_pronta
{
    class Program
    {
        static Clases.proceso pr = new Clases.proceso();
        static void Main(string[] args)
        {

            if (args.Length == 0)
            {
                Console.WriteLine("Esta aplicacion solicita 1 argumento ...\n 1: para actualizar DOC_PRO = 'P' \n 2: para actualizar DOC_PRO = NULL");
                Console.Read();
            }
            else
            {
               
                   
                    pr.update_pedidos(Convert.ToInt32(args[0]));
                   
                    pr.update_recibos(Convert.ToInt32(args[0]));
                   
                    pr.update_pedidos_guate(Convert.ToInt32(args[0]));
                
            }
        }
    }
}
