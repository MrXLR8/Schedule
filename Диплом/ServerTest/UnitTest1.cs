using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Server
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CommandParse()
        {
            string input ="start port:27015";
            CommandLine test= new CommandLine(input);

            Assert.AreEqual(test.command, "start");
            Assert.AreEqual(test.getParametr("port"), "27015");

        }
    }
}
