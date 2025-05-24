 using System;

namespace TicTacToe
{
    class Program
    {
        static char[,] board = new char[3, 3]; // Игровое поле 3x3
        static char currentPlayer = 'X'; // Текущий игрок (X или O)
        static int lastRow = -1, lastCol = -1; // Для подсветки последнего хода

        static void Main(string[] args)
        {
            InitializeBoard();
            bool gameOver = false;

            Console.WriteLine("Добро пожаловать в Крестики-нолики!");
            Console.WriteLine("Игрок X (красный) ходит первым. Вводите координаты (строка, столбец) от 0 до 2.");
            Console.WriteLine("Нажмите любую клавишу, чтобы начать...");
            Console.ReadKey(); // Ждём нажатия перед стартом
            
            while (!gameOver)
            {
                Console.Clear(); // Очищаем консоль перед каждым ходом
                PrintBoard(); // Выводим только поле
                Console.WriteLine($"Ход игрока {currentPlayer}");

                int row, col;
                GetPlayerInput(out row, out col);

                if (IsValidMove(row, col))
                {
                    board[row, col] = currentPlayer;
                    lastRow = row;
                    lastCol = col;

                    if (CheckWin())
                    {
                        PrintBoard();
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine($"Игрок {currentPlayer} победил!");
                        Console.ResetColor();
                        gameOver = true;
                    }
                    else if (IsBoardFull())
                    {
                        PrintBoard();
                        Console.WriteLine("Ничья!");
                        gameOver = true;
                    }
                    else
                    {
                        SwitchPlayer();
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Некорректный ход. Попробуйте ещё раз.");
                    Console.ResetColor();
                }
            }

            Console.WriteLine("Игра окончена. Нажмите любую клавишу для выхода...");
            Console.ReadKey();
        }
       
        // Инициализация пустого поля
        static void InitializeBoard()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    board[i, j] = '-';
                }
            }
        }

        // Вывод поля в консоль
        static void PrintBoard()
        {
            Console.Clear();
            Console.WriteLine("  0 1 2");
            for (int i = 0; i < 3; i++)
            {
                Console.Write($"{i} ");
                for (int j = 0; j < 3; j++)
                {
                    if (i == lastRow && j == lastCol) // Подсветка последнего хода серым
                    {
                        Console.BackgroundColor = ConsoleColor.DarkGray;
                    }

                    if (board[i, j] == 'X')
                    {
                        Console.ForegroundColor = ConsoleColor.Red; // Крестики красные
                    }
                    else if (board[i, j] == 'O')
                    {
                        Console.ForegroundColor = ConsoleColor.Blue; // Нолики синие
                    }

                    Console.Write($"{board[i, j]} ");
                    Console.ResetColor();
                }
                Console.WriteLine();
            }
        }

        // Получение хода от игрока
        static void GetPlayerInput(out int row, out int col)
        {
            Console.Write("Введите номер строки (0-2): ");
            while (!int.TryParse(Console.ReadLine(), out row) || row < 0 || row > 2)
            {
                Console.Write("Ошибка! Введите число от 0 до 2: ");
            }

            Console.Write("Введите номер столбца (0-2): ");
            while (!int.TryParse(Console.ReadLine(), out col) || col < 0 || col > 2)
            {
                Console.Write("Ошибка! Введите число от 0 до 2: ");
            }
        }

        // Проверка на допустимость хода
        static bool IsValidMove(int row, int col)
        {
            return board[row, col] == '-';
        }

        // Проверка победы
        static bool CheckWin()
        {
            // Проверка строк и столбцов
            for (int i = 0; i < 3; i++)
            {
                if (board[i, 0] == currentPlayer && board[i, 1] == currentPlayer && board[i, 2] == currentPlayer)
                    return true;

                if (board[0, i] == currentPlayer && board[1, i] == currentPlayer && board[2, i] == currentPlayer)
                    return true;
            }

            // Проверка диагоналей
            if (board[0, 0] == currentPlayer && board[1, 1] == currentPlayer && board[2, 2] == currentPlayer)
                return true;

            if (board[0, 2] == currentPlayer && board[1, 1] == currentPlayer && board[2, 0] == currentPlayer)
                return true;

            return false;
        }

        // Проверка на ничью (все клетки заполнены)
        static bool IsBoardFull()
        {
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    if (board[i, j] == '-')
                        return false;
                }
            }
            return true;
        }

        // Смена игрока
        static void SwitchPlayer()
        {
            currentPlayer = (currentPlayer == 'X') ? 'O' : 'X';
        }
    }
}