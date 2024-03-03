// Define RoundManager object
const RoundManager = {
    create(southPlayer, eastPlayer, northPlayer, westPlayer) {
        return {
            playRound(roundNumber, firstInRound, southNorthPoints, eastWestPoints, hangingPoints) {
                return {
                    southNorthPoints: 0,
                    eastWestPoints: 0,
                    hangingPoints: 0,
                    noTricksForOneOfTheTeams: false,
                    contract: { type: 'Contract' }
                };
            }
        };
    }
};

// Define bidTapped function
function bidTapped(belotGame, bidType) {
    document.getElementById("bidsPanel").style.visibility = "collapse";
    BelotGame.player.getBidAction = bidType;
}

// Define BelotGame object
const BelotGame = {
    create(southPlayer, eastPlayer, northPlayer, westPlayer) {
        const roundManager = RoundManager.create(southPlayer, eastPlayer, northPlayer, westPlayer);
        const players = [southPlayer, eastPlayer, northPlayer, westPlayer];

        function playGame(firstToPlay = PlayerPosition.South) {
            let southNorthPoints = 0;
            let eastWestPoints = 0;
            let firstInRound = firstToPlay;
            let roundNumber = 1;
            let hangingPoints = 0;
        
            while (true) {
                const roundResult = roundManager.playRound(
                    roundNumber,
                    firstInRound,
                    southNorthPoints,
                    eastWestPoints,
                    hangingPoints
                );
        
                // Update points and other game state based on round result
                southNorthPoints += roundResult.southNorthPoints;
                eastWestPoints += roundResult.eastWestPoints;
                hangingPoints = roundResult.hangingPoints;
        
                // Check if game over conditions are met
                if (southNorthPoints >= 151 && southNorthPoints > eastWestPoints) {
                    // Game over - south-north team wins
                    break;
                }
        
                if (eastWestPoints >= 151 && eastWestPoints > southNorthPoints) {
                    // Game over - east-west team wins
                    break;
                }
        
                // Update round number and first player for the next round
                roundNumber++;
                firstInRound = PlayerPositionExtensions.Next(firstInRound);
            }
        
            const gameResult = {
                roundsPlayed: roundNumber,
                southNorthPoints: southNorthPoints,
                eastWestPoints: eastWestPoints
            };
        
            players.forEach(player => player.endOfGame(gameResult));
        
            return gameResult;
        }
        

        return {
            playGame
        };
    }
};

const playerContext = new BasePlayerContext();
const southPlayer = new IPlayer();
const eastPlayer = new IPlayer();
const northPlayer = new IPlayer();
const westPlayer = new IPlayer();

const belotGame = BelotGame.create(southPlayer, eastPlayer, northPlayer, westPlayer);

function openCardsCheckBoxTapped() {
    updateOtherPlayerCards();
}

function updateOtherPlayerCards() {
    console.log(belotGame)
    const players = [belotGame.eastPlayer, belotGame.northPlayer, belotGame.westPlayer];
    players.forEach((player, index) => {
        const playerPanel = document.getElementById(["East", "North", "West"][index] + "CardsPanel");
        playerPanel.innerHTML = "";
        player.myCards.forEach((card, cardIndex) => {
            const cardControl = document.createElement("div");
            cardControl.classList.add("card-control");
            cardControl.textContent = card.toString();
            cardControl.style.marginTop = (cardIndex === 0 ? 0 : -80) + "px";
            if (document.getElementById("OpenCardsCheckBox").checked) {
                cardControl.style.width = "100px";
            } else {
                cardControl.style.width = "0";
                cardControl.style.height = "0";
                cardControl.style.padding = "0";
                cardControl.style.margin = "0";
                cardControl.style.overflow = "hidden";
            }
            playerPanel.appendChild(cardControl);
        });
    });
}


window.addEventListener("DOMContentLoaded", () => {
 document.getElementById("openCardsCheckBox").addEventListener("click", openCardsCheckBoxTapped);
document.getElementById("buttonPass").addEventListener("click", (event) => {
    bidTapped("Pass")
});



})
// Start the game
// belotGame.playGame(PlayerPosition)

