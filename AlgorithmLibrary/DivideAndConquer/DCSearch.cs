﻿using System;
using System.Collections.Generic;
using System.Linq;

namespace AlgorithmLibrary.DivideAndConquer
{
    public class DCSearch<T> : ISearch<T> where T : IComparable<T>
    {
        public T Max(IEnumerable<T> list, int rank)
        {
            return Min(list, list.Count() - rank);
        }

        public T Min(IEnumerable<T> list, int rank = 1)
        {
            return QuickSearch(list.ToArray(), 0, list.Count() - 1, rank);
        }

        private T QuickSearch(T[] array, int startIndex, int endIndex, int rank)
        {
            if (rank - 1 < startIndex || rank - 1 > endIndex)
            {
                return default(T);
            }

            int indexOfPivot = new Random().Next(endIndex - startIndex) + startIndex;
            T temp = array[startIndex];
            array[startIndex] = array[indexOfPivot];
            array[indexOfPivot] = temp;

            int i = startIndex;
            for (var j = i + 1; j <= endIndex; j++)
            {
                if (array[j].CompareTo(array[startIndex]) <= 0)
                {
                    temp = array[++i];
                    array[i] = array[j];
                    array[j] = temp;
                }
            }

            if (i != startIndex)
            {
                temp = array[i];
                array[i] = array[startIndex];
                array[startIndex] = temp;

                if (i == rank - 1)
                {
                    return array[i];
                }
                else if (i > rank - 1)
                {
                    return QuickSearch(array, startIndex, i - 1, rank);
                }
                else
                {
                    return QuickSearch(array, i + 1, endIndex, rank);
                }
            }
            else
            {
                if (i == rank - 1)
                {
                    return array[i];
                }
                else
                {
                    return QuickSearch(array, i + 1, endIndex, rank);
                }
            }
        }
    }
}
