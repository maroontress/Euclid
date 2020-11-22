namespace Maroontress.Euclid
{
    /// <summary>
    /// Provides extension methods of the <c>(float X, float Y, float Z)</c>
    /// tuple representing a vector in three-dimensional Euclidean space.
    /// </summary>
    public static class XyzTupleExtentions
    {
        /// <summary>
        /// Gets the opposite vector.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The opposite vector of <paramref name="p"/>.
        /// </returns>
        public static (float X, float Y, float Z) Opposite(
            in this (float X, float Y, float Z) p)
        {
            var (x, y, z) = p;
            return (-x, -y, -z);
        }

        /// <summary>
        /// Gets the sum of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The sum of <paramref name="p"/> and <paramref name="q"/>.
        /// </returns>
        public static (float X, float Y, float Z) Add(
            in this (float X, float Y, float Z) p,
            in (float X, float Y, float Z) q)
        {
            return (
                p.X + q.X,
                p.Y + q.Y,
                p.Z + q.Z);
        }

        /// <summary>
        /// Gets the sum of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The sum of <paramref name="p"/> and <paramref name="q"/>.
        /// </returns>
        public static (float X, float Y, float Z) Add(
            in this (float X, float Y, float Z) p,
            Position q)
        {
            return (
                p.X + q.X,
                p.Y + q.Y,
                p.Z + q.Z);
        }

        /// <summary>
        /// Gets the difference between two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The difference between the two vectors, which represents
        /// subtracting <paramref name="q"/> from <paramref name="p"/>.
        /// </returns>
        public static (float X, float Y, float Z) Sub(
            in this (float X, float Y, float Z) p,
            in (float X, float Y, float Z) q)
        {
            return (
                p.X - q.X,
                p.Y - q.Y,
                p.Z - q.Z);
        }

        /// <summary>
        /// Gets the difference between two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The difference between the two vectors, which represents
        /// subtracting <paramref name="q"/> from <paramref name="p"/>.
        /// </returns>
        public static (float X, float Y, float Z) Sub(
            in this (float X, float Y, float Z) p,
            Position q)
        {
            return (
                p.X - q.X,
                p.Y - q.Y,
                p.Z - q.Z);
        }

        /// <summary>
        /// Gets the scalar multiplication.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="m">
        /// A scalar.
        /// </param>
        /// <returns>
        /// A vector multiplied by <paramref name="m"/>.
        /// </returns>
        public static (float X, float Y, float Z) Mul(
            in this (float X, float Y, float Z) p,
            float m)
        {
            var (x, y, z) = p;
            return (
                m * x,
                m * y,
                m * z);
        }

        /// <summary>
        /// Gets the scalar division.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="m">
        /// A scalar.
        /// </param>
        /// <returns>
        /// A vector divided by <paramref name="m"/>.
        /// </returns>
        public static (float X, float Y, float Z) Div(
            in this (float X, float Y, float Z) p,
            float m)
        {
            var (x, y, z) = p;
            return (
                x / m,
                y / m,
                z / m);
        }

        /// <summary>
        /// Gets the squared length.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The squared length.
        /// </returns>
        public static float SquareLength(
            in this (float X, float Y, float Z) p)
        {
            return p.DotProduct(p);
        }

        /// <summary>
        /// Gets the length.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The length.
        /// </returns>
        public static float Length(
            in this (float X, float Y, float Z) p)
        {
            return Toolkit.Sqrt(p.SquareLength());
        }

        /// <summary>
        /// Gets the cross product of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The cross product of two vectors, which represents
        /// <paramref name="p"/> &#215; <paramref name="q"/>.
        /// </returns>
        public static (float X, float Y, float Z) CrossProduct(
            in this (float X, float Y, float Z) p,
            in (float X, float Y, float Z) q)
        {
            var (px, py, pz) = p;
            var (qx, qy, qz) = q;
            return (
                (py * qz) - (pz * qy),
                (pz * qx) - (px * qz),
                (px * qy) - (py * qx));
        }

        /// <summary>
        /// Gets the cross product of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The cross product of two vectors, which represents
        /// <paramref name="p"/> &#215; <paramref name="q"/>.
        /// </returns>
        public static (float X, float Y, float Z) CrossProduct(
            in this (float X, float Y, float Z) p,
            Position q)
        {
            var (px, py, pz) = p;
            var (qx, qy, qz) = q.ToTuple();
            return (
                (py * qz) - (pz * qy),
                (pz * qx) - (px * qz),
                (px * qy) - (py * qx));
        }

        /// <summary>
        /// Gets the normalized vector.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// A unit vector, which is the specified vector with a length of one.
        /// </returns>
        public static (float X, float Y, float Z) Normalize(
            in this (float X, float Y, float Z) p)
        {
            var m = p.Length();
            return p.Div(m);
        }

        /// <summary>
        /// Gets the dot product of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="p"/> and <paramref name="q"/>.
        /// </returns>
        public static float DotProduct(
            in this (float X, float Y, float Z) p,
            in (float X, float Y, float Z) q)
        {
            return (p.X * q.X)
                + (p.Y * q.Y)
                + (p.Z * q.Z);
        }

        /// <summary>
        /// Gets the dot product of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <returns>
        /// The dot product of <paramref name="p"/> and <paramref name="q"/>.
        /// </returns>
        public static float DotProduct(
            in this (float X, float Y, float Z) p,
            Position q)
        {
            return (p.X * q.X)
                + (p.Y * q.Y)
                + (p.Z * q.Z);
        }

        /// <summary>
        /// Gets the linear interpolation (Lerp) of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <param name="t">
        /// A parameter between zero and one, inclusively.
        /// </param>
        /// <returns>
        /// The linear-interpolated vector. This method guarantees that the
        /// return value equals <paramref name="p"/> when <paramref name="t"/>
        /// equals zero, and equals <paramref name="q"/> when <paramref
        /// name="t"/> equals one.
        /// </returns>
        public static (float X, float Y, float Z) GetLerp(
            in this (float X, float Y, float Z) p,
            in (float X, float Y, float Z) q,
            float t)
        {
            var u = 1.0f - t;
            return p.Mul(u).Add(q.Mul(t));
        }

        /// <summary>
        /// Gets the linear interpolation (Lerp) of two vectors.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <param name="q">
        /// Another vector.
        /// </param>
        /// <param name="t">
        /// A parameter between zero and one, inclusively.
        /// </param>
        /// <returns>
        /// The linear-interpolated vector. This method guarantees that the
        /// return value equals <paramref name="p"/> when <paramref name="t"/>
        /// equals zero, and equals <paramref name="q"/> when <paramref
        /// name="t"/> equals one.
        /// </returns>
        public static (float X, float Y, float Z) GetLerp(
            in this (float X, float Y, float Z) p,
            Position q,
            float t)
        {
            var u = 1.0f - t;
            return p.Mul(u).Add(q.Mul(t));
        }

        /// <summary>
        /// Gets the vector with the X component inversed.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The vector with the X component inversed.
        /// </returns>
        public static (float X, float Y, float Z) InverseX(
            in this (float X, float Y, float Z) p)
        {
            return (-p.X, p.Y, p.Z);
        }

        /// <summary>
        /// Gets the vector with the Y component inversed.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The vector with the Y component inversed.
        /// </returns>
        public static (float X, float Y, float Z) InverseY(
            in this (float X, float Y, float Z) p)
        {
            return (p.X, -p.Y, p.Z);
        }

        /// <summary>
        /// Gets the vector with the Z component inversed.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The vector with the Z component inversed.
        /// </returns>
        public static (float X, float Y, float Z) InverseZ(
            in this (float X, float Y, float Z) p)
        {
            return (p.X, p.Y, -p.Z);
        }

        /// <summary>
        /// Gets the new <see cref="Position"/> object representing this.
        /// </summary>
        /// <param name="p">
        /// A vector.
        /// </param>
        /// <returns>
        /// The new <see cref="Position"/> object representing <c>this</c>
        /// tuple.
        /// </returns>
        public static Position ToPosition(
            in this (float X, float Y, float Z) p)
        {
            var (x, y, z) = p;
            return new Position(x, y, z);
        }
    }
}
