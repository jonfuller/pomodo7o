using System;
using System.ComponentModel.Composition;
using System.Windows;
using Pomodo7o;

namespace Extensions
{
    [Export(typeof(IPomodoroPublisher))]
    public class AnnoyingMessageBox : IPomodoroPublisher
    {
        public void Dispose()
        {
        }

        public void WorkStarted()
        {
            MessageBox.Show("work started");
        }

        public void WorkPercent(int percent)
        {
            if(percent % 10 == 0)
                MessageBox.Show("work percent:" + percent);
        }

        public void WorkTimeLeft(TimeSpan remaining)
        {
        }

        public void WorkComplete()
        {
            MessageBox.Show("work complete");
        }

        public void RestStarted()
        {
            MessageBox.Show("rest started");
        }

        public void RestPercent(int percent)
        {
            if(percent % 10 == 0)
                MessageBox.Show("rest percent:" + percent);
        }

        public void RestTimeLeft(TimeSpan remaining)
        {
        }

        public void RestComplete()
        {
            MessageBox.Show("rest complete");
        }
    }
}
