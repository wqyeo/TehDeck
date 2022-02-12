using TehDeck;

namespace Example {
    public class JokerCard : Card {

        public JokerCard() {
            Name = "Joker";
        }

        public override bool IsEqualTo(Card other) {
            return other.GetType() == typeof(JokerCard);
        }
    }
}
