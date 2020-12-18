using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;

namespace update_peidos_pronta.Clases
{
    class proceso
    {
        conexionXML con = new conexionXML();

        public void update_pedidos(int tipo_proceso)
        {
            if (tipo_proceso == 1)
            {
                Console.WriteLine("VALIDANDO  NCF ...");
                DataTable dt_ped = new DataTable();

                con.conectar("EX");
                
                SqlCommand get_fac = new SqlCommand("SELECT  [COD_CIA],[NUM_PED],[COD_ZON],[COD_CLT],[TIP_DOC], [FEC_PED],[MON_CIV],[NUM_ITM],NCF_PREFIJO,NCF,COD_PAIS,'FACTURA' as documento FROM EXACTUS.ERPADMIN.alFAC_ENC_PED WHERE DOC_PRO is null and  NCF IS NULL AND CreateDate >= DATEADD(DAY,-15,GETDATE()) AND COD_CIA='DISMO' AND TIP_DOC='F' UNION ALL SELECT  [COD_CIA],[NUM_DEV],[COD_ZON],[COD_CLT],'NC' as TIP_DOC, [FEC_DEV],[MON_SIV],[NUM_ITM],NCF_PREFIJO,NCF,COD_PAIS ,'NOTA_CREDITO' as documento FROM EXACTUS.ERPADMIN.alFAC_ENC_DEV WHERE  DOC_PRO is null and  NCF IS NULL AND CreateDate >= DATEADD(DAY,-15,GETDATE()) AND COD_CIA='DISMO'  ORDER BY  NCF DESC", con.conex);
                SqlDataAdapter da = new SqlDataAdapter(get_fac);
                da.Fill(dt_ped);
                con.Desconectar("EX");


                for (int i = 0; i < dt_ped.Rows.Count; i++)
                {
                    DataRow row_ped = dt_ped.Rows[i];

                    string NUM_PED = row_ped["NUM_PED"].ToString();
                    string COD_ZON = row_ped["COD_ZON"].ToString();
                    string FEC_PED = row_ped["FEC_PED"].ToString();
                    string COD_PAIS = row_ped["COD_PAIS"].ToString();
                    string documento = row_ped["documento"].ToString();

                    string NUEVO_NCF = nuevo_ncf(COD_ZON,COD_PAIS,documento);

                    
                     update_pedido_ncf(NUM_PED, NUEVO_NCF, COD_PAIS, COD_ZON,documento);
                    
                }

                update_pedido_proces("P");
                


            }
            else if (tipo_proceso == 2)
            {
                Console.WriteLine("Actualizando Pedidos PRONTA-ENTREGA Estableciendo DOC_PRO = NULL...");

                update_pedido_proces("N");
            }

        }

        public void update_pedido_ncf(string pedido, string ncf,string tipo_doc ,string ruta,string documento)
        {
            string doc_tipo="";
            Console.WriteLine("Estableciando Nuevo valor al  pedido "+pedido+". Nuevo NCF. "+ncf+".");
            if (documento == "FACTURA")
            {
                if (tipo_doc == "FCF")
                {
                    doc_tipo = "F";
                }
                else if (tipo_doc == "CCF")
                {
                    doc_tipo = "C";
                }
            }
            else if (documento == "NOTA_CREDITO")
            {
                doc_tipo = "N";
            }
            string prefijo = doc_tipo + ruta.Substring(1);

            con.conectar("EX");

            if (documento == "FACTURA")
            {
                SqlCommand cmdupfac = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET NCF_PREFIJO = '" + prefijo + "', NCF = '" + ncf + "' where NUM_PED = '" + pedido + "'", con.conex);
                cmdupfac.ExecuteNonQuery();
            }
            else if (documento == "NOTA_CREDITO")
            {
                SqlCommand cmdupde = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[alFAC_ENC_DEV] SET NCF_PREFIJO = '" + prefijo + "', NCF = '" + ncf + "' where NUM_DEV = '" + pedido + "'", con.conex);
                cmdupde.ExecuteNonQuery();
            }

            SqlCommand cmdu = new SqlCommand("UPDATE [EXACTUS].[ERPADMIN].[NCF_SECUENCIA_RT] SET ULTIMO_VALOR = '" + ncf + "' where PREFIJO = '" + prefijo + "'", con.conex);
            cmdu.ExecuteNonQuery();



            con.Desconectar("EX");

        }

        public string nuevo_ncf( string ruta,string tipo_doc,string documento)
        {
            string ULTIMONCF="";
            string NCF="";
            int nuevoval = 0;
            string consulta = "";

            con.conectar("EX");
            if (documento == "FACTURA")
            {
                consulta = "SELECT TOP(1) NCF FROM [EXACTUS].[ERPADMIN].[alFAC_ENC_PED]   where  COD_ZON = '" + ruta + "' and COD_PAIS = '" + tipo_doc + "' and CreateDate >= DATEADD(DAY,-15,GETDATE())  order by NCF desc";
            }
            else if (documento == "NOTA_CREDITO")
            {
                consulta = "SELECT TOP(1) NCF FROM [EXACTUS].[ERPADMIN].[alFAC_ENC_DEV]   where  COD_ZON = '" + ruta + "' and CreateDate >= DATEADD(DAY,-15,GETDATE())  order by NCF desc";
            }
            
            SqlCommand CMNCF = new SqlCommand(consulta, con.conex);


            CMNCF.CommandTimeout = 0;
            SqlDataReader drcf = CMNCF.ExecuteReader();

            while (drcf.Read())
            {

                ULTIMONCF = Convert.ToString(drcf["NCF"]);
            }

            drcf.Close();
            con.Desconectar("EX");

            nuevoval = Convert.ToInt32(ULTIMONCF);
            nuevoval = nuevoval + 1;

            string nvalor = Convert.ToString(nuevoval);


            int ceros = ULTIMONCF.Length - nvalor.Length;

            switch (ceros)
            {
                case 1:
                    NCF = "0" + nuevoval;
                    break;
                case 2:
                    NCF = "00" + nuevoval;
                    break;
                case 3:
                    NCF = "000" + nuevoval;
                    break;
                case 4:
                    NCF = "0000" + nuevoval;
                    break;
                case 5:
                    NCF = "00000" + nuevoval;
                    break;
                case 6:
                    NCF = "000000" + nuevoval;
                    break;
                case 7:
                    NCF = "0000000" + nuevoval;
                    break;
                case 8:
                    NCF = "00000000" + nuevoval;
                    break;
                case 9:
                    NCF = "000000000" + nuevoval;
                    break;
                case 10:
                    NCF = "0000000000" + nuevoval;
                    break;

                case 11:
                    NCF = "0000000000" + nuevoval;
                    break;
            }


            return NCF;


        }

        public void update_pedido_proces(string stado)
        {
            if (stado == "P")
            {
                con.conectar("EX");

                SqlCommand cmdup = new SqlCommand("UPDATE  [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = 'P'  where  RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA] where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')   and DOC_PRO is null", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");

            }
            else if (stado == "N")
            {

                con.conectar("EX");
                SqlCommand cmdup = new SqlCommand("UPDATE  [EXACTUS].[ERPADMIN].[alFAC_ENC_PED] SET DOC_PRO = NULL  where  RIGHT ([COD_ZON],3) in (SELECT [RUTA] FROM [EXACTUS].[dismo].[RUTA] where RUTA not like 'E%' and DESCRIPCION like '%PRONTA%')   and DOC_PRO = 'P' ", con.conex);
                cmdup.ExecuteNonQuery();
                con.Desconectar("EX");

            }
            else if (stado == "E")
            {
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
