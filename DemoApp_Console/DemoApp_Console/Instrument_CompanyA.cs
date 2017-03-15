/*
 *  Author: Troy Davis
 *  Project: Module08 - DemoApp - Console
 *      this file extends the abstract class Instrument and implements the IInstrument interface for Company A 
 *  Class: IN 2017 (Advanced C#)
 *  Date: Mar 15, 2017 
 *  Revision: Original
 */

using System;

namespace DemoApp_Console
{
    public class CompanyA_Instrument : Instrument
    {
        private const string VENDOR = "Company A";
        private const string DESCRIPTION = "Instrument A";

        public CompanyA_Instrument() : base(VENDOR, DESCRIPTION)
        {
        }
        public override void RecvSignal(Signal s)
        {
            // compute output based on the Signal received

            // this instrument computes the value of Signal.ChannelA raised to the power of Signal.ChannelB
            //      NOTE: method uses Math.Pow to differentiate from how Company B implemented output
            int valOut = (int)Math.Pow((double)s.ChannelA, (double)s.ChannelB);
            // output results
            Console.WriteLine("\t{0} - Instrument Output = {1}", this.Vendor(), valOut.ToString());
        }
    }
}