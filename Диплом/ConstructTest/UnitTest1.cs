using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Builder;
namespace ConstructTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void IntervalFill()
        {
            IntervalCollection interval = new IntervalCollection();
            Interval add;
            add = new Interval(1, new TimeSpan(9, 0, 0), new TimeSpan(10, 20, 0));
            interval.setTime(add);

            add = new Interval(2, new TimeSpan(10, 20, 0), new TimeSpan(11, 50, 0));
            interval.setTime(add);

            add = new Interval(3, new TimeSpan(12, 10, 0), new TimeSpan(13, 30, 0));
            interval.setTime(add);

            add = new Interval(4, new TimeSpan(13, 40, 0), new TimeSpan(15, 00, 0));
            interval.setTime(add);

            add = new Interval(5, new TimeSpan(15, 20, 0), new TimeSpan(16, 40, 0));
            interval.setTime(add);


            add = new Interval(3, new TimeSpan(12, 15, 0), new TimeSpan(13, 25, 0));
            interval.setTime(add);





            Assert.IsTrue(
                interval.timeList[2].start == new TimeSpan(12, 15, 0)
                );

            


        }


        [TestMethod]
        public void FlaseIntervalFill()
        {
            IntervalCollection interval = new IntervalCollection();
            Interval add;
            add = new Interval(1, new TimeSpan(9, 0, 0), new TimeSpan(10, 20, 0));
            interval.setTime(add);

            add = new Interval(2, new TimeSpan(10, 20, 0), new TimeSpan(11, 50, 0));
            interval.setTime(add);

            add = new Interval(3, new TimeSpan(12, 10, 0), new TimeSpan(13, 30, 0));
            interval.setTime(add);

            add = new Interval(4, new TimeSpan(13, 40, 0), new TimeSpan(15, 00, 0));
            interval.setTime(add);

            add = new Interval(5, new TimeSpan(15, 20, 0), new TimeSpan(16, 40, 0));
            interval.setTime(add);


            add = new Interval(3, new TimeSpan(10, 15, 0), new TimeSpan(18, 25, 0));
            interval.setTime(add);

            Assert.IsFalse(
    interval.setTime(add)
    );


        }
    }
}
