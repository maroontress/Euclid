namespace Maroontress.Euclid.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    /// <summary>
    /// Provides assertion methods for <see cref="Position"/>, <see
    /// cref="Matrix33"/>, and so on.
    /// </summary>
    public static class Asserts
    {
        private static readonly IEnumerable<ToScalar> ToScalars
            = ImmutableArray.Create<ToScalar>(p => p.X, p => p.Y, p => p.Z);

        private static readonly IEnumerable<ToColumnVector> ToColumnVectors
            = ImmutableArray.Create<ToColumnVector>(
                p => p.Column1Tuple(),
                p => p.Column2Tuple(),
                p => p.Column3Tuple());

        private delegate float ToScalar((float X, float Y, float Z) p);

        private delegate (float X, float Y, float Z)
                ToColumnVector(Matrix33 m);

        /// <summary>
        /// Tests whether the specified tuples representing positions are equal
        /// and throws an exception when the difference between X, Y, or Z
        /// components of them is more than <see cref="float.Epsilon"/>.
        /// </summary>
        /// <param name="p1">
        /// The first tuple to compare.
        /// </param>
        /// <param name="p2">
        /// The second tuple to compare.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the two tuples are not equal.
        /// </exception>
        public static void AreEqual(
            (float X, float Y, float Z) p1,
            (float X, float Y, float Z) p2)
        {
            AreEqual(p1, p2, float.Epsilon);
        }

        /// <summary>
        /// Tests whether the specified tuples representing positions are equal
        /// and throws an exception when the difference between X, Y, or Z
        /// components of them is more than the specified accuracy.
        /// </summary>
        /// <param name="p1">
        /// The first tuple to compare.
        /// </param>
        /// <param name="p2">
        /// The second tuple to compare.
        /// </param>
        /// <param name="delta">
        /// The required accuracy.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the two tuples are not equal.
        /// </exception>
        public static void AreEqual(
            (float X, float Y, float Z) p1,
            (float X, float Y, float Z) p2,
            float delta)
        {
            AreEqualTuples(p1, p2, delta);
        }

        /// <summary>
        /// Tests whether the specified tuple representing a position and the
        /// specified position are equal and throws an exception when the
        /// difference between X, Y, or Z components of them is more than
        /// <see cref="float.Epsilon"/>.
        /// </summary>
        /// <param name="p1">
        /// The first tuple to compare.
        /// </param>
        /// <param name="p2">
        /// The second position to compare.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the tuple and position are not equal.
        /// </exception>
        public static void AreEqual(
            (float X, float Y, float Z) p1,
            Position p2)
        {
            AreEqual(p1, p2, float.Epsilon);
        }

        /// <summary>
        /// Tests whether the specified tuple representing a position and the
        /// specified position are equal and throws an exception when the
        /// difference between X, Y, or Z components of them is more than the
        /// specified accuracy.
        /// </summary>
        /// <param name="p1">
        /// The first tuple to compare.
        /// </param>
        /// <param name="p2">
        /// The second position to compare.
        /// </param>
        /// <param name="delta">
        /// The required accuracy.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the tuple and position are not equal.
        /// </exception>
        public static void AreEqual(
            (float X, float Y, float Z) p1,
            Position p2,
            float delta)
        {
            AreEqual(p1, p2.ToTuple(), delta);
        }

        /// <summary>
        /// Tests whether the specified positions are equal and throws an
        /// exception when the difference between X, Y, or Z components of them
        /// is more than <see cref="float.Epsilon"/>.
        /// </summary>
        /// <param name="p1">
        /// The first position to compare.
        /// </param>
        /// <param name="p2">
        /// The second position to compare.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the two positions are not equal.
        /// </exception>
        public static void AreEqual(Position p1, Position p2)
        {
            AreEqual(p1, p2, float.Epsilon);
        }

        /// <summary>
        /// Tests whether the specified positions are equal and throws an
        /// exception when the difference between X, Y, or Z components of them
        /// is more than the specified accuracy.
        /// </summary>
        /// <param name="p1">
        /// The first position to compare.
        /// </param>
        /// <param name="p2">
        /// The second position to compare.
        /// </param>
        /// <param name="delta">
        /// The required accuracy.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the two positions are not equal.
        /// </exception>
        public static void AreEqual(Position p1, Position p2, float delta)
        {
            AreEqual(p1.ToTuple(), p2.ToTuple(), delta);
        }

        /// <summary>
        /// Tests whether the specified matrices are equal and throws an
        /// exception when the difference between corresponding components of
        /// them is more than the specified accuracy.
        /// </summary>
        /// <param name="m1">
        /// The first matrix to compare.
        /// </param>
        /// <param name="m2">
        /// The second matrix to compare.
        /// </param>
        /// <param name="delta">
        /// The required accuracy.
        /// </param>
        /// <exception cref="AssertFailedException">
        /// Thrown if the two matrix are not equal.
        /// </exception>
        public static void AreEqual(Matrix33 m1, Matrix33 m2, float delta)
        {
            foreach (var v in ToColumnVectors)
            {
                AreEqual(v(m1), v(m2), delta);
            }
        }

        private static void AreEqualTuples(
            (float X, float Y, float Z) p1,
            (float X, float Y, float Z) p2,
            float delta)
        {
            foreach (var m in ToScalars)
            {
                Assert.AreEqual(m(p1), m(p2), delta);
            }
        }
    }
}
