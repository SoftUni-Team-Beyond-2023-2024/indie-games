using System;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

public class Board
{
    //num for every field
    char[,] fieldsForAMove = new char[3, 3] { {'1', '2', '3'},
                                              {'4', '5', '6'},
                                              {'7', '8', '9'} };

    public void SetField(int currentSelectedRow, int currentSelectedColumn, char symbol)
    {
        fieldsForAMove[currentSelectedRow, currentSelectedColumn] = symbol;
    }

    public List<int> FreeFields()
    {
        List<int> freeFields = new List<int>();
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (fieldsForAMove[i, j] != 'X' &&
                    fieldsForAMove[i, j] != 'O')
                {
                    freeFields.Add(i * 3 + j + 1);
                }

            }
        }
        return freeFields;
    }

    public int GetResult()
    {
        if(GetResultHelper('X') == 1)
        {
            return 1;
        }else if(GetResultHelper('O') == 1)
        {
            return -1;
        }else
        {
            int counter = 0;
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (fieldsForAMove[i, j] != 'X' &&
                        fieldsForAMove[i, j] != 'O')
                    {
                        counter++;
                    }

                }
            }

            if (counter == 0)
            {
                return 0;
            }
            return 2;
        } 
        
    }

    private int GetResultHelper(char symbol)
    {
        for (int i = 0; i < 3; i++)
        {
            // Check rows
            if (fieldsForAMove[i, 0] == symbol && fieldsForAMove[i, 1] == symbol && fieldsForAMove[i, 2] == symbol)
            {
                return 1;
            }

            // Check columns
            if (fieldsForAMove[0, i] == symbol && fieldsForAMove[1, i] == symbol && fieldsForAMove[2, i] == symbol)
            {
                return 1;
            }
        }

        // Check diagonals
        if (fieldsForAMove[0, 0] == symbol && fieldsForAMove[1, 1] == symbol && fieldsForAMove[2, 2] == symbol)
        {
            return 1;
        }

        if (fieldsForAMove[0, 2] == symbol && fieldsForAMove[1, 1] == symbol && fieldsForAMove[2, 0] == symbol)
        {
            return 1;
        }

        return 0;
    }

    /*
    public int getFirstEmpty()
    {
        for(int i = 0; i < 3; i++)
        {
            for(int j = 0; j < 3; j++)
            {
                if (fieldsForAMove[i,j] != 'X' && fieldsForAMove[i,j] != 'O' )
                {
                    return i * 3 + j+1;
                }
            }
        }
        return -1;
    }
    */

    public int getRandomEmpty()
    {
        //checking for at least 1 empty field
        int counter = 0;
        for( int i = 0;i < 3;i++)
        {
            for (int j = 0;j < 3;j++)
            {
                if (fieldsForAMove[i,j] != 'X' &&
                    fieldsForAMove[i, j] != 'O')
                {
                    counter++;
                }
                
            }
        }

        if (counter == 0)
        {
            return -1;
        }
        else
        {
            while (true)
            {
                Random random = new Random();
                int randomRow = random.Next(3);
                int randomColumn = random.Next(3);
                if (fieldsForAMove[randomRow, randomColumn] != 'X' &&
                    fieldsForAMove[randomRow, randomColumn] != 'O')
                {
                    return randomRow * 3 + randomColumn + 1;
                }
            }
        }
        
    }

    public int getThirdField()
    {
        //checking for at least 1 empty field
        int counter = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (fieldsForAMove[i, j] != 'X' &&
                    fieldsForAMove[i, j] != 'O')
                {
                    counter++;
                }

            }
        }

        if (counter == 0)
        {
            return -1;
        }
        else
        {
            // Check for computer's potential winning move (two in a row)
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (fieldsForAMove[i, j] == 'O')
                    {
                        // Check horizontally for two 'O's
                        if (j < 2 && fieldsForAMove[i, j + 1] == 'O' && fieldsForAMove[i, (j + 2) % 3] >= '1' && fieldsForAMove[i, (j + 2) % 3] <= '9')
                        {
                            return fieldsForAMove[i, (j + 2) % 3] - '0';
                        }
                        // Check vertically for two 'O's
                        if (i < 2 && fieldsForAMove[i + 1, j] == 'O' && fieldsForAMove[(i + 2) % 3, j] >= '1' && fieldsForAMove[(i + 2) % 3, j] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, j] - '0';
                        }
                        // Check diagonally (from top-left to bottom-right) for two 'O's
                        if (i == j && i < 2 && fieldsForAMove[i + 1, j + 1] == 'O' && fieldsForAMove[(i + 2) % 3, (j + 2) % 3] >= '1' && fieldsForAMove[(i + 2) % 3, (j + 2) % 3] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, (j + 2) % 3] - '0';
                        }
                        // Check diagonally (from top-right to bottom-left) for two 'O's
                        if (i + j == 2 && i < 2 && fieldsForAMove[i + 1, j - 1] == 'O' && fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] >= '1' && fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] - '0';
                        }
                    }
                }
            }

            // Check for human's potential winning move (two in a row) and block it
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (fieldsForAMove[i, j] == 'X')
                    {
                        // Check horizontally for two 'X's
                        if (j < 2 && fieldsForAMove[i, j + 1] == 'X' && fieldsForAMove[i, (j + 2) % 3] >= '1' && fieldsForAMove[i, (j + 2) % 3] <= '9')
                        {
                            return fieldsForAMove[i, (j + 2) % 3] - '0';
                        }
                        // Check vertically for two 'X's
                        if (i < 2 && fieldsForAMove[i + 1, j] == 'X' && fieldsForAMove[(i + 2) % 3, j] >= '1' && fieldsForAMove[(i + 2) % 3, j] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, j] - '0';
                        }
                        // Check diagonally (from top-left to bottom-right) for two 'X's
                        if (i == j && i < 2 && fieldsForAMove[i + 1, j + 1] == 'X' && fieldsForAMove[(i + 2) % 3, (j + 2) % 3] >= '1' && fieldsForAMove[(i + 2) % 3, (j + 2) % 3] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, (j + 2) % 3] - '0';
                        }
                        // Check diagonally (from top-right to bottom-left) for two 'X's
                        if (i + j == 2 && i < 2 && fieldsForAMove[i + 1, j - 1] == 'X' && fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] >= '1' && fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] <= '9')
                        {
                            return fieldsForAMove[(i + 2) % 3, (j - 2 + 3) % 3] - '0';
                        }
                    }
                }
            }

            // If no winning move is found, return a random empty field
            return getRandomEmpty();
        }
    }



    public void DrawBoard()
    {
        //X and O colors
        ConsoleColor xColor = ConsoleColor.Red;
        ConsoleColor oColor = ConsoleColor.Blue;
        // Draw X and O
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"First  : ");
        Console.ForegroundColor = xColor;
        Console.Write($"X");

        Console.WriteLine("");

        Console.ForegroundColor = ConsoleColor.White;
        Console.Write($"Second : ");
        Console.ForegroundColor = oColor;
        Console.Write($"O");
        Console.ForegroundColor = ConsoleColor.White;

        Console.WriteLine("");
        //board
        Console.WriteLine($"-------------");
        // Print the board with colors
        PrintCell(0, 0, xColor, oColor);
        PrintCell(0, 1, xColor, oColor);
        PrintCell(0, 2, xColor, oColor);
        Console.Write("|");
        Console.WriteLine("");
        Console.WriteLine($"-------------");
        PrintCell(1, 0, xColor, oColor);
        PrintCell(1, 1, xColor, oColor);
        PrintCell(1, 2, xColor, oColor);
        Console.Write("|");
        Console.WriteLine("");
        Console.WriteLine($"-------------");
        PrintCell(2, 0, xColor, oColor);
        PrintCell(2, 1, xColor, oColor);
        PrintCell(2, 2, xColor, oColor);
        Console.Write("|");
        Console.WriteLine("");
        Console.WriteLine($"-------------");
        Console.WriteLine("");
    }

    private void PrintCell(int row, int column, ConsoleColor xColor, ConsoleColor oColor)
    {
        Console.Write("| ");
        if (fieldsForAMove[row, column] == 'X')
        {
            Console.ForegroundColor = xColor;
            Console.Write(fieldsForAMove[row, column]);
        }
        else if (fieldsForAMove[row, column] == 'O')
        {
            Console.ForegroundColor = oColor;
            Console.Write(fieldsForAMove[row, column]);
        }
        else
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(fieldsForAMove[row, column]);
        }
        Console.ForegroundColor = ConsoleColor.White;
        Console.Write(" ");
    }
}

