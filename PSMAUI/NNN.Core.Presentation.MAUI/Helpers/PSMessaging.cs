using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NNN.Core.Presentation.MAUI.Helpers
{
    public static class PSMessaging
    {
        public static void Publish<TSender>(TSender sender, string message)
            where TSender : class
        {
            MessagingCenter.Send<TSender>(sender, message); //  will be replaced by Mediator
        }
        public static void Subscribe<TSender>(object obj, string message, Action<TSender> callBack)
            where TSender : class
        {
            MessagingCenter.Subscribe<TSender>(obj, message, callBack); //  will be replaced by Mediator
        }
    }
}
