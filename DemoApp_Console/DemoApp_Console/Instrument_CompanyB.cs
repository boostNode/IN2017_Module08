/*
 *  Author: Troy Davis
 *  Project: Module08 - DemoApp - Console
 *      this file extends the abstract class Instrument and implements the IInstrument interface for Company B
 *  Class: IN 2017 (Advanced C#)
 *  Date: Mar 15, 2017 
 *  Revision: Original
 */

using System;

namespace DemoApp_Console
{
    public class CompanyB_Instrument : Instrument
    {
        private const string VENDOR = "Company B";
        private const string DESCRIPTION = "Instrument B";

        public CompanyB_Instrument() : base(VENDOR, DESCRIPTION)
        {
        }
        public override void RecvSignal(Signal s)
        {
            // compute output based on the Signal received

            // this instrument computes the value of Signal.ChannelA raised to the power of Signal.ChannelB
            //      NOTE: method uses a for loop to differentiate from how Company A implemented output
            int valOut = s.ChannelA;
            for (int i = 1; i < s.ChannelB; i++) { valOut *= s.ChannelA; }
            // output results
            Console.WriteLine("\t{0} - Instrument Output = {1}", this.Vendor(), valOut.ToString());
        }
    }
}