using System;
using MathNet.Numerics.LinearAlgebra;
using MathNet.Numerics.LinearAlgebra.Double;

namespace MPT.Lib.Portfolio
{
	public class Portfolio : Object
	{
		private DenseMatrix weights;
		private DenseMatrix expectedReturns;
		private DenseMatrix covarianceMatrix;
		private DenseMatrix utilityMatrix;
		private int n = 0;
		protected double r = 0;

		public Portfolio (int N)
		{
			this.n = N;
			this.weights = DenseMatrix.Create (N,1,1.0/N);
			this.expectedReturns = DenseMatrix.Create (N, 1, 0.0);
			this.covarianceMatrix = DenseMatrix.Create (N, N, 0.0);
		}

		public Portfolio (DenseMatrix C, DenseMatrix M)
		{
			if (C.ColumnCount != C.RowCount) {
				throw(new InvalidCovarianceMatrixException (C));
			}

			this.N = C.RowCount;
			this.covarianceMatrix = C;
			this.expectedReturns = M;
		}

		public Portfolio (double[] W, double[] M)
		{
			this.n = W.GetLength (0);

			//column vector 
			this.weights = new DenseMatrix(this.n, 1, W);

			if (this.weights.ColumnSums().At(0) != 1) 
			{
				InvalidPortfolioException e = new InvalidPortfolioException (this);
				throw(e);
			}

			if (W.GetLength(0) != M.GetLength(0)) 
			{
				throw(new MatrixDimensionMismatchException());
			}

			this.expectedReturns = new DenseMatrix (N, 1, M);
		}

		public Portfolio (double[][] C)
		{
			if (C.GetLength(0) != C.GetLength(1))
			{
				var e = new InvalidCovarianceMatrixException (C);
				throw(e);
			}

			this.n = C.GetLength (0);

			this.utilityMatrix = DenseMatrix.Create (N, 1, 1.0);
		}

		public Portfolio (double[][] C, double[] M)
		{
			if (C.GetLength(0) != C.GetLength(1))
			{
				var e = new InvalidCovarianceMatrixException (C);
				throw(e);
			}

			this.n = C.GetLength (0);
			this.utilityMatrix = DenseMatrix.Create (N, 1, 1.0);
			this.covarianceMatrix = DenseMatrix.OfRowArrays (C);
		}

		public int N => this.n;

		public DenseMatrix Weights {
			get {
				return this.weights;
			}
			set {
				this.weights = value;
			}
		}

		public DenseMatrix ExpectedReturns {
			get {
				return this.expectedReturns;
			}
			set {
				this.expectedReturns = value;
			}
		}
	
		public DenseMatrix CovarianceMatrix {
			get {
				return this.covarianceMatrix;
			}
			set {
				this.covarianceMatrix = value;
			}
		}

		public DenseMatrix UtilityMatrix {
			get {
				return this.utilityMatrix;
			}
			set {
				this.utilityMatrix = value;
			}
		}

		public double R {
			get {
				return this.r;
			}
			set {
				this.r = value;
			}
		}

		public virtual double ExpectedPortfolioReturn
		{
			get {
				return this.Weights.Multiply(ExpectedReturns.Transpose()).At(0,0);
			}
		}

		public double StandardDeviation
		{
			get {
				return this.Weights.Transpose().Multiply(CovarianceMatrix).Multiply(Weights).At(0,0);
			}	
		}

		public double Variance
		{
			get {
				return Math.Pow (StandardDeviation, 2);
			}
		}

		public DenseMatrix ComputeMVP()
		{
			return Cov_m1xUtil.Divide(Util_TxCov_m1xUtil);
		}

		public void SetWeightsToMVP()
		{
			Weights = ComputeMVP ();
		}

		public override string ToString ()
		{
			return string.Format ("[Portfolio: {0}]", Weights.ToMatrixString());
		}

		public override bool Equals (object obj)
		{
			if (this.GetType () != obj.GetType ()) {
				return false;
			} else {
				return true;
			}
		}

		public override int GetHashCode ()
		{
			return Weights.GetHashCode() * CovarianceMatrix.GetHashCode() * ExpectedReturns.GetHashCode();
		}

		protected DenseMatrix Cov_m1xUtil {
			get {
				return CovarianceMatrix.Inverse ().Multiply (UtilityMatrix);
			}
		}

		protected DenseMatrix Cov_m1xM {
			get {
				return CovarianceMatrix.Inverse ().Multiply (ExpectedReturns);
			}
		}

		protected double M_TxCov_m1xUtil {
			get {
				return ExpectedReturns.TransposeThisAndMultiply (this.Cov_m1xUtil);
			}
		}

		protected double Util_TxCov_m1xUtil {
			get {
				return UtilityMatrix.TransposeThisAndMultiply (this.Cov_m1xUtil);
			}
		}

		protected double M_TxCov_m1xM {
			get {
				return ExpectedReturns.TransposeThisAndMultiply (this.Cov_m1xM);
			}
		}

		protected double  Util_TxCov_m1xM {
			get {
				return UtilityMatrix.TransposeThisAndMultiply (this.Cov_m1xM);
			}
		}
	}
}

