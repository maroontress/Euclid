namespace Maroontress.Euclid
{
    /// <summary>
    /// Represents the nested coordinate system.
    /// </summary>
    public sealed class CoordinateSystem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateSystem"/>
        /// class.
        /// </summary>
        /// <param name="posture">
        /// The posture against the parent coordinate system.
        /// </param>
        /// <param name="parent">
        /// The parent coordinate system.
        /// </param>
        public CoordinateSystem(Posture posture, CoordinateSystem parent)
        {
            Parent = parent;
            Posture = posture;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CoordinateSystem"/>
        /// class, which parent is <see cref="World"/> coordinate system.
        /// </summary>
        /// <param name="posture">
        /// The posture against the parent coordinate system.
        /// </param>
        public CoordinateSystem(Posture posture)
            : this(posture, World)
        {
        }

        private CoordinateSystem()
        {
            Parent = this;
            Posture = Posture.Identity;
        }

        /// <summary>
        /// Gets the world coordinate system.
        /// </summary>
        public static CoordinateSystem World { get; } = new CoordinateSystem();

        /// <summary>
        /// Gets the parent coordinate system.
        /// </summary>
        public CoordinateSystem Parent { get; }

        /// <summary>
        /// Gets the posture against the parent coordinate system.
        /// </summary>
        public Posture Posture { get; }

        /// <summary>
        /// Translates the local position in <c>this</c> coordinate system into
        /// the world position in the world coordinate system.
        /// </summary>
        /// <param name="localPosition">
        /// The local position.
        /// </param>
        /// <returns>
        /// The world position in the world coordinate system.
        /// </returns>
        public Position TranslateIntoWorld(Position localPosition)
        {
            var p = localPosition;
            var c = this;
            while (!ReferenceEquals(c, World))
            {
                p = c.Posture.FromLocalToParent(p);
                c = c.Parent;
            }
            return p;
        }

        /// <summary>
        /// Translates the world position in the world coordinate system into
        /// the local position in <c>this</c> coordinate system.
        /// </summary>
        /// <param name="worldPosition">
        /// The world position.
        /// </param>
        /// <returns>
        /// The local position in <c>this</c> coordinate system.
        /// </returns>
        public Position TranslateIntoLocal(Position worldPosition)
        {
            var p = ReferenceEquals(Parent, World)
                ? worldPosition
                : Parent.TranslateIntoLocal(worldPosition);
            return Posture.FromParentToLocal(p);
        }

        /// <summary>
        /// Gets new coordinate system whose parent is this one.
        /// </summary>
        /// <param name="posture">
        /// The posture against <c>this</c> coordinate system.
        /// </param>
        /// <returns>
        /// The new coordinate system.
        /// </returns>
        public CoordinateSystem NewChild(Posture posture)
        {
            return new CoordinateSystem(posture, this);
        }
    }
}
