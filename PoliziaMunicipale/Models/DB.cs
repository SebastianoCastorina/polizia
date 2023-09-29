using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Description;

namespace PoliziaMunicipale.Models
{
    public static class DB
    {
        public static void AggiungiTrasgressore(string surname, string name, string address, string city, int cap, string cf)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [ANAGRAFICA] (COGNOME, NOME, INDIRIZZO, CITTA, CAP, CF) VALUES(@surname, @name, @address, @city, @cap,@cf)";
                cmd.Parameters.AddWithValue("surname", surname);
                cmd.Parameters.AddWithValue("name", name);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("city", city);
                cmd.Parameters.AddWithValue("cap", cap);
                cmd.Parameters.AddWithValue("cf", cf);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch { }
            finally
            {
                conn.Close();
            }
        }

        public static void AggiungiViolazione(string description)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [TIPO_VIOLAZIONE] (DESCRIZIONE) VALUES(@description)";
                cmd.Parameters.AddWithValue("description", description);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch { }
            finally
            {
                conn.Close();
            }
        }

        public static List<Trasgressore> getAllTrasgressori()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from ANAGRAFICA", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<Trasgressore> trasgressori = new List<Trasgressore>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Trasgressore t = new Trasgressore();
                t.Id = Convert.ToInt32(sqlDataReader["IDANAGRAFICA"]);
                t.Surname = sqlDataReader["COGNOME"].ToString();
                t.Name = sqlDataReader["NOME"].ToString();
                t.Address = sqlDataReader["INDIRIZZO"].ToString();
                t.City = sqlDataReader["CITTA"].ToString();
                t.CAP = Convert.ToInt32(sqlDataReader["CAP"]);
                t.CF = sqlDataReader["CF"].ToString();
                trasgressori.Add(t);
            }

            conn.Close();
            return trasgressori;
        }

        public static List<Violazione> getAllViolazioni()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("select * from TIPO_VIOLAZIONE", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<Violazione> violazioni = new List<Violazione>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                Violazione v = new Violazione();
                v.Id = Convert.ToInt32(sqlDataReader["IDVIOLAZIONE"]);
                v.Description = sqlDataReader["DESCRZIONE"].ToString();
                violazioni.Add(v);
            }

            conn.Close();
            return violazioni;
        }

        public static void AggiungiVerbale(DateTime dataViolazione,string address,string agent,DateTime dataVerbale,double amount,int points,int idT,int idV)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ConnectionString.ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = conn;
                cmd.CommandText = "INSERT INTO [VERBALE] (DATA_VIOLAZIONE,INDIRIZZO_VIOLAZIONE,NOMINATIVO_AGENTE,DATA_TRASCRIZIONE_VERBALE,IMPORTO,DECURTAMENTO_PUNTI,IDANAGRAFICA,IDVIOLAZIONE) " +
                    "VALUES(@dataViolazione,@address,@agent,@dataVerbale,@amount,@points,@idT,@idV)";
                cmd.Parameters.AddWithValue("dataViolazione", dataViolazione);
                cmd.Parameters.AddWithValue("address", address);
                cmd.Parameters.AddWithValue("agent", agent);
                cmd.Parameters.AddWithValue("dataVerbale", dataVerbale);
                cmd.Parameters.AddWithValue("amount", amount);
                cmd.Parameters.AddWithValue("points", points);
                cmd.Parameters.AddWithValue("idT", idT);
                cmd.Parameters.AddWithValue("idV", idV);
                int IsOk = cmd.ExecuteNonQuery();

            }

            catch { }
            finally
            {
                conn.Close();
            }
        }

        public static List<VerbaliByT> getCountVerbaliByTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT ANAGRAFICA.IDANAGRAFICA,COGNOME,NOME, COUNT(*) AS TotVerbali FROM VERBALE INNER JOIN ANAGRAFICA " +
                "ON ANAGRAFICA.IDANAGRAFICA = VERBALE.IDANAGRAFICA GROUP BY ANAGRAFICA.IDANAGRAFICA,COGNOME,NOME", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<VerbaliByT> verbaliByT = new List<VerbaliByT>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                VerbaliByT v = new VerbaliByT();
                v.IdT = Convert.ToInt32(sqlDataReader["IDANAGRAFICA"]);
                v.Surname = sqlDataReader["COGNOME"].ToString();
                v.Name = sqlDataReader["NOME"].ToString();
                v.TotVerbali = Convert.ToInt32(sqlDataReader["TotVerbali"]);
                verbaliByT.Add(v);
            }

            conn.Close();
            return verbaliByT;
        }

        public static List<PuntiByT> getPuntiByTrasgressore()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT ANAGRAFICA.IDANAGRAFICA,COGNOME,NOME, SUM(DECURTAMENTO_PUNTI) AS TotPuntiPersi FROM VERBALE " +
                                            "INNER JOIN ANAGRAFICA ON ANAGRAFICA.IDANAGRAFICA = VERBALE.IDANAGRAFICA GROUP BY ANAGRAFICA.IDANAGRAFICA, COGNOME, NOME", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<PuntiByT> puntiByT = new List<PuntiByT>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                PuntiByT p = new PuntiByT();
                p.IdT = Convert.ToInt32(sqlDataReader["IDANAGRAFICA"]);
                p.Surname = sqlDataReader["COGNOME"].ToString();
                p.Name = sqlDataReader["NOME"].ToString();
                p.TotPuntiPersi = Convert.ToInt32(sqlDataReader["TotPuntiPersi"]);
                puntiByT.Add(p);
            }

            conn.Close();
            return puntiByT;
        }

        public static List<AnMag10Punti> getTrasgressoriMag10Punti()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT COGNOME, NOME, DATA_VIOLAZIONE, IMPORTO, DECURTAMENTO_PUNTI FROM VERBALE INNER JOIN ANAGRAFICA "+
                                            "ON ANAGRAFICA.IDANAGRAFICA = VERBALE.IDANAGRAFICA WHERE DECURTAMENTO_PUNTI > 10", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<AnMag10Punti> anMag10 = new List<AnMag10Punti>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                AnMag10Punti a = new AnMag10Punti();
                a.Surname = sqlDataReader["COGNOME"].ToString();
                a.Name = sqlDataReader["NOME"].ToString();
                a.DataViolazione = Convert.ToDateTime(sqlDataReader["DATA_VIOLAZIONE"]);
                a.Amount = Convert.ToDouble(sqlDataReader["IMPORTO"]);
                a.PuntiPersi = Convert.ToInt32(sqlDataReader["DECURTAMENTO_PUNTI"]);
                anMag10.Add(a);
            }

            conn.Close();
            return anMag10;
        }

        public static List<ImportoMag400> getImportoMag400()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["ConnectionStringDB"].ToString();
            SqlConnection conn = new SqlConnection(connectionString);

            SqlCommand cmd = new SqlCommand("SELECT COGNOME, NOME , DATA_VIOLAZIONE, IMPORTO, DECURTAMENTO_PUNTI FROM VERBALE INNER JOIN ANAGRAFICA "+
                                            "ON ANAGRAFICA.IDANAGRAFICA = VERBALE.IDANAGRAFICA WHERE IMPORTO > 400", conn);
            SqlDataReader sqlDataReader;

            conn.Open();

            List<ImportoMag400> amountMag400 = new List<ImportoMag400>();
            sqlDataReader = cmd.ExecuteReader();
            while (sqlDataReader.Read())
            {
                ImportoMag400 a = new ImportoMag400();
                a.Surname = sqlDataReader["COGNOME"].ToString();
                a.Name = sqlDataReader["NOME"].ToString();
                a.DataViolazione = Convert.ToDateTime(sqlDataReader["DATA_VIOLAZIONE"]);
                a.Amount = Convert.ToDouble(sqlDataReader["IMPORTO"]);
                a.Points = Convert.ToInt32(sqlDataReader["DECURTAMENTO_PUNTI"]);
                amountMag400.Add(a);
            }

            conn.Close();
            return amountMag400;
        }
    }
}