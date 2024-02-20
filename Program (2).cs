
using System.Security.Cryptography.X509Certificates;
using System.Threading.Channels;

namespace Tic_Tac_Toe
{
    internal class Program
    {
        static void Main(string[] args)
        {
            //DrawBoard();
            string oneMoreGame = "No";
            do
            {
                Console.Clear();
                Board board = new Board();
                Game game = new Game();
                string whichDifficulty = BeforeTheGameStarts(board);
                game.PlayGame(whichDifficulty, board);
                Console.WriteLine("Do you want to play again?");
                Console.WriteLine("Yes | No");
                oneMoreGame = Console.ReadLine();
            }while (oneMoreGame.ToLower() == "yes");
        }




        static string BeforeTheGameStarts(Board board)
        {
            board.DrawBoard();

            //chosing the difficulty
            Console.WriteLine($"easy | hard");
            Console.WriteLine($"");

            string whichDifficulty = "";// Easy | Hard
            bool isTheDifficultyCorrect = false;
            do
            {

                Console.WriteLine($"Select a difficulty:");
                whichDifficulty = Console.ReadLine().ToLower();
                switch (whichDifficulty)
                {
                    case "hard":
                        isTheDifficultyCorrect = true;
                        break;

                    case "easy":
                        isTheDifficultyCorrect = true;
                        break;

                    default:
                        break;
                }

                Console.Clear();

                board.DrawBoard();

            } while (isTheDifficultyCorrect == false);

            //chosing who's tern is it
            Random random = new Random();
            bool doesPlayerMoveFirst = true;//random.Next(0, 2) == 0;

            //output
            if (doesPlayerMoveFirst)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"You are with ");
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"X");
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write($"You are with ");
                Console.ForegroundColor = ConsoleColor.Blue;
                Console.WriteLine("O");
            }
            Console.ForegroundColor = ConsoleColor.White;


            return whichDifficulty;
        }        
    }
}