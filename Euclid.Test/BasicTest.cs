namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class BasicTest
    {
        [TestMethod]
        public void RightTriangle()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            var a = new Position(3, 0, 0);
            var b = new Position(0, 4, 0);
            var c = a.Sub(b);
            Assert.AreEqual(3, c.X, float.Epsilon);
            Assert.AreEqual(-4, c.Y, float.Epsilon);
            Assert.AreEqual(0, c.Z, float.Epsilon);
            var length = c.Length();
            Assert.AreEqual(5, length, float.Epsilon);
        }

        [TestMethod]
        public void RightTriangleByTuple()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            var a = (3f, 0f, 0f);
            var b = (0f, 4f, 0f);
            var c = a.Sub(b);
            Assert.AreEqual(3, c.X, float.Epsilon);
            Assert.AreEqual(-4, c.Y, float.Epsilon);
            Assert.AreEqual(0, c.Z, float.Epsilon);
            var length = c.Length();
            Assert.AreEqual(5, length, float.Epsilon);
        }
    }
}
