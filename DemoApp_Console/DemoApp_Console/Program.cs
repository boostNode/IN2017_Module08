/*
 *  Author: Troy Davis
 *  Project: Module08 - DemoApp - Console
 *      
 *      Simulates an instrument panel that outputs a Signal from which installed Instruments can display output
 *      
 *      Inheritance: see public abstract class Instrument and extended classes CompanyA_Instrument & CompanyB_Instrument
 *      Abstract Classes: see public abstract class Instrument
 *      Methods: see public class Panel and extended classes CompanyA_Instrument & CompanyB_Instrument
 *      Properties: see public class Signal
 *      Interfaces: see public interface IPanel, implemented in public class Panel
 *                  see public interface IInstrument, implemented in public class CompanyA_Instrument (extended from abstract class Instrument)
 *                  see public interface IInstrument, implemented in public class CompanyB_Instrument(extended from abstract class Instrument)
 *                  
 *  Class: IN 2017 (Advanced C#)
 *  Date: Mar 15, 2017 
 *  Revision: Original
 */

using System;
using System.Collections.Generic;
using System.Timers;

namespace DemoApp_Console
{
    class Program
    {
        static void Main(string[] args)
        {
            // instantiate instrument Panel
            Panel p = new Panel();
            // instantiate and plug in Instruments
            p.AddInstrument(new CompanyA_Instrument());
            p.AddInstrument(new CompanyB_Instrument());
            // operate the Panel
            p.Operate();
        }
    }
    public class Signal
    {
        // Signal properties
        public int ChannelA { get; set; }
        public int ChannelB { get; set; }
    }
    public interface IPanel
    {
        void AddInstrument(Instrument i);
        void SendSignal(Instrument i, Signal s);
    }
    public class Panel : IPanel
    {
        // class variables
        Signal sig;                 // Signal object
        List<Instrument> listInst;  // List/Collection of Instrument objects
        Timer panelTimer;           // Panel timer

        // Panel constructor (initializes panel)
        public Panel()
        {
            // initialize the panel

            // display greeting
            Console.WriteLine("Module 08 - DemoApp - Instrument Panel Interface - Console\n");
            Console.WriteLine("\tInstruments added to the panel should compute the value of\n");
            Console.WriteLine("\tChannel A raised to the power Channel B\n");
            Console.Write("\tPress <Enter> to begin operation:"); Console.ReadLine();

            // instantiate new Signal (used to communicate data of the specification/type Signal)
            sig = new Signal();

            // instantiate list of Instruments (will hold Instrument objects that are added to the panel)
            listInst = new List<Instrument>();

            // instantiate panelTimer (will drive refresh of the instrument panel)
            panelTimer = new Timer(3000);
            // register panelTimer event handler
            panelTimer.Elapsed += PanelTimer_Elapsed;
        }
        public void Operate()
        {
            // display prompt for user to exit
            Console.WriteLine("\nPress 'Enter' at any time to exit: \n");

            // start panelTimer
            panelTimer.Start();

            // wait on user to exit
            Console.ReadLine();
        }
        private void PanelTimer_Elapsed(object sender, ElapsedEventArgs e)
        {
            // this methods generates a random number and exponent then assigns the values to the
            // class variables intChannelA and intChannelB

            // local variables
            int intMinRandomNumber = 2;
            int intMaxRandomNumber = 6;

            // update channels A & B with random numbers
            Random ranNumberGenerator = new Random();
            this.sig.ChannelA = ranNumberGenerator.Next(intMinRandomNumber, intMaxRandomNumber);
            this.sig.ChannelB = ranNumberGenerator.Next(intMinRandomNumber, intMaxRandomNumber);

            // update display
            UpdateDisplay();
        }
        private void UpdateDisplay()
        {
            Console.WriteLine("\nPanel Driver Update: Channel A = {0} , Channel B = {1}", this.sig.ChannelA, this.sig.ChannelB);
            foreach (Instrument i in listInst) { SendSignal(i, this.sig); }
            Console.WriteLine("");
        }
        // methods req'd for IPanel interface
        public void AddInstrument(Instrument inst)
        {
            Console.WriteLine("\n\tInstrument Added to Panel: {0}, {1}", inst.Vendor(), inst.Description());
            // see if instrument has already been added to List listInst
            bool found = false;
            foreach (Instrument i in listInst)
            {
                if ( inst == i ) { found = true; }
            }
            // add instrument to List listInst if it doesn't already exist
            if ( !found ) { listInst.Add(inst); }
        }
        public void SendSignal(Instrument i, Signal s)
        {
            i.RecvSignal(s);
        }
    }
    public interface IInstrument
    {
        void RecvSignal(Signal sig);
    }
    public abstract class Instrument : IInstrument
    {
        // member variables
        private string vendor;
        private string description;
        // base constructor
        public Instrument(string vendor, string description)
        {
            this.vendor = vendor;
            this.description = description;
        }
        public string Vendor()
        {
            return this.vendor;
        }
        public string Description()
        {
            return this.description;
        }
        // method required for interface IInstrument
        public abstract void RecvSignal(Signal sig);
    }
}
