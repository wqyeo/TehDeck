using System;
using System.Collections.Generic;
using System.Linq;

namespace TehDeck {
    public class Deck {

        public DeckInfo DeckInfo {
            get; protected set;
        }

        protected Stack<Card> cards;

        public Deck(DeckInfo _deckInfo) {
            DeckInfo = _deckInfo;

            if (DeckInfo == null) {
                throw new ArgumentNullException("_deckInfo");
            }

            Generate();
        }

        protected void Generate() {
            cards = new Stack<Card>();

            CardInfo[] cardsWithUniqueCount = DeckInfo.GetCardInfosByCondition(CardHasUniqueCount);
            CardInfo[] cardsWithoutUniqueCount = DeckInfo.GetCardInfosByCondition(CardHasNoUniqueCount);

            if (!GenerateUniqueCards()) {
                // TODO: Warn about hitting deck limit before generating all unique count cards.
                cards = Shuffle(cards);
                return;
            }

            GenerateNonUniqueCards();
            cards = Shuffle(cards);

            #region Local_Function

            void GenerateNonUniqueCards() {
                while (!HitDeckLimit()) {

                    foreach (var item in cardsWithoutUniqueCount) {
                        if (HitDeckLimit()) {
                            break;
                        }

                        cards.Push(item.CardRef);
                    }
                }
            }

            // Returns false if we hit deck limit before generating all the unique cards.
            bool GenerateUniqueCards() {
                foreach (var item in cardsWithUniqueCount) {
                    for (int i = 0; i < item.UniqueCount; ++i) {
                        if (HitDeckLimit()) {
                            return false;
                        }

                        cards.Push(item.CardRef);
                    }
                }

                return true;
            }

            bool HitDeckLimit() {
                return cards.Count >= DeckInfo.CardCount;
            }

            bool CardHasUniqueCount(CardInfo cardInfo) {
                return cardInfo.UniqueCount > 0;
            }

            bool CardHasNoUniqueCount(CardInfo cardInfo) {
                return cardInfo.UniqueCount <= 0;
            }

            #endregion
        }

        public Stack<Card> Shuffle(Stack<Card> stack) {
            Random rnd = new Random();
            return new Stack<Card>(stack.OrderBy(x => rnd.Next()));
        }
    }
}
