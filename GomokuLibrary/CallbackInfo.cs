using System.Collections.Generic;
using System.Runtime.Serialization;

namespace GomokuLibrary
{
    /**
      * Class Name: CallbackInfo.cs		
      * Purpose: A multiplayer game application which incorporates multiple assemblies and WCF
      * Coders: Haris Khalid, Woojin Shin, Sterling Gault, Sagar Thapa
      * Date: Apr 3rd, 2022
      */
    //Data contract
    [DataContract]
    public class CallbackInfo
    {
        //Data members
        [DataMember]
        public bool GameEnd { get; set; }
        [DataMember]
        public bool PlayerOneTurn { get; set; }
        [DataMember]
        public int PlayerOneScore { get; set; }
        [DataMember]
        public int PlayerTwoScore { get; set; }
        [DataMember]
        public string Results { get; set; }
        [DataMember]
        public Mark SelectedMark { get; private set; }
        [DataMember]
        public List<int> WinningCells { get; private set; }
        //C'tor
        public CallbackInfo(bool GameEnd, bool PlayerOneTurn, int PlayerOneScore, int PlayerTwoScore, string Results, Mark SelectedMark, List<int> WinningCells)
        {
            this.GameEnd = GameEnd;
            this.PlayerOneTurn = PlayerOneTurn;
            this.PlayerOneScore = PlayerOneScore;
            this.PlayerTwoScore = PlayerTwoScore;
            this.Results = Results;
            this.SelectedMark = SelectedMark;
            this.WinningCells = WinningCells;
        }
    }
}
