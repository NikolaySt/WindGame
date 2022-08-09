using System.Runtime.InteropServices;

namespace WindGame
{
    public static class Solution
    {
        public static int SumLeftLeavesAfterStorm(int[,] field, string winds)
        {
            var result = field.Clone() as int[,];
            foreach (var wind in winds)
            {
                result = wind switch
                {
                    'U' => ShiftUp(result),
                    'D' => ShiftDown(result),
                    'L' => ShiftLeft(result),
                    'R' => ShiftRight(result),
                    _ => result
                };

                //Console.WriteLine(wind);
                //Print(result);
            }

            var elementSize = Marshal.SizeOf(typeof(int));
            var rows = field.GetLength(0);
            var cols = field.GetLength(1);
            var unRolledResult = new int[rows * cols];
            Buffer.BlockCopy(result, 0, unRolledResult, 0, rows * cols * elementSize);
            return unRolledResult.Sum();
        }

        private static void Print(int[,] field)
        {
            Console.WriteLine("------------------------");
            for (var i = 0; i < field.GetLength(0); i++)
            {
                for (var j = 0; j < field.GetLength(1); j++)
                    Console.Write(field[i, j] + " ");
                Console.WriteLine();
            }
            Console.WriteLine("------------------------");
        }

        private static int GetElementSize<T>()
        {
            return Marshal.SizeOf(typeof(T));
        }

        private static (int, int) GetSize(int[,] field)
        {
            var rows = field.GetLength(0);
            var cols = field.GetLength(1);
            return (rows, cols);
        }

        private static int[,] ShiftDown(int[,] field)
        {
            var (rows, cols) = GetSize(field);
            var result = new int[rows, cols];
            Array.Copy(field, 0, result, rows, rows * cols - rows);
            return result;
        }

        private static int[,] ShiftUp(int[,] field)
        {
            var (rows, cols) = GetSize(field);
            var result = new int[rows, cols];
            Array.Copy(field, rows, result, 0, rows * cols - rows);
            return result;
        }

        private static int[,] ShiftLeft(int[,] field)
        {
            var (rows, cols) = GetSize(field);
            var elementSize = GetElementSize<int>();
            for (var row = 0; row < rows; row++)
            {
                var shiftRow = new int[cols];
                Buffer.BlockCopy(field, row * (cols) * elementSize + elementSize, shiftRow, 0, (cols - 1) * elementSize);
                Buffer.BlockCopy(shiftRow, 0, field, row * cols * elementSize, cols * elementSize);
            }
            return field;
        }

        private static int[,] ShiftRight(int[,] field)
        {
            var (rows, cols) = GetSize(field);
            var elementSize = GetElementSize<int>();
            for (var row = 0; row < rows; row++)
            {
                var shiftRow = new int[cols];
                Buffer.BlockCopy(field, row * (cols) * elementSize, shiftRow, elementSize, (cols - 1) * elementSize);
                Buffer.BlockCopy(shiftRow, 0, field, row * cols * elementSize, cols * elementSize);
            }
            return field;
        }
    }
}