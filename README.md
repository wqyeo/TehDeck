TehDeck
=======

TehDeck is a .NET API designed to ease the creation of custom Cards and Decks for games.<br>
This project is part of [Nuget Package](https://www.nuget.org/packages/TehDeck/1.0.0).

## Usage

**Creating a custom Card**<br>Inherit from `Card` class, and override `IsEqualTo(Card)` function. It's that simple.

**Creating a Deck**<br>First, create `CardInfo` objects from your `Card` objects.<br>Just do a `new CardInfo(card, uniqueCount)` to create the object.
> `uniqueCount` means how many cards of this type can be in the deck upon creation.<br>A non-positive number signifies it doesn't have a unique count.

Now, bunch them up into a `IList<CardInfo>` and pass it to `DeckInfo` when creating the object.<br>
> `DeckInfo` constructor requires an Integer too, signifying how many cards will be in the deck upon creation.

Finally, create a `Deck` class, passing in the `DeckInfo` object you have created.

**Manipulating the deck**
<br>You can draw a card with`Deck.NextCard()`.<br>
You can peek the next card in the stack wtih `Deck.PeekCard()`.

There are functions for inserting cards into the deck, shuffling the deck and resetting the deck as well.

You can inherit the `Deck` class if you wish to add more functionality to your custom Deck of cards.

## Example

**Check Sample Project for the full source code**

```
            // Generate card information first.
            List<CardInfo> cardInfos = new List<CardInfo>();
            // There should only be 1 joker card.
            cardInfos.Add(new CardInfo(new JokerCard(), 1));
            // Fill the rest of the deck with blanks.
            cardInfos.Add(new CardInfo(new BlankCard()));

            // The deck will only have 6 cards, populated with the above card information.
            DeckInfo deckInfo = new DeckInfo(6, cardInfos);
            // Create the deck.
            deck = new Deck(deckInfo);```