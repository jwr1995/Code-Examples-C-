using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MPT.Lib.Portfolio
{
	public class MinimumVarianceLinePortfolio : Portfolio
	{
		private double expectedPortfolioReturn;

		public MinimumVarianceLinePortfolio (DenseMatrix C, DenseMatrix M, double expectedPortolioReturn) : base (C, M)
		{
			RecomputeWeights (expectedPortolioReturn);
		}

		public void RecomputeWeights(double expectedPortfolioReturn = this.expectedPortfolioReturn)
		{
			this.expectedPortfolioReturn = expectedPortfolioReturn;
			Weights = (Cov_m1xUtil.Multiply (DetOne) + Cov_m1xM.Multiply (DetTwo)) / DetDenominator;
		}

		private double DetOne
		{
			get {
				return M_TxCov_m1xM - Util_TxCov_m1xM * ExpectedPortfolioReturn;;
			}		
		}

		private double DetTwo
		{
			get {
				return 	Util_TxCov_m1xUtil * ExpectedPortfolioReturn - M_TxCov_m1xUtil;
			}
		}

		private double DetDenominator
		{
			get {
				return Util_TxCov_m1xUtil * M_TxCov_m1xM - Util_TxCov_m1xM * M_TxCov_m1xUtil;
			}
		}

		public override double ExpectedPortfolioReturn {
			get {
				return this.expectedPortfolioReturn;
			}
		}
	}
}

