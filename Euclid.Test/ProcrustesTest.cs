namespace Maroontress.Euclid.Test
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Maroontress.Euclid;
    using Microsoft.VisualStudio.TestTools.UnitTesting;

    [TestClass]
    public sealed class ProcrustesTest
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
        public void NoNoise0()
        {
            NoNoise(new[]
            {
                new Position(1, -1, 1),
                new Position(1, 1, 1),
                new Position(-1, 1, 1),
                new Position(-1, -1, 1),
                new Position(1, -1, -1),
                new Position(1, 1, -1),
                new Position(-1, 1, -1),
                new Position(-1, -1, -1),
            });
        }

        [TestMethod]
        public void NoNoise1()
        {
            var sqr2 = MathF.Sqrt(2);

            NoNoise(new[]
            {
                new Position(1, -1, 1),
                new Position(1, 1, 1),
                new Position(-1, 1, 1),
                new Position(-1, -1, 1),
                new Position(0, -sqr2, -1),
                new Position(0, sqr2, -1),
                new Position(-sqr2, 0, -1),
                new Position(sqr2, 0, -1),
            });
        }

        [TestMethod]
        public void NoNoise2()
        {
            NoNoise(new[]
            {
                new Position(1, -1, 1),
                new Position(1, 1, 1),
                new Position(-1, 1, 1),
                new Position(-1, -1, 1),
                new Position(1, 0, 0),
                new Position(-0.5f, -MathF.Sqrt(3) * 0.5f, 0),
                new Position(-0.5f, MathF.Sqrt(3) * 0.5f, 0),
            });
        }

        [TestMethod]
        public void NoNoise3()
        {
            NoNoise(new[]
            {
                new Position(1, -1, 1),
                new Position(1, 1, 1),
                new Position(-1, 1, 1),
                new Position(-1, -1, 1),
                new Position(-0.5f, -MathF.Sqrt(3) * 0.5f, 0),
                new Position(-0.5f, MathF.Sqrt(3) * 0.5f, 0),
            });
        }

        [TestMethod]
        public void WithNoise()
        {
            var alpha = 60 * MathF.PI / 180;
            var rotation = Rotations.EulerRotation(alpha, alpha, alpha);

            var noises = new[]
            {
                /*
                    var random = new Random();
                    for (var j = 0; j < a.Length; ++j)
                    {
                        var x = (float)random.NextDouble();
                        var y = (float)random.NextDouble();
                        var z = (float)random.NextDouble();
                        Console.WriteLine($"({x}f, {y}f, {z}f),");
                    }
                */
                (0.26472986f, 0.6756402f, 0.36577225f),
                (0.16883223f, 0.96580094f, 0.1628179f),
                (0.7984452f, 0.032898724f, 0.18579741f),
                (0.8777863f, 0.69848627f, 0.60312724f),
                (0.104392275f, 0.717291f, 0.71676695f),
                (0.95056075f, 0.8252618f, 0.7798941f),
                (0.54226536f, 0.23723066f, 0.55440533f),
            };

            var model = new[]
            {
                new Position(1, -1, 1),
                new Position(1, 1, 1),
                new Position(-1, 1, 1),
                new Position(-1, -1, 1),
                new Position(1, 0, 0),
                new Position(-0.5f, -MathF.Sqrt(3) * 0.5f, 0),
                new Position(-0.5f, MathF.Sqrt(3) * 0.5f, 0),
            };

            var (_, firstRigidbody) = Centering(model);
            var firstObserved = ObservedRigidbody(
                firstRigidbody,
                rotation,
                noises.Select(p => p.ToPosition())
                    .ToArray());
            var firstR = EstimateRotation(firstRigidbody, firstObserved);
            var firstC = firstRigidbody.Select(p => firstR.Map(p))
                .ToArray();
            var firstResult = GetDelta(firstC, firstObserved);
            var firstSorted = firstResult.Sorted;

            var exceptWorst = firstSorted.Take(firstC.Length - 1)
                .Select(p => (A: firstRigidbody[p.Index],
                    B: firstObserved[p.Index]))
                .ToArray();

            var (secondOffset, secondRigidbody)
                = Centering(exceptWorst.Select(p => p.A));
            var (secondCenter, secondObserved)
                = Centering(exceptWorst.Select(p => p.B));
            var secondR = EstimateRotation(secondRigidbody, secondObserved);
            var secondC = secondRigidbody.Select(p => secondR.Map(p))
                .ToArray();
            var secondResult = GetDelta(secondC, secondObserved);
            Assert.IsTrue(firstResult.Average > secondResult.Average);

            var shift = secondR.Map(secondOffset.Opposite());
            var g = secondCenter.Add(shift);
            Assert.IsTrue(g.Length() > 0);
        }

        private static (Position, Position[])
            Centering(IEnumerable<Position> model)
        {
            var array = model.ToArray();
            var center = array.Aggregate((p, q) => p.Add(q))
                .Div(array.Length);
            var newArray = array.Select(p => p.Sub(center))
                .ToArray();
            return (center, newArray);
        }

        private static (float Average, (float Distance, int Index)[] Sorted)
            GetDelta(Position[] a, Position[] b)
        {
            var sum = 0f;
            var n = a.Length;
            var list = new List<(float Distance, int Index)>(n);
            for (var k = 0; k < n; ++k)
            {
                var d = a[k].Sub(b[k]).Length();
                sum += d;
                list.Add((d, k));
            }
            var average = sum / n;
            var sorted = list.OrderBy(p => p.Distance)
                .ToArray();
            return (average, sorted);
        }

        private static Matrix33 EstimateRotation(
            IEnumerable<(Position A, Position B)> mappings)
        {
            /*
                See: Wikipedia, "Orthogonal Procrustes problem",
                https://en.wikipedia.org/wiki/Orthogonal_Procrustes_problem
            */

            var allMappings = mappings.ToArray();

            float Product(Func<Position, float> s1, Func<Position, float> s2)
            {
                return allMappings.Select(p => s1(p.A) * s2(p.B))
                    .Sum();
            }

            var p11 = Product(p => p.X, p => p.X);
            var p12 = Product(p => p.X, p => p.Y);
            var p13 = Product(p => p.X, p => p.Z);
            var p21 = Product(p => p.Y, p => p.X);
            var p22 = Product(p => p.Y, p => p.Y);
            var p23 = Product(p => p.Y, p => p.Z);
            var p31 = Product(p => p.Z, p => p.X);
            var p32 = Product(p => p.Z, p => p.Y);
            var p33 = Product(p => p.Z, p => p.Z);

            // (p, q, pq) <= (M, Transpose(M), M Transpose(M))
            var p = new Matrix33(
                (p11, p21, p31),
                (p12, p22, p32),
                (p13, p23, p33));
            var q = p.Transpose();
            var pq = p.Mul(q);

            /*
                M = V D Transpose(U)
                => M Transpose(M) = V D Transpose(U) U D Transpose(V)
                => M Transpose(M) = V DD Transpose(V)
                => M Transpose(M) V = V DD
            */

            // (dd, v) <= (DD, V)
            var (dd, v) = pq.EigenvaluesAndVectors(0.000_001f);
            CheckOrthogonal(v);

            /*
                Likewise,

                Transpose(M) M = U DD Transpose(U)
                => Transpose(R) R U = U DD

                So, you can get U as follows:

                    var qp = q.Mul(p);
                    var (_, u) = qp.EigenvaluesAndVectors(0.000_001f);

                However, the orders of eigenvectors of V and U do not
                correspond when the eigenvalue has double root. So we get U
                with M, V, D as follows:

                    Transpose(M) = U D Transpose(V)
                    => U = Transpose(M) V Inverse(D)
            */
            var (squareLambda1, _, _) = dd.Column1Tuple();
            var (_, squareLambda2, _) = dd.Column2Tuple();
            var (_, _, squareLambda3) = dd.Column3Tuple();
            var lambda1 = MathF.Sqrt(squareLambda1);
            var lambda2 = MathF.Sqrt(squareLambda2);
            var lambda3 = MathF.Sqrt(squareLambda3);
            var inverseD = new Matrix33(
                (1 / lambda1, 0, 0),
                (0, 1 / lambda2, 0),
                (0, 0, 1 / lambda3));
            var u = q.Mul(v).Mul(inverseD);
            CheckOrthogonal(u);

            /*
                R = U Transpose(V)
            */
            var result = u.Mul(v.Transpose());
            return result;
        }

        private static void CheckOrthogonal(Matrix33 m)
        {
            var delta = 0.000_001f;
            var det = m.Determinant();
            var v1 = m.Column1Tuple();
            var v2 = m.Column2Tuple();
            var v3 = m.Column3Tuple();
            Assert.AreEqual(1, det, delta);
            Assert.AreEqual(0, v1.DotProduct(v2), delta);
            Assert.AreEqual(0, v2.DotProduct(v3), delta);
            Assert.AreEqual(0, v3.DotProduct(v1), delta);
            Assert.AreEqual(1, v1.Length(), delta);
            Assert.AreEqual(1, v2.Length(), delta);
            Assert.AreEqual(1, v3.Length(), delta);
        }

        private static void NoNoise(Position[] model)
        {
            static IEnumerable<(int X, int Y, int Z)> NewRange3D(int n)
            {
                var range = Enumerable.Range(0, n);

                IEnumerable<(int X, int Y, int Z)> RangeX(int y, int z)
                    => range.Select(x => (x, y, z));

                IEnumerable<(int X, int Y, int Z)> RangeY(int z)
                    => range.SelectMany(y => RangeX(y, z));

                return range.SelectMany(z => RangeY(z));
            }

            static float ToRadian(int k, int n)
            {
                return k * 2 * MathF.PI / n;
            }

            var delta = 0.000_001f;
            var n = 10;
            foreach (var (x, y, z) in NewRange3D(n))
            {
                var alpha = ToRadian(x, n);
                var beta = ToRadian(y, n);
                var gamma = ToRadian(z, n);
                var rotation = Rotations.EulerRotation(gamma, beta, alpha);

                var (_, m) = Centering(model);
                var r = EstimateRotationWithoutNoise(m, rotation);
                Asserts.AreEqual(rotation.Column1(), r.Column1(), delta);
                Asserts.AreEqual(rotation.Column2(), r.Column2(), delta);
                Asserts.AreEqual(rotation.Column3(), r.Column3(), delta);
            }
        }

        private static Matrix33 EstimateRotationWithoutNoise(
            Position[] rigidbody, Matrix33 rotation)
        {
            var mappings = rigidbody.Select(p => (p, rotation.Map(p)));
            return EstimateRotation(mappings);
        }

        private static Matrix33 EstimateRotation(
            Position[] rigidbody, Position[] observed)
        {
            var n = rigidbody.Length;
            var mappingList = new List<(Position, Position)>(n);
            for (var k = 0; k < n; ++k)
            {
                mappingList.Add((rigidbody[k], observed[k]));
            }
            return EstimateRotation(mappingList);
        }

        private static Position[] ObservedRigidbody(
            Position[] rigidbody, Matrix33 rotation, Position[] noises)
        {
            var trueB = rigidbody.Select(p => rotation.Map(p))
                .ToArray();
            var observedB = new Position[trueB.Length];
            for (var k = 0; k < observedB.Length; ++k)
            {
                var noise = noises[k].Sub((0.5f, 0.5f, 0.5f))
                    .Mul(0.01f);
                observedB[k] = trueB[k].Add(noise);
            }
            var centerOfObservedB = observedB.Aggregate((p, q) => p.Add(q))
                .Div(observedB.Length);
            return observedB.Select(p => p.Sub(centerOfObservedB))
                .ToArray();
        }
    }
}
