namespace Sudoku
{
    using global::SudokuGame;
    
    using System;
    using System.Collections.Generic;
    internal class SudokuGame : methods
    internal class SudokuGame : Methods
    {
        static void Main()
        {
            int[,] board = GenerateSudoku();
            bool[,] userEntered = new bool[9, 9];
            PlaySudoku(board, userEntered);

            // Wait for a key press before closing the console window
            Console.ReadLine();
        }

          }
    }
