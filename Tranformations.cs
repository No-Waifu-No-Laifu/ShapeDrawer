using System;
using System.Diagnostics;

namespace asgn5v1
{
    public static class Transformations
    {
        public static double PreviousXPos = 0;
        public static double PreviousYPos = 0;
        public static double PreviousZPos = 0;

        public static double[,] TranslateToOrigin(double[,] vertices, double[,] tnet)
        {
            double x, y, z;
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            PreviousXPos = vertices[0, 0];
            PreviousYPos = vertices[0, 1];
            PreviousZPos = vertices[0, 2];
            x = vertices[0, 0] * -1;
            y = vertices[0, 1] * -1;
            z = vertices[0, 2] * -1;
            transformation[3, 0] = x;
            transformation[3, 1] = y;
            transformation[3, 2] = z;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] Translate(double[,] tnet, double xdistance, double ydistance, double zdistance)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            PreviousXPos = xdistance * -1;
            PreviousYPos = ydistance * -1;
            PreviousZPos = zdistance * -1;
            transformation[3, 0] = xdistance;
            transformation[3, 1] = ydistance;
            transformation[3, 2] = zdistance;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] TranslateBaselineToXaxis(double[,] originalVertices, double[,] currentVertices, double[,] tnet)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            int arows = originalVertices.GetLength(0);
            int acols = originalVertices.GetLength(1);
            int i = 0;
            for (; i < arows; i++)
            {
                if(originalVertices[i,1] == 0)
                {
                    break;
                }
            }
            double x = currentVertices[i, 0] * -1;
            double y = currentVertices[i, 1] * -1;
            double z = currentVertices[i, 2] * -1;

            return Translate(tnet, 0, y, 0);
        }

        public static double[,] ReflectOnYAxis(double[,] tnet)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[1, 1] = transformation[1, 1] * -1;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] TranslateToCenter(double[,] tnet, double screenWidth, double screenHeight)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            double transformX = screenWidth / 2;
            double transformY = screenHeight / 2;

            PreviousXPos = tnet[3, 0];
            PreviousYPos = tnet[3, 1];
            PreviousZPos = tnet[3, 2];
            transformation[3, 0] = transformX;
            transformation[3, 1] = transformY;
            transformation[3, 2] = 0;
            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] ScaleToIntial(double[,] vertices, double[,] tnet, double screenHeight)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);
            
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

            transformation[0, 0] = scale;
            transformation[1, 1] = scale;
            transformation[2, 2] = scale;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] ScaleUniform(double[,] vertices, double[,] tnet, double factor)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[0, 0] = factor;
            transformation[1, 1] = factor;
            transformation[2, 2] = factor;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] ShearHorizontal(double[,] tnet, double factor)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[1, 0] = factor;

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] RotateOnX(double[,] tnet, double radians)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[1, 1] = Math.Cos(radians);
            transformation[1, 2] = Math.Sin(radians);
            transformation[2, 1] = -Math.Sin(radians);
            transformation[2, 2] = Math.Cos(radians);

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] RotateOnY(double[,] tnet, double radians)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[0, 0] = Math.Cos(radians);
            transformation[2, 0] = -Math.Sin(radians);
            transformation[0, 2] = Math.Sin(radians);
            transformation[2, 2] = Math.Cos(radians);

            return MatrixMultiply(tnet, transformation);
        }

        public static double[,] RotateOnZ(double[,] tnet, double radians)
        {
            double[,] transformation = new double[4, 4];
            SetIdentity(transformation, 4, 4);

            transformation[0, 0] = Math.Cos(radians);
            transformation[1, 0] = -Math.Sin(radians);
            transformation[0, 1] = Math.Sin(radians);
            transformation[1, 1] = Math.Cos(radians);

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

        public static void SetIdentity(double[,] A, int nrow, int ncol)
        {
            for (int i = 0; i < nrow; i++)
            {
                for (int j = 0; j < ncol; j++) A[i, j] = 0.0d;
                A[i, i] = 1.0d;
            }
        }// end of setIdentity

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