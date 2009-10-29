﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Lone_Mars {
    public struct FInt {
        public long RawValue;
        public const int SHIFT_AMOUNT = 12; //12 is 4096

        public const long One = 1 << SHIFT_AMOUNT;
        public const int OneI = 1 << SHIFT_AMOUNT;

        public static FInt OneF = new FInt(1, true);        
        public static FInt ZeroF = new FInt(0, true);
        public static FInt NOneF = ZeroF - OneF;

        #region Constructors
        public FInt(long StartingRawValue, bool UseMultiple) {
            this.RawValue = StartingRawValue;
            if (UseMultiple)
                this.RawValue = this.RawValue << SHIFT_AMOUNT;
        }
        public FInt(double DoubleValue) {
            DoubleValue *= (double)One;
            this.RawValue = (int)Math.Round(DoubleValue);
        }
        #endregion

        public int IntValue {
            get { return (int)(this.RawValue >> SHIFT_AMOUNT); }
        }

        public int ToInt() {
            return (int)(this.RawValue >> SHIFT_AMOUNT);
        }

        public double ToDouble() {
            return (double)this.RawValue / (double)One;
        }

        public FInt Inverse {
            get { return new FInt(-this.RawValue, false); }
        }

        #region FromParts
        /// <summary>
        /// Create a fixed-int number from parts.  For example, to create 1.5 pass in 1 and 500.
        /// </summary>
        /// <param name="PreDecimal">The number above the decimal.  For 1.5, this would be 1.</param>
        /// <param name="PostDecimal">The number below the decimal, to three digits.  
        /// For 1.5, this would be 500. For 1.005, this would be 5.</param>
        /// <returns>A fixed-int representation of the number parts</returns>
        public static FInt FromParts(int PreDecimal, int PostDecimal) {
            FInt f = new FInt(PreDecimal);
            if (PostDecimal != 0)
                f.RawValue += (new FInt(PostDecimal) / 1000).RawValue;

            return f;
        }
        #endregion

        #region *
        public static FInt operator *(FInt one, FInt other) {
            return new FInt((one.RawValue * other.RawValue) >> SHIFT_AMOUNT, false);
        }

        public static FInt operator *(FInt one, int multi) {
            return one * (FInt)multi;
        }

        public static FInt operator *(int multi, FInt one) {
            return one * (FInt)multi;
        }

        public static void Multiply(ref FInt one, ref FInt other, out FInt dest) {
            dest.RawValue = (one.RawValue * other.RawValue) >> SHIFT_AMOUNT;
        }
        #endregion

        #region /
        public static FInt operator /(FInt one, FInt other) {
            return new FInt((one.RawValue << SHIFT_AMOUNT) / (other.RawValue), false);
        }

        public static void Divide(ref FInt one, ref FInt other, out FInt dest) {
            dest.RawValue = (one.RawValue << SHIFT_AMOUNT) / (other.RawValue);
        }

        public static FInt operator /(FInt one, int divisor) {
            return one / (FInt)divisor;
        }

        public static FInt operator /(int divisor, FInt one) {
            return (FInt)divisor / one;
        }
        #endregion

        #region %
        public static FInt operator %(FInt one, FInt other) {
            return new FInt((one.RawValue) % (other.RawValue), false);
        }

        public static FInt operator %(FInt one, int divisor) {
            return one % (FInt)divisor;
        }

        public static FInt operator %(int divisor, FInt one) {
            return (FInt)divisor % one;
        }
        #endregion

        #region +
        public static FInt operator +(FInt one, FInt other) {
            return new FInt(one.RawValue + other.RawValue, false);
        }

        public static FInt operator +(FInt one, int other) {
            return one + (FInt)other;
        }

        public static FInt operator +(int other, FInt one) {
            return one + (FInt)other;
        }

        public static void Add(ref FInt one, ref FInt other, out FInt dest) {
            dest.RawValue = (one.RawValue + other.RawValue);
        }
        #endregion

        #region -
        public static FInt operator -(FInt one, FInt other) {
            return new FInt(one.RawValue - other.RawValue, false);
        }

        public static void Sub(ref FInt one, ref FInt other, out FInt dest) {
            dest.RawValue = (one.RawValue - other.RawValue);
        }

        public static FInt operator -(FInt one, int other) {
            return one - (FInt)other;
        }

        public static FInt operator -(int other, FInt one) {
            return (FInt)other - one;
        }
        #endregion

        #region ==
        public static bool operator ==(FInt one, FInt other) {
            return one.RawValue == other.RawValue;
        }

        public static bool operator ==(FInt one, int other) {
            return one == (FInt)other;
        }

        public static bool operator ==(int other, FInt one) {
            return (FInt)other == one;
        }
        #endregion

        #region !=
        public static bool operator !=(FInt one, FInt other) {
            return one.RawValue != other.RawValue;
        }

        public static bool operator !=(FInt one, int other) {
            return one != (FInt)other;
        }

        public static bool operator !=(int other, FInt one) {
            return (FInt)other != one;
        }
        #endregion

        #region >=
        public static bool operator >=(FInt one, FInt other) {
            return one.RawValue >= other.RawValue;
        }

        public static bool operator >=(FInt one, int other) {
            return one >= (FInt)other;
        }

        public static bool operator >=(int other, FInt one) {
            return (FInt)other >= one;
        }
        #endregion

        #region <=
        public static bool operator <=(FInt one, FInt other) {
            return one.RawValue <= other.RawValue;
        }

        public static bool operator <=(FInt one, int other) {
            return one <= (FInt)other;
        }

        public static bool operator <=(int other, FInt one) {
            return (FInt)other <= one;
        }
        #endregion

        #region >
        public static bool operator >(FInt one, FInt other) {
            return one.RawValue > other.RawValue;
        }

        public static bool operator >(FInt one, int other) {
            return one > (FInt)other;
        }

        public static bool operator >(int other, FInt one) {
            return (FInt)other > one;
        }
        #endregion

        #region <
        public static bool operator <(FInt one, FInt other) {
            return one.RawValue < other.RawValue;
        }

        public static bool operator <(FInt one, int other) {
            return one < (FInt)other;
        }

        public static bool operator <(int other, FInt one) {
            return (FInt)other < one;
        }
        #endregion

        public static explicit operator int(FInt src) {
            return (int)(src.RawValue >> SHIFT_AMOUNT);
        }

        public static explicit operator FInt(int src) {
            return new FInt(src, true);
        }

        public static explicit operator FInt(long src) {
            return new FInt(src, true);
        }

        public static explicit operator FInt(ulong src) {
            return new FInt((long)src, true);
        }

        public static FInt operator <<(FInt one, int Amount) {
            return new FInt(one.RawValue << Amount, false);
        }

        public static FInt operator >>(FInt one, int Amount) {
            return new FInt(one.RawValue >> Amount, false);
        }

        public override bool Equals(object obj) {
            if (obj is FInt)
                return ((FInt)obj).RawValue == this.RawValue;
            else
                return false;
        }

        public override int GetHashCode() {
            return RawValue.GetHashCode();
        }

        public override string ToString() {
            return this.RawValue.ToString();
        }

        public static FInt Clamp(FInt val, FInt min, FInt max) {
            if (val < min) {
                return min;
            } else if (val > max) {
                return max;
            } else {
                return val;
            }
        }

        #region PI, DoublePI
        public static FInt PI = new FInt(12868, false); //PI x 2^12
        public static FInt TwoPIF = PI * 2; //radian equivalent of 260 degrees
        public static FInt PIOver180F = PI / (FInt)180; //PI / 180
        #endregion

        #region Sqrt
        public static FInt Sqrt(FInt f, int NumberOfIterations) {
            if (f.RawValue < 0) //NaN in Math.Sqrt
                throw new ArithmeticException("Input Error");
            if (f.RawValue == 0)
                return (FInt)0;
            FInt k = f + FInt.OneF >> 1;
            for (int i = 0; i < NumberOfIterations; i++)
                k = (k + (f / k)) >> 1;

            if (k.RawValue < 0)
                throw new ArithmeticException("Overflow");
            else
                return k;
        }

        public static FInt Sqrt(FInt f) {
            byte numberOfIterations = 8;
            if (f.RawValue > 0x64000)
                numberOfIterations = 12;
            if (f.RawValue > 0x3e8000)
                numberOfIterations = 16;
            return Sqrt(f, numberOfIterations);
        }
        #endregion

        #region Sin
        public static FInt Sin(FInt i) {
            FInt j = (FInt)0;
            for (; i < 0; i += new FInt(25736, false))
                ;
            if (i > new FInt(25736, false))
                i %= new FInt(25736, false);
            FInt k = (i * new FInt(10, false)) / new FInt(714, false);
            if (i != 0 && i != new FInt(6434, false) && i != new FInt(12868, false) &&
                i != new FInt(19302, false) && i != new FInt(25736, false))
                j = (i * new FInt(100, false)) / new FInt(714, false) - k * new FInt(10, false);
            if (k <= new FInt(90, false))
                return sin_lookup(k, j);
            if (k <= new FInt(180, false))
                return sin_lookup(new FInt(180, false) - k, j);
            if (k <= new FInt(270, false))
                return sin_lookup(k - new FInt(180, false), j).Inverse;
            else
                return sin_lookup(new FInt(360, false) - k, j).Inverse;
        }

        private static FInt sin_lookup(FInt i, FInt j) {
            if (j > 0 && j < new FInt(10, false) && i < new FInt(90, false))
                return new FInt(SIN_TABLE[i.RawValue], false) +
                    ((new FInt(SIN_TABLE[i.RawValue + 1], false) - new FInt(SIN_TABLE[i.RawValue], false)) /
                    new FInt(10, false)) * j;
            else
                return new FInt(SIN_TABLE[i.RawValue], false);
        }

        private static int[] SIN_TABLE = {
        0, 71, 142, 214, 285, 357, 428, 499, 570, 641, 
        711, 781, 851, 921, 990, 1060, 1128, 1197, 1265, 1333, 
        1400, 1468, 1534, 1600, 1665, 1730, 1795, 1859, 1922, 1985, 
        2048, 2109, 2170, 2230, 2290, 2349, 2407, 2464, 2521, 2577, 
        2632, 2686, 2740, 2793, 2845, 2896, 2946, 2995, 3043, 3091, 
        3137, 3183, 3227, 3271, 3313, 3355, 3395, 3434, 3473, 3510, 
        3547, 3582, 3616, 3649, 3681, 3712, 3741, 3770, 3797, 3823, 
        3849, 3872, 3895, 3917, 3937, 3956, 3974, 3991, 4006, 4020, 
        4033, 4045, 4056, 4065, 4073, 4080, 4086, 4090, 4093, 4095, 
        4096
    };
        #endregion

        private static FInt mul(FInt F1, FInt F2) {
            return F1 * F2;
        }

        #region Cos, Tan, Asin
        public static FInt Cos(FInt i) {
            return Sin(i + new FInt(6435, false));
        }

        public static FInt Tan(FInt i) {
            return Sin(i) / Cos(i);
        }

        public static FInt Asin(FInt F) {
            bool isNegative = F < 0;
            F = Abs(F);

            if (F > FInt.OneF)
                throw new ArithmeticException("Bad Asin Input:" + F.ToDouble());

            FInt f1 = mul(mul(mul(mul(new FInt(145103 >> FInt.SHIFT_AMOUNT, false), F) -
                new FInt(599880 >> FInt.SHIFT_AMOUNT, false), F) +
                new FInt(1420468 >> FInt.SHIFT_AMOUNT, false), F) -
                new FInt(3592413 >> FInt.SHIFT_AMOUNT, false), F) +
                new FInt(26353447 >> FInt.SHIFT_AMOUNT, false);
            FInt f2 = PI / new FInt(2, true) - (Sqrt(FInt.OneF - F) * f1);

            return isNegative ? f2.Inverse : f2;
        }
        #endregion

        #region ATan, ATan2
        public static FInt Atan(FInt F) {
            return Asin(F / Sqrt(FInt.OneF + (F * F)));
        }

        public static FInt Atan2(FInt F1, FInt F2) {
            if (F2.RawValue == 0 && F1.RawValue == 0)
                return (FInt)0;

            FInt result = (FInt)0;
            if (F2 > 0)
                result = Atan(F1 / F2);
            else if (F2 < 0) {
                if (F1 >= 0)
                    result = (PI - Atan(Abs(F1 / F2)));
                else
                    result = (PI - Atan(Abs(F1 / F2))).Inverse;
            } else
                result = (F1 >= 0 ? PI : PI.Inverse) / new FInt(2, true);

            return result;
        }
        #endregion

        #region Abs
        public static FInt Abs(FInt F) {
            if (F < 0)
                return F.Inverse;
            else
                return F;
        }
        #endregion
    }
}
