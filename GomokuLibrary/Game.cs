using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;

namespace GomokuLibrary
{
    /**
      * Class Name: Game.cs		
      * Purpose: A multiplayer game application which incorporates multiple assemblies and WCF
      * Coders: Haris Khalid, Woojin Shin, Sterling Gault, Sagar Thapa
      * Date: Apr 3rd, 2022
      */
    // Defines the client callback contract
    [ServiceContract]
    public interface ICallback
    {
        [OperationContract(IsOneWay = true)]
        void UpdateGui(CallbackInfo info);
    }
    // Defines the WCF service endpoint (network facing interface)
    [ServiceContract(CallbackContract = typeof(ICallback))]
    public interface IGame
    {
        [OperationContract]
        Mark Play(bool playerOneFirstMove, int cellPosition);
        [OperationContract]
        string GetMark(int cellPosition);
        [OperationContract]
        void CheckWinner();
        [OperationContract(IsOneWay = true)]
        void CreateNewGame();
        [OperationContract(IsOneWay = true)]
        void Repopulate();
        bool GameEnd { [OperationContract] get; }
        bool PlayerOneTurn { [OperationContract] get; [OperationContract] set; }
        int PlayerOneScore { [OperationContract] get; }
        int PlayerTwoScore { [OperationContract] get; }
        [OperationContract(IsOneWay = true)]
        void RegisterForCallbacks();
        [OperationContract(IsOneWay = true)]
        void UnregisterFromCallbacks();
    }
    // Implements the WCF service
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
    public class Game : IGame
    {
        private List<Mark> marks = null;
        private bool gameEnd;
        private bool playerOneTurn;
        private int playerOneScore = 0, playerTwoScore = 0;
        private string result = "";
        private List<int> winningCells = null;
        private Mark selectedMark = null;
        private HashSet<ICallback> callbacks = new HashSet<ICallback>();
        private static uint objectCount = 0;
        private uint objectId;
        //C'tor
        public Game()
        {
            objectId = ++objectCount;
            Console.WriteLine($"Creating Game Object #{objectId}");
            marks = new List<Mark>();
            Repopulate();
        }
        public bool GameEnd
        {
            get
            {
                return gameEnd;
            }
        }
        public bool PlayerOneTurn
        {
            get { return playerOneTurn; }
            set
            {
                playerOneTurn = value;
            }
        }
        //get mark index based on cell position
        public string GetMark(int cellPosition)
        {
            return marks[cellPosition].ToString();
        }
        public int PlayerOneScore
        {
            get { return playerOneScore; }
        }

        public int PlayerTwoScore
        {
            get { return playerTwoScore; }
        }
        /*Method Name: Play
         *Purpose: play the game starting with player one initial move
         *Accepts: bool, int
         *Returns: Mark
         */
        public Mark Play(bool playerOneFirstMove, int cellPosition)
        {
            if (marks[cellPosition].MarkValue == Symbol.Blank)
            {
                if (playerOneFirstMove)
                {
                    marks[cellPosition] = new Mark(Symbol.X, cellPosition);
                    playerOneTurn = false;
                }
                else
                {
                    marks[cellPosition] = new Mark(Symbol.O, cellPosition);
                    playerOneTurn = true;
                }
                Console.WriteLine($"Game Object #{objectId} Playing with {marks[cellPosition]}.");
            }
            else
            {
                //if the selected cell is used then return null.
                return null;
            }
            selectedMark = marks[cellPosition];
            return marks[cellPosition];
        }
        /*Method Name: CheckWinner
         *Purpose: indicates whether the player has won or not based on the winning patterns 
         *Accepts: nothing
         *Returns: void
         */
        public void CheckWinner()
        {
            List<int> winners = new List<int>();

            //check row line
            //first row on grid [0][1][2][3][4]
            if (marks[0].MarkValue != Symbol.Blank && (marks[1].MarkValue == marks[0].MarkValue) && (marks[2].MarkValue == marks[0].MarkValue) && (marks[3].MarkValue == marks[0].MarkValue) && (marks[4].MarkValue == marks[0].MarkValue))
            {
                winners.Add(0);
                winners.Add(1);
                winners.Add(2);
                winners.Add(3);
                winners.Add(4);
                gameEnd = true; //end the game
            }
            //second row on grid [5][6][7][8][9]
            if (marks[5].MarkValue != Symbol.Blank && (marks[6].MarkValue == marks[5].MarkValue) && (marks[7].MarkValue == marks[5].MarkValue) && (marks[8].MarkValue == marks[5].MarkValue) && (marks[9].MarkValue == marks[5].MarkValue))
            {
                winners.Add(5);
                winners.Add(6);
                winners.Add(7);
                winners.Add(8);
                winners.Add(9);
                gameEnd = true; //end the game
            }
            //third row on grid [10][11][12][13][14]
            if (marks[10].MarkValue != Symbol.Blank && (marks[11].MarkValue == marks[10].MarkValue) && (marks[12].MarkValue == marks[10].MarkValue) && (marks[13].MarkValue == marks[10].MarkValue) && (marks[14].MarkValue == marks[10].MarkValue))
            {
                winners.Add(10);
                winners.Add(11);
                winners.Add(12);
                winners.Add(13);
                winners.Add(14);
                gameEnd = true; //end the game
            }
            //fourth row on grid [15][16][17][18][19]
            if (marks[15].MarkValue != Symbol.Blank && (marks[16].MarkValue == marks[15].MarkValue) && (marks[17].MarkValue == marks[15].MarkValue) && (marks[18].MarkValue == marks[15].MarkValue) && (marks[19].MarkValue == marks[15].MarkValue))
            {
                winners.Add(15);
                winners.Add(16);
                winners.Add(17);
                winners.Add(18);
                winners.Add(19);
                gameEnd = true; //end the game
            }
            //fifth row on grid [20][21][22][23][24]
            if (marks[20].MarkValue != Symbol.Blank && (marks[21].MarkValue == marks[20].MarkValue) && (marks[22].MarkValue == marks[20].MarkValue) && (marks[23].MarkValue == marks[20].MarkValue) && (marks[24].MarkValue == marks[20].MarkValue))
            {
                winners.Add(20);
                winners.Add(21);
                winners.Add(22);
                winners.Add(23);
                winners.Add(24);
                gameEnd = true; //end the game
            }
            //check coloumn line
            //first column on grid [0][5][10][15][20]
            if (marks[0].MarkValue != Symbol.Blank && (marks[5].MarkValue == marks[0].MarkValue) && (marks[10].MarkValue == marks[0].MarkValue) && (marks[15].MarkValue == marks[0].MarkValue) && (marks[20].MarkValue == marks[0].MarkValue))
            {
                winners.Add(0);
                winners.Add(5);
                winners.Add(10);
                winners.Add(15);
                winners.Add(20);
                gameEnd = true; //end the game
            }
            //second column on grid [1][6][11][16][21]
            if (marks[1].MarkValue != Symbol.Blank && (marks[6].MarkValue == marks[1].MarkValue) && (marks[11].MarkValue == marks[1].MarkValue) && (marks[16].MarkValue == marks[1].MarkValue) && (marks[21].MarkValue == marks[1].MarkValue))
            {
                winners.Add(1);
                winners.Add(6);
                winners.Add(11);
                winners.Add(16);
                winners.Add(21);
                gameEnd = true; //end the game
            }
            //third column on grid [2][7][12][17][22]
            if (marks[2].MarkValue != Symbol.Blank && (marks[7].MarkValue == marks[2].MarkValue) && (marks[12].MarkValue == marks[2].MarkValue) && (marks[17].MarkValue == marks[2].MarkValue) && (marks[22].MarkValue == marks[2].MarkValue))
            {
                winners.Add(2);
                winners.Add(7);
                winners.Add(12);
                winners.Add(17);
                winners.Add(22);
                gameEnd = true; //end the game
            }
            //fourth column on grid [3][8][13][18][23]
            if (marks[3].MarkValue != Symbol.Blank && (marks[8].MarkValue == marks[3].MarkValue) && (marks[13].MarkValue == marks[3].MarkValue) && (marks[18].MarkValue == marks[3].MarkValue) && (marks[23].MarkValue == marks[3].MarkValue))
            {
                winners.Add(3);
                winners.Add(8);
                winners.Add(13);
                winners.Add(18);
                winners.Add(23);
                gameEnd = true; //end the game
            }
            //fifth column on grid [4][9][14][19][24]
            if (marks[4].MarkValue != Symbol.Blank && (marks[9].MarkValue == marks[4].MarkValue) && (marks[14].MarkValue == marks[4].MarkValue) && (marks[19].MarkValue == marks[4].MarkValue) && (marks[24].MarkValue == marks[4].MarkValue))
            {
                winners.Add(4);
                winners.Add(9);
                winners.Add(14);
                winners.Add(19);
                winners.Add(24);
                gameEnd = true; //end the game
            }
            //check diagonal line
            //top left to bottom right [0][6][12][18][24]
            if (marks[0].MarkValue != Symbol.Blank && (marks[6].MarkValue == marks[0].MarkValue) && (marks[12].MarkValue == marks[0].MarkValue) && (marks[18].MarkValue == marks[0].MarkValue) && (marks[24].MarkValue == marks[0].MarkValue))
            {
                winners.Add(0);
                winners.Add(6);
                winners.Add(12);
                winners.Add(18);
                winners.Add(24);
                gameEnd = true; //end the game
            }
            //top right to bottom left [4][8][12][16][20]
            if (marks[4].MarkValue != Symbol.Blank && (marks[8].MarkValue == marks[4].MarkValue) && (marks[12].MarkValue == marks[4].MarkValue) && (marks[16].MarkValue == marks[4].MarkValue) && (marks[20].MarkValue == marks[4].MarkValue))
            {
                winners.Add(4);
                winners.Add(8);
                winners.Add(12);
                winners.Add(16);
                winners.Add(20);
                gameEnd = true; //end the game
            }
            //if no more blank cell is left
            if (!marks.Any(mark => mark.MarkValue == Symbol.Blank))
            {
                result = "Tie!"; //game is tied
                gameEnd = true;
            }
            //if the winners returns a null then its a tie
            if (winners.Count != 0)
            {
                Console.WriteLine($"Game #{objectId} Won!");
                CountScores();
            }
            winningCells = winners;
            UpdateAllClients(gameEnd);
        }
        /*Method Name: CountScores
         *Purpose: count points of each player based on conditions 
         *Accepts: nothing
         *Returns: void
         */
        public void CountScores()
        {
            if (gameEnd && playerOneTurn)
            {
                playerTwoScore += 1;
                result = "Player Two Won!";
            }
            else if (gameEnd && !playerOneTurn)
            {
                playerOneScore += 1;
                result = "Player One Won!";
            }
        }
        /*Method Name: CreateNewGame
         *Purpose: creates a new game  
         *Accepts: nothing
         *Returns: void
         */
        public void CreateNewGame()
        {
            Console.WriteLine($"GamePlay #{objectId} left.");
            Repopulate();
            playerOneScore = 0;
            playerTwoScore = 0;
        }
        // Helper methods
        /*Method Name: Repopulate
         *Purpose: repopulate and resets values  
         *Accepts: nothing
         *Returns: void
         */
        public void Repopulate()
        {
            //clear all marks in current game
            marks.Clear();
            //create a list of blank cells (25 cells)
            for (int i = 0; i < 25; ++i)
            {
                marks.Add(new Mark(Symbol.Blank, i));
            }
            //resets member variables
            playerOneTurn = true;
            result = "";
            gameEnd = false;
            selectedMark = null;
            UpdateAllClients(gameEnd);
        }
        /*Method Name: UpdateAllClients
         *Purpose: updates clients if there are any changes made  
         *Accepts: bool
         *Returns: void
         */
        public void UpdateAllClients(bool gameEnd)
        {

            CallbackInfo info = new CallbackInfo(gameEnd, playerOneTurn, playerOneScore, playerTwoScore, result, selectedMark, winningCells);

            foreach (ICallback cb in callbacks)
            {
                cb.UpdateGui(info);
            }
        }
        public void RegisterForCallbacks()
        {
            // Identify which client is calling this method
            ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();

            // Add the client object to the callbacks collection
            if (!callbacks.Contains(cb))
            {
                callbacks.Add(cb);
            }
        }
        public void UnregisterFromCallbacks()
        {
            // Identify which client is calling this method
            ICallback cb = OperationContext.Current.GetCallbackChannel<ICallback>();

            // Remove the client object from the callbacks collection
            if (callbacks.Contains(cb))
            {
                callbacks.Remove(cb);
            }
        }
    }
}