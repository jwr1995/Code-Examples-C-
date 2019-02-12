using System;
using MathNet.Numerics.Statistics;

namespace MPT.Lib.General
{
	/// <summary>
	/// Statistics is a static class to serve as a wrapper for
	/// the MathNet.Numerics.Statistics namespace with selected
	/// functions specific to Markowiz Portfolio Theory
	/// </summary>
	public static class Statistics
	{
		public Statistics ()
		{
		}

		public double Expectation(double[] x)
		{
			return ArrayStatistics.Mean (x);
		}

		public double Expectation(double[] x, double[] p)
		{
			double E = 0;
			for (int i = 0; i<x.GetLength(0);i++)
			{
				E+=x[i]*p[i];
			}
			return E;
		}

		public double Variance(double[] x)
		{
			return ArrayStatistics.Variance (x);
		}

		public double Covariance(double[] x, double[] y)
		{
			return ArrayStatistics.Covariance (x, y);
		}

	}

	//public enum Distribution { Uniform, Binomial, Normal, Poisson, Logarithmic, Gaussian }
}

