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

        /// <summary>
        /// Gets or sets the function that returns the sine of the specified
        /// angle in radians.
        /// </summary>
        /// <remarks>
        /// The default function is <see cref="Math.Sin(double)"/>.
        /// </remarks>
        public static Func<float, float> Sin { get; set; }
            = x => (float)Math.Sin(x);

        /// <summary>
        /// Gets or sets the function that returns the cosine of the specified
        /// angle in radians.
        /// </summary>
        /// <remarks>
        /// The default function is <see cref="Math.Cos(double)"/>.
        /// </remarks>
        public static Func<float, float> Cos { get; set; }
            = x => (float)Math.Cos(x);

        /// <summary>
        /// Gets or sets the function that returns the angle in radian whose
        /// tangent is the quotient of two specified numbers.
        /// </summary>
        /// <remarks>
        /// The default function is <see cref="Math.Atan2(double, double)"/>.
        /// </remarks>
        public static Func<float, float, float> Atan2 { get; set; }
            = (y, x) => (float)Math.Atan2(y, x);
    }
}
