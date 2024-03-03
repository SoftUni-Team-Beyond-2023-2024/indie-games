const PlayerPosition = {
    South: 0,
    East: 1,
    North: 2,
    West: 3
};

const PlayerPositionExtensions = {
    Next(playerPosition) {
        return (playerPosition + 1) % 4;
    },

    Index(playerPosition) {
        return playerPosition === PlayerPosition.East ? 1 :
               playerPosition === PlayerPosition.North ? 2 :
               playerPosition === PlayerPosition.West ? 3 : 0;
    },

    IsInSameTeamWith(position, otherPlayerPosition) {
        return Math.abs(position - otherPlayerPosition) === 2 || position === otherPlayerPosition;
    },

    GetTeammate(playerPosition) {
        return (playerPosition + 2) % 4;
    }
};

// BasePlayerContext class
class BasePlayerContext {
    constructor() {
        this.roundNumber = 0;
        this.firstToPlayInTheRound = 0;
        this.myPosition = 0;
        this.southNorthPoints = 0;
        this.eastWestPoints = 0;
        this.myCards = [];
        this.bids = [];
        this.currentContract = null;
    }
}

// IPlayer interface
class IPlayer {
    getBid(context) {}
    getAnnounces(context) {}
    playCard(context) {}
    endOfTrick(trickActions) {}
    endOfRound(roundResult) {}
    endOfGame(gameResult) {}
}

// PlayCardAction class
class PlayCardAction {
    constructor(card, announceBeloteIfAvailable = true) {
        this.card = card;
        this.belote = announceBeloteIfAvailable;
        this.player = null;
        this.trickNumber = 0;
    }
}

// PlayerGetAnnouncesContext class
class PlayerGetAnnouncesContext extends BasePlayerContext {
    constructor() {
        super();
        this.announces = [];
        this.currentTrickActions = [];
        this.availableAnnounces = [];
    }
}

// PlayerGetBidContext class
class PlayerGetBidContext extends BasePlayerContext {
    constructor() {
        super();
        this.availableBids = 0;
    }
}

// PlayerPlayCardContext class
class PlayerPlayCardContext extends BasePlayerContext {
    constructor() {
        super();
        this.announces = [];
        this.currentTrickActions = [];
        this.roundActions = [];
        this.availableCardsToPlay = [];
        this.currentTrickNumber = 0;
    }
}







