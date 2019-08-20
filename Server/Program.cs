using System;

namespace Server {
    public class PrinterI : Demo.PrinterDisp_ {
        public override void printString (string s, Ice.Current current) {
            Console.WriteLine (s);
        }
        public override string op (string sin, out string sout, Ice.Current current = null) {
            sout = "";
            try {
                // Try to write file contents here...
                Console.WriteLine (sin); // In params are initialized
                sout = "Hello World! server:" + DateTime.Now; // Assign out param
                return "Done";
            } catch (System.Exception ex) {
                // GenericError e = new GenericError ("cannot write file", ex);
                // e.reason = "Exception during write operation";
                 throw ex;
            }
            return "Faile"; 
        }
    }

    public class Program {
        public static int Main (string[] args) {
            try {
                using (Ice.Communicator communicator = Ice.Util.initialize (ref args)) {
                    var adapter =
                        communicator.createObjectAdapterWithEndpoints ("SimplePrinterAdapter", "default -h localhost -p 10000");
                    adapter.add (new PrinterI (), Ice.Util.stringToIdentity ("SimplePrinter"));
                    adapter.activate ();
                    communicator.waitForShutdown ();
                }
            } catch (Exception e) {
                Console.Error.WriteLine (e);
                return 1;
            }
            return 0;
        }
    }
}