 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using MySql.Data.MySqlClient;
using Npgsql;

namespace update_peidos_pronta
{
    class conexionXML
    {


        //DataTable DT = Login.DTconexion;
        DataTable DT = XMLRW.Readxml("CONFIGURACION");
        
        public MySqlConnection mysqlconec = new MySqlConnection();
        public MySqlConnection mysqlconecgt = new MySqlConnection();
        public SqlConnection conex;
        public SqlConnection condm;
        public SqlConnection conmas;
        public SqlConnection conseg;
        public SqlConnection condw;
        public NpgsqlConnection pgcon;
        public NpgsqlConnection pgcongt;
        String Sqlserver;
        String Sqlserverseg;
        String Sqlserverdw;
        String Mysqlserver;
        String UserSQL;
        String UserSQLseg;
        String UserSQLdw;
        String UserMysql;
        String DBEXACTUS;
        String DBSEGURIDAD;
        String DBWEB;
        String DBDM;
        String DBDW;
        String SQLPass;
        String SQLPSEG;
        String SQLPDW;
        String MYsqlPass;
        String PGSQLserver;
        String pgdb;
        String pglogin;
        String pgID;
        String PGSQLservergt;
        String pgdbgt;
        String pglogingt;
        String pgIDgt;



        public void conectar(string Database)
        {
            credeciales();
            try
            {
                string cadex = "data source=" + Sqlserver + ";initial catalog=" + DBEXACTUS + " ;user Id =" + UserSQL + " ; password = " + SQLPass + "";
                conex = new SqlConnection(cadex);

                string cadm = "data source=" + Sqlserver + ";initial catalog=" + DBDM + " ;user Id =" + UserSQL + " ; password = " + SQLPass + "";
                condm = new SqlConnection(cadm);

                string myscad = "Server = " + Mysqlserver + "; Database = " + DBWEB + "; Uid=" + UserMysql + "; Pwd =" + MYsqlPass + ";";
                mysqlconec.ConnectionString = myscad;

                string cadmas = "data source=" + Sqlserver + ";initial catalog=master ;user Id =" + UserSQL + " ; password = " + SQLPass + "";
                conmas = new SqlConnection(cadmas);

                string cadseg = "data source=" + Sqlserverseg + ";initial catalog="+DBSEGURIDAD+" ;user Id =" + UserSQLseg + " ; password = " + SQLPSEG + "";
                conseg = new SqlConnection(cadseg);

                string pgcad = "Server=" + PGSQLserver + ";User Id=" + pglogin + "; " + "Password=" + pgID + ";Database=" + pgdb + ";";
                 pgcon = new NpgsqlConnection(pgcad);

                string myscadgt = "Server=" + PGSQLservergt + ";User Id=" + pglogingt + "; " + "Password=" + pgIDgt + ";Database=" + pgdbgt + ";";
                mysqlconecgt.ConnectionString = myscadgt;
                //string caddw = "data source=" + Sqlserverdw + ";initial catalog=" + DBDW + " ;user Id =" + UserSQLdw + " ; password = " + SQLPDW + "";
                //condw = new SqlConnection(caddw);


                if (Database == "EX")
                {

                    conex.Open();

                }
                else if (Database == "DM")
                {
                    condm.Open();


                }
                else if (Database == "WEB")
                {

                    mysqlconec.Open();
                }

                else if (Database == "MAS")
                {

                    conmas.Open();
                }
                else if (Database == "SEG")
                {

                    conseg.Open();
                }

                else if (Database == "ODOO")
                {

                    pgcon.Open();
                }

                else if (Database == "ODOOGT")
                {

                    mysqlconecgt.Open();
                }
                else if (Database == "DW")
                {

                    condw.Open();
                }
            }
            catch (Exception error_e)
            {
                Console.WriteLine(error_e.ToString(), "Error de Conecxion");
                
                
            
            }
        }
        public void Desconectar(string Database)
        {

            if (Database == "EX")   
            {

                conex.Close();

            }
            else if (Database == "DM")
            {
                condm.Close();


            }
            else if (Database == "WEB")
            {

                mysqlconec.Close();
            }


            else if (Database == "MAS")
            {

                conmas.Close();
            }
            else if (Database == "SEG")
            {

                conseg.Close();
            }
            else if (Database == "ODOO")
            {

                pgcon.Close();
            }
            else if (Database == "ODOOGT")
            {

                mysqlconecgt.Close();
            }
            else if (Database == "DW")
            {

                condw.Close();
            }
        }
              
      

        public void credeciales()
        {

            DataRow row = DT.Rows[0];
            
            Sqlserver = Convert.ToString(row["SERVIDORSQL"]);
            //Sqlserverdw = Convert.ToString(row["SERVIDORSQLCUBE"]);
            PGSQLserver = Convert.ToString(row["SERVIDORPGSQL"]);
            PGSQLservergt = Convert.ToString(row["SERVIDORPGSQLGT"]);
            Sqlserverseg = Convert.ToString(row["SERVIDORSQLSEG"]);
            UserSQL = Convert.ToString(row["LOGIN"]);
            UserSQLseg = Convert.ToString(row["LOGINSG"]);
           // UserSQLdw = Convert.ToString(row["LOGINCUBE"]);
            pglogin = Convert.ToString(row["LOGINPG"]);
            pglogingt = Convert.ToString(row["LOGINPGGT"]);
            DBEXACTUS = Convert.ToString(row["DBSQLEX"]);
           // DBDW = Convert.ToString(row["DBCUBE"]);
            pgdb = Convert.ToString(row["DBPG"]);
            pgdbgt = Convert.ToString(row["DBPGGT"]);
            Mysqlserver = Convert.ToString(row["SERVIDORWEB"]);
            UserMysql = Convert.ToString(row["LOGINWEB"]);
            DBWEB = Convert.ToString(row["DBWEB"]);
           
            DBDM = Convert.ToString(row["DBSQLDM"]);
            DBSEGURIDAD = Convert.ToString(row["DBSQLSG"]);

            
            SQLPass = Encripter.Desencriptar(Convert.ToString(row["KEYID"]));
           // SQLPDW = Encripter.Desencriptar(Convert.ToString(row["KEYCUBE"]));
            MYsqlPass = Encripter.Desencriptar(Convert.ToString(row["KEYIDWEB"]));
            SQLPSEG = Encripter.Desencriptar(Convert.ToString(row["KEYIDSG"]));
            pgID = Encripter.Desencriptar(Convert.ToString(row["KEYIDPG"]));
            pgIDgt = Encripter.Desencriptar(Convert.ToString(row["KEYIDPGGT"]));

        }




    }
}
