using System;
using System.Collections.Generic;
using System.Linq;

namespace LaddersAndSnake
{
    public class Program
    {
        public static void printBoard(Tile[] tiles, int laddersNum, int snakesNum)
        {
            FileStream fs = new FileStream(@"C:\Users\win10\Desktop\mandelProject\OutPut.txt",
                        FileMode.Create,
                        FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);

            Console.WriteLine("\n--ladders--");
            writer.WriteLine("\n--ladders--");
            for (int i = 0; i < tiles.Length; i++)
            {
                if (i == laddersNum)
                {
                    Console.WriteLine("\n--snakes--");
                    writer.WriteLine("\n--snakes--");
                }
                else if (i == laddersNum + snakesNum)
                {
                    Console.WriteLine("\n--golden tiles--");
                    writer.WriteLine("\n--golden tiles--");
                }
                if (i >= tiles.Length - 2)
                {
                    Console.WriteLine(tiles[i].StartTileNum);
                    writer.WriteLine(tiles[i].StartTileNum);
                }
                else
                {
                    Console.WriteLine(tiles[i].StartTileNum + " => " + tiles[i].EndTileNum);
                    writer.WriteLine(tiles[i].StartTileNum + " => " + tiles[i].EndTileNum);
                }

            }
            writer.Close();
            fs.Close();
        }
        public static void buildBoard(int laddersNum, int snakesNum, Tile[] tiles)
        {
            Random rnd = new Random();
            int[] usedTiles = new int[tiles.Length * 2];
            int y = 0;
            int lotteryNumber;
            for (int i = 0; i < laddersNum; i++)
            {
                tiles[i] = new Tile();
                tiles[i].TileType = "ladder";
                lotteryNumber = rnd.Next(1, 91);
                while (usedTiles.Contains(lotteryNumber))
                    lotteryNumber = rnd.Next(1, 91);
                tiles[i].StartTileNum = lotteryNumber;
                usedTiles[y++] = lotteryNumber;
                lotteryNumber = rnd.Next(tiles[i].StartTileNum + (11 - tiles[i].StartTileNum % 10), 91);
                while (usedTiles.Contains(lotteryNumber))
                    lotteryNumber = rnd.Next(tiles[i].StartTileNum + (11 - tiles[i].StartTileNum % 10), 91);
                tiles[i].EndTileNum = lotteryNumber;
                usedTiles[y++] = lotteryNumber;
            }
            for (int i = 0; i < snakesNum; i++)
            {
                int x = laddersNum + i;
                tiles[x] = new Tile();
                tiles[x].TileType = "snake";
                lotteryNumber = rnd.Next(11, 100);
                while (usedTiles.Contains(lotteryNumber))
                    lotteryNumber = rnd.Next(11, 100);
                tiles[x].StartTileNum = lotteryNumber;
                usedTiles[y++] = lotteryNumber;
                lotteryNumber = rnd.Next(1, tiles[x].StartTileNum - tiles[x].StartTileNum % 10 + 1);
                while (usedTiles.Contains(lotteryNumber))
                    lotteryNumber = rnd.Next(1, tiles[x].StartTileNum - tiles[x].StartTileNum % 10 + 1);
                tiles[x].EndTileNum = lotteryNumber;
                usedTiles[y++] = lotteryNumber;

            }
            for (int i = 0; i < 2; i++)
            {
                int x = laddersNum + snakesNum + i;
                tiles[x] = new Tile();
                tiles[x].TileType = "gold";
                lotteryNumber = rnd.Next(1, 100);
                while (usedTiles.Contains(lotteryNumber))
                    lotteryNumber = rnd.Next(1, 100);
                tiles[x].StartTileNum = lotteryNumber;
                tiles[x].EndTileNum = -1;
                usedTiles[y++] = lotteryNumber;
            }

            printBoard(tiles, laddersNum, snakesNum);
        }
        public static int numberLottery()
        {
            Random rnd = new Random();
            int x = rnd.Next(1, 7);
            int y = rnd.Next(1, 7);
            return x + y;
        }
        public static void gameSimulation(Player player1, Player player2, Tile[] tiles)
        {
            FileStream fs = new FileStream(@"C:\Users\win10\Desktop\mandelProject\OutPut.txt",
                        FileMode.Append,
                        FileAccess.Write);
            StreamWriter writer = new StreamWriter(fs);
            Console.WriteLine("\n--Simulation Started--");
            writer.WriteLine("\n--Simulation Started--");

            Player currentPlayer = player1;
            Player otherPlayer = player2;
            bool flag = true;
            int roundNum = 1;
            while (flag)
            {
                Console.WriteLine("\n--round " + roundNum + " --");
                writer.WriteLine("\n--round " + roundNum + " --");
                for (int j = 0; j < 2; j++)
                {
                    int steps = numberLottery();
                    Console.WriteLine(currentPlayer.Name + " rolled " + steps);
                    writer.WriteLine(currentPlayer.Name + " rolled " + steps);
                    if (currentPlayer.Location + steps >= 100)
                    {
                        flag = false;
                        Console.WriteLine(currentPlayer.Name + " has won!!!");
                        writer.WriteLine(currentPlayer.Name + " has won!!!");
                        break;
                    }
                    else
                    {
                        bool SpecialTileFlage = false;
                        for (int i = 0; i < tiles.Length; i++)
                            if (currentPlayer.Location + steps == tiles[i].StartTileNum)
                            {
                                if (tiles[i].EndTileNum != -1)
                                {
                                    currentPlayer.Location = tiles[i].EndTileNum;
                                    Console.WriteLine(currentPlayer.Name + " has landed on a " + tiles[i].TileType);
                                    writer.WriteLine(currentPlayer.Name + " has landed on a " + tiles[i].TileType);

                                }

                                else
                                {
                                    currentPlayer.Location = otherPlayer.Location;
                                    otherPlayer.Location = tiles[i].StartTileNum;
                                    Console.WriteLine(currentPlayer.Name + " landed on golden tile and switched with " + otherPlayer.Name);
                                    writer.WriteLine(currentPlayer.Name + " landed on golden tile and switched with " + otherPlayer.Name);
                                    Console.WriteLine(otherPlayer.Name + " is on " + otherPlayer.Location);
                                    writer.WriteLine(otherPlayer.Name + " is on " + otherPlayer.Location);
                                }
                                SpecialTileFlage = true;
                            }
                        if (!SpecialTileFlage)
                            currentPlayer.Location = currentPlayer.Location + steps;
                    }
                    Console.WriteLine(currentPlayer.Name + " is on " + currentPlayer.Location);
                    writer.WriteLine(currentPlayer.Name + " is on " + currentPlayer.Location);

                    currentPlayer = currentPlayer == player1 ? player2 : player1;
                    otherPlayer = otherPlayer == player1 ? player2 : player1;
                }
                roundNum++;
            };
            writer.Close();
            fs.Close();
        }
        public static void Main(string[] args)
        {
            Player player1 = new Player();
            Player player2 = new Player();
            Console.WriteLine("enter first player name");
            player1.Name = Console.ReadLine();
            Console.WriteLine("enter second player name");
            player2.Name = Console.ReadLine();
            Console.WriteLine("enter number of ladders");
            int laddersNum = int.Parse(Console.ReadLine());
            Console.WriteLine("enter number of snakes");
            int snakesNum = int.Parse(Console.ReadLine());
            Tile[] tiles = new Tile[laddersNum + snakesNum + 2];

            buildBoard(laddersNum, snakesNum, tiles);
            gameSimulation(player1, player2, tiles);

        }
    }
}