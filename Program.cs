using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Problem1
{
    /// <summary>
    /// This program fills a board with L shapes assuming that one tiles is taken out
    /// k represents the size of the k x k matrix
    /// i represents the row
    /// j represents the column
    /// </summary>
    class Program
    {
        static void Main(string[] args)
        {
            int k, row, col, len;
            int[,] grid;
            int tileNum = 0;
            Coord boxLoc;

            Console.WriteLine("Please enter the value of k as an integer >= 1");
            while(!Int32.TryParse(Console.ReadLine(), out k) || k < 1)
            {
                Console.WriteLine("Please enter an integer <= 1 for the value of k");
            }

            len = (int)Math.Pow(2, k);


            Console.WriteLine("Please enter a value for 0 <= i <= 2^k - 1");
            while (!Int32.TryParse(Console.ReadLine(), out row) || !(row >= 0) || !(row < len))
            {
                Console.WriteLine("Please enter a value for 0 <= i <= 2^k - 1");
            }

            Console.WriteLine("Please enter a value for j as an integer where 0 <= j <= 2^k - 1");
            while (!Int32.TryParse(Console.ReadLine(), out col) || !(col >= 0) || !(col < len))
            {
                Console.WriteLine("Please enter a value for j as an integer where 0 <= j <= 2^k - 1");
            }



            
            grid = new int[ len, len];

            grid[row, col] = 0;

            // the first one is m and the second is n
            boxLoc = new Coord(new Coord(0, len -1), new Coord(0, len -1));

            
            dnc(ref grid, row, col, boxLoc, ref tileNum);

            for (int i = 0; i < grid.GetLength(0); i++)
            {
                for(int j = 0; j< grid.GetLength(0); j++)
                {
                    Console.Write(String.Format("{0}\t", grid[i, j]));
                }
                Console.WriteLine();
            }
            Console.ReadLine();
        }
        //this function is very similar to a function you would find in a quad tree


        /*
         * Recursive program which tiles the board
         * 
         * grid is a 2d array
         * row is the row position of a labeled point
         * col is the column position of a labeled point
         * coord box has the size of the quadrant
         * tileNum holds the current number to be placed
         * 
         * 
         * Time complexity: O(log n)
         */
        static void dnc(ref int[,] grid, int row, int col, Coord box, ref int tileNum)
        {
            int colMid, rowMid;


            //forcing ceiling so that the location of colmid/rowmid
            //isn't switched when there's an odd number divivded by 2

            colMid = (box.Col.Max + box.Col.Min) / 2;
            rowMid = (box.Row.Max + box.Row.Min) / 2;
            

            if (box.Row.Max - box.Row.Min >= 1)
            {
                tileNum++;

                if (row <= rowMid && col <= colMid)//NW
                {

                    grid[rowMid, colMid + 1] = tileNum; //NE
                    grid[rowMid + 1, colMid] = tileNum;//SW
                    grid[rowMid + 1, colMid + 1] = tileNum; //SE

                    if (!((box.Row.Max - box.Row.Min) == 1)) { 
                        dnc(ref grid, row, col,
                            new Coord(new Coord(box.Row.Min, rowMid), new Coord(box.Col.Min, colMid)), ref tileNum);//NW

                        dnc(ref grid, rowMid, colMid + 1,
                            new Coord(new Coord(box.Row.Min, rowMid), new Coord(colMid + 1, box.Col.Max)), ref tileNum); //NE

                        dnc(ref grid, rowMid + 1, colMid,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(box.Col.Min, colMid)), ref tileNum); // SW

                        dnc(ref grid, rowMid + 1, colMid + 1,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(colMid + 1, box.Col.Max)), ref tileNum); // SE
                    }
                }
                else if (row <= rowMid && col > colMid) //NE
                {
                    grid[rowMid, colMid] = tileNum; //NW
                    grid[rowMid+1, colMid] = tileNum;//SW
                    grid[rowMid+1, colMid+1] = tileNum; //SE

                    if (!((box.Row.Max - box.Row.Min) == 1))
                    {
                        dnc(ref grid, rowMid, colMid,
                        new Coord(new Coord(box.Row.Min, rowMid), new Coord(box.Col.Min, colMid)), ref tileNum);//NW

                        dnc(ref grid, row, col,
                            new Coord(new Coord(box.Row.Min, rowMid), new Coord(colMid + 1, box.Col.Max)), ref tileNum); //NE

                        dnc(ref grid, rowMid + 1, colMid,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(box.Col.Min, colMid)), ref tileNum); // SW

                        dnc(ref grid, rowMid + 1, colMid + 1,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(colMid + 1, box.Col.Max)), ref tileNum); // SE
                    }
                }
               
                else if (row > rowMid && col <= colMid) //SW
                {
                    grid[rowMid, colMid] = tileNum; //NW
                    grid[rowMid, colMid+1] = tileNum; //NE
                    grid[rowMid+1, colMid+1] = tileNum; //SE

                    if (!((box.Row.Max - box.Row.Min) == 1))
                    {
                        dnc(ref grid, rowMid, colMid,
                        new Coord(new Coord(box.Row.Min, rowMid), new Coord(box.Col.Min, colMid)), ref tileNum);//NW

                        dnc(ref grid, rowMid, colMid + 1,
                            new Coord(new Coord(box.Row.Min, rowMid), new Coord(colMid + 1, box.Col.Max)), ref tileNum); //NE

                        dnc(ref grid, row, col,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(box.Col.Min, colMid)), ref tileNum); // SW

                        dnc(ref grid, rowMid + 1, colMid + 1,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(colMid + 1, box.Col.Max)), ref tileNum); // SE
                    }
                }
                
                else if (row > rowMid && col > colMid)//SE
                {
                    grid[rowMid, colMid] = tileNum; //NW
                    grid[rowMid, colMid+1] = tileNum; //NE
                    grid[rowMid+1, colMid ] = tileNum;//SW

                    if (!((box.Row.Max - box.Row.Min) == 1))
                    {
                        dnc(ref grid, rowMid, colMid,
                        new Coord(new Coord(box.Row.Min, rowMid), new Coord(box.Col.Min, colMid)), ref tileNum);//NW

                        dnc(ref grid, rowMid, colMid + 1,
                            new Coord(new Coord(box.Row.Min, rowMid), new Coord(colMid + 1, box.Col.Max)), ref tileNum); //NE

                        dnc(ref grid, rowMid + 1, colMid,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(box.Col.Min, colMid)), ref tileNum); // SW

                        dnc(ref grid, row, col,
                            new Coord(new Coord(rowMid + 1, box.Row.Max), new Coord(colMid + 1, box.Col.Max)), ref tileNum); // SE
                    }
                }
            }
            return;
        }
    }
}

