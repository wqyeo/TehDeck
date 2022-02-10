namespace TehDeck {
    /// <summary>
    /// Information for this card to be used in a deck.
    /// </summary>
    [System.Serializable]
    public class CardInfo {

        public Card CardRef {
            get; set;
        }

        /// <summary>
        /// Negative for unlimited copies of this card.
        /// </summary>
        public int UniqueCount {
            get; set;
        }

        /// <summary>
        /// Information for this card to be used in a deck.
        /// </summary>
        /// <param name="_cardRef">Reference to the card.</param>
        /// <param name="_uniqueCount">Negative for unlimited copies of this card.</param>
        public CardInfo(Card _cardRef, int _uniqueCount) {
            CardRef = _cardRef;
            UniqueCount = _uniqueCount;
        }
    }
}
