using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _4opeenrijv1
{
    class Bord
    {
        public string[,] matrixbord;


        public Bord()
        {
            this.matrixbord = new string[6, 7] { { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" } };
        }


        public string[,] GetMatrixbord()
        {
            return matrixbord;
        }


        public void BordAanmaken()
        {
            Console.Clear();

            //long[,] arr = new long[5, 4] { { 1, 2, 3, 4 }, { 1, 1, 1, 1 }, { 2, 2, 2, 2 }, { 3, 3, 3, 3 }, { 4, 4, 4, 4 } };
            matrixbord = new string[6, 7] { { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" }, { "|_|", "|_|", "|_|", "|_|", "|_|", "|_|", "|_|" } };


            int rowLength = matrixbord.GetLength(0);
            int colLength = matrixbord.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", matrixbord[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            
        }

        public void setMatrixBord(string[,] newmatrixbord)
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    matrixbord[i, j] = newmatrixbord[i, j];
                }
            }
        }

        public void printBord()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    Console.Write(string.Format("{0} ", matrixbord[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(" 1   2   3   4   5   6   7 ");
            Console.Write(Environment.NewLine + Environment.NewLine);
        }


        public void BordVerversen(string[,] matrixbord)
        {
            Console.Clear();

            int rowLength = matrixbord.GetLength(0);
            int colLength = matrixbord.GetLength(1);

            for (int i = 0; i < rowLength; i++)
            {
                for (int j = 0; j < colLength; j++)
                {
                    Console.Write(string.Format("{0} ", matrixbord[i, j]));
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }
            Console.Write(" 1   2   3   4   5   6   7 ");
            Console.Write(Environment.NewLine + Environment.NewLine);
        }

        
        public bool bovensteRijControleren(int selectie)
        {
            bool leeg;
            
            if (matrixbord[0, selectie - 1] == "|_|")
            {
                leeg = true;
            }
            else
            {
                leeg = false;
            }

            return leeg;
        }

        public List<int> getValidLocation()
        {
            List<int> validLocations = new List<int>();

            for (int i = 1; i < 8; i++)
            {
                bool available = bovensteRijControleren(i);
                if (available)
                {
                    validLocations.Add(i-1);
                }
            }
            return validLocations;
        }

        public int getRijvanColumn(string[,] matrixbord, int selectie)
        {
            for (int i=0; i < 5; i++)
            {
                if (matrixbord[i, selectie] == "|_|")
                {
                    return i;
                }
            }

            return -1;
        }

        public void steentje_plaatsen(int beurt, int selectie, string[,] matrixbord)
        {
            //string selectie;
            int teller = 5;

            while (teller >= 0)
            {
                if (matrixbord[teller, selectie-1] == "|_|")
                {
                    if (beurt == 0)
                    {
                        matrixbord[teller, selectie-1] = "|X|";
                        break;
                    }
                    else
                    {
                        matrixbord[teller, selectie-1] = "|O|";
                        break;
                    }
                }
                else
                {
                    teller = teller - 1;
                }
            }

            BordVerversen(matrixbord);

        }


        public void steentje_plaatsenAlg(int beurt, int selectie, string[,] matrixbord)
        {
            //string selectie;
            int teller = 5;

            while (teller >= 0)
            {
                if (matrixbord[teller, selectie - 1] == "|_|")
                {
                    if (beurt == 0)
                    {
                        matrixbord[teller, selectie - 1] = "|X|";
                        break;
                    }
                    else
                    {
                        matrixbord[teller, selectie - 1] = "|O|";
                        break;
                    }
                }
                else
                {
                    teller = teller - 1;
                }
            }

            //BordVerversen(matrixbord);

        }


        public bool WinControleren(int beurt, Bord bord)
        {
            string kruisjeofbolletje;

            if (beurt == 0)
            {
                kruisjeofbolletje = "|X|";
            }
            else
            {
                kruisjeofbolletje = "|O|";
            }

            bool winOrNot = false;


            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 7; j++)
                {
                    if (j < 4)
                    {
                        //horizontale sequences
                        if (matrixbord[i, j] == kruisjeofbolletje &&
                            matrixbord[i, j + 1] == kruisjeofbolletje &&
                            matrixbord[i, j + 2] == kruisjeofbolletje &&
                            matrixbord[i, j + 3] == kruisjeofbolletje)
                        {
                            winOrNot = true;
                            break;
                        }
                        //downward diagonals
                        if (i < 3)
                        {
                            if (matrixbord[i, j] == kruisjeofbolletje &&
                                matrixbord[i + 1, j + 1] == kruisjeofbolletje &&
                                matrixbord[i + 2, j + 2] == kruisjeofbolletje &&
                                matrixbord[i + 3, j + 3] == kruisjeofbolletje)
                            {
                                winOrNot = true;
                                break;
                            }
                        }
                        //upward diagonals
                        else
                        {
                            if (matrixbord[i, j] == kruisjeofbolletje &&
                                matrixbord[i - 1, j + 1] == kruisjeofbolletje &&
                                matrixbord[i - 2, j + 2] == kruisjeofbolletje &&
                                matrixbord[i - 3, j + 3] == kruisjeofbolletje)
                            {
                                winOrNot = true;
                                break;
                            }
                        }

                    }
                    //verticale sequences
                    if (i < 3 && j < 7)
                    {
                        if (matrixbord[i, j] == kruisjeofbolletje &&
                            matrixbord[i + 1, j] == kruisjeofbolletje &&
                            matrixbord[i + 2, j] == kruisjeofbolletje &&
                            matrixbord[i + 3, j] == kruisjeofbolletje)
                        {
                            winOrNot = true;
                            break;
                        }
                    }
                }
            }
            return winOrNot;

        }

    }
}
