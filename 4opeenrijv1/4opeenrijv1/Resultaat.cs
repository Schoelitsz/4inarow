using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4opeenrijv1
{
    class Resultaat
    {
        public int resultaatID;
        public int spelerID;
        public int spelerID1;
        public int spelerID2;
        public int resultaatID1;
        public int resultaatID2;

        //public int eindresultaat;
        //public int aantalKeerGewonnen;
        //public int aantalKeerGespeeld;
        //public int aantalKeerGelijkspel;


        public void WinnaarOpslaan(string speler)
        {
            string connectionString;
            connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand myCommand =
                new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '" + speler + "';", connection);
            
            if (myCommand.ExecuteScalar() != null)
            {
                spelerID = (int)myCommand.ExecuteScalar();
            }


            SqlCommand myCommand8 =
                new SqlCommand("SELECT ResultaatID FROM Resultaten WHERE SpelerID = '" + spelerID + "';", connection);
            
            if (myCommand8.ExecuteScalar() != null)
            {
                resultaatID = (int)myCommand8.ExecuteScalar();
                
                SqlCommand myCommand3 =
                    new SqlCommand(
                        "Update Resultaten Set Gewonnenspellen = Gewonnenspellen + 1 Where SpelerID =" + spelerID + ";",
                        connection);
                myCommand3.ExecuteNonQuery();

                SqlCommand myCommand4 =
                    new SqlCommand(
                        "Update Resultaten Set totaalspellen = totaalspellen + 1 Where SpelerID =" + spelerID + ";",
                        connection);
                myCommand4.ExecuteNonQuery();
            }
            else
            {
                SqlCommand myCommand2 =
                                    new SqlCommand(
                                        "INSERT INTO Resultaten (Gewonnenspellen, Totaalspellen, Gelijkspellen, SpelerID) VALUES('1', '1', '', '" +
                                        spelerID + "');", connection);
                myCommand2.ExecuteNonQuery();
            }

            connection.Close();
        }


        public void VerliezerOpslaan(string speler)
        {
            string connectionString;
            connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand myCommand =
                new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '" + speler + "';", connection);
            if (myCommand.ExecuteScalar() != null)
            {
                spelerID = (int)myCommand.ExecuteScalar();
            }

            SqlCommand myCommand8 =
                new SqlCommand("SELECT ResultaatID FROM Resultaten WHERE SpelerID = '" + spelerID + "';", connection);


            if (myCommand8.ExecuteScalar() != null)
            {
                resultaatID = (int)myCommand8.ExecuteScalar();

                SqlCommand myCommand3 =
                    new SqlCommand(
                        "Update Resultaten Set totaalspellen = totaalspellen + 1 Where SpelerID =" + spelerID + ";",
                        connection);
                myCommand3.ExecuteNonQuery();

            }
            else
            {
                SqlCommand myCommand2 = new SqlCommand("INSERT INTO Resultaten (Gewonnenspellen, Totaalspellen, Gelijkspellen, SpelerID) VALUES('', '1', '', '" + spelerID + "');", connection);
                myCommand2.ExecuteNonQuery();
            }


          connection.Close();

        }


        public void GelijkspelOpslaan(string speler1, string speler2)
        {
            string connectionString;
            connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand myCommand =
                new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '" + speler1 + "';", connection);
            if (myCommand.ExecuteScalar() != null)
            {
                spelerID1 = (int)myCommand.ExecuteScalar();
            }

            SqlCommand myCommand2 =
                new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '" + speler2 + "';", connection);
            if (myCommand.ExecuteScalar() != null)
            {
                spelerID2 = (int)myCommand.ExecuteScalar();
            }

            SqlCommand myCommand8 =
                new SqlCommand("SELECT ResultaatID FROM Resultaten WHERE SpelerID = '" + spelerID1 + "';", connection);
            if (myCommand8.ExecuteScalar() != null)
            {
                resultaatID1 = (int)myCommand8.ExecuteScalar();

                SqlCommand myCommand4 =
                    new SqlCommand(
                        "Update Resultaten Set totaalspellen = totaalspellen + 1 Where SpelerID =" + spelerID1 + ";",
                        connection);
                myCommand4.ExecuteNonQuery();
                SqlCommand myCommand5 =
                    new SqlCommand(
                        "Update Resultaten Set gelijkspellen = gelijkspellen + 1 Where SpelerID =" + spelerID1 + ";",
                        connection);
                myCommand5.ExecuteNonQuery();
            }
            else
            {
                SqlCommand myCommand3 = new SqlCommand("INSERT INTO Resultaten (Gewonnenspellen, Totaalspellen, Gelijkspellen, SpelerID) VALUES('', '1', '1', '" + spelerID1 + "');", connection);
                myCommand3.ExecuteNonQuery();
            }

            SqlCommand myCommand9 =
                new SqlCommand("SELECT ResultaatID FROM Resultaten WHERE SpelerID = '" + spelerID2 + "';", connection);
            if (myCommand8.ExecuteScalar() != null)
            {
                resultaatID2 = (int) myCommand8.ExecuteScalar();
                
                SqlCommand myCommand4 =
                    new SqlCommand(
                        "Update Resultaten Set totaalspellen = totaalspellen + 1 Where SpelerID =" + spelerID2 + ";",
                        connection);
                myCommand4.ExecuteNonQuery();
                SqlCommand myCommand5 =
                    new SqlCommand(
                        "Update Resultaten Set gelijkspellen = gelijkspellen + 1 Where SpelerID =" + spelerID2 + ";",
                        connection);
                myCommand5.ExecuteNonQuery();
            }
            else
            {
                SqlCommand myCommand3 = new SqlCommand("INSERT INTO Resultaten (Gewonnenspellen, Totaalspellen, Gelijkspellen, SpelerID) VALUES('', '1', '1', '" + spelerID2 + "');", connection);
                myCommand3.ExecuteNonQuery();
            }

            connection.Close();
        }


        public void GelijkspelOpslaan(string speler1)
        {
            string connectionString;
            connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
            SqlConnection connection = new SqlConnection(connectionString);

            connection.Open();
            SqlCommand myCommand =
                new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '" + speler1 + "';", connection);
            if (myCommand.ExecuteScalar() != null)
            {
                spelerID1 = (int)myCommand.ExecuteScalar();
            }

           SqlCommand myCommand8 =
                new SqlCommand("SELECT ResultaatID FROM Resultaten WHERE SpelerID = '" + spelerID1 + "';", connection);
            if (myCommand8.ExecuteScalar() != null)
            {
                resultaatID1 = (int)myCommand8.ExecuteScalar();

                SqlCommand myCommand4 =
                    new SqlCommand(
                        "Update Resultaten Set totaalspellen = totaalspellen + 1 Where SpelerID =" + spelerID1 + ";",
                        connection);
                myCommand4.ExecuteNonQuery();
                SqlCommand myCommand5 =
                    new SqlCommand(
                        "Update Resultaten Set gelijkspellen = gelijkspellen + 1 Where SpelerID =" + spelerID1 + ";",
                        connection);
                myCommand5.ExecuteNonQuery();
            }
            else
            {
                SqlCommand myCommand3 = new SqlCommand("INSERT INTO Resultaten (Gewonnenspellen, Totaalspellen, Gelijkspellen, SpelerID) VALUES('', '1', '1', '" + spelerID1 + "');", connection);
                myCommand3.ExecuteNonQuery();
            }
            
            

            connection.Close();
        }
    }


}
