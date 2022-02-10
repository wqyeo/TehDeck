using System;
using System.Collections.Generic;
using System.Text;

namespace TehDeck {
    /// <summary>
    /// Manages how a deck will be created.
    /// </summary>
    [System.Serializable]
    public class DeckInfo {

        private List<CardInfo> cardInfos;

        /// <summary>
        /// How many cards will there be in this deck.
        /// </summary>
        public int CardCount {
            get; set;
        }

        public DeckInfo(int _cardCount) {
            cardInfos = new List<CardInfo>();
            CardCount = _cardCount;

            if (CardCount <= 0) {
                throw new ArgumentException("CardCount should be a positive number.", "_cardCount");
            }
        }

        public DeckInfo(int _cardCount, IList<CardInfo> _cardInfos) {
            cardInfos = (List<CardInfo>) _cardInfos;
            CardCount = _cardCount;

            if (CardCount <= 0) {
                throw new ArgumentException("CardCount should be a positive number.", "_cardCount");
            } else if (cardInfos == null) {
                throw new ArgumentNullException("_cardInfos");
            }
        }

        public void AddCardInfo(CardInfo cardInfo) {
            if (TryRemoveFirstDuplicate(cardInfo.CardRef)) {
                // TODO: Warn about replaced
            }

            cardInfos.Add(cardInfo);
        }

        public void AddCardInfo(Card cardRef, int uniqueCount) {
            AddCardInfo(new CardInfo(cardRef, uniqueCount));
        }

        public bool RemoveCardInfo(Card cardRef) {
            return TryRemoveFirstDuplicate(cardRef);
        }

        public CardInfo[] GetCardInfos() {
            return cardInfos.ToArray();
        }

        public CardInfo[] GetCardInfosByCondition(Predicate<CardInfo> condition) {
            List<CardInfo> matching = new List<CardInfo>();
            foreach (var item in cardInfos) {
                if (condition(item)) {
                    matching.Add(item);
                }
            }

            return matching.ToArray();
        }

        private bool TryRemoveFirstDuplicate(Card cardRef) {

            for (int i = 0; i < cardInfos.Count; i++) {
                if (cardInfos[i].CardRef.IsEqualTo(cardRef)) {
                    cardInfos.RemoveAt(i);
                    return true;
                }
            }
            return false;
        }
    }
}
