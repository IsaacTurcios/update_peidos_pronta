using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Data;

 
namespace update_peidos_pronta
{
    class XMLRW
    {

       
     
      
      public  static  DataTable Readxml(string tablename)
        {
            string path = "C:/CORRECT/CORRECTX.xml";
            //string DBEXname,string DBDMname,string DBWebname,string SQluser,string WebUser,string SQLpass,string Webpass

            XmlDocument XMLconetconf = new XmlDocument();
            XMLconetconf.Load(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);
            
            DataTable dtxml = new DataTable();

            if (tablename != String.Empty)
            {
                dtxml = ds.Tables[tablename].Copy();
            }
            return dtxml;
         
         
        
        }
        public static void write(String campo, String SQLserver,String Web,String SQLserversg, String SQLserverpg, String SQLserverpggt, String DMdatabase,String DBExactus,String DBSeguridad, String DBPGSQL, String DBPGSQLGT, String DBWeb, String SQLogin,String SQLoginseg, String Myqlogin, String pglogin, String pglogingt, String SqlID ,String SqlIDseg, String MsqlID, String pgID, String pgIDgt, String HOST ,String USER, String SFTPKEY, String PUERTO,String AUTOMATICO)
    {



            //try
            //{
            if (campo == "CONFIGURACION")
            {
                string path = "C:/CORRECT/CORRECTX.xml";
                XmlDocument DISMOXML = new XmlDocument();
                DISMOXML.Load(path);


                DISMOXML.SelectSingleNode("//CONFIGURACION/SERVIDORSQL").InnerText = SQLserver;
                DISMOXML.SelectSingleNode("//CONFIGURACION/SERVIDORWEB").InnerText = Web;
                DISMOXML.SelectSingleNode("//CONFIGURACION/SERVIDORSQLSEG").InnerText = SQLserversg;
                DISMOXML.SelectSingleNode("//CONFIGURACION/SERVIDORPGSQL").InnerText = SQLserverpg;
                DISMOXML.SelectSingleNode("//CONFIGURACION/SERVIDORPGSQLGT").InnerText = SQLserverpggt;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBSQLDM").InnerText = DMdatabase;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBSQLEX").InnerText = DBExactus;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBSQLSG").InnerText = DBSeguridad;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBWEB").InnerText = DBWeb;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBPG").InnerText = DBPGSQL;
                DISMOXML.SelectSingleNode("//CONFIGURACION/DBPGGT").InnerText = DBPGSQLGT;
                DISMOXML.SelectSingleNode("//CONFIGURACION/LOGIN").InnerText = SQLogin;
                DISMOXML.SelectSingleNode("//CONFIGURACION/LOGINSG").InnerText = SQLoginseg;
                DISMOXML.SelectSingleNode("//CONFIGURACION/LOGINWEB").InnerText = Myqlogin;
                DISMOXML.SelectSingleNode("//CONFIGURACION/LOGINPG").InnerText = pglogin;
                DISMOXML.SelectSingleNode("//CONFIGURACION/LOGINPGGT").InnerText = pglogingt;
                DISMOXML.SelectSingleNode("//CONFIGURACION/KEYID").InnerText = SqlID;
                DISMOXML.SelectSingleNode("//CONFIGURACION/KEYIDSG").InnerText = SqlIDseg;
                DISMOXML.SelectSingleNode("//CONFIGURACION/KEYIDWEB").InnerText = MsqlID;
                DISMOXML.SelectSingleNode("//CONFIGURACION/KEYIDPG").InnerText = pgID;
                DISMOXML.SelectSingleNode("//CONFIGURACION/KEYIDPGGT").InnerText = pgIDgt;

                DISMOXML.Save(path);
            }
            else

            {
                string path = "C:/CORRECT/CORRECTX.xml";
                XmlDocument DISMOXML = new XmlDocument();
                DISMOXML.Load(path);


                DISMOXML.SelectSingleNode("//SFTP/HOST").InnerText = HOST;
                DISMOXML.SelectSingleNode("//SFTP/USER").InnerText = USER;
                DISMOXML.SelectSingleNode("//SFTP/SFTPKEY").InnerText = SFTPKEY;
                DISMOXML.SelectSingleNode("//SFTP/PUERTO").InnerText = PUERTO;
                DISMOXML.SelectSingleNode("//SFTP/AUTOMATICO").InnerText = AUTOMATICO;
                

                DISMOXML.Save(path);

            }

        }

        public static DataTable Read_acceso(string tablename)
        {
            string path = "C:/CORRECT/CORRECTLOGIN.xml";
            //string DBEXname,string DBDMname,string DBWebname,string SQluser,string WebUser,string SQLpass,string Webpass

            XmlDocument XMLconetconf = new XmlDocument();
            XMLconetconf.Load(path);
            DataSet ds = new DataSet();
            ds.ReadXml(path);

            DataTable dtxml = new DataTable();

            if (tablename != String.Empty)
            {
                dtxml = ds.Tables[tablename].Copy();
            }
            return dtxml;



        }
        public static void writeLogin(String usuario, String vercion, String empresa, String tipo_usuario)
        {

            string path = "C:/CORRECT/CORRECTLOGIN.xml";
            XmlDocument DISMOXML = new XmlDocument();
            DISMOXML.Load(path);


            DISMOXML.SelectSingleNode("//LOGIN/USUARIO").InnerText = usuario;
            DISMOXML.SelectSingleNode("//LOGIN/VERCION").InnerText = vercion;
            DISMOXML.SelectSingleNode("//LOGIN/EMPRESA").InnerText = empresa;
            DISMOXML.SelectSingleNode("//LOGIN/TIPO_USUARIO").InnerText = tipo_usuario;

            DISMOXML.Save(path);
        }

        //catch (Exception ex)
        //{
        //    MessageBox.Show(ex.ToString(), "Error de Escritura XML");

        //}


        //}
    }
}
