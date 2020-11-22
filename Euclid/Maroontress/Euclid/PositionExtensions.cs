namespace Maroontress.Euclid
{
    /// <summary>
    /// Provides extension methods of the <see cref="Position"/> class.
    /// </summary>
    public static class PositionExtensions
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
        public static Position Opposite(this Position p)
        {
            return new Position(-p.X, -p.Y, -p.Z);
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
        public static Position Add(this Position p, Position q)
        {
            return new Position(
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
        public static Position Add(
            this Position p,
            in (float X, float Y, float Z) q)
        {
            return new Position(
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
        public static Position Sub(this Position p, Position q)
        {
            return new Position(
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
        public static Position Sub(
            this Position p,
            in (float X, float Y, float Z) q)
        {
            return new Position(
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
        public static Position Mul(this Position p, float m)
        {
            return new Position(
                m * p.X,
                m * p.Y,
                m * p.Z);
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
        public static Position Div(this Position p, float m)
        {
            return new Position(
                p.X / m,
                p.Y / m,
                p.Z / m);
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
        public static float SquareLength(this Position p)
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
        public static float Length(this Position p)
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
        public static Position CrossProduct(
            this Position p, Position q)
        {
            return new Position(
                (p.Y * q.Z) - (p.Z * q.Y),
                (p.Z * q.X) - (p.X * q.Z),
                (p.X * q.Y) - (p.Y * q.X));
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
        public static Position CrossProduct(
            this Position p,
            in (float X, float Y, float Z) q)
        {
            return new Position(
                (p.Y * q.Z) - (p.Z * q.Y),
                (p.Z * q.X) - (p.X * q.Z),
                (p.X * q.Y) - (p.Y * q.X));
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
        public static Position Normalize(this Position p)
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
        public static float DotProduct(this Position p, Position q)
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
            this Position p,
            in (float X, float Y, float Z) q)
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
        public static Position GetLerp(
            this Position p, Position q, float t)
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
        public static Position GetLerp(
            this Position p,
            in (float X, float Y, float Z) q,
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
        public static Position InverseX(this Position p)
        {
            return new Position(-p.X, p.Y, p.Z);
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
        public static Position InverseY(this Position p)
        {
            return new Position(p.X, -p.Y, p.Z);
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
        public static Position InverseZ(this Position p)
        {
            return new Position(p.X, p.Y, -p.Z);
        }

        /// <summary>
        /// Gets the new XYZ tuple representing <c>this</c> position.
        /// </summary>
        /// <param name="p">
        /// The position.
        /// </param>
        /// <returns>
        /// The new XYZ tuple.
        /// </returns>
        public static (float X, float Y, float Z) ToTuple(this Position p)
        {
            return (p.X, p.Y, p.Z);
        }
    }
}
