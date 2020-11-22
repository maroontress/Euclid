namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class XyzTupleExtensionsTest
    {
        [TestMethod]
        public void Opposite()
        {
            var p = (1f, 2f, 3f);
            var q = p.Opposite();
            Asserts.AreEqual((-1, -2, -3), q);
        }

        [TestMethod]
        public void Add()
        {
            var p = (1f, 2f, 3f);
            var q = (4f, 5f, 6f);
            var r = p.Add(q);
            Asserts.AreEqual((5, 7, 9), r);
        }

        [TestMethod]
        public void AddPosition()
        {
            var p = (1f, 2f, 3f);
            var r = p.Add((4, 5, 6));
            Asserts.AreEqual((5, 7, 9), r);
        }

        [TestMethod]
        public void Sub()
        {
            var p = (3f, 5f, 7f);
            var q = (1f, 2f, 3f);
            var r = p.Sub(q);
            Asserts.AreEqual((2, 3, 4), r);
        }

        [TestMethod]
        public void SubPosition()
        {
            var p = (3f, 5f, 7f);
            var r = p.Sub(new Position(1, 2, 3));
            Asserts.AreEqual((2, 3, 4), r);
        }

        [TestMethod]
        public void Mul()
        {
            var p = (1f, 2f, 3f);
            var q = p.Mul(2);
            Asserts.AreEqual((2, 4, 6), q);
        }

        [TestMethod]
        public void Div()
        {
            var p = (1f, 2f, 3f);
            var q = p.Div(2);
            Asserts.AreEqual((0.5f, 1.0f, 1.5f), q);
        }

        [TestMethod]
        public void SquareLength()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            var p = (1f, 2f, 3f);
            var q = p.SquareLength();
            Assert.AreEqual(14, q, float.Epsilon);
        }

        [TestMethod]
        public void Length()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            var p = (1f, 2f, 3f);
            var q = p.Length();
            Assert.AreEqual(MathF.Sqrt(14), q, float.Epsilon);
        }

        [TestMethod]
        public void CrossProduct()
        {
            var p = (1f, 1f, 1f);
            var q = (-1f, -1f, 1f);
            var r = p.CrossProduct(q);
            Asserts.AreEqual((2f, -2f, 0f), r);
        }

        [TestMethod]
        public void CrossProductPosition()
        {
            var p = (1f, 1f, 1f);
            var r = p.CrossProduct(new Position(-1, -1, 1));
            Asserts.AreEqual((2f, -2f, 0f), r);
        }

        [TestMethod]
        public void Normalize()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            var p = (1f, 2f, 3f);
            var q = p.Normalize();
            var a = MathF.Sqrt(14);
            Asserts.AreEqual((1 / a, 2 / a, 3 / a), q);
        }

        [TestMethod]
        public void DotProduct()
        {
            var p = (1f, 2f, 3f);
            var q = (4f, 5f, 6f);
            var r = p.DotProduct(q);
            Assert.AreEqual(32, r, float.Epsilon);
        }

        [TestMethod]
        public void DotProductTuple()
        {
            var p = (1f, 2f, 3f);
            var r = p.DotProduct(new Position(4, 5, 6));
            Assert.AreEqual(32, r, float.Epsilon);
        }

        [TestMethod]
        public void GetLerp()
        {
            var p = (1f, 2f, 3f);
            var q = (4f, 5f, 6f);
            var r = p.GetLerp(q, 0.25f);
            var x = (0.75f * 1) + (0.25f * 4);
            var y = (0.75f * 2) + (0.25f * 5);
            var z = (0.75f * 3) + (0.25f * 6);
            Asserts.AreEqual((x, y, z), r);
        }

        [TestMethod]
        public void GetLerpPosition()
        {
            var p = (1f, 2f, 3f);
            var r = p.GetLerp(new Position(4, 5, 6), 0.25f);
            var x = (0.75f * 1) + (0.25f * 4);
            var y = (0.75f * 2) + (0.25f * 5);
            var z = (0.75f * 3) + (0.25f * 6);
            Asserts.AreEqual((x, y, z), r);
        }

        [TestMethod]
        public void InverseX()
        {
            var p = (1f, 2f, 3f);
            var q = p.InverseX();
            Asserts.AreEqual((-1, 2, 3), q);
        }

        [TestMethod]
        public void InverseY()
        {
            var p = (1f, 2f, 3f);
            var q = p.InverseY();
            Asserts.AreEqual((1, -2, 3), q);
        }

        [TestMethod]
        public void InverseZ()
        {
            var p = (1f, 2f, 3f);
            var q = p.InverseZ();
            Asserts.AreEqual((1, 2, -3), q);
        }

        [TestMethod]
        public void ToPosition()
        {
            var p = (1f, 2f, 3f);
            var q = p.ToPosition();
            Assert.AreEqual(1, q.X, float.Epsilon);
            Assert.AreEqual(2, q.Y, float.Epsilon);
            Assert.AreEqual(3, q.Z, float.Epsilon);
        }
    }
}
