﻿using System;

namespace AlgorithmLibrary
{
    using System.Numerics;

    using Entity;

    public class MatrixHelper
    {
        public static int[][] Multiply(int[][] matFirst, int[][] matSecond)
        {
            if (matFirst[0].Length != matSecond.Length)
            {
                throw new ArgumentException("The width of the first matrix must equal to the height of the second matrix.");
            }

            var height = matSecond[0].Length;
            var result = new int[matFirst.Length][];
            for (var i = 0; i < matFirst.Length; i++)
            {
                result[i] = new int[height];
                for (var j = 0; j < matSecond[0].Length; j++)
                {
                    var sum = 0;
                    for (var k = 0; k < matFirst[0].Length; k++)
                    {
                        sum += matFirst[i][k] * matSecond[k][j];
                    }
                    result[i][j] = sum;
                }
            }

            return result;
        }


        public static BigIntegerMatrix Multiply(BigIntegerMatrix matFirst, BigIntegerMatrix matSecond)
        {
            if (matFirst[0].Length != matSecond.Length)
            {
                throw new ArgumentException("The width of the first matrix must equal to the height of the second matrix.");
            }

            if (matFirst.Length <= 5 || matSecond[0].Length <= 5)
            {
                return matFirst * matSecond;
            }


            var (a, b, c, d) = DivideMatrix(matFirst);
            var (e, f, g, h) = DivideMatrix(matFirst);

            var p1 = Multiply(a, f - h);
            var p2 = Multiply(a + b, h);
            var p3 = Multiply(c + d, e);
            var p4 = Multiply(d, g - e);
            var p5 = Multiply(a + d, e + h);
            var p6 = Multiply(b - d, g + h);
            var p7 = Multiply(a - c, e + f);

            var mat1 = p5 + p4 - p2 + p6;
            var mat2 = p1 + p2;
            var mat3 = p3 + p4;
            var mat4 = p1 + p5 - p3 - p7;

            return CombineMatrix(mat1, mat2, mat3, mat4);
        }

        public static int[][] Add(int[][] matFirst, int[][] matSecond)
        {
            if (matFirst.Length != matSecond.Length || matFirst[0].Length != matSecond[0].Length)
            {
                throw new ArgumentException("The two matrix must have the same size.");
            }

            var height = matFirst[0].Length;
            var result = new int[matFirst.Length][];
            for (var i = 0; i < matFirst.Length; i++)
            {
                result[i] = new int[height];
                for (var j = 0; j < matFirst[0].Length; j++)
                {
                    result[i][j] = matFirst[i][j] + matSecond[i][j];
                }
            }

            return result;
        }

        private static (BigIntegerMatrix mat1, BigIntegerMatrix mat2, BigIntegerMatrix mat3, BigIntegerMatrix mat4) DivideMatrix(BigIntegerMatrix mat)
        {
            BigIntegerMatrix mat1, mat2, mat3, mat4;
            mat1 = new BigIntegerMatrix(new BigInteger[mat.Length / 2][]);
            mat2 = new BigIntegerMatrix(new BigInteger[mat.Length / 2][]);
            mat3 = new BigIntegerMatrix(new BigInteger[mat.Length - (mat.Length / 2)][]);
            mat4 = new BigIntegerMatrix(new BigInteger[mat.Length - (mat.Length / 2)][]);
            for (var i = 0; i < mat.Length; i++)
            {
                for (var j = 0; j < mat[i].Length; j++)
                {
                    if (i < mat.Length / 2)
                    {
                        if (j < mat[0].Length / 2)
                        {
                            if (j == 0)
                            {
                                mat1[i] = new BigInteger[mat[0].Length / 2];
                            }

                            mat1[i][j] = mat[i][j];
                        }
                        else
                        {
                            if (j == mat[0].Length / 2)
                            {
                                mat2[i] = new BigInteger[mat[0].Length - (mat[0].Length / 2)];
                            }

                            mat2[i][j - (mat[0].Length / 2)] = mat[i][j];
                        }
                    }
                    else
                    {
                        if (j < mat[0].Length / 2)
                        {
                            if (j == 0)
                            {
                                mat3[i - (mat.Length / 2)] = new BigInteger[mat[0].Length / 2];
                            }

                            mat3[i - (mat.Length / 2)][j] = mat[i][j];
                        }
                        else
                        {
                            if (j == mat[0].Length / 2)
                            {
                                mat4[i - (mat.Length / 2)] = new BigInteger[mat[0].Length - (mat[0].Length / 2)];
                            }

                            mat4[i - (mat.Length / 2)][j - (mat[0].Length / 2)] = mat[i][j];
                        }
                    }
                }
            }

            return (mat1, mat2, mat3, mat4);
        }

        private static BigIntegerMatrix CombineMatrix(
            BigIntegerMatrix mat1,
            BigIntegerMatrix mat2,
            BigIntegerMatrix mat3,
            BigIntegerMatrix mat4)
        {
            BigIntegerMatrix mat = new BigIntegerMatrix(new BigInteger[mat1.Length + mat3.Length][]);
            for (var i = 0; i < mat1.Length; i++)
            {
                for (var j = 0; j < mat1.Length; j++)
                {
                    if (j == 0)
                    {
                        mat[i] = new BigInteger[mat1[0].Length + mat2[0].Length];
                    }

                    mat[i][j] = mat1[i][j];
                }
            }

            for (var i = 0; i < mat2.Length; i++)
            {
                for (var j = 0; j < mat2.Length; j++)
                {
                    if (j == 0)
                    {
                        mat[i] = new BigInteger[mat1[0].Length + mat2[0].Length];
                    }

                    mat[i][mat1[0].Length + j] = mat2[i][j];
                }
            }

            for (var i = 0; i < mat3.Length; i++)
            {
                for (var j = 0; j < mat3.Length; j++)
                {
                    if (j == 0)
                    {
                        mat[i] = new BigInteger[mat1[0].Length + mat2[0].Length];
                    }

                    mat[mat1.Length + i][j] = mat3[i][j];
                }
            }

            for (var i = 0; i < mat4.Length; i++)
            {
                for (var j = 0; j < mat4.Length; j++)
                {
                    if (j == 0)
                    {
                        mat[i] = new BigInteger[mat1[0].Length + mat2[0].Length];
                    }

                    mat[mat1.Length + i][mat1[0].Length + j] = mat4[i][j];
                }
            }

            return mat;
        }
    }
}
