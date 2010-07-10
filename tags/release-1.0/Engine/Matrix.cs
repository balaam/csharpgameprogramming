using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Engine
{
    public class Matrix
    {
        double _m11, _m12, _m13;
        double _m21, _m22, _m23;
        double _m31, _m32, _m33;
        double _m41, _m42, _m43;
        public static readonly Matrix Identity =
            new Matrix(new Vector(1, 0, 0),
                       new Vector(0, 1, 0),
                       new Vector(0, 0, 1),
                       new Vector(0, 0, 1));

        public Matrix()
            : this(Identity)
        {

        }

        public Matrix(Matrix m)
        {
            _m11 = m._m11;
            _m12 = m._m12;
            _m13 = m._m13;

            _m21 = m._m21;
            _m22 = m._m22;
            _m23 = m._m23;

            _m31 = m._m31;
            _m32 = m._m32;
            _m33 = m._m33;

            _m41 = m._m41;
            _m42 = m._m42;
            _m43 = m._m43;
        }

        public Matrix(Vector x, Vector y, Vector z, Vector o)
        {
            _m11 = x.X; _m12 = x.Y; _m13 = x.Z;
            _m21 = y.X; _m22 = y.Y; _m23 = y.Z;
            _m31 = z.X; _m32 = z.Y; _m33 = z.Z;
            _m41 = o.X; _m42 = o.Y; _m43 = o.Z;
        }

        public static Matrix operator *(Matrix mA, Matrix mB)
        {
            Matrix result = new Matrix();

            result._m11 = mA._m11 * mB._m11 + mA._m12 * mB._m21 + mA._m13 * mB._m31;
            result._m12 = mA._m11 * mB._m12 + mA._m12 * mB._m22 + mA._m13 * mB._m32;
            result._m13 = mA._m11 * mB._m13 + mA._m12 * mB._m23 + mA._m13 * mB._m33;

            result._m21 = mA._m21 * mB._m11 + mA._m22 * mB._m21 + mA._m23 * mB._m31;
            result._m22 = mA._m21 * mB._m12 + mA._m22 * mB._m22 + mA._m23 * mB._m32;
            result._m23 = mA._m21 * mB._m13 + mA._m22 * mB._m23 + mA._m23 * mB._m33;

            result._m31 = mA._m31 * mB._m11 + mA._m32 * mB._m21 + mA._m33 * mB._m31;
            result._m32 = mA._m31 * mB._m12 + mA._m32 * mB._m22 + mA._m33 * mB._m32;
            result._m33 = mA._m31 * mB._m13 + mA._m32 * mB._m23 + mA._m33 * mB._m33;

            result._m41 = mA._m41 * mB._m11 + mA._m42 * mB._m21 + mA._m43 * mA._m31 + mB._m41;
            result._m42 = mA._m41 * mB._m12 + mA._m42 * mB._m22 + mA._m43 * mB._m32 + mB._m42;
            result._m43 = mA._m41 * mB._m13 + mA._m42 * mB._m23 + mA._m43 * mB._m33 + mB._m43;

            return result;
        }

        public void SetTranslation(Vector translation)
        {
            _m41 = translation.X;
            _m42 = translation.Y;
            _m43 = translation.Z;
        }

        public Vector GetTranslation()
        {
            return new Vector(_m41, _m42, _m43);
        }

        public void SetScale(Vector scale)
        {
            _m11 = scale.X;
            _m22 = scale.Y;
            _m33 = scale.Z;
        }

        public Vector GetScale()
        {
            Vector result = new Vector();
            result.X = (new Vector(_m11, _m12, _m13)).Length();
            result.Y = (new Vector(_m21, _m22, _m23)).Length();
            result.Z = (new Vector(_m31, _m32, _m33)).Length();
            return result;
        }

        public void SetRotate(Vector axis, double angle)
        {
            double angleSin = Math.Sin(angle);
            double angleCos = Math.Cos(angle);

            double a = 1.0 - angleCos;
            double ax = a * axis.X;
            double ay = a * axis.Y;
            double az = a * axis.Z;

            _m11 = ax * axis.X + angleCos;
            _m12 = ax * axis.Y + axis.Z * angleSin;
            _m13 = ax * axis.Z - axis.Y * angleSin;

            _m21 = ay * axis.X - axis.Z * angleSin;
            _m22 = ay * axis.Y + angleCos;
            _m23 = ay * axis.Z + axis.X * angleSin;

            _m31 = az * axis.X + axis.Y * angleSin;
            _m32 = az * axis.Y - axis.X * angleSin;
            _m33 = az * axis.Z + angleCos;
        }

        public double Determinate()
        {
            return _m11 * (_m22 * _m33 - _m23 * _m32) +
                    _m12 * (_m23 * _m31 - _m21 * _m33) +
                    _m13 * (_m21 * _m32 - _m22 * _m31);
        }

        public Matrix Inverse()
        {
            double determinate = Determinate();
            System.Diagnostics.Debug.Assert(Math.Abs(determinate) > Double.Epsilon,
                "No determinate");

            double oneOverDet = 1.0 / determinate;

            Matrix result = new Matrix();
            result._m11 = (_m22 * _m33 - _m23 * _m32) * oneOverDet;
            result._m12 = (_m13 * _m32 - _m12 * _m33) * oneOverDet;
            result._m13 = (_m12 * _m23 - _m13 * _m22) * oneOverDet;

            result._m21 = (_m23 * _m31 - _m21 * _m33) * oneOverDet;
            result._m22 = (_m11 * _m33 - _m13 * _m31) * oneOverDet;
            result._m23 = (_m13 * _m21 - _m11 * _m23) * oneOverDet;

            result._m31 = (_m21 * _m32 - _m22 * _m31) * oneOverDet;
            result._m32 = (_m12 * _m31 - _m11 * _m32) * oneOverDet;
            result._m33 = (_m11 * _m22 - _m12 * _m21) * oneOverDet;

            result._m41 = -(_m41 * result._m11 + _m42 * result._m21 + _m43 * result._m31);
            result._m42 = -(_m41 * result._m12 + _m42 * result._m22 + _m43 * result._m32);
            result._m43 = -(_m41 * result._m13 + _m42 * result._m23 + _m43 * result._m33);

            return result;
        }

        public static Vector operator *(Vector v, Matrix m)
        {

            return new Vector(v.X * m._m11 + v.Y * m._m21 + v.Z * m._m31 + m._m41,
                              v.X * m._m12 + v.Y * m._m22 + v.Z * m._m32 + m._m42,
                              v.X * m._m13 + v.Y * m._m23 + v.Z * m._m33 + m._m43);
        }

    }
}
