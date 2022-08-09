// See https://aka.ms/new-console-template for more information

using System.Diagnostics;
using System.Runtime.InteropServices;
using ConsoleApp8;

Console.WriteLine("Hello, World!");

const int rows = 10000;
const int cols = 10000;
var field = new int[rows, cols];
var random = new Random();
var unrolledRandomValues = Enumerable.Repeat(0, rows * cols).Select(i => random.Next(1, 5)).ToArray();

var elementSize = Marshal.SizeOf(typeof(int));

Buffer.BlockCopy(unrolledRandomValues, 0, field, 0, rows * cols * elementSize);

var wind = "UULDDDULULULULLL";

var watch = new Stopwatch();
watch.Start();
var sum = Solution.SumLeftLeavesAfterStorm(field, wind);
watch.Stop();

Console.WriteLine($"Matrix = [{rows}x{cols}]={rows * cols}, Winds {wind}, Sum {sum}, CalculationTime: {watch.Elapsed}");

Console.ReadLine();