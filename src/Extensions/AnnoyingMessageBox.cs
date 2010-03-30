using System.Windows;
using Pomodo7o;

namespace Extensions
{
    public class AnnoyingMessageBox : BasePublisher
    {
        public override void WorkStarted()
        {
            MessageBox.Show("work started");
        }

        public override void WorkPercent(int percent)
        {
            if(percent % 10 == 0)
                MessageBox.Show("work percent:" + percent);
        }

        public override void WorkComplete()
        {
            MessageBox.Show("work complete");
        }

        public override void RestStarted()
        {
            MessageBox.Show("rest started");
        }

        public override void RestPercent(int percent)
        {
            if(percent % 10 == 0)
                MessageBox.Show("rest percent:" + percent);
        }

        public override void RestComplete()
        {
            MessageBox.Show("rest complete");
        }
    }
}
