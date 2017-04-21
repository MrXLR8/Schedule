using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Конструктор;
namespace ConstructTest
{
    [TestClass]
    public class UnitTest1
    {


        [TestMethod]
        public void IntervalFill()
        {
            Intervals interval = new Intervals();
            Time add;
            add = new Time(1, new TimeSpan(9, 0, 0), new TimeSpan(10, 20, 0));
            interval.setTime(add);

            add = new Time(2, new TimeSpan(10, 20, 0), new TimeSpan(11, 50, 0));
            interval.setTime(add);

            add = new Time(4, new TimeSpan(13, 40, 0), new TimeSpan(15, 00, 0));
            interval.setTime(add);

            add = new Time(5, new TimeSpan(15, 20, 0), new TimeSpan(16, 40, 0));
            interval.setTime(add);


            Time check;
            check = new Time(3, new TimeSpan(12, 10, 0), new TimeSpan(13, 30, 0));

            Assert.IsTrue(interval.setTime(check));
            Assert.IsTrue(
                interval.timeList[3].start == new TimeSpan(12, 10, 0)
                );
        }


        [TestMethod]
        public void FlaseIntervalFill()
        {
            Intervals interval = new Intervals();
            Time add;
            add = new Time(1, new TimeSpan(9, 0, 0), new TimeSpan(7, 20, 0));
            Assert.IsFalse(interval.setTime(add));

            add = new Time(2, new TimeSpan(10, 20, 0), new TimeSpan(11, 50, 0));
            interval.setTime(add);

            add = new Time(4, new TimeSpan(13, 40, 0), new TimeSpan(15, 00, 0));
            interval.setTime(add);

            add = new Time(5, new TimeSpan(15, 20, 0), new TimeSpan(16, 40, 0));
            interval.setTime(add);


            Time check;
            check = new Time(3, new TimeSpan(9, 10, 0), new TimeSpan(13, 30, 0));

            Assert.IsFalse(interval.setTime(check));
            Assert.IsNull(
                interval.timeList[check.index]
                );
        }
    }
}
