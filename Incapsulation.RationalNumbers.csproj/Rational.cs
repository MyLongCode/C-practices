using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Incapsulation.RationalNumbers
{
    public class Rational
    {
        public int numerator; //up 
        public int denominator; //down 
        public bool isNan = false;

        public Rational(int numerator, int denominator)
        {
            
            if (denominator == 0) 
            {
                this.numerator = numerator;
                this.denominator = denominator;
                isNan = true;
            }
            else if(numerator == 0)
            {
                this.numerator = 0;
                this.denominator = 1;
            }
            else
            {
                this.numerator = numerator / GCD(numerator, denominator);
                this.denominator = denominator / GCD(numerator, denominator);
                if (this.denominator < 0)
                {
                    this.denominator = -this.denominator;
                    this.numerator = -this.numerator;
                }
            }
        }
        public Rational(int dividend)
        {
            this.numerator = dividend;
            this.denominator = 1;
        }


        int GCD(int a, int b)
        {
            while (b != 0)
            {
                var temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }

        public int Denominator { get { return denominator; } }
        public int Numerator { get { return numerator; } }
        public bool IsNan { get { return isNan; } }

        public static Rational operator+(Rational r1, Rational r2)
        {
            return new Rational(r1.numerator * r2.denominator + r2.numerator * r1.denominator, r2.denominator * r1.denominator);
        }

        public static Rational operator-(Rational r1, Rational r2)
        {
            return new Rational((r1.numerator * r2.denominator) - (r2.numerator * r1.denominator), r2.denominator * r1.denominator);
        }

        public static Rational operator*(Rational r1, Rational r2)
        {
            return new Rational(r1.numerator * r2.numerator, r1.denominator *r2.denominator);
        }
        public static Rational operator/(Rational r1, Rational r2)
        {
            if (r2.isNan == true) return new Rational(1, 0);
            return new Rational(r1.numerator * r2.denominator, r1.denominator * r2.numerator);
        }

        public static implicit operator double(Rational r1)
        {
            if (r1.denominator == 0) return double.NaN;
            return (double)r1.numerator / r1.denominator;
        }

        public static implicit operator int(Rational r1)
        {
            if (r1.numerator% r1.denominator != 0 && r1.numerator != 0) throw new Exception();
            return (int)r1.numerator / r1.denominator;
        }

        public static implicit operator Rational(int v)
        {
            return new Rational(v);
        }
    }
}
