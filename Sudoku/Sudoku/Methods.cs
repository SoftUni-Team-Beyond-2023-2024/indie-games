using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SudokuGame
{
    internal class Methods
    {

        static void Main()
        {
            int[,] board = GenerateSudoku();
            bool[,] userEntered = new bool[9, 9];
            PlaySudoku(board, userEntered);

            // Wait for a key press before closing the console window
            Console.ReadLine();
        }
        
        static bool GenerateSudokuHelper(int[,] board)
        {
            //test rep
            Random random = new Random();

            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        List<int> nums = new List<int> { 1, 2, 3, 4, 5, 6, 7, 8, 9 };
                        Shuffle(nums);

                        foreach (int num in nums)
                        {
                            if (IsValidMove(board, row, col, num))
                            {
                                board[row, col] = num;

                                if (GenerateSudokuHelper(board))
                                    return true;

                                board[row, col] = 0;
                            }
                        }

                        return false; // No valid move
                    }
                }
            }

            return true; // Sudoku generated successfully
        }

        // Shuffle elements in a list
        public static void Shuffle<T>(List<T> list)
        {
            Random random = new Random();
            int n = list.Count;
            for (int i = n - 1; i > 0; i--)
            {
                int j = random.Next(0, i + 1);
                T temp = list[i];
                list[i] = list[j];
                list[j] = temp;
            }
        }

        // Generate initial Sudoku state
        protected static int[,] GenerateSudoku()
        {
            int[,] sudoku = new int[9, 9];
            GenerateSudokuHelper(sudoku);
            return sudoku;
        }

        // Main logic for playing Sudoku
        protected static void PlaySudoku(int[,] board, bool[,] userEntered)
        {
            Console.WriteLine("Wtite a type of dificulty you want(Easy, Medium or Hard).");
            String commnad = Console.ReadLine().ToLower();

            switch (commnad)
            {

                case "easy":
                    RemoveNumbers(board, 24);
                    break;
                case "medium":
                    RemoveNumbers(board, 39);
                    break;
                case "hard":
                    RemoveNumbers(board, 61);
                    break;
            }

            do
            {
                Console.Clear();
                PrintBoard(board, userEntered);

                Console.Write("Enter row (1-9): ");
                if (!int.TryParse(Console.ReadLine(), out int row) || row < 1 || row > 9)
                {
                    Console.WriteLine("Invalid row! Try again.");
                    Console.ReadLine();
                    continue;
                }

                Console.Write("Enter column (1-9): ");
                if (!int.TryParse(Console.ReadLine(), out int col) || col < 1 || col > 9)
                {
                    Console.WriteLine("Invalid column! Try again.");
                    Console.ReadLine();
                    continue;
                }

                // Check and validate the move
                if (board[row - 1, col - 1] == 0)
                {
                    Console.Write("Enter number (1-9): ");
                    if (!int.TryParse(Console.ReadLine(), out int num) || num < 1 || num > 9)
                    {
                        Console.WriteLine("Invalid number! Try again.");
                        Console.ReadLine();
                        continue;
                    }

                    if (!userEntered[row - 1, col - 1] && IsValidMove(board, row - 1, col - 1, num))
                    {
                        board[row - 1, col - 1] = num;
                        userEntered[row - 1, col - 1] = true; // Mark the number as user-entered
                    }
                    else
                    {
                        Console.WriteLine("Invalid move! Try again.");
                        Console.ReadLine(); // Pause to display the message
                    }
                }
                else
                {
                    Console.WriteLine("This cell cannot be changed. Try again.");
                    Console.ReadLine();
                }

            } while (!IsSudokuSolved(board));

            Console.Clear();
            PrintBoard(board, userEntered);
            Console.WriteLine("Congratulations! Sudoku solved!");
        }

        // Check the validity of a move
        public static bool IsValidMove(int[,] board, int row, int col, int num)
        {
            return IsRowValid(board, row, num) && IsColValid(board, col, num) && IsBoxValid(board, row - row % 3, col - col % 3, num);
        }

        // Check the validity of numbers in a row
        static bool IsRowValid(int[,] board, int row, int num)
        {
            for (int col = 0; col < 9; col++)
            {
                if (board[row, col] == num)
                    return false;
            }
            return true;
        }

        // Check the validity of numbers in a column
        static bool IsColValid(int[,] board, int col, int num)
        {
            for (int row = 0; row < 9; row++)
            {
                if (board[row, col] == num)
                    return false;
            }
            return true;
        }

        // Check the validity of numbers in a 3x3 box
        static bool IsBoxValid(int[,] board, int startRow, int startCol, int num)
        {
            for (int row = 0; row < 3; row++)
            {
                for (int col = 0; col < 3; col++)
                {
                    if (board[startRow + row, startCol + col] == num)
                        return false;
                }
            }
            return true;
        }

        // Check if Sudoku is solved (no empty cells)
        static bool IsSudokuSolved(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                        return false;
                }
            }
            return true;
        }

        // Display the state of Sudoku
        static void PrintBoard(int[,] board, bool[,] userEntered)
        {
            Console.Clear();

            ConsoleColor def = ConsoleColor.White;

            for (int i = 0; i < 9; i++)
            {
                for (int j = 0; j < 9; j++)
                {
                    if (board[i, j] == 0)
                    {
                        Console.Write("  ");
                    }
                    else
                    {
                        // Use a different color for the user-entered numbers
                        Console.ForegroundColor = (userEntered[i, j]) ? ConsoleColor.Blue : ConsoleColor.White;
                        Console.Write($"{board[i, j]} ");
                    }

                    Console.ForegroundColor = def;

                    if (j == 2 || j == 5) Console.Write("| ");
                }

                Console.WriteLine();

                if (i == 2 || i == 5)
                    Console.WriteLine("------+-------+------");
            }

            Console.WriteLine();
        }

        // Remove numbers from Sudoku to adjust difficulty
        static void RemoveNumbers(int[,] board, int count)
        {
            Random random = new Random();

            while (count > 0)
            {
                int row = random.Next(9);
                int col = random.Next(9);

                if (board[row, col] != 0)
                {
                    int temp = board[row, col];
                    board[row, col] = 0;

                    // Check if only one possible number is left to fill
                    if (!IsValidMove(board, row, col, temp) || !IsUniqueSolution(board))
                    {
                        board[row, col] = temp; // Restore the number
                        count--;
                    }
                    else
                    {
                        count--;
                    }
                }
            }
        }

        // Check if there is a unique solution for Sudoku
        static bool IsUniqueSolution(int[,] board)
        {
            int[,] tempBoard = new int[9, 9];
            Array.Copy(board, tempBoard, board.Length);
            return SolveSudoku(tempBoard);
        }

        // Solve Sudoku using backtracking
        static bool SolveSudoku(int[,] board)
        {
            for (int row = 0; row < 9; row++)
            {
                for (int col = 0; col < 9; col++)
                {
                    if (board[row, col] == 0)
                    {
                        for (int num = 1; num <= 9; num++)
                        {
                            if (IsValidMove(board, row, col, num))
                            {
                                board[row, col] = num;

                                if (SolveSudoku(board))
                                    return true;

                                board[row, col] = 0;
                            }
                        }

                        return false; // No valid move
                    }
                }
            }

            return true; // Sudoku solved successfully
        }
    }
}

