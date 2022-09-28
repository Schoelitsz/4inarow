using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace _4opeenrijv1
{
    
    class Speler
    {
        public int spelerID;
        public string naam;
        

        //max 50 tekens
        public string Maakspeler()
        {
            Speler speler0 = new Speler();

            while (speler0.naam == null)
            {
                Console.WriteLine("Vul naam in:");
                string naam = Console.ReadLine();
                Console.WriteLine(naam + ", Is dit correct?Y/N");
                string userantwoord = Console.ReadLine();
                if (userantwoord.ToUpper() == "Y")
                {
                    string connectionString;
                    connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
                    SqlConnection connection = new SqlConnection(connectionString);

                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("INSERT INTO Spelers VALUES('" + naam + "');", connection);
                    myCommand.ExecuteNonQuery();

                    connection.Close();
                    return naam;
                }
                else if (userantwoord.ToUpper() == "N") 
                {
                    speler0.naam = null;
                }
                else
                {
                    Console.WriteLine("No valid input");
                    speler0.naam = null;
                }

            }

            return naam;
        }


        public string Kiesspeler()
        {
            Speler speler0 = new Speler();

            while (speler0.naam == null)
            {
                Console.WriteLine("Vul naam in:");
                string naam = Console.ReadLine();
                Console.WriteLine(naam + ", Is dit correct?Y/N");
                string userantwoord = Console.ReadLine();
                if (userantwoord.ToUpper() == "Y")
                {
                    string connectionString;
                    connectionString = "Server=LAPTOP-LT05RHTU;Database=4opeenrij;Trusted_Connection=true";
                    SqlConnection connection = new SqlConnection(connectionString);

                    connection.Open();
                    SqlCommand myCommand = new SqlCommand("SELECT SpelerID FROM Spelers WHERE Naam = '"+ naam +"';", connection);
                    
                    if (myCommand.ExecuteScalar() != null)
                    {
                        spelerID = (int)myCommand.ExecuteScalar();
                        return naam;
                    }
                    else
                    {
                        Console.WriteLine("No valid input");
                        speler0.naam = null;
                    }
                    
                    connection.Close();

                    
                }
                else if (userantwoord.ToUpper() == "N")
                {
                    speler0.naam = null;
                }
                else
                {
                    Console.WriteLine("No valid input");
                    speler0.naam = null;
                }

            }

            return naam;
        }



    }
}

