using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe_AI_DataMining
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] play = new string[9];
            string[] boardSituation = new string[] { "x", "b", "b", "b", "b", "b", "b", "b", "b" };
            AIProcessor aIProcessor = null;

            FileReader fr = new FileReader();
            List<string[]> knownPlays = fr.ReadFile("C:/Users/Paulo/Documents/teste ia/Assets/Data Mining/tic-tac-toe.data");

            //foreach(string[] line in knownPlays)
            //{
            //    Debug.Log(string.Join(",", line));
            //}

            aIProcessor = new AIProcessor(knownPlays);
            //string[] boardSituation = new string[] { "x", "b", "b", "b", "b", "b", "b", "b", "b" };

            //string[] play = aIProcessor.playTurn(boardSituation, "easy", "o");
            //Debug.Log("play");
            //Debug.Log(string.Join(",", play));

            bool winner = false;
            for (int k = 0; k < 4; k++)
            {
                Console.WriteLine("Winner: " + winner);

                if (winner) {
                    Console.WriteLine("End.");
                    Console.ReadLine();
                    break;
                }

                if (!(winner = aIProcessor.hasWinner(boardSituation)))
                {
                    play = aIProcessor.playTurn(boardSituation, "easy", "o");
                    Console.WriteLine("O: ");
                    printBoard(boardSituation);
                }

                if (!(winner = aIProcessor.hasWinner(boardSituation)))
                {
                    play = aIProcessor.playTurn(boardSituation, "hard", "x");
                    Console.WriteLine("X: ");
                    printBoard(boardSituation);
                }
            }

        }

        private static void printBoard(string[] board)
        {
            string line = board[0] + " | " + board[1] + " | " + board[2] + " | " + "\n" +
                          board[3] + " | " + board[4] + " | " + board[5] + " | " + "\n" +
                          board[6] + " | " + board[7] + " | " + board[8] + " | ";

            Console.WriteLine(line);
            Console.ReadLine();
        }
    }       
}
