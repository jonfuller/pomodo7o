using System;
using Growl.Connector;
using Pomodo7o;

namespace Growler
{
    public class GrowlPublisher : BasePublisher
    {
        private readonly GrowlConnector _growler;

        public GrowlPublisher()
        {
            _growler = new GrowlConnector();

            _growler.Register(
                new Application(Properties.Resources.Growl_Application) { Icon = Properties.Resources.tomato_work },
                new[]{
                        new NotificationType(
                            Properties.Resources.NotificationType_WorkStart,
                            Properties.Resources.NotificationDisplay_WorkStart,
                            Properties.Resources.tomato_work, true),
                        new NotificationType(
                            Properties.Resources.NotificationType_WorkComplete,
                            Properties.Resources.NotificationDisplay_WorkComplete,
                            Properties.Resources.tomato_work, true),
                        new NotificationType(
                            Properties.Resources.NotificationType_RestStart,
                            Properties.Resources.NotificationDisplay_RestStart,
                            Properties.Resources.tomato_rest, true),
                        new NotificationType(
                            Properties.Resources.NotificationType_RestComplete,
                            Properties.Resources.NotificationDisplay_RestComplete,
                            Properties.Resources.tomato_rest, true),
                });
        }

        public override void WorkStarted()
        {
            Notify(
                Properties.Resources.NotificationType_WorkStart,
                Properties.Resources.Title_WorkStart);
        }

        public override void WorkComplete()
        {
            Notify(
                Properties.Resources.NotificationType_WorkComplete,
                Properties.Resources.Title_WorkComplete);
        }

        public override void RestStarted()
        {
            Notify(
                Properties.Resources.NotificationType_RestStart,
                Properties.Resources.Title_RestStart);
        }

        public override void RestComplete()
        {
            Notify(
                Properties.Resources.NotificationType_RestComplete,
                Properties.Resources.Title_RestComplete);
        }

        private void Notify(string type, string title)
        {
            _growler.Notify(new Notification(
                Properties.Resources.Growl_Application,
                type,
                Guid.NewGuid().ToString(),
                title,
                String.Empty));
        }
    }
}
