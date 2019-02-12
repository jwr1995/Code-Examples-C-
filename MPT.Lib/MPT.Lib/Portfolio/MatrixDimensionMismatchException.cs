using System;

namespace MPT.Lib.Portfolio
{
	public class MatrixDimensionMismatchException : Exception
	{
		public MatrixDimensionMismatchException () : base("Matrix dimensions must agree")
		{
		}
	}
}

