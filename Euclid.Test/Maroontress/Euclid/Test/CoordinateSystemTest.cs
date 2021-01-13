namespace Maroontress.Euclid.Test
{
    using System;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class CoordinateSystemTest
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
        public void World()
        {
            var system = CoordinateSystem.World;
            Assert.AreSame(system, system.Parent);
            var pose = system.Posture;
            Assert.AreSame(Posture.Identity, pose);
        }

        [TestMethod]
        public void ParentAndPosture()
        {
            var p = new Position(1, 2, 3);
            var m = new Matrix33(
                (0, 1, 0),
                (0, 0, 1),
                (1, 0, 0));
            var pose = new Posture(p, m);
            var system = new CoordinateSystem(pose);
            Assert.AreSame(CoordinateSystem.World, system.Parent);
            Assert.AreSame(pose, system.Posture);
        }

        [TestMethod]
        public void NewChild()
        {
            var p = new Position(1, 2, 3);
            var m = new Matrix33(
                (0, 1, 0),
                (0, 0, 1),
                (1, 0, 0));
            var pose = new Posture(p, m);
            var system = CoordinateSystem.World.NewChild(pose);
            Assert.AreSame(CoordinateSystem.World, system.Parent);
            Assert.AreSame(pose, system.Posture);
        }

        [TestMethod]
        public void TranslateIntoWorld()
        {
            var sunPose = new Posture(
                new Position(1, 2, 3),
                new Matrix33(
                    (0, 1, 0),
                    (0, 0, 1),
                    (1, 0, 0)));
            var sunSystem = CoordinateSystem.World
                .NewChild(sunPose);

            var earthPose = new Posture(
                new Position(-4, -5, -6),
                new Matrix33(
                    (0, 0, 1),
                    (1, 0, 0),
                    (0, 1, 0)));
            var earthSystem = sunSystem.NewChild(earthPose);

            var p = earthSystem.TranslateIntoWorld(
                new Position(7, 8, 9));

            // Translate (7, 8, 9) to the Sun coordinate system:
            // | 0 1 0 || 7 |   | -4 |   | 4 |
            // | 0 0 1 || 8 | + | -5 | = | 4 |
            // | 1 0 0 || 9 |   | -6 |   | 1 |
            //
            // And then, translate it to the World coordinate system:
            // | 0 0 1 || 4 |   | 1 |   | 2 |
            // | 1 0 0 || 4 | + | 2 | = | 6 |
            // | 0 1 0 || 1 |   | 3 |   | 7 |
            AssertAreEqual((2, 6, 7), p);
        }

        [TestMethod]
        public void TranslateIntoLocal()
        {
            var sunPose = new Posture(
                new Position(1, 2, 3),
                new Matrix33(
                    (0, 1, 0),
                    (0, 0, 1),
                    (1, 0, 0)));
            var sunSystem = CoordinateSystem.World
                .NewChild(sunPose);

            var earthPose = new Posture(
                new Position(-4, -5, -6),
                new Matrix33(
                    (0, 0, 1),
                    (1, 0, 0),
                    (0, 1, 0)));
            var earthSystem = sunSystem.NewChild(earthPose);

            var p = earthSystem.TranslateIntoLocal(
                new Position(2, 6, 7));

            // Translate (2, 6, 7) to the Sun coordinate system:
            // | 0 1 0 || 2 - 1 |   | 4 |
            // | 0 0 1 || 6 - 2 | = | 4 |
            // | 1 0 0 || 7 - 3 |   | 1 |
            //
            // And then, translate it to the Earth coordinate system:
            // | 0 0 1 || 4 - (-4) |   | 7 |
            // | 1 0 0 || 4 - (-5) | = | 8 |
            // | 0 1 0 || 1 - (-6) |   | 9 |
            AssertAreEqual((7, 8, 9), p);
        }

        private static void AssertAreEqual(
            (float X, float Y, float Z) p,
            Position q)
        {
            Assert.AreEqual(p.X, q.X, float.Epsilon);
            Assert.AreEqual(p.Y, q.Y, float.Epsilon);
            Assert.AreEqual(p.Z, q.Z, float.Epsilon);
        }
    }
}
