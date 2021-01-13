namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class ToolkitTest
    {
        [TestMethod]
        public void Sqrt()
        {
            Assert.AreEqual(2f, Toolkit.Sqrt(4), 0.000_000_1f);
            Assert.AreEqual(3f, Toolkit.Sqrt(9), 0.000_000_1f);
        }

        [TestMethod]
        public void Atan2()
        {
            var root3 = MathF.Sqrt(3);
            Assert.AreEqual(
                (float)(Math.PI / 4),
                Toolkit.Atan2(1f, 1f),
                0.000_000_1f);
            Assert.AreEqual(
                (float)(Math.PI / 3),
                Toolkit.Atan2(root3 / 2, 0.5f),
                0.000_000_1f);
        }

        [TestMethod]
        public void Cos()
        {
            var root2 = MathF.Sqrt(2);
            Assert.AreEqual(
                root2 / 2,
                Toolkit.Cos((float)(Math.PI / 4)),
                0.000_000_1f);
            Assert.AreEqual(
                0.5f,
                Toolkit.Cos((float)(Math.PI / 3)),
                0.000_000_1f);
        }

        [TestMethod]
        public void Sin()
        {
            var root2 = MathF.Sqrt(2);
            Assert.AreEqual(
                root2 / 2,
                Toolkit.Sin((float)(Math.PI / 4)),
                0.000_000_1f);
            Assert.AreEqual(
                0.5f,
                Toolkit.Sin((float)(Math.PI / 6)),
                0.000_000_1f);
        }

        [TestMethod]
        public void WithMathF()
        {
            Toolkit.Sqrt = MathF.Sqrt;
            Toolkit.Atan2 = MathF.Atan2;
            Toolkit.Cos = MathF.Cos;
            Toolkit.Sin = MathF.Sin;

            Sqrt();
            Atan2();
            Cos();
            Sin();
        }
    }
}
