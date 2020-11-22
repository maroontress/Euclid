namespace Maroontress.Euclid.Test
{
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class PositionTest
    {
        [TestMethod]
        public void XyzProperties()
        {
            var p = new Position(1, 2, 3);
            Assert.AreEqual(1f, p.X, float.Epsilon);
            Assert.AreEqual(2f, p.Y, float.Epsilon);
            Assert.AreEqual(3f, p.Z, float.Epsilon);
        }

        [TestMethod]
        public void Origin()
        {
            var p = Position.Origin;
            Assert.AreEqual(0f, p.X, float.Epsilon);
            Assert.AreEqual(0f, p.Y, float.Epsilon);
            Assert.AreEqual(0f, p.Z, float.Epsilon);
        }

        [TestMethod]
        public void XUnit()
        {
            var p = Position.XUnit;
            Assert.AreEqual(1f, p.X, float.Epsilon);
            Assert.AreEqual(0f, p.Y, float.Epsilon);
            Assert.AreEqual(0f, p.Z, float.Epsilon);
        }

        [TestMethod]
        public void YUnit()
        {
            var p = Position.YUnit;
            Assert.AreEqual(0f, p.X, float.Epsilon);
            Assert.AreEqual(1f, p.Y, float.Epsilon);
            Assert.AreEqual(0f, p.Z, float.Epsilon);
        }

        [TestMethod]
        public void ZUnit()
        {
            var p = Position.ZUnit;
            Assert.AreEqual(0f, p.X, float.Epsilon);
            Assert.AreEqual(0f, p.Y, float.Epsilon);
            Assert.AreEqual(1f, p.Z, float.Epsilon);
        }
    }
}
