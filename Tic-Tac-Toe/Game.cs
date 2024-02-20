using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tic_Tac_Toe
{
    public class Game
    {
        public void PlayGame(string whichDifficulty, Board board)
        {
            switch (whichDifficulty)
            {
                /*
                case "hard":
                    isTheDifficultyCorrect = true;
                    break;

                case "medium":
                    isTheDifficultyCorrect = true;
                    break;
                */

                case "easy":
                    PlayGameEasy(board);
                    break;
                case "hard":
                    PlayGameHard(board);
                    break;

                default:
                    break;
            }
        }

        private int GetValidIndexFromInput(Board board)
        {
            Console.WriteLine($"Enter the field index:");
            int currentSelectedField;
            bool isIndexValid = false;
            do
            {
                currentSelectedField = int.Parse(Console.ReadLine());
                if (currentSelectedField >= 1 && currentSelectedField <= 9)
                {
                    List<int> freeFields = board.FreeFields();
                    for (int j = 0; j < freeFields.Count; j++)
                    {
                        if (freeFields[j] == currentSelectedField)
                        {
                            isIndexValid = true;
                        }
                    }
                }
                if(!isIndexValid)
                {
                    Console.WriteLine("Index not valid!");
                }
                
                
            } while (!isIndexValid);
            return currentSelectedField;
        }


        public void PlayGameEasy(Board board)
        {
            for (int i = 0; i < 9; i++)
            {

                if (i % 2 == 0)
                {
                    int currentSelectedField = GetValidIndexFromInput(board);
                    int currentSelectedRow = (currentSelectedField - 1) / 3;
                    int currentSelectedColumn = (currentSelectedField - 1) % 3;
                    board.SetField(currentSelectedRow, currentSelectedColumn, 'X');
                }
                else
                {
                    Thread.Sleep(1000);
                    int currentSelectedField = board.getRandomEmpty();
                    int currentSelectedRow = (currentSelectedField - 1) / 3;
                    int currentSelectedColumn = (currentSelectedField - 1) % 3;
                    board.SetField(currentSelectedRow, currentSelectedColumn, 'O');
                }
                Console.Clear();
                board.DrawBoard();
                int gameResult = board.GetResult();
                if(gameResult !=2)
                {
                    GameEnd(gameResult);
                    break;
                }
            }
        }

        public void PlayGameHard(Board board)
        {
            for (int i = 0; i < 9; i++)
            {

                if (i % 2 == 0)
                {
                    int currentSelectedField = GetValidIndexFromInput(board);
                    int currentSelectedRow = (currentSelectedField - 1) / 3;
                    int currentSelectedColumn = (currentSelectedField - 1) % 3;
                    board.SetField(currentSelectedRow, currentSelectedColumn, 'X');
                }
                else
                {
                    Thread.Sleep(1000);
                    int currentSelectedField = board.getThirdField();//this function will stop you if you have 2 in a row and will win if it has 2 in a row
                    int currentSelectedRow = (currentSelectedField - 1) / 3;
                    int currentSelectedColumn = (currentSelectedField - 1) % 3;
                    board.SetField(currentSelectedRow, currentSelectedColumn, 'O');
                }
                Console.Clear();
                board.DrawBoard();
                int gameResult = board.GetResult();
                if (gameResult != 2)
                {
                    GameEnd(gameResult);
                    break;
                }
            }
        }

        public void GameEnd(int gameResult)
        {
            switch (gameResult)
            {
                case -1:
                    Console.WriteLine($"You lost!");
                    break;
                case 0:
                    Console.WriteLine($"Draw!");
                    break;
                case 1:
                    Console.WriteLine($"You won!");
                    break;
                default:
                    break;
            }
        }
    }
}


