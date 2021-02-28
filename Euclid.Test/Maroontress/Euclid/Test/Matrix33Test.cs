namespace Maroontress.Euclid.Test
{
    using System;
    using System.Linq;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class Matrix33Test
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
        public void New()
        {
            var x = new Position(1, 2, 3);
            var y = new Position(4, 5, 6);
            var z = new Position(7, 8, 9);
            var m = new Matrix33(x, y, z);
            var xAxis = m.Map(Position.XUnit);
            var yAxis = m.Map(Position.YUnit);
            var zAxis = m.Map(Position.ZUnit);
            Asserts.AreEqual((1, 2, 3), xAxis);
            Asserts.AreEqual((4, 5, 6), yAxis);
            Asserts.AreEqual((7, 8, 9), zAxis);
        }

        [TestMethod]
        public void NewTuple()
        {
            var m = new Matrix33((1, 2, 3), (4, 5, 6), (7, 8, 9));
            var xAxis = m.Map(Position.XUnit);
            var yAxis = m.Map(Position.YUnit);
            var zAxis = m.Map(Position.ZUnit);
            Asserts.AreEqual((1, 2, 3), xAxis);
            Asserts.AreEqual((4, 5, 6), yAxis);
            Asserts.AreEqual((7, 8, 9), zAxis);
        }

        [TestMethod]
        public void Identity()
        {
            var m = Matrix33.Identity;
            var xAxis = m.Map(Position.XUnit);
            var yAxis = m.Map(Position.YUnit);
            var zAxis = m.Map(Position.ZUnit);
            Asserts.AreEqual(Position.XUnit, xAxis);
            Asserts.AreEqual(Position.YUnit, yAxis);
            Asserts.AreEqual(Position.ZUnit, zAxis);
        }

        [TestMethod]
        public void Transpose()
        {
            var x = new Position(1, 2, 3);
            var y = new Position(4, 5, 6);
            var z = new Position(7, 8, 9);
            var m = new Matrix33(x, y, z);
            var t = m.Transpose();
            var xAxis = t.Map(Position.XUnit);
            var yAxis = t.Map(Position.YUnit);
            var zAxis = t.Map(Position.ZUnit);
            Asserts.AreEqual((1, 4, 7), xAxis);
            Asserts.AreEqual((2, 5, 8), yAxis);
            Asserts.AreEqual((3, 6, 9), zAxis);
        }

        [TestMethod]
        public void Map()
        {
            var x = new Position(1, 2, 3);
            var y = new Position(4, 5, 6);
            var z = new Position(7, 8, 9);
            var m = new Matrix33(x, y, z);
            var p = new Position(10, 11, 12);
            var q = m.Map(p);
            // | 1 4 7 | 10 |
            // | 2 5 8 | 11 |
            // | 3 6 9 | 12 |
            Asserts.AreEqual(
                (10 + 44 + 84, 20 + 55 + 96, 30 + 66 + 108),
                q);
        }

        [TestMethod]
        public void MapTuple()
        {
            var x = new Position(1, 2, 3);
            var y = new Position(4, 5, 6);
            var z = new Position(7, 8, 9);
            var m = new Matrix33(x, y, z);
            var q = m.Map((10, 11, 12));
            // | 1 4 7 | 10 |
            // | 2 5 8 | 11 |
            // | 3 6 9 | 12 |
            Asserts.AreEqual(
                (10 + 44 + 84, 20 + 55 + 96, 30 + 66 + 108),
                q);
        }

        [TestMethod]
        public void Mul()
        {
            var m = new Matrix33((1, 2, 3), (4, 5, 6), (7, 8, 9));
            var n = new Matrix33(
                (10, 11, 12), (13, 14, 15), (16, 17, 18));
            var mn = m.Mul(n);
            var xAxis = mn.Map(Position.XUnit);
            var yAxis = mn.Map(Position.YUnit);
            var zAxis = mn.Map(Position.ZUnit);
            // | 1 4 7 | 10 13 16 |
            // | 2 5 8 | 11 14 17 |
            // | 3 6 9 | 12 15 18 |
            Asserts.AreEqual(
                (10 + 44 + 84, 20 + 55 + 96, 30 + 66 + 108),
                xAxis);
            Asserts.AreEqual(
                (13 + 56 + 105, 26 + 70 + 120, 39 + 84 + 135),
                yAxis);
            Asserts.AreEqual(
                (16 + 68 + 126, 32 + 85 + 144, 48 + 102 + 162),
                zAxis);
        }

        [TestMethod]
        public void ToXyzwTuple1()
        {
            var alpha = 60 * MathF.PI / 180;
            var m = Rotations.EulerRotation(alpha, alpha, alpha);
            var (w, x, y, z) = m.ToWxyzTuple();
            var q = Rotations.ToQuaternion(alpha, alpha, alpha);
            Assert.AreEqual(q.W, w, float.Epsilon);
            Assert.AreEqual(q.X, x, float.Epsilon);
            Assert.AreEqual(q.Y, y, float.Epsilon);
            Assert.AreEqual(q.Z, z, float.Epsilon);
        }

        [TestMethod]
        public void ToXyzwTuple2()
        {
            var alpha = 150 * MathF.PI / 180;
            var beta = -60 * MathF.PI / 180;
            var gamma = -30 * MathF.PI / 180;
            var m = Rotations.EulerRotation(alpha, beta, gamma);
            var q = m.ToWxyzTuple();
            var n = Rotations.WithQuaternion(q);
            Asserts.AreEqual(m, n, 0.000_000_1f);
        }

        [TestMethod]
        public void ToXyzwTuple3()
        {
            var alpha = 150 * MathF.PI / 180;
            var beta = 60 * MathF.PI / 180;
            var gamma = 180 * MathF.PI / 180;
            var m = Rotations.EulerRotation(alpha, beta, gamma);
            var q = m.ToWxyzTuple();
            var n = Rotations.WithQuaternion(q);
            Asserts.AreEqual(m, n, 0.000_000_1f);
        }

        [TestMethod]
        public void ToXyzwTuple4()
        {
            // The rotation around the axis (1, 1, 1), with a rotation angle
            // of 120 degree.
            var m = new Matrix33((0, 1, 0), (0, 0, 1), (1, 0, 0));
            var (x, y, z, w) = m.ToWxyzTuple();
            Assert.AreEqual(0.5f, x, float.Epsilon);
            Assert.AreEqual(0.5f, y, float.Epsilon);
            Assert.AreEqual(0.5f, z, float.Epsilon);
            Assert.AreEqual(0.5f, w, float.Epsilon);
        }

        [TestMethod]
        public void Determinant()
        {
            var m = new Matrix33((-2, -1, 2), (2, 1, 0), (-3, 3, -1));
            var d = m.Determinant();
            Assert.AreEqual(18, d);
        }

        [TestMethod]
        public void Column123()
        {
            var m = new Matrix33((1, 2, 3), (4, 5, 6), (7, 8, 9));
            var xAxis = m.Column1();
            var yAxis = m.Column2();
            var zAxis = m.Column3();
            Asserts.AreEqual((1, 2, 3), xAxis);
            Asserts.AreEqual((4, 5, 6), yAxis);
            Asserts.AreEqual((7, 8, 9), zAxis);
        }

        [TestMethod]
        public void Column123Tuple()
        {
            var m = new Matrix33((1, 2, 3), (4, 5, 6), (7, 8, 9));
            var xAxis = m.Column1Tuple();
            var yAxis = m.Column2Tuple();
            var zAxis = m.Column3Tuple();
            Asserts.AreEqual((1, 2, 3), xAxis);
            Asserts.AreEqual((4, 5, 6), yAxis);
            Asserts.AreEqual((7, 8, 9), zAxis);
        }

        [TestMethod]
        public void EigenvaluesAndVectors1()
        {
            var delta = 0.000_001f;
            // |  5  1 -2 |
            // |  1  6 -1 |
            // | -2 -1  5 |
            var m = new Matrix33(
                (5, 1, -2),
                (1, 6, -1),
                (-2, -1, 5));
            var lambda1 = 3f;
            var lambda2 = 5f;
            var lambda3 = 8f;
            var u1 = (1f, 0f, 1f).Normalize();
            var u2 = (-1f, 2f, 1f).Normalize();
            var u3 = (-1f, -1f, 1f).Normalize();
            var v1 = m.Map(u1);
            var v2 = m.Map(u2);
            var v3 = m.Map(u3);
            Asserts.AreEqual(u1.Mul(lambda1), v1, delta);
            Asserts.AreEqual(u2.Mul(lambda2), v2, delta);
            Asserts.AreEqual(u3.Mul(lambda3), v3, delta);

            var (d, v) = m.EigenvaluesAndVectors(0.001f);
            Asserts.AreEqual((lambda1, 0f, 0f), d.Column1(), delta);
            Asserts.AreEqual((0f, lambda2, 0f), d.Column2(), delta);
            Asserts.AreEqual((0f, 0f, lambda3), d.Column3(), delta);
            Asserts.AreEqual(u1, v.Column1(), delta);
            Asserts.AreEqual(u2, v.Column2(), delta);
            Asserts.AreEqual(u3, v.Column3(), delta);
        }

        [TestMethod]
        public void EigenvaluesAndVectors2()
        {
            var delta = 0.000_001f;
            // |  0  1  1 |
            // |  1  0 -1 |
            // |  1 -1  0 |
            var m = new Matrix33(
                (0, 1, 1),
                (1, 0, -1),
                (1, -1, 0));
            var lambda1 = -2f;
            var lambda2 = 1f;
            var lambda3 = 1f;
            var u1 = (1f, -1f, -1f).Normalize();
            var u2 = (1f, 1f, 0f).Normalize();
            var u3 = (1f, -1f, 2f).Normalize();
            var v1 = m.Map(u1);
            var v2 = m.Map(u2);
            var v3 = m.Map(u3);
            Asserts.AreEqual(u1.Mul(lambda1), v1, delta);
            Asserts.AreEqual(u2.Mul(lambda2), v2, delta);
            Asserts.AreEqual(u3.Mul(lambda3), v3, delta);

            var (d, v) = m.EigenvaluesAndVectors(0.001f);
            Asserts.AreEqual((lambda1, 0f, 0f), d.Column1(), delta);
            Asserts.AreEqual((0f, lambda2, 0f), d.Column2(), delta);
            Asserts.AreEqual((0f, 0f, lambda3), d.Column3(), delta);
            Asserts.AreEqual(u1, v.Column1(), delta);
            var all = new[]
            {
                v.Column2(),
                v.Column3(),
            };
            var sorted = all.OrderBy(p => p.Z)
                .ToArray();
            Asserts.AreEqual(u2, sorted[0], delta);
            Asserts.AreEqual(u3, sorted[1], delta);
        }
    }
}
