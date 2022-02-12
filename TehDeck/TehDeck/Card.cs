namespace TehDeck {
    [System.Serializable]
    public abstract class Card {
        public string Name {
            get; protected set;
        }

        public abstract bool IsEqualTo(Card other);

        public static explicit operator CardInfo(Card other) {
            return new CardInfo(other);
        }
    }
}
