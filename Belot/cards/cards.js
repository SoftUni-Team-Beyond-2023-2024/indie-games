const CardSuit = {
    Club: '♣',
    Diamond: '♦',
    Heart: '♥',
    Spade: '♠'
};

const CardType = {
    Seven: '7',
    Eight: '8',
    Nine: '9',
    Ten: '10',
    Jack: 'J',
    Queen: 'Q',
    King: 'K',
    Ace: 'A'
};

class Card {
    constructor(suit, type) {
        this._suit = suit;
        this._type = type;
        this._hashCode = suit * 8 + type;
        this._trumpOrder = [1, 2, 7, 5, 8, 3, 4, 6][type];
        this._noTrumpOrder = [1, 2, 3, 7, 4, 5, 6, 8][type];
    }

    get Suit() {
        return this._suit;
    }

    get Type() {
        return this._type;
    }

    get TrumpOrder() {
        return this._trumpOrder;
    }

    get NoTrumpOrder() {
        return this._noTrumpOrder;
    }

    static GetCard(suit, type) {
        return Card.AllCards[suit * 8 + type];
    }

    static get AllSuits() {
        return Object.values(CardSuit);
    }

    static get AllTypes() {
        return Object.values(CardType);
    }

    equals(other) {
        return other instanceof Card && this._hashCode === other._hashCode;
    }

    hashCode() {
        return this._hashCode;
    }

    toString() {
        return `${this._type}${this._suit}`;
    }
}

Card.AllCards = [];
for (let suit in CardSuit) {
    for (let type in CardType) {
        let card = new Card(CardSuit[suit], CardType[type]);
        Card.AllCards[card._hashCode] = card;
    }
}

function ToBidType(cardSuit) {
    switch (cardSuit) {
        case CardSuit.Club: return BidType.Clubs;
        case CardSuit.Diamond: return BidType.Diamonds;
        case CardSuit.Heart: return BidType.Hearts;
        case CardSuit.Spade: return BidType.Spades;
        default: return BidType.Pass;
    }
}

function GetValue(card, contract) {
    const values = contract & BidType.AllTrumps ? TrumpValues : NoTrumpValues;
    return values[card.Type];
}
