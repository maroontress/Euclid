namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class PositionExtensionsTest
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
        public void Opposite()
        {
            var p = new Position(1, 2, 3);
            var q = p.Opposite();
            Asserts.AreEqual((-1, -2, -3), q);
        }

        [TestMethod]
        public void Add()
        {
            var p = new Position(1, 2, 3);
            var q = new Position(4, 5, 6);
            var r = p.Add(q);
            Asserts.AreEqual((5, 7, 9), r);
        }

        [TestMethod]
        public void AddTuple()
        {
            var p = new Position(1, 2, 3);
            var r = p.Add((4, 5, 6));
            Asserts.AreEqual((5, 7, 9), r);
        }

        [TestMethod]
        public void Sub()
        {
            var p = new Position(3, 5, 7);
            var q = new Position(1, 2, 3);
            var r = p.Sub(q);
            Asserts.AreEqual((2, 3, 4), r);
        }

        [TestMethod]
        public void SubTuple()
        {
            var p = new Position(3, 5, 7);
            var r = p.Sub((1, 2, 3));
            Asserts.AreEqual((2, 3, 4), r);
        }

        [TestMethod]
        public void Mul()
        {
            var p = new Position(1, 2, 3);
            var q = p.Mul(2);
            Asserts.AreEqual((2, 4, 6), q);
        }

        [TestMethod]
        public void Div()
        {
            var p = new Position(1, 2, 3);
            var q = p.Div(2);
            Asserts.AreEqual((0.5f, 1.0f, 1.5f), q);
        }

        [TestMethod]
        public void SquareLength()
        {
            var p = new Position(1, 2, 3);
            var q = p.SquareLength();
            Assert.AreEqual(14, q, float.Epsilon);
        }

        [TestMethod]
        public void Length()
        {
            var p = new Position(1, 2, 3);
            var q = p.Length();
            Assert.AreEqual(MathF.Sqrt(14), q, float.Epsilon);
        }

        [TestMethod]
        public void CrossProduct()
        {
            var p = new Position(1, 1, 1);
            var q = new Position(-1, -1, 1);
            var r = p.CrossProduct(q);
            Asserts.AreEqual((2f, -2f, 0f), r);
        }

        [TestMethod]
        public void CrossProductTuple()
        {
            var p = new Position(1, 1, 1);
            var r = p.CrossProduct((-1, -1, 1));
            Asserts.AreEqual((2f, -2f, 0f), r);
        }

        [TestMethod]
        public void Normalize()
        {
            var p = new Position(1, 2, 3);
            var q = p.Normalize();
            var a = MathF.Sqrt(14);
            Asserts.AreEqual((1 / a, 2 / a, 3 / a), q);
        }

        [TestMethod]
        public void DotProduct()
        {
            var p = new Position(1, 2, 3);
            var q = new Position(4, 5, 6);
            var r = p.DotProduct(q);
            Assert.AreEqual(32, r, float.Epsilon);
        }

        [TestMethod]
        public void DotProductTuple()
        {
            var p = new Position(1, 2, 3);
            var r = p.DotProduct((4, 5, 6));
            Assert.AreEqual(32, r, float.Epsilon);
        }

        [TestMethod]
        public void GetLerp()
        {
            var p = new Position(1, 2, 3);
            var q = new Position(4, 5, 6);
            var r = p.GetLerp(q, 0.25f);
            var x = (0.75f * 1) + (0.25f * 4);
            var y = (0.75f * 2) + (0.25f * 5);
            var z = (0.75f * 3) + (0.25f * 6);
            Asserts.AreEqual((x, y, z), r);
        }

        [TestMethod]
        public void GetLerpTuple()
        {
            var p = new Position(1, 2, 3);
            var r = p.GetLerp((4, 5, 6), 0.25f);
            var x = (0.75f * 1) + (0.25f * 4);
            var y = (0.75f * 2) + (0.25f * 5);
            var z = (0.75f * 3) + (0.25f * 6);
            Asserts.AreEqual((x, y, z), r);
        }

        [TestMethod]
        public void InverseX()
        {
            var p = new Position(1, 2, 3);
            var q = p.InverseX();
            Asserts.AreEqual((-1, 2, 3), q);
        }

        [TestMethod]
        public void InverseY()
        {
            var p = new Position(1, 2, 3);
            var q = p.InverseY();
            Asserts.AreEqual((1, -2, 3), q);
        }

        [TestMethod]
        public void InverseZ()
        {
            var p = new Position(1, 2, 3);
            var q = p.InverseZ();
            Asserts.AreEqual((1, 2, -3), q);
        }

        [TestMethod]
        public void ToTuple()
        {
            var p = new Position(1, 2, 3);
            var (x, y, z) = p.ToTuple();
            Assert.AreEqual(1, x, float.Epsilon);
            Assert.AreEqual(2, y, float.Epsilon);
            Assert.AreEqual(3, z, float.Epsilon);
        }
    }
}
