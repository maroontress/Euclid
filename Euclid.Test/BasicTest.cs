namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class BasicTest
    {
        [TestInitialize]
        public void InitializeToolkit()
        {
            Toolkit.Sqrt = MathF.Sqrt;
            Toolkit.Atan2 = MathF.Atan2;
            Toolkit.Cos = MathF.Cos;
            Toolkit.Sin = MathF.Sin;
        }

        [TestMethod]
        public void RightTriangle()
        {
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
