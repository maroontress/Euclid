namespace Maroontress.Euclid.Test
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    public static class Asserts
    {
        public static void AreEqual(
            (float X, float Y, float Z) p,
            (float X, float Y, float Z) q)
        {
            Assert.AreEqual(p.X, q.X, float.Epsilon);
            Assert.AreEqual(p.Y, q.Y, float.Epsilon);
            Assert.AreEqual(p.Z, q.Z, float.Epsilon);
        }

        public static void AreEqual(
            (float X, float Y, float Z) p,
            Position q)
        {
            Assert.AreEqual(p.X, q.X, float.Epsilon);
            Assert.AreEqual(p.Y, q.Y, float.Epsilon);
            Assert.AreEqual(p.Z, q.Z, float.Epsilon);
        }

        public static void AreEqual(
            (float X, float Y, float Z) p,
            Position q,
            float delta)
        {
            Assert.AreEqual(p.X, q.X, delta);
            Assert.AreEqual(p.Y, q.Y, delta);
            Assert.AreEqual(p.Z, q.Z, delta);
        }

        public static void AreEqual(
            Position p,
            Position q)
        {
            AreEqual(p, q, float.Epsilon);
        }

        public static void AreEqual(
            Position p1, Position p2, float delta)
        {
            Assert.AreEqual(p1.X, p2.X, delta);
            Assert.AreEqual(p1.Y, p2.Y, delta);
            Assert.AreEqual(p1.Z, p2.Z, delta);
        }

        public static void AreEqual(
            Matrix33 m1, Matrix33 m2, float delta)
        {
            var x1 = m1.Map(Position.XUnit);
            var x2 = m2.Map(Position.XUnit);
            AreEqual(x1, x2, delta);
            var y1 = m1.Map(Position.YUnit);
            var y2 = m2.Map(Position.YUnit);
            AreEqual(y1, y2, delta);
            var z1 = m1.Map(Position.ZUnit);
            var z2 = m2.Map(Position.ZUnit);
            AreEqual(z1, z2, delta);
        }
    }
}
