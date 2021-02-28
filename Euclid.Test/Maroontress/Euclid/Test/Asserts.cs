namespace Maroontress.Euclid.Test
{
    using System;
    using System.Collections.Generic;
    using System.Collections.Immutable;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

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

        public static void AreEqual(
            (float X, float Y, float Z) p,
            (float X, float Y, float Z) q)
        {
            AreEqual(p, q, float.Epsilon);
        }

        public static void AreEqual(
            (float X, float Y, float Z) p,
            (float X, float Y, float Z) q,
            float delta)
        {
            AreEqualTuples(p, q, delta);
        }

        public static void AreEqual(
            (float X, float Y, float Z) p,
            Position q)
        {
            AreEqual(p, q, float.Epsilon);
        }

        public static void AreEqual(
            (float X, float Y, float Z) p,
            Position q,
            float delta)
        {
            AreEqual(p, q.ToTuple(), delta);
        }

        public static void AreEqual(
            Position p,
            Position q)
        {
            AreEqual(p, q, float.Epsilon);
        }

        public static void AreEqual(
            Position p, Position q, float delta)
        {
            AreEqual(p.ToTuple(), q.ToTuple(), delta);
        }

        public static void AreEqual(
            Matrix33 m1, Matrix33 m2, float delta)
        {
            foreach (var v in ToColumnVectors)
            {
                AreEqual(v(m1), v(m2), delta);
            }
        }

        private static void AreEqualTuples(
            (float X, float Y, float Z) p,
            (float X, float Y, float Z) q,
            float delta)
        {
            foreach (var m in ToScalars)
            {
                Assert.AreEqual(m(p), m(q), delta);
            }
        }
    }
}
