using System.Runtime.Serialization;

namespace GomokuLibrary
{
    /**
      * Class Name: Mark.cs		
      * Purpose: A multiplayer game application which incorporates multiple assemblies and WCF
      * Coders: Haris Khalid, Woojin Shin, Sterling Gault, Sagar Thapa
      * Date: Apr 3rd, 2022
      */
    // Public types/enums
    public enum Symbol { X, O, Blank };
    [DataContract]
    public class Mark
    {
        // Public properties and methods
        [DataMember]
        public Symbol MarkValue { get; private set; }
        [DataMember]
        public int CellPosition { get; private set; }
        // C'tor
        internal Mark(Symbol MarkValue, int CellPosition)
        {
            this.MarkValue = MarkValue;
            this.CellPosition = CellPosition;
        }
        public override string ToString()
        {
            return MarkValue.ToString();
        }
    }
}
