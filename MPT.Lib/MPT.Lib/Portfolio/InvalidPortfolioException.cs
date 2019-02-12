using System;

namespace MPT.Lib.Portfolio
{
	public class InvalidPortfolioException : Exception
	{
		private double sum;

		public InvalidPortfolioException (Portfolio p) : 
		base ("Invalid portfolio weights, must equal 1, current sum = " + p.Weights .ColumnSums ().At (0))
		{
			this.sum = p.Weights.ColumnSums ().At (0);
		}

		public double Sum() => this.sum;
	}
}

