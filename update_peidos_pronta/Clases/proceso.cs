using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace update_peidos_pronta.Clases
{
    class proceso
    {
        conexionXML con = new conexionXML();

        public void update_pedidos(int tipo_proceso)
        {
            if (tipo_proceso == 1)
            {
                Console.WriteLine("Actualizando Pedidos PRONTA-ENTREGA Estableciendo DOC_PRO = P ...");
                con.conectar("EX");

                SqlCommand cmdup = new SqlCommand("UPDATE  [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = 'P'  where  RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA] where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')   and DOC_PRO is null", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");
            }
            else if (tipo_proceso == 2)
            {
                Console.WriteLine("Actualizando Pedidos PRONTA-ENTREGA Estableciendo DOC_PRO = NULL...");
                con.conectar("EX");

                SqlCommand cmdup = new SqlCommand("UPDATE  [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = NULL  where  RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA] where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')   and DOC_PRO = 'P' ", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");

            }

        }

        public void update_recibos(int tipo_proceso)
        {
            if (tipo_proceso == 1)
            {
                Console.WriteLine("Actualizando Recibos PRONTA-ENTREGA Estableciendo DOC_PRO = P ...");
                con.conectar("EX");
                SqlCommand cmdup = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alCXC_DOC_APL] SET DOC_PRO = 'P' where RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA]  where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')  and DOC_PRO is null", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");
            }
            else if (tipo_proceso == 2)
            {
                Console.WriteLine("Actualizando Recibos PRONTA-ENTREGA Estableciendo DOC_PRO = NULL ...");
                con.conectar("EX");
                SqlCommand cmdup = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alCXC_DOC_APL] SET DOC_PRO = NULL  where RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA]  where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')  and DOC_PRO = 'P'", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");

            }
        }


        public void update_pedidos_guate(int tipo_proceso)
        {
            if (tipo_proceso == 1)
            {
                Console.WriteLine("Actualizando Pedidos GUATEMALA Estableciendo DOC_PRO = P ...");
                con.conectar("EX");

                SqlCommand cmdup = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = 'P' where  COD_CIA = 'DISMOGT' and DOC_PRO is null", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");
            }
            else if (tipo_proceso == 2)
            {
                Console.WriteLine("Actualizando Pedidos GUATEMALA Estableciendo DOC_PRO = NULL...");
                con.conectar("EX");

                SqlCommand cmdup = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = NULL where  COD_CIA = 'DISMOGT' and DOC_PRO = 'P'", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");

            }

        }

    }
}
