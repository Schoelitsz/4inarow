using System;


namespace _4opeenrijv1
{
    class Spel
    {
        static Speler speler = new Speler();
        static Computer computer = new Computer();
        //static Bord bord = new Bord();
        static Resultaat resultaat = new Resultaat();
        
        public static string speler1 = "null";
        public static string speler2 = "null";
        public static int beurt;

        private static Bord speelbord;

        public Spel spel = new Spel();


        static void Main()
        {
            //string testString = "|X||X||_||X|";
            Console.WriteLine("Welcome to the game");

            speler1 = Speler1deelname();
            speler2 = Speler2deelname();

            Console.WriteLine(speler1);
            Console.WriteLine(speler2);

            //bord.BordAanmaken();
            speelbord = new Bord();
            Console.Clear();
            speelbord.printBord();

            //string[,] testBord = new string[6, 7] { { "|X|", "|_|", "|_|", "|_|", "|_|", "|X|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" } };
            //computer.ScorePosition(testBord, 0);
            //Console.WriteLine(computer.evaluateSubString(testString, 1));


            if (speler2 == "Computer")
            {
                GameRunnerComputer();
            }
            else
            {
                GameRunner2spelers();
            }

            OpnieuwSpelen();

        }

        public static void OpnieuwSpelen()
        {
            Console.WriteLine("Nieuw spel starten? J voor Ja, N voor Nee");
            string antwoord = Console.ReadLine();
            if (antwoord.ToUpper() == "J")
            {
                //string[] args = new string[1];
                Main();
            }
            else
            {
                Console.WriteLine("Tot de volgende keer");
                System.Threading.Thread.Sleep(3000);
            }
        }



        public static string Speler1deelname()
        {
            while (speler1 == "null")
            {
                while (speler1 == "null")
                {
                    Console.WriteLine(
                        "Voor speler 1, nieuwe speler toevoegen of bestaande speler? N voor nieuwe, B voor bestaande");
                    string userinput = Console.ReadLine();
                    if (userinput.ToUpper() == "N")
                    {
                        speler1 = speler.Maakspeler();
                        break;
                    }
                    else if (userinput.ToUpper() == "B")
                    {
                        speler1 = speler.Kiesspeler();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Not a valid input");
                    }
                }
            }

            return speler1;

        }


        public static string Speler2deelname()
        {
            while (speler2 == "null")
            {
                Console.WriteLine("Wil je tegen een speler of tegen de computer? C voor computer, S voor speler");
                string userinput2 = Console.ReadLine();
                if (userinput2.ToUpper() == "C")
                {
                    speler2 = "Computer";
                    break;
                }
                else if (userinput2.ToUpper() == "S")
                {
                    Console.WriteLine("Voor speler 2, nieuwe speler toevoegen of bestaande speler? N voor nieuwe, B voor bestaande");
                    string userinput3 = Console.ReadLine();
                    if (userinput3.ToUpper() == "N")
                    {
                        speler2 = speler.Maakspeler();
                        break;
                    }
                    else if (userinput3.ToUpper() == "B")
                    {
                        speler2 = speler.Kiesspeler();
                        break;
                    }
                    else { Console.WriteLine("No valid input"); }
                }
                else { Console.WriteLine("No valid input"); }
            }

            return speler2;
        }


        public static void GameRunner2spelers()
        {
            
            bool game_over = false;
            beurt = 0;
            int aantalbeurtenteller = 0;


            while (game_over == false & aantalbeurtenteller < 42)
            {
                if (beurt == 0)
                {
                    Console.WriteLine(speler1 + ", Kies je rij (1 t/m 7):");
                    string selectie = Console.ReadLine(); 
                    
                    int selectie1 = TestChangeInput(selectie);

                    if (selectie1 < 8)
                    {
                        bool rijVrij = speelbord.bovensteRijControleren(selectie1);
                        if (rijVrij)
                        {
                            speelbord.steentje_plaatsen(beurt, selectie1, speelbord.GetMatrixbord());
                            bool winOrNot = speelbord.WinControleren(beurt, speelbord);
                            if (winOrNot == true)
                            {
                                Console.WriteLine(speler1 + " heeft gewonnen!");
                                game_over = true;
                                resultaat.WinnaarOpslaan(speler1);
                                resultaat.VerliezerOpslaan(speler2);
                            }

                            beurt = 1;
                            aantalbeurtenteller = aantalbeurtenteller + 1;
                        }
                        else
                        {
                            Console.WriteLine("Rij is vol, kies opnieuw");
                            //System.Threading.Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Foutieve ingave, kies opnieuw");
                        //System.Threading.Thread.Sleep(3000);
                    }

                }
                else
                {
                    Console.WriteLine(speler2 + ", Kies je rij (1 t/m 7):");
                    string selectie = Console.ReadLine(); 
                    
                    int selectie1 = TestChangeInput(selectie);

                    if (selectie1 < 8)
                    {
                        bool rijVrij = speelbord.bovensteRijControleren(selectie1);
                        if (rijVrij)
                        {
                            speelbord.steentje_plaatsen(beurt, selectie1, speelbord.GetMatrixbord());
                            bool winOrNot = speelbord.WinControleren(beurt, speelbord);
                            if (winOrNot == true)
                            {
                                Console.WriteLine(speler2 + " heeft gewonnen!");
                                game_over = true;
                                resultaat.WinnaarOpslaan(speler2);
                                resultaat.VerliezerOpslaan(speler1);
                            }

                            beurt = 0;
                            aantalbeurtenteller = aantalbeurtenteller + 1;
                        }
                        else
                        {
                            Console.WriteLine("Rij is vol, kies opnieuw");
                            //System.Threading.Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Foutieve ingave, kies opnieuw");
                    }
                }
            }

            if (aantalbeurtenteller >= 42)
            {
                Console.WriteLine("Gelijkspel!");
                game_over = true;
                resultaat.GelijkspelOpslaan(speler1, speler2);
            }
            
        }


        public static void GameRunnerComputer()
        {

            bool game_over = false;
            beurt = 0;
            int aantalbeurtenteller = 0;


            while (game_over == false & aantalbeurtenteller < 42)
            {
                if (beurt == 0)
                {
                    Console.WriteLine(speler1 + ", Kies je rij (1 t/m 7):");
                    string selectie = Console.ReadLine(); 
                    
                    int selectie1 = TestChangeInput(selectie);
                    
                    //int selectie1 = Convert.ToInt32(selectie);

                    if (selectie1 < 8)
                    {
                        bool rijVrij = speelbord.bovensteRijControleren(selectie1);
                        if (rijVrij)
                        {
                            speelbord.steentje_plaatsen(beurt, selectie1, speelbord.GetMatrixbord());
                            bool winOrNot = speelbord.WinControleren(beurt, speelbord);
                            if (winOrNot == true)
                            {
                                Console.WriteLine(speler1 + " heeft gewonnen!");
                                game_over = true;
                                resultaat.WinnaarOpslaan(speler1);

                            }

                            beurt = 1;
                            aantalbeurtenteller = aantalbeurtenteller + 1;
                        }
                        else
                        {
                            Console.WriteLine("Rij is vol, kies opnieuw");
                            //System.Threading.Thread.Sleep(3000);
                        }
                    }
                    else
                    {
                        Console.WriteLine("Foutieve ingave, kies opnieuw");
                    }

                }
                else
                {
                    //computer
                    Console.WriteLine(speler2 + ", Kies je rij (1 t/m 7):");
                    int? selectie1 = computer.Algoritme(speelbord);
                    int selectie2 = (int) selectie1;
                    //System.Threading.Thread.Sleep(3000);

                    bool rijVrij = speelbord.bovensteRijControleren(selectie2+1);
                    if (rijVrij)
                    {

                        speelbord.steentje_plaatsen(beurt, selectie2+1, speelbord.GetMatrixbord());
                        bool winOrNot = speelbord.WinControleren(beurt, speelbord);
                        if (winOrNot == true)
                        {
                            Console.WriteLine(speler2 + " heeft gewonnen!");
                            game_over = true;
                            resultaat.VerliezerOpslaan(speler1);
                        }

                        beurt = 0;
                        aantalbeurtenteller = aantalbeurtenteller + 1;
                    }
                    
                    else
                    {
                        Console.WriteLine("Rij is vol, kies opnieuw");
                        System.Threading.Thread.Sleep(3000);
                    }

                    
                }


            }

            if (aantalbeurtenteller >= 42)
            {
                Console.WriteLine("Gelijkspel!");
                game_over = true;
                resultaat.GelijkspelOpslaan(speler1);
            }

        }


        public static int TestChangeInput(string input)
        {
            int input2 = 0;
            switch (input)
            {
                case "1":
                    input2 = 1;
                    break;
                case "2":
                    input2 = 2;
                    break;
                case "3":
                    input2 = 3;
                    break;
                case "4":
                    input2 = 4;
                    break;
                case "5":
                    input2 = 5;
                    break;
                case "6":
                    input2 = 6;
                    break;
                case "7":
                    input2 = 7;
                    break;
                default:
                    input2 = 8;
                    break;
            }
            
            return input2;
        }

    }
}
