/*
    https://en.wikipedia.org/wiki/Jacobi_eigenvalue_algorithm
*/

namespace Maroontress.Euclid
{
    using System;

    /// <summary>
    /// Provides Jacobi eigenvalue algorithm for 3x3 square matrices.
    /// </summary>
    public static class Jacobi
    {
        /// <summary>
        /// Gets the eigenvalues and eigenvectors of the specified symmetric
        /// matrix by the Jacobi eigenvalue algorithm.
        /// </summary>
        /// <param name="threshold">
        /// All absolute values of off-diagonal entries in D are less than this
        /// threshold.
        /// </param>
        /// <param name="a11">
        /// The component (1, 1) of the symmetric matrix.
        /// </param>
        /// <param name="a12">
        /// The component (1, 2) of the symmetric matrix.
        /// </param>
        /// <param name="a13">
        /// The component (1, 3) of the symmetric matrix.
        /// </param>
        /// <param name="a22">
        /// The component (2, 2) of the symmetric matrix.
        /// </param>
        /// <param name="a23">
        /// The component (2, 3) of the symmetric matrix.
        /// </param>
        /// <param name="a33">
        /// The component (3, 3) of the symmetric matrix.
        /// </param>
        /// <returns>
        /// The tuple containing a diagonal matrix D and an orthogonal matrix
        /// V. The diagonal entries in D and the column vectors in V represent
        /// the eigenvalues and eigenvectors of the specified symmetric matrix,
        /// respectively.
        /// </returns>
        public static (Matrix33 D, Matrix33 V) EigenvaluesAndVectors(
            float threshold,
            float a11,
            float a12,
            float a13,
            float a22,
            float a23,
            float a33)
        {
            var v = Matrix33.Identity;

            static (float AbsIJ, Action UpdateIJ) GetMaxComponent(
                (float IJ, Action UpdateIJ)[] all)
            {
                var max = Math.Abs(all[0].IJ);
                var index = 0;
                for (var k = 1; k < 3; ++k)
                {
                    var a = Math.Abs(all[k].IJ);
                    if (max < a)
                    {
                        max = a;
                        index = k;
                    }
                }
                return (max, all[index].UpdateIJ);
            }

            static (float II,
                    float JJ,
                    float IJ,
                    float IK,
                    float JK,
                    Matrix33 P)
                Update(
                    Func<float, float, Matrix33> transposeG,
                    float ii,
                    float jj,
                    float ij,
                    float ik,
                    float jk,
                    Matrix33 p)
            {
                var doubleTheta = Toolkit.Atan2(2 * ij, jj - ii);
                var theta = doubleTheta / 2;
                var cos = Toolkit.Cos(theta);
                var sin = Toolkit.Sin(theta);
                var gt = transposeG(cos, sin);
                var newP = p.Mul(gt);
                var cosCos = cos * cos;
                var sinSin = sin * sin;
                var sinCos = sin * cos;
                var doubleSinCos = 2 * sinCos;
                var newII
                    = (cosCos * ii) - (doubleSinCos * ij) + (sinSin * jj);
                var newJJ
                    = (sinSin * ii) + (doubleSinCos * ij) + (cosCos * jj);
                var newIJ = 0;
                var newIK = (cos * ik) - (sin * jk);
                var newJK = (sin * ik) + (cos * jk);
                return (newII, newJJ, newIJ, newIK, newJK, newP);
            }

            static Matrix33 TransposeG12(float cos, float sin)
            {
                return new Matrix33(
                    (cos, -sin, 0),
                    (sin, cos, 0),
                    (0, 0, 1));
            }

            static Matrix33 TransposeG13(float cos, float sin)
            {
                return new Matrix33(
                    (cos, 0, -sin),
                    (0, 1, 0),
                    (sin, 0, cos));
            }

            static Matrix33 TransposeG23(float cos, float sin)
            {
                return new Matrix33(
                    (1, 0, 0),
                    (0, cos, -sin),
                    (0, sin, cos));
            }

            for (;;)
            {
                void Update12()
                {
                    (a11, a22, a12, a13, a23, v)
                        = Update(TransposeG12, a11, a22, a12, a13, a23, v);
                }

                void Update13()
                {
                    (a11, a33, a13, a12, a23, v)
                        = Update(TransposeG13, a11, a33, a13, a12, a23, v);
                }

                void Update23()
                {
                    (a22, a33, a23, a12, a13, v)
                        = Update(TransposeG23, a22, a33, a23, a12, a13, v);
                }

                var components = new (float IJ, Action UpdateIJ)[]
                {
                    (a12, Update12),
                    (a13, Update13),
                    (a23, Update23),
                };
                var (absIJ, updateIJ) = GetMaxComponent(components);
                if (absIJ < threshold)
                {
                    var d = new Matrix33(
                        (a11, a12, a13),
                        (a12, a22, a23),
                        (a13, a23, a33));
                    return (d, v);
                }
                updateIJ();
            }
        }
    }
}
