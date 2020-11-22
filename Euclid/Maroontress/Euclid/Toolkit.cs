namespace Maroontress.Euclid
{
    using System;

    /// <summary>
    /// Provides the dependency layer to customize.
    /// </summary>
    public static class Toolkit
    {
        /// <summary>
        /// Gets or sets the function that returns the square root of a
        /// specified number.
        /// </summary>
        /// <remarks>
        /// The default function is <see cref="Math.Sqrt(double)"/>.
        /// </remarks>
        public static Func<float, float> Sqrt { get; set; }
            = x => (float)Math.Sqrt(x);
    }
}
