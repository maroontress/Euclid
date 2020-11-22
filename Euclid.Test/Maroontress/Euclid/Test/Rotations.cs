namespace Maroontress.Euclid.Test
{
    using System;

    public static class Rotations
    {
        public static Matrix33 WithQuaternion(
            (float W, float X, float Y, float Z) q)
        {
            /*
                https://wikimedia.org/api/rest_v1/media/math/render/svg/7407b88fc6d5545fdf36714c4bc497303cc659aa
            */
            var ww = q.W * q.W;
            var xx = q.X * q.X;
            var yy = q.Y * q.Y;
            var zz = q.Z * q.Z;
            var xy = q.X * q.Y;
            var wz = q.W * q.Z;
            var wy = q.W * q.Y;
            var xz = q.X * q.Z;
            var wx = q.W * q.X;
            var yz = q.Y * q.Z;

            var xAxis = (
                ww + xx - yy - zz,
                2 * (xy + wz),
                2 * (xz - wy));
            var yAxis = (
                2 * (xy - wz),
                ww - xx + yy - zz,
                2 * (wx + yz));
            var zAxis = (
                2 * (wy + xz),
                2 * (yz - wx),
                ww - xx - yy + zz);
            return new Matrix33(xAxis, yAxis, zAxis);
        }

        public static (float W, float X, float Y, float Z)
            ToQuaternion(float phi, float theta, float psi)
        {
            /*
                https://wikimedia.org/api/rest_v1/media/math/render/svg/d081a6bf413a578916923e963434fba9d8a162a7
            */
            var a = phi / 2;
            var b = theta / 2;
            var c = psi / 2;
            var cosA = MathF.Cos(a);
            var sinA = MathF.Sin(a);
            var cosB = MathF.Cos(b);
            var sinB = MathF.Sin(b);
            var cosC = MathF.Cos(c);
            var sinC = MathF.Sin(c);
            var w = (cosA * cosB * cosC) + (sinA * sinB * sinC);
            var x = (sinA * cosB * cosC) - (cosA * sinB * sinC);
            var y = (cosA * sinB * cosC) + (sinA * cosB * sinC);
            var z = (cosA * cosB * sinC) - (sinA * sinB * cosC);
            return (w, x, y, z);
        }

        public static Matrix33 EulerRotation(
            float phi, float theta, float psi)
        {
            /*
                https://wikimedia.org/api/rest_v1/media/math/render/svg/e03f373d475143a18284d28f70a6a65055463339
            */
            var m = AroundZAxis(psi)
                .Mul(AroundYAxis(theta))
                .Mul(AroundXAxis(phi));
            return m;
        }

        public static Matrix33 AroundXAxis(float theta)
        {
            var cos = MathF.Cos(theta);
            var sin = MathF.Sin(theta);
            var m = new Matrix33(
                //     X    Y    Z
                // |   1    0    0  |
                // |   0   cos -sin |
                // |   0   sin  cos |
                (1, 0, 0),
                (0, cos, sin),
                (0, -sin, cos));
            return m;
        }

        public static Matrix33 AroundYAxis(float theta)
        {
            var cos = MathF.Cos(theta);
            var sin = MathF.Sin(theta);
            var m = new Matrix33(
                //     X    Y    Z
                // |  cos   0   sin |
                // |   0    1    0  |
                // | -sin   0   cos |
                (cos, 0, -sin),
                (0, 1, 0),
                (sin, 0, cos));
            return m;
        }

        public static Matrix33 AroundZAxis(float theta)
        {
            var cos = MathF.Cos(theta);
            var sin = MathF.Sin(theta);
            var m = new Matrix33(
                //     X    Y    Z
                // |  cos -sin   0  |
                // |  sin  cos   0  |
                // |   0    0    1  |
                (cos, sin, 0),
                (-sin, cos, 0),
                (0, 0, 1));
            return m;
        }
    }
}
