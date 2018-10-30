﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using AlgorithmLibrary.Basic;
using AlgorithmLibrary.DivideAndConquer;

namespace ConsoleApp
{
    using System.Numerics;

    using Entity;

    public class Program
    {
        public static void Main(string[] args)
        {
            int arrayLength = 8;
            var array = new int[arrayLength];
            for (int i = 0; i < arrayLength; i++)
            {
                array[i] = new Random().Next(arrayLength * 10);
            }

            TestSort(array);

            int power = 999;
            TestPowerOfN(5, power);

            int depth = 20;
            TestFibonacci(depth);

            var matrixLength = 8;
            TestMatrixMultiply(matrixLength);

            Console.ReadLine();
        }

        private static void TestSort(int[] array)
        {
            Console.WriteLine("Test array - Insertion sort -> Merge sort -> Quick sort -> Linear sort:");
            Print(array);
            var st = new Stopwatch();
            st.Start();
            var result = new InsertionSort<int>().Sort(array);
            st.Stop();
            Console.WriteLine(st.ElapsedMilliseconds);
            Print(result);

            st.Restart();
            result = new MergedSort<int>().Sort(array);
            st.Stop();
            Console.WriteLine(st.ElapsedMilliseconds);
            Print(result);

            st.Restart();
            result = new QuickSort<int>().Sort(array);
            st.Stop();
            Console.WriteLine(st.ElapsedMilliseconds);
            Print(result);

            st.Restart();
            result = new LinearSort().Sort(array);
            st.Stop();
            Console.WriteLine(st.ElapsedMilliseconds);
            Print(result);
        }

        private static void TestPowerOfN(BigInteger x, int n)
        {
            Console.WriteLine($"Test {x} power of {n} - Recursive -> Split and recursive:");
            var st = new Stopwatch();
            st.Restart();
            var value = new NPowerCalculator().Power(x, n);
            st.Stop();
            Console.WriteLine(value);
            Console.WriteLine(st.ElapsedMilliseconds);

            st.Restart();
            value = new DcNPowerCalculator().Power(x, n);
            st.Stop();
            Console.WriteLine(value);
            Console.WriteLine(st.ElapsedMilliseconds);
        }

        private static void TestFibonacci(int n)
        {
            Console.WriteLine("Test fibonacci - Recursive -> Matrix power of n:");
            var st = new Stopwatch();
            st.Restart();
            var value = new Fibonacci().Calculate(n);
            st.Stop();
            Console.WriteLine(value);
            Console.WriteLine(st.ElapsedMilliseconds);

            st.Restart();
            value = new DcFibonacci().Calculate(n);
            st.Stop();
            Console.WriteLine(value);
            Console.WriteLine(st.ElapsedMilliseconds);
        }

        private static void TestMatrixMultiply(int n)
        {
            Console.WriteLine("Test matrix multiply - Loop (n^3) -> Strassen Algorithm (n^log7 or n^2.81):");
            var st = new Stopwatch();
            var matrix2 = new BigIntegerMatrix(new System.Numerics.BigInteger[n][]);
            for (var i = 0; i < n; i++)
            {
                matrix2[i] = new System.Numerics.BigInteger[n];
                for (var j = 0; j < n; j++)
                {
                    matrix2[i][j] = new Random(i * j).Next(100);
                }
            }

            st.Restart();
            var resultMat = matrix2 * matrix2;
            st.Stop();
            Print(resultMat.AsEnumerable());
            Console.WriteLine(st.ElapsedMilliseconds);

            st.Restart();
            resultMat = BigIntegerMatrix.StrassenMultiply(matrix2, matrix2);
            st.Stop();
            Print(resultMat.AsEnumerable());
            Console.WriteLine(st.ElapsedMilliseconds);
        }

        private static void Print<T>(IEnumerable<T> array)
        {
            foreach (var item in array)
            {
                if (item is IEnumerable enumerable)
                {
                    foreach (var subItem in enumerable)
                    {
                        Console.Write(subItem);
                        Console.Write(",");
                    }

                    Console.WriteLine();
                }
                else
                {
                    Console.Write(item);
                    Console.Write(",");
                }
            }

            Console.WriteLine();
        }
    }
}
