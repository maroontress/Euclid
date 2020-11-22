namespace Maroontress.Euclid
{
    /// <summary>
    /// The position and rotation.
    /// </summary>
    public sealed class Posture
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Posture"/> class.
        /// </summary>
        /// <param name="position">
        /// The position in the parent coordinate system, which represents the
        /// origin of this coordinate system.
        /// </param>
        /// <param name="rotation">
        /// The rotation in the parent coordinate system, which represents the
        /// three axes of this coordinate system. It must be a rotation matrix.
        /// </param>
        public Posture(Position position, Matrix33 rotation)
        {
            Position = position;
            Rotation = rotation;
        }

        /// <summary>
        /// Gets the identity posture, whose position and rotation are <see
        /// cref="Position.Origin"/> and <see cref="Matrix33.Identity"/>.
        /// </summary>
        public static Posture Identity { get; } = new Posture(
            Position.Origin,
            Matrix33.Identity);

        /// <summary>
        /// Gets the position.
        /// </summary>
        public Position Position { get; }

        /// <summary>
        /// Gets the rotation.
        /// </summary>
        public Matrix33 Rotation { get; }

        /// <summary>
        /// Translates the specified local position in this coordinate system
        /// into the position in the parent coordinate system.
        /// </summary>
        /// <param name="localPosition">
        /// The local position.
        /// </param>
        /// <returns>
        /// The position in the parent coordinate system.
        /// </returns>
        public Position FromLocalToParent(Position localPosition)
        {
            return Rotation.Map(localPosition).Add(Position);
        }

        /// <summary>
        /// Translates the specified position in the parent coordinate system
        /// into the local position in this coordinate system.
        /// </summary>
        /// <param name="parentPosition">
        /// The position in the parent coordinate system.
        /// </param>
        /// <returns>
        /// The local position.
        /// </returns>
        public Position FromParentToLocal(Position parentPosition)
        {
            return Rotation.Transpose().Map(parentPosition.Sub(Position));
        }
    }
}
