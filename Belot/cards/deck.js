class Deck {
    constructor() {
        this._listOfCards = [...Card.AllCards];
        this._currentCardIndex = 0;
    }

    shuffle() {
        // Fisher-Yates shuffle algorithm
        for (let i = this._listOfCards.length - 1; i > 0; i--) {
            const j = Math.floor(Math.random() * (i + 1));
            [this._listOfCards[i], this._listOfCards[j]] = [this._listOfCards[j], this._listOfCards[i]];
        }
        this._currentCardIndex = 0;
    }

    getNextCard() {
        return this._listOfCards[this._currentCardIndex++];
    }
}
