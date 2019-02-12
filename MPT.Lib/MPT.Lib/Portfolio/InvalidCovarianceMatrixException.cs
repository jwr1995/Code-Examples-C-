using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MPT.Lib.Portfolio
{
	public class InvalidCovarianceMatrixException : Exception
	{
		private int[] dimensions = {1,1};

		public InvalidCovarianceMatrixException (DenseMatrix C) : base ("Invalid matrix; dimensions must be equal")
		{
			this.dimensions [0] = C.RowCount;
			this.dimensions [1] = C.ColumnCount;
		}

		public InvalidCovarianceMatrixException (double[][] C) : base ("Invalid matrix; dimensions must be equal")
		{
			this.dimensions [0] = C.GetLength (0);
			this.dimensions [1] = C.GetLength (1);
		}

		public int[] Dimensions => this.dimensions;
	}
}

