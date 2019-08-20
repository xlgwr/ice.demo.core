using System;

namespace Client {
    public class Program {
        public static int Main (string[] args) {
            try {
                using (Ice.Communicator communicator = Ice.Util.initialize (ref args)) {
                    var obj = communicator.stringToProxy ("SimplePrinter:default -h localhost -p 10000");
                    var printer = Demo.PrinterPrxHelper.checkedCast (obj);
                    if (printer == null) {
                        throw new ApplicationException ("Invalid proxy");
                    }

                    printer.printString ("Hello World!\n" + DateTime.Now);
                    string outStr = "";
                    Console.WriteLine (printer.op ("Hello World!\n" + DateTime.Now, out outStr));
                    Console.WriteLine (outStr);
                }
            } catch (Exception e) {
                Console.Error.WriteLine (e);
                return 1;
            }
            return 0;
        }
    }
}