using System;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MPT.Lib.Portfolio
{
	public class MinimumVariancePortfolio : Portfolio
	{
		public MinimumVariancePortfolio (DenseMatrix C, DenseMatrix M) : base(C,M)
		{
			base.SetWeightsToMVP ();
		}

		public MinimumVariancePortfolio (double[][] C, double[] M) : base(C,M)
		{
			base.SetWeightsToMVP ();
		}

		public MinimumVariancePortfolio (double[][] C) : base(C)
		{
			base.SetWeightsToMVP ();
		}

		public override string ToString ()
		{
			return string.Format ("[MinimumVariancePortfolio {0}]", Weights.ToMatrixString());
		}

		public override bool Equals (object obj)
		{
			if (this.GetType () == obj.GetType ()) {
				return true;
			} else {
				return false;
			}
		}
	}
}

