namespace Maroontress.Euclid.Test
{
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class PostureTest
    {
        [TestMethod]
        public void Identity()
        {
            var pose = Posture.Identity;
            Assert.AreSame(Position.Origin, pose.Position);
            Assert.AreSame(Matrix33.Identity, pose.Rotation);
        }

        [TestMethod]
        public void PositionAndRotation()
        {
            var p = new Position(1, 2, 3);
            var m = new Matrix33(
                (0, 1, 0),
                (0, 0, 1),
                (1, 0, 0));
            var pose = new Posture(p, m);
            Assert.AreSame(p, pose.Position);
            Assert.AreSame(m, pose.Rotation);
        }

        [TestMethod]
        public void FromLocalToParent()
        {
            var p = new Position(1, 2, 3);
            var m = new Matrix33(
                (0, 1, 0),
                (0, 0, 1),
                (1, 0, 0));
            // Note thar both 'p' and 'm' are represented in the parent
            // coordinate system.
            var pose = new Posture(p, m);
            var q = pose.FromLocalToParent(new Position(4, 5, 6));
            // | 0 0 1 || 4 |   | 1 |   | 6 + 1 |
            // | 1 0 0 || 5 | + | 2 | = | 4 + 2 |
            // | 0 1 0 || 6 |   | 3 |   | 5 + 3 |
            Asserts.AreEqual((7f, 6f, 8f), q);
        }

        [TestMethod]
        public void FromParentToLocal()
        {
            var p = new Position(1, 2, 3);
            var m = new Matrix33(
                (0, 1, 0),
                (0, 0, 1),
                (1, 0, 0));
            // Note thar both 'p' and 'm' are represented in the parent
            // coordinate system.
            var pose = new Posture(p, m);
            var q = pose.FromParentToLocal(new Position(7, 6, 8));
            // | 0 1 0 || 7 - 1 |   | 4 |
            // | 0 0 1 || 6 - 2 | = | 5 |
            // | 1 0 0 || 8 - 3 |   | 6 |
            Asserts.AreEqual((4f, 5f, 6f), q);
        }
    }
}
