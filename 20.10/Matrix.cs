using System;

public class Matrix
{
    private int[,] data;

    public int Rows { get; private set; }

    public int Cols { get; private set; }

    public Matrix(int rows, int cols)
    {
        if (rows <= 0 || cols <= 0)
        {
            throw new ArgumentException("Розміри матриці повинні бути додатними.");
        }

        Rows = rows;
        Cols = cols;
        data = new int[rows, cols];
    }

    public int this[int row, int col]
    {
        get
        {
            return data[row, col];
        }
        set
        {
            data[row, col] = value;
        }
    }

    public static Matrix operator +(Matrix first, Matrix second)
    {
        CheckSameSize(first, second);

        Matrix result = new Matrix(first.Rows, first.Cols);

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < first.Cols; j++)
            {
                result[i, j] = first[i, j] + second[i, j];
            }
        }

        return result;
    }

    public static Matrix operator -(Matrix first, Matrix second)
    {
        CheckSameSize(first, second);

        Matrix result = new Matrix(first.Rows, first.Cols);

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < first.Cols; j++)
            {
                result[i, j] = first[i, j] - second[i, j];
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix first, Matrix second)
    {
        if (first.Cols != second.Rows)
        {
            throw new ArgumentException("Кількість стовпців першої матриці повинна дорівнювати кількості рядків другої.");
        }

        Matrix result = new Matrix(first.Rows, second.Cols);

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < second.Cols; j++)
            {
                int sum = 0;

                for (int k = 0; k < first.Cols; k++)
                {
                    sum = sum + first[i, k] * second[k, j];
                }

                result[i, j] = sum;
            }
        }

        return result;
    }

    public static Matrix operator *(Matrix matrix, int number)
    {
        Matrix result = new Matrix(matrix.Rows, matrix.Cols);

        for (int i = 0; i < matrix.Rows; i++)
        {
            for (int j = 0; j < matrix.Cols; j++)
            {
                result[i, j] = matrix[i, j] * number;
            }
        }

        return result;
    }

    public static bool operator ==(Matrix first, Matrix second)
    {
        if (ReferenceEquals(first, second))
        {
            return true;
        }

        if (first is null || second is null)
        {
            return false;
        }

        if (first.Rows != second.Rows || first.Cols != second.Cols)
        {
            return false;
        }

        for (int i = 0; i < first.Rows; i++)
        {
            for (int j = 0; j < first.Cols; j++)
            {
                if (first[i, j] != second[i, j])
                {
                    return false;
                }
            }
        }

        return true;
    }

    public static bool operator !=(Matrix first, Matrix second)
    {
        return !(first == second);
    }

    public override bool Equals(object? obj)
    {
        if (!(obj is Matrix other))
        {
            return false;
        }

        return this == other;
    }

    public override int GetHashCode()
    {
        int hash = 0;

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                hash = hash + data[i, j];
            }
        }

        return hash;
    }

    public override string ToString()
    {
        string text = "";

        for (int i = 0; i < Rows; i++)
        {
            for (int j = 0; j < Cols; j++)
            {
                text = text + data[i, j] + " ";
            }

            text = text + Environment.NewLine;
        }

        return text;
    }

    private static void CheckSameSize(Matrix first, Matrix second)
    {
        if (first.Rows != second.Rows || first.Cols != second.Cols)
        {
            throw new ArgumentException("Матриці повинні мати однаковий розмір.");
        }
    }
}
