using System;
using System.Diagnostics;

namespace asgn5v1
{
    public static class Tranformations
    {
        public static double[,] TranslateToOrigin(double[,] vertices, double[,] tnet)
        {
            double x, y, z;
            double[,] tranformation = new double[4, 4];

            x = vertices[0, 0] * -1;
            y = vertices[0, 1] * -1;
            z = vertices[0, 2] * -1;
            tranformation[0, 0] = 1;
            tranformation[1, 1] = 1;
            tranformation[2, 2] = 1;
            tranformation[3, 3] = 1;
            tranformation[3, 0] = x;
            tranformation[3, 1] = y;
            tranformation[3, 2] = z;

            return MatrixMultiply(tnet, tranformation);
        }

        public static double[,] ReflectOnYAxis(double[,] tnet)
        {
            double[,] tranformation = new double[4, 4];

            tranformation[0, 0] = 1;
            tranformation[1, 1] = 1;
            tranformation[2, 2] = 1;
            tranformation[3, 3] = 1;
            tranformation[1, 1] = tranformation[1, 1] * -1;

            return MatrixMultiply(tnet, tranformation);
        }

        public static double[,] TranslateToCenter(double[,] tnet, double screenWidth, double screenHeight)
        {
            double[,] transformation = new double[4, 4];

            transformation[0, 0] = 1;
            transformation[1, 1] = 1;
            transformation[2, 2] = 1;
            transformation[3, 3] = 1;
            double transformX = screenWidth / 2;
            double transformY = screenHeight / 2;

            transformation[3, 0] = transformX;
            transformation[3, 1] = transformY;
            transformation[3, 2] = 0;
            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] ScaleToIntial(double[,] vertices, double[,] tnet, double screenHeight)
        {
            double[,] transformation = new double[4, 4];

            int rows = vertices.GetLength(0);
            int cols = vertices.GetLength(1);
            double min, max;

            min = vertices[0, 1];
            max = vertices[0, 1];
            for (int i = 1; i < rows; i++)
            {
                min = Math.Min(vertices[i, 1], min);
                max = Math.Max(vertices[i, 1], max);
            }
            double height = max - min;
            double scale = (screenHeight / 2) / height;

            transformation[3, 3] = 1;

            transformation[0, 0] = scale;
            transformation[1, 1] = scale;
            transformation[2, 2] = scale;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] MatrixMultiply(double[,] A, double[,] B)
        {
            if (A.GetLength(1) != B.GetLength(0))
            {
                throw new InvalidOperationException("A's # of columns not same as B's number of rows");
            }
            int arows = A.GetLength(0);
            int bcols = B.GetLength(1);
            int brows = B.GetLength(0);
            double[,] result = new double[arows, bcols];
            for (int i = 0; i < arows; i++)
            {
                for (int j = 0; j < bcols; j++)
                {
                    double temp = 0.0d;
                    for (int k = 0; k < brows; k++)
                        temp += A[i, k] * B[k, j];
                    result[i, j] = temp;
                }
            }
            return result;
        }

        public static void PrintMatrix(double[,] matrix)
        {
            int rows = matrix.GetLength(0);
            int cols = matrix.GetLength(1);
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    Debug.Write(matrix[i, j] + " ");
                }
                Debug.WriteLine("");
            }
        }
    }
}