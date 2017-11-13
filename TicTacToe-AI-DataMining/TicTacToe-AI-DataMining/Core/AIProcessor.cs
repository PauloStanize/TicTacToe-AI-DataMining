using System;
using System.Collections;
using System.Collections.Generic;


namespace TicTacToe_AI_DataMining
{
    public class AIProcessor
    {

        /*
         Criar o player, valor que simboliza x ou o para o jogador;
         devolver a melhor posição para o usuario jogar;
         verificar a jogada compativel com o player, tipo x só pode retornar espaços com x
        */

        //lista de jogadas conhecidas, lidas da lista minerada
        private List<string[]> knownPlays;


        public AIProcessor(List<string[]> knownPlays)
        {
            this.knownPlays = knownPlays;
        }

        public string[] playTurn(string[] boardSituation, string difficult, string player)
        {
            boardSituation = normalizeBoardSituation(boardSituation);

            switch (difficult)
            {
                case "easy":
                    boardSituation = makeARandomPlay(boardSituation, player);
                    break;

                case "hard":
                    boardSituation = makeTheBestPlay(boardSituation, player);
                    break;
            }

            return boardSituation;
        }

        public bool hasWinner(string[] boardSituation)
        {
            string player = "x";

            //checking top row
            if (boardSituation[0] == player && boardSituation[1] == player && boardSituation[2] == player)
            {
                return true;
            }

            //checking mid row
            if (boardSituation[3] == player && boardSituation[4] == player && boardSituation[5] == player)
            {
                return true;
            }

            //checking bot row
            if (boardSituation[6] == player && boardSituation[7] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking first column
            if (boardSituation[0] == player && boardSituation[3] == player && boardSituation[6] == player)
            {
                return true;
            }

            //checking second column
            if (boardSituation[1] == player && boardSituation[4] == player && boardSituation[7] == player)
            {
                return true;
            }

            //checking third column
            if (boardSituation[2] == player && boardSituation[5] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking diagonal
            if (boardSituation[0] == player && boardSituation[4] == player && boardSituation[8] == player)
            {
                return true;
            }


            player = "o";
            //checking top row
            if (boardSituation[0] == player && boardSituation[1] == player && boardSituation[2] == player)
            {
                return true;
            }

            //checking mid row
            if (boardSituation[3] == player && boardSituation[4] == player && boardSituation[5] == player)
            {
                return true;
            }

            //checking bot row
            if (boardSituation[6] == player && boardSituation[7] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking first column
            if (boardSituation[0] == player && boardSituation[3] == player && boardSituation[6] == player)
            {
                return true;
            }

            //checking second column
            if (boardSituation[1] == player && boardSituation[4] == player && boardSituation[7] == player)
            {
                return true;
            }

            //checking third column
            if (boardSituation[2] == player && boardSituation[5] == player && boardSituation[8] == player)
            {
                return true;
            }

            //checking diagonal
            if (boardSituation[0] == player && boardSituation[4] == player && boardSituation[8] == player)
            {
                return true;
            }

            return false;
        }

        private string[] makeARandomPlay(string[] boardSituation, string player)
        {
            int randomPositionToPlay = findARandomPositionToPlay(boardSituation);
            boardSituation[randomPositionToPlay] = player;

            return boardSituation;
        }

        private string[] makeTheBestPlay(string[] boardSituation, string player)
        {
            double playRate = 0, tempPlayRate = 0;
            int whereToPlay = -1;

            List<string[]> compatiblePlays = getCompatiblePlays(boardSituation);
            compatiblePlays = filterByWinPlays(compatiblePlays, player);
            List<int> emptyPlaces = getEmptyPlaces(boardSituation);

            /*
             Para cada espaço vazio calcular o número de vezes que a jogada foi feita no espaço;
             Achar o espaço com maior número de jogadas vitoriosas;
             Marcar a jogada no melhor espaço.
             */
            for (int i = 0; i < emptyPlaces.Count; i++)
            {
                //se nao houverem jogadas compativeis, jogue no primeiro espaço vazio
                if (compatiblePlays.Count < 1)
                {
                    whereToPlay = emptyPlaces[0];
                    break;
                }
                tempPlayRate = calculateAppearanceRate(emptyPlaces[i], compatiblePlays, player);

                if (tempPlayRate > playRate)
                {
                    playRate = tempPlayRate;
                    whereToPlay = emptyPlaces[i];
                }
            }

            boardSituation[whereToPlay] = player;

            return boardSituation;
        }

        private int findARandomPositionToPlay(string[] boardSituation)
        {
            System.Random rnd = new System.Random();

            //List <string[]> compatiblePlays = getCompatiblePlays(boardSituation);
            //compatiblePlays = filterByWinPlays(compatiblePlays);

            List<int> possiblePlays = new List<int>();

            //procurando o indice de espaços vazios onde posso jogar
            for (int i = 0; i < 9; i++)
            {
                //pega os espaços vazios para a primeira jogada compatível
                //if(compatiblePlays[0][i] == "b")
                //{
                //    possiblePlays.Add(i);
                //}

                if (boardSituation[i] == "b")
                {
                    possiblePlays.Add(i);
                }
            }

            int randomPosition = rnd.Next(0, possiblePlays.Count);

            return possiblePlays[randomPosition];
        }

        private string[] normalizeBoardSituation(string[] boardSituation)
        {
            for (int i = 0; i < 9; i++)
            {
                if (!(boardSituation[i] == "x" || boardSituation[i] == "o" || boardSituation[i] == "b"))
                {
                    boardSituation[i] = "b";
                }
            }

            return boardSituation;
        }

        private List<string[]> getCompatiblePlays(string[] boardSituation)
        {
            bool isCompatible = true;
            List<string[]> compatiblePlays = new List<string[]>();

            for (int i = 0; i < knownPlays.Count; i++)
            {
                isCompatible = true;

                for (int j = 0; j < 9; j++)
                {
                    //se a possição estiver vazia considere compatível
                    if (boardSituation[j] == "b") continue;

                    if (boardSituation[j] != knownPlays[i][j])
                    {
                        isCompatible = false;
                        break;
                    }
                }

                if (isCompatible == true)
                {
                    compatiblePlays.Add(knownPlays[i]);
                }
            }

            return compatiblePlays;
        }

        private List<string[]> filterByWinPlays(List<string[]> listOfPlays, string player)
        {
            //se o player é x entao pego o status positive que na lista quer dizer que ele venceu, se o player for o pego o negative, com 
            //isso x ganha se status for positive e o ganha se status for negative
            string status = player == "x" ? "positive" : "negative";

            //filtrando a lista, retiranddo as jogadas que resultaram em derrotas
            for (int i = 0; i < listOfPlays.Count;)
            {
                if (listOfPlays[i][9] != status)
                {
                    listOfPlays.RemoveAt(i);
                }
                else
                {
                    i++; //incrementar só quando não remover item da lista
                }
            }

            return listOfPlays;
        }

        private List<int> getEmptyPlaces(string[] boardSituation)
        {
            List<int> emptyPlaces = new List<int>();

            for (int i = 0; i < boardSituation.Length; i++)
            {
                if (boardSituation[i] == "b")
                    emptyPlaces.Add(i);
            }

            return emptyPlaces;
        }

        /*
            Calcula o numero de vezes que a jogada aparece baseado na contagem dos dados de jogadas anteriores 
        */
        private double calculateAppearanceRate(int whereToPlay, List<string[]> listOfPlays, string player)
        {
            double xCount = 0, avaliatedCount = 0;

            foreach (string[] play in listOfPlays)
            {
                //se for um espaço vazio, pule a operação
                if (play[whereToPlay] == "b")
                    continue;

                //contabiliza a jogada que foi avaliada
                avaliatedCount++;
                if (play[whereToPlay] == "x")
                    xCount++;
            }

            if (player == "x")
                return ((avaliatedCount / xCount) * 10.0);

            //se player for o
            return (100 - ((avaliatedCount / xCount) * 10));
        }
    }
}