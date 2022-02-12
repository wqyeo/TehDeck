using TehDeck;

namespace Example {
    public class BlankCard : Card {

        public BlankCard() {
            Name = "Blank";
        }

        public override bool IsEqualTo(Card other) {
            return other.GetType() == typeof(BlankCard);
        }
    }
}
