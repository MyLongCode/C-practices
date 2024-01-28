using System;
using System.Linq;

namespace GaussAlgorithm;

public class Solver
{
    public static void ChangeRow(double[][] matrix, double[] freeMembers, int row, int workRow
    , int col, int rowLength)
    {
        var correlation = (matrix[row][col] / matrix[workRow][col]);
        for (var j = 0; j < rowLength; j++)
            matrix[row][j] = Math.Round(matrix[row][j] - (matrix[workRow][j] * correlation), 7);
        freeMembers[row] = Math.Round(freeMembers[row] - (freeMembers[workRow] * correlation), 7);
    }

    public double[] Solve(double[][] matrix, double[] freeMembers)
	{
		var countRows = matrix.Length;
		var countColumns = matrix[0].Length;
        var preparedSolve = new double[countColumns];
        var useRow = new double[countRows];
		var flag = false;

		for (var column = 0; column < countColumns; column++)
		{
			for(var row = 0; row < countRows; row++)
				if(matrix[row][column] != 0 && useRow[row] != 1 && !flag)
				{
					flag = true;
					useRow[row] = 1;
                    for (var i = 0; i < countRows; i++)
						if (i != row)
							ChangeRow(matrix, freeMembers, i, row, column, countColumns);
				}
			if (!flag) preparedSolve[column] = 0;
			flag = false;
		}	
		for(var column = 0; column < countRows; column++)
		{
			if (matrix[column].All(x => x == 0) && freeMembers[column] != 0)
				throw new NoSolutionException("---");
			for (var row = 0; row < countColumns; row++)
				if (matrix[column][row] != 0)
				{
					preparedSolve[row] = freeMembers[column] / matrix[column][row];
					break;
				}
		}
		return preparedSolve;
	}
}