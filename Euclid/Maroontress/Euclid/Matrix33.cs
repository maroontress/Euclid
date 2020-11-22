#pragma warning disable SA1117

namespace Maroontress.Euclid
{
    /// <summary>
    /// Represents the 3x3 square matrix.
    /// </summary>
    public sealed class Matrix33
    {
        private readonly float m1;
        private readonly float m2;
        private readonly float m3;
        private readonly float m4;
        private readonly float m5;
        private readonly float m6;
        private readonly float m7;
        private readonly float m8;
        private readonly float m9;

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix33"/> class.
        /// </summary>
        /// <param name="cx">
        /// The first column vector.
        /// </param>
        /// <param name="cy">
        /// The second column vector.
        /// </param>
        /// <param name="cz">
        /// The third column vector.
        /// </param>
        public Matrix33(Position cx, Position cy, Position cz)
        {
            m1 = cx.X;
            m4 = cx.Y;
            m7 = cx.Z;

            m2 = cy.X;
            m5 = cy.Y;
            m8 = cy.Z;

            m3 = cz.X;
            m6 = cz.Y;
            m9 = cz.Z;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Matrix33"/> class.
        /// </summary>
        /// <param name="cx">
        /// The first column vector.
        /// </param>
        /// <param name="cy">
        /// The second column vector.
        /// </param>
        /// <param name="cz">
        /// The third column vector.
        /// </param>
        public Matrix33(
            in (float X, float Y, float Z) cx,
            in (float X, float Y, float Z) cy,
            in (float X, float Y, float Z) cz)
        {
            m1 = cx.X;
            m4 = cx.Y;
            m7 = cx.Z;

            m2 = cy.X;
            m5 = cy.Y;
            m8 = cy.Z;

            m3 = cz.X;
            m6 = cz.Y;
            m9 = cz.Z;
        }

        private Matrix33(params float[] array)
        {
            m1 = array[0];
            m2 = array[1];
            m3 = array[2];

            m4 = array[3];
            m5 = array[4];
            m6 = array[5];

            m7 = array[6];
            m8 = array[7];
            m9 = array[8];
        }

        /// <summary>
        /// Gets the identity matrix.
        /// </summary>
        public static Matrix33 Identity { get; } = new Matrix33(
            1, 0, 0,
            0, 1, 0,
            0, 0, 1);

        /// <summary>
        /// Gets the transpose matrix.
        /// </summary>
        /// <returns>
        /// The new transpose matrix.
        /// </returns>
        public Matrix33 Transpose()
        {
            return new Matrix33(
                m1, m4, m7,
                m2, m5, m8,
                m3, m6, m9);
        }

        /// <summary>
        /// Translate the specified position.
        /// </summary>
        /// <param name="p">
        /// The position, which represents the column vector.
        /// </param>
        /// <returns>
        /// The position representing the result of the matrix product.
        /// </returns>
        public Position Map(Position p)
        {
            var x = p.DotProduct((m1, m2, m3));
            var y = p.DotProduct((m4, m5, m6));
            var z = p.DotProduct((m7, m8, m9));
            return new Position(x, y, z);
        }

        /// <summary>
        /// Translate the specified position.
        /// </summary>
        /// <param name="p">
        /// The position, which represents the column vector.
        /// </param>
        /// <returns>
        /// The position representing the result of the matrix product.
        /// </returns>
        public (float X, float Y, float Z) Map((float X, float Y, float Z) p)
        {
            var x = p.DotProduct((m1, m2, m3));
            var y = p.DotProduct((m4, m5, m6));
            var z = p.DotProduct((m7, m8, m9));
            return (x, y, z);
        }

        /// <summary>
        /// Gets the matrix product of <c>this</c> and <paramref name="a"/>.
        /// </summary>
        /// <param name="a">
        /// Another matrix.
        /// </param>
        /// <returns>
        /// The matrix product <c>this</c> matrix and <paramref name="a"/>.
        /// </returns>
        public Matrix33 Mul(Matrix33 a)
        {
            var x = (a.m1, a.m4, a.m7);
            var y = (a.m2, a.m5, a.m8);
            var z = (a.m3, a.m6, a.m9);
            return new Matrix33(Map(x), Map(y), Map(z));
        }

        /// <summary>
        /// Gets the quaternion representation when this matrix is the rotation
        /// matrix.
        /// </summary>
        /// <returns>
        /// The WXYZ tuple representing the quaternion.
        /// </returns>
        public (float W, float X, float Y, float Z) ToWxyzTuple()
        {
            /*
                z = a + bi + cj + dk (with |z| = 1)

                    | aa + bb - cc - dd    2bc - 2ad            2bd + 2ac
                R = | 2bc + 2ad            aa - bb + cc - dd    2cd - 2ab
                    | 2bd - 2ac            2cd + 2ab            aa - bb - cc + dd
            */

            var aTrace = m1 + m5 + m9;
            var bTrace = m1 - m5 - m9;
            var cTrace = -m1 + m5 - m9;
            var dTrace = -m1 - m5 + m9;
            if (// m5 + m9 > 0
                aTrace > bTrace
                // m1 + m9 > 0
                && aTrace > cTrace
                // m1 + m5 > 0
                && aTrace > dTrace)
            {
                var s = Toolkit.Sqrt(aTrace + 1.0f) * 2;
                var a = 0.25f * s;
                var b = (m8 - m6) / s;
                var c = (m3 - m7) / s;
                var d = (m4 - m2) / s;
                return (a, b, c, d);
            }
            if (// m1 - m5 > 0
                bTrace > cTrace
                // m1 - m9 > 0
                && bTrace > dTrace)
            {
                var s = Toolkit.Sqrt(bTrace + 1.0f) * 2;
                var b = 0.25f * s;
                var a = (m8 - m6) / s;
                var c = (m4 + m2) / s;
                var d = (m3 + m7) / s;
                return (a, b, c, d);
            }
            if (// m5 - m9 > 0
                cTrace > dTrace)
            {
                var s = Toolkit.Sqrt(cTrace + 1.0f) * 2;
                var c = 0.25f * s;
                var a = (m3 - m7) / s;
                var b = (m4 + m2) / s;
                var d = (m8 + m6) / s;
                return (a, b, c, d);
            }
            else
            {
                var s = Toolkit.Sqrt(dTrace + 1.0f) * 2;
                var d = 0.25f * s;
                var a = (m4 - m2) / s;
                var b = (m3 + m7) / s;
                var c = (m8 + m6) / s;
                return (a, b, c, d);
            }
        }

        /// <summary>
        /// Gets the determinant of this matrix.
        /// </summary>
        /// <returns>
        /// The determinant.
        /// </returns>
        public float Determinant()
        {
            return (m1 * m5 * m9)
                + (m2 * m6 * m7)
                + (m3 * m4 * m8)
                - (m3 * m5 * m7)
                - (m2 * m4 * m9)
                - (m1 * m6 * m8);
        }

        /// <summary>
        /// Gets the first column vector.
        /// </summary>
        /// <returns>
        /// The first column vector, which is equivalent to the result of
        /// <c>this.Map(Position.XUnit)</c>.
        /// </returns>
        public Position Column1() => new Position(m1, m4, m7);

        /// <summary>
        /// Gets the second column vector.
        /// </summary>
        /// <returns>
        /// The second column vector, which is equivalent to the result of
        /// <c>this.Map(Position.YUnit)</c>.
        /// </returns>
        public Position Column2() => new Position(m2, m5, m8);

        /// <summary>
        /// Gets the third column vector.
        /// </summary>
        /// <returns>
        /// The third column vector, which is equivalent to the result of
        /// <c>this.Map(Position.ZUnit)</c>.
        /// </returns>
        public Position Column3() => new Position(m3, m6, m9);

        /// <summary>
        /// Gets the tuple representing the first column vector.
        /// </summary>
        /// <returns>
        /// The first column vector, which is equivalent to the result of
        /// <c>this.Map((1f, 0f, 0f))</c>.
        /// </returns>
        public (float X, float Y, float Z) Column1Tuple() => (m1, m4, m7);

        /// <summary>
        /// Gets the tuple representing the second column vector.
        /// </summary>
        /// <returns>
        /// The second column vector, which is equivalent to the result of
        /// <c>this.Map((0f, 1f, 0f))</c>.
        /// </returns>
        public (float X, float Y, float Z) Column2Tuple() => (m2, m5, m8);

        /// <summary>
        /// Gets the tuple representing the third column vector.
        /// </summary>
        /// <returns>
        /// The third column vector, which is equivalent to the result of
        /// <c>this.Map((0f, 0f, 1f))</c>.
        /// </returns>
        public (float X, float Y, float Z) Column3Tuple() => (m3, m6, m9);
    }
}
