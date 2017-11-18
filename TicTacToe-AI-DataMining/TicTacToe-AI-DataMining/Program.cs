using System;
using System.Collections.Generic;

namespace TicTacToe_AI_DataMining
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] boardSituation = new string[] { "x", "o", "b", "b", "o", "b", "b", "x", "b" };
            AIProcessor aIProcessor = null;

            FileReader fr = new FileReader();
            List<string[]> knownPlays = fr.ReadFile("C:/Users/Paulo/Documents/teste ia/Assets/Data Mining/tic-tac-toe.data");
            
            aIProcessor = new AIProcessor(knownPlays);

            bool winner = false;
            for (int k = 0; k <= aIProcessor.remainingPlays(boardSituation); k++)
            {
                Console.WriteLine("Winner: " + winner);

                if (winner) {
                    Console.WriteLine("End.");
                    Console.ReadLine();
                    break;
                }

                if (!(winner = aIProcessor.hasWinner(boardSituation)))
                {
                    boardSituation = aIProcessor.playTurn(boardSituation, "hard", "o");
                    Console.WriteLine("O: ");
                    printBoard(boardSituation);
                }

                if (!(winner = aIProcessor.hasWinner(boardSituation)))
                {
                    boardSituation = aIProcessor.playTurn(boardSituation, "easy", "x");
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
