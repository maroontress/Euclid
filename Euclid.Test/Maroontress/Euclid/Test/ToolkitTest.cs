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
            Assert.AreEqual(2f, Toolkit.Sqrt(4), float.Epsilon);
            Assert.AreEqual(3f, Toolkit.Sqrt(9), float.Epsilon);
        }

        [TestMethod]
        public void WithMathF()
        {
            Toolkit.Sqrt = MathF.Sqrt;

            Assert.AreEqual(2f, Toolkit.Sqrt(4), float.Epsilon);
            Assert.AreEqual(3f, Toolkit.Sqrt(9), float.Epsilon);
        }
    }
}
