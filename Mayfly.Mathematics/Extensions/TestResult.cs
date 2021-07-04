using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Meta.Numerics.Statistics;
using Mayfly.Mathematics;

namespace Mayfly.Extensions
{
    public static class TestResultExtensions
    {
        public static bool IsPassed(this TestResult test, double alpha)
        {
            return test.Probability <= alpha;
        }

        public static bool IsPassed(this TestResult test)
        {
            return test.IsPassed(Mayfly.Mathematics.UserSettings.DefaultAlpha);
        }

        //public static double P(this TestResult test, TestDirection direction)
        //{
        //    switch (direction)
        //    {
        //        case TestDirection.Left:
        //            return test.LeftProbability;
        //        case TestDirection.Right:
        //            return test.RightProbability;
        //    }
            
        //    return test.RightProbability;
        //}

        //public static bool IsPassed(this TestResult test, TestDirection direction, double alpha)
        //{
        //    return test.P(direction) < alpha;
        //}

        //public static bool IsPassed(this TestResult test, TestDirection direction)
        //{
        //    return test.IsPassed(direction, Mayfly.Mathematics.UserSettings.DefaultAlpha);
        //}
    }

    //public enum TestDirection
    //{
    //    Left,
    //    Right
    //}
}
