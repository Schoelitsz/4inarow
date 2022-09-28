using System;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using Microsoft.VisualBasic.CompilerServices;
using System.Data;
using System.IO;
using System.Linq;

namespace _4opeenrijv1
{
    class Computer
    {
        public Bord bord = new Bord();
        
        //public int beurt = 1;

        
        public int? Algoritme(Bord speelbord)
        {
            //Random rnd = new Random();
            //int selectie1 = rnd.Next(1, 8);

            //int selectie1 = BesteZet(bord.GetMatrixbord(), 1);

            (int?,int) columnEnScore = Minimax(speelbord.GetMatrixbord(), 7, int.MinValue, int.MaxValue, true);
            int? selectie1 = columnEnScore.Item1;

            return selectie1;
        }

        
        public int ScorePosition(string[,] matrixbord, int player)
        {
            int score = 0;

            //Score center column
            string column = "";
            int centerCount = 0;
            for (int i = 0; i < 6; i++)
            {
                column += matrixbord[i, 3];
            }
            

            for (int i = 0; i < column.Length; i += 3)
            {
                string substring = column.Substring(i, 3);
                if (substring.Equals("|O|"))
                {
                    centerCount++; // die was weg
                    //score += 5; //5 //score += centerCount * 3;  *3 is te sterk +3 is te zwak
                }
            }
            score += centerCount * 3;


            string window = "";
            for (int i = 0; i < 6; i++)
            {

                for (int c = 0; c < 7; c++)
                {
                    //horizontale sequences
                    if (c < 4)
                    {
                        window = matrixbord[i, c] + matrixbord[i, c + 1] + matrixbord[i, c + 2] +
                                        matrixbord[i, c + 3];
                        //string window = row[c] + row[c+1] + row[c+2] + row[c+3];
                        score += evaluateSubString(window, player);

                        //downward diagonals
                        if (i < 3)
                        {
                            window = matrixbord[i, c] + matrixbord[i + 1, c + 1] + matrixbord[i + 2, c + 2] + matrixbord[i + 3, c + 3];
                            score += evaluateSubString(window, player);
                        }
                        //upward diagonals
                        else
                        {
                            window = matrixbord[i, c] + matrixbord[i - 1, c + 1] + matrixbord[i - 2, c + 2] + matrixbord[i - 3, c + 3];
                            score += evaluateSubString(window, player);
                        }
                    }
                    //verticale sequences
                    if (i < 3 && c < 7)
                    {
                        window = matrixbord[i, c] + matrixbord[i + 1, c] + matrixbord[i + 2, c] +
                                        matrixbord[i + 3, c];
                        //string window = column[c] + column[c + 1] + column[c + 2] + column[c + 3];
                        score += evaluateSubString(window, player);
                    }

                }

            }
            return score;
        }


        public int evaluateSubString(string substring, int player)
        {

            int score = 0;
            string playerString, opponentString;
            if (player == 0)
            {
                playerString = "|X|";
                opponentString = "|O|";
            }
            else
            {
                playerString = "|O|";
                opponentString = "|X|";
            }

            int countPieces = 0;
            int countOpponentPieces = 0;
            int countEmpty = 0;

            for (int i = 0; i <= substring.Length-3; i+=3)
            {
                string tileString = substring.Substring(i, 3);
                if (tileString.Equals(playerString)) countPieces++;
                if (tileString.Equals(opponentString)) countOpponentPieces++;
                if (tileString.Equals("|_|")) countEmpty++;
            }

            if (countPieces == 4)
            {
                score += 100;
            }
            else if (countPieces == 3 && countEmpty == 1)
            {
                score += 5;//20
            }
            else if (countPieces == 2 && countEmpty == 2)
            {
                score += 2;//5//2
            }
            
            if (countOpponentPieces == 4) //opponent wint - deze check niet aanwezig in Python voorbeeld voor AI dus 3 op een rij was belangrijker te voorkomen dan 4 op een rij
            {
                score -= 200;// was 100 maar gaf eigen 3 op een rij voorkeur tov blokkeren van 3 op een rij van tegenstander
            }

            else if (countOpponentPieces == 3 && countEmpty == 1)
            {
                score -= 40 ;// score -=4; origineel - als tegenstander al 3 op een rij heeft worden maar 4 punten afgehaald. verandert naar -40
            }

            return score;
        }

        public int BesteZet(string[,] matrixbord, int player)
        {
            Random random = new Random();
            int bestescore = -10000;
            int besteColumn = random.Next(0, 7);

            List<int> validLocations = bord.getValidLocation();
            foreach (int i in validLocations)
            {
                //int rij = bord.getRijvanColumn(matrixbord, beurt);



                Bord bord2 = new Bord();
                ////string[,] matrixclone = (string[,])matrixbord.Clone();
                ////string[,] matrixclone = matrixbord;
                string[,] matrixclone = new string[6,7];

                for (int row = 0; row < 6; row++)
                {
                    for (int col = 0; col < 7; col++)
                    {
                        matrixclone[row, col] = matrixbord[row, col];
                    }
                }
                bord2.setMatrixBord(matrixclone);

                

                //matrixbord.CopyTo(matrixclone, 1);

                //Bord bord2 = (Bord)matrixbord.Clone();
                //bord2 = bord;


                //deze 2 regels eronder zorgen voor tonen van testgegevens en wachttijd
                bord2.printBord();
                System.Threading.Thread.Sleep(1000);

                bord2.steentje_plaatsenAlg(player, i+1, matrixclone);


                int score = ScorePosition(matrixclone, player);
                
                //regel hieronder is tonen van testgegevens
                Console.WriteLine(score + " " + i);
                if (score > bestescore)
                {
                    bestescore = score;
                    besteColumn = i;
                }

            }
            return besteColumn;
            
        }



        public bool isTerminalNode()
        {
            return bord.WinControleren(0,bord) | bord.WinControleren(1,bord) | bord.getValidLocation().Count == 0 ;
        }


        public (int?, int) Minimax(string[,] matrixbord, int depth, int alpha, int beta, bool maximizingPlayer)
        {
            List<int> validLocations = bord.getValidLocation();
            bool isTerminal = isTerminalNode();

            if (depth == 0 | isTerminal)
            {
                if (isTerminal)
                {
                    if (bord.WinControleren(0, bord))
                    {
                        return (null, 100000000);
                    }
                    else if (bord.WinControleren(1, bord))
                    {
                        return (null, -1000000000);
                    }
                    else
                    {
                        return (null, 0);
                    }
                }
                else
                {
                    return (null, ScorePosition(matrixbord, 1));
                }
            }


            if (maximizingPlayer)
            {
                int value = int.MinValue;
                Random random = new Random();
                int index = random.Next(validLocations.Count);
                int column = validLocations[index];
                foreach (int coll in validLocations)
                {
                    Bord bord2 = new Bord();
                    bord2.setMatrixBord(matrixbord);
                    bord2.steentje_plaatsenAlg(1, coll + 1, bord2.GetMatrixbord());

                    int newScore = Minimax(bord2.GetMatrixbord(), depth - 1, alpha ,beta , false).Item2;
                    if (newScore > value && bord2.bovensteRijControleren(coll +1)) //&& bord2.bovensteRijControleren(coll +1) zorgt ervoor dat hij niet in een loop komt
                    {
                        value = newScore;
                        column = coll;
                    }
                    alpha = Math.Max(alpha, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                    //testgegevens
                    //bord2.printBord();
                    //System.Threading.Thread.Sleep(1000);
                    //Console.WriteLine(value + " " + coll);
                }
                return (column, value);
            }

            else
            {
                int value = int.MaxValue;
                Random random = new Random();
                int index = random.Next(validLocations.Count);
                int column = validLocations[index];
                foreach (int coll in validLocations)
                {
                    Bord bord2 = new Bord();
                    bord2.setMatrixBord(matrixbord);
                    bord2.steentje_plaatsenAlg(0, coll + 1, bord2.GetMatrixbord());

                    int newScore = Minimax(bord2.GetMatrixbord(), depth - 1, alpha, beta, true).Item2;
                    if (newScore < value && bord2.bovensteRijControleren(coll+1)) //&& bord2.bovensteRijControleren(coll +1) zorgt ervoor dat hij niet in een loop komt
                    {
                        value = newScore;
                        column = coll;
                    }
                    beta = Math.Min(beta, value);
                    if (alpha >= beta)
                    {
                        break;
                    }
                    //testgegevens
                    //bord2.printBord();
                    //System.Threading.Thread.Sleep(1000);
                    //Console.WriteLine(value + " " + coll);
                }
                return (column, value);
            }
        }






        

        //public (int?,int) Minimax(string[,] matrixbord, int depth, bool maximizingPlayer)
        //{

        //    List<int> validLocations = bord.getValidLocation();
        //    bool isTerminal = isTerminalNode();


        //    if (depth == 0 | isTerminal)
        //    {
        //        if (isTerminal)
        //        {
        //            if (bord.WinControleren(0, bord))
        //            { 
        //                //Tuple<int,int> returnValue= new Tuple<int,int>(0, 100000000);
        //                return (null,100000000);
        //            }
        //            else if (bord.WinControleren(1, bord))
        //            {
        //                //Tuple<int, int> returnValue = new Tuple<int, int>(0, -100000000);
        //                return (null, -1000000000);
        //            }
        //            else
        //            {
        //                //Tuple<int, int> returnValue = new Tuple<int, int>(0, 0);
        //                return (null,0);
        //            }
        //        }
        //        else
        //        {
        //            //Tuple<System.Nullable(int), int>? returnValue = new Tuple<int, int>(null, ScorePosition(matrixbord,1));
        //            return (null, ScorePosition(matrixbord,1));
        //        }
        //    }

        //    //depth = depth - 1;

        //    if (maximizingPlayer)
        //    {
        //        int value = int.MinValue;
        //        Random random = new Random();
        //        int index = random.Next(validLocations.Count);
        //        int column = validLocations[index];
        //        foreach (int coll in validLocations)
        //        {
        //            Bord bord2 = new Bord();
        //            string[,] matrixclone = new string[6, 7];
        //            for (int row = 0; row < 6; row++)
        //            {
        //                for (int col = 0; col < 7; col++)
        //                {
        //                    matrixclone[row, col] = matrixbord[row, col];
        //                }
        //            }
        //            bord2.setMatrixBord(matrixclone);

        //            bord2.steentje_plaatsen(1,coll+1,matrixclone);

        //            //depth = depth - 1;
        //            int newScore = Minimax(matrixclone, depth-1, false).Item2;
        //            if (newScore > value)
        //            {
        //                value = newScore;
        //                column = coll;
        //            }

        //        }
        //        return (column,value);

        //    }

        //    else
        //    {
        //        int value = int.MaxValue;
        //        Random random = new Random();
        //        int index = random.Next(validLocations.Count);
        //        int column = validLocations[index];
        //        foreach (int coll in validLocations)
        //        {
        //            Bord bord3= new Bord();
        //            string[,] matrixclone2 = new string[6, 7];
        //            for (int row = 0; row < 6; row++)
        //            {
        //                for (int col = 0; col < 7; col++)
        //                {
        //                    matrixclone2[row, col] = matrixbord[row, col];
        //                }
        //            }
        //            bord3.setMatrixBord(matrixclone2);

        //            bord3.steentje_plaatsen(0, coll+1, matrixclone2);

        //            //depth = depth - 1;
        //            int newScore = Minimax(matrixclone2, depth-1, true).Item2;
        //            if (newScore < value)
        //            {
        //                value = newScore;
        //                column = coll;
        //            }
        //        }
        //        return (column, value);
        //    }

        //}






        //public int ScorePosition(string[,] matrixbord, int player)        
        //{
        //    int score = 0;

        //    //Score center column
        //    string column = "";
        //    int centerCount = 0;
        //    for (int i = 0; i < 6; i++)
        //    {
        //        column += matrixbord[i, 3];
        //    }
        //    // origineel  : for (int i = 0; i < column.Length - 3; i += 3)
        //    // tijdens demo worde center colom niet met voorkeur gekozen maar linker colom
        //    // Length-3 verwijdert omdat laatste 3 symbolen in colom substring nooiet geevalueerd werdt. 
        //    //  |_||_||_||_||_||_||0| laatste (belangrijkste) werdt niet gezien
        //    // gewicht verandert van : score += centerCount * 3; naar score += centerCount + 5; om center colom niet belangerijker te maken dan 3 op een rij horizontaal
        //    //------------------------------------------------------------------------------------

        //    for (int i = 0; i < column.Length; i += 3)
        //    {
        //        string substring = column.Substring(i, 3);
        //        if (substring.Equals("|O|"))
        //        {
        //            score += 5; //score += centerCount * 3;  *3 is te sterk +3 is te zwak
        //        }
        //    }


        //    //Score Horizontal rij
        //    for (int i = 0; i < 6; i++)
        //    {
        //        //string[] row = Enumerable.Range(0, matrixbord.GetLength(1)).Select(x => matrixbord[i, x]).ToArray();
        //        for (int c = 0; c < 4; c++)
        //        {
        //            string window = matrixbord[i, c] + matrixbord[i, c + 1] + matrixbord[i, c + 2] +
        //                            matrixbord[i, c + 3];
        //            //string window = row[c] + row[c+1] + row[c+2] + row[c+3];
        //            score += evaluateSubString(window, player);

        //        }
        //    }

        //    //Score Verticaal column
        //    for (int i = 0; i < 3; i++)
        //    {
        //        //string[] column = Enumerable.Range(0, matrixbord.GetLength(1)-1).Select(x => matrixbord[x, i]).ToArray();
        //        for (int c = 0; c < 7; c++)
        //        {
        //            string window = matrixbord[i, c] + matrixbord[i + 1, c] + matrixbord[i + 2, c] +
        //                            matrixbord[i + 3, c];
        //            //string window = column[c] + column[c + 1] + column[c + 2] + column[c + 3];
        //            score += evaluateSubString(window, player);
        //        }
        //    }

        //    //Score positive sloped diagonal 
        //    for (int r = 0; r < 3; r++)
        //    {
        //        for (int c = 0; c < 4; c++)
        //        {
        //            string window = matrixbord[r, c] + matrixbord[r+1,c+1] + matrixbord[r+2,c+2] + matrixbord[r+3,c+3];
        //            score += evaluateSubString(window, player);
        //        }
        //    }

        //    //Score negative sloped diagonal 
        //    for (int r = 3; r < 6; r++)
        //    {
        //        for (int c = 0; c < 4; c++)
        //        {
        //            string window = matrixbord[r, c] + matrixbord[r-1,c+1] + matrixbord[r-2,c+2] + matrixbord[r-3,c+3];
        //            score += evaluateSubString(window, player);
        //        }
        //    }

        //    return score;
        //}


    }

}
