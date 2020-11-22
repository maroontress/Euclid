namespace Maroontress.Euclid
{
    /// <summary>
    /// Represents a vector in three-dimensional Euclidean space.
    /// </summary>
    /// <remarks>
    /// The objects of this class are immutable.
    /// </remarks>
    public sealed class Position
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Position"/>
        /// class.
        /// </summary>
        /// <param name="x">
        /// The X component of the position.
        /// </param>
        /// <param name="y">
        /// The Y component of the position.
        /// </param>
        /// <param name="z">
        /// The Z component of the position.
        /// </param>
        public Position(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        /// <summary>
        /// Gets the origin (zero vector).
        /// </summary>
        public static Position Origin { get; } = new Position(0, 0, 0);

        /// <summary>
        /// Gets the unit vector along the X axis.
        /// </summary>
        public static Position XUnit { get; } = new Position(1, 0, 0);

        /// <summary>
        /// Gets the unit vector along the Y axis.
        /// </summary>
        public static Position YUnit { get; } = new Position(0, 1, 0);

        /// <summary>
        /// Gets the unit vector along the Z axis.
        /// </summary>
        public static Position ZUnit { get; } = new Position(0, 0, 1);

        /// <summary>
        /// Gets the X component of the position.
        /// </summary>
        public float X { get; }

        /// <summary>
        /// Gets the Y component of the position.
        /// </summary>
        public float Y { get; }

        /// <summary>
        /// Gets the Z component of the position.
        /// </summary>
        public float Z { get; }
    }
}
