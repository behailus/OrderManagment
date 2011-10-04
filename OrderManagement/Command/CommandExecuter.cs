using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Elmah;
using OrderManagement.Web.Infrastructure;

namespace OrderManagement.Web.Command
{
    public static class CommandExecuter
    {
        public static void ExcuteLater(ICommand command)
        {
            Task.Factory.StartNew(() =>
            {
                var succcessfully = false;
                try
                {
                    DocumentStoreHolder.TryAddSession(command);
                    command.Execute();
                    succcessfully = true;
                }
                finally
                {
                    DocumentStoreHolder.TryComplete(command, succcessfully);
                }
            }, TaskCreationOptions.LongRunning)
                .ContinueWith(task =>
                {
                    ErrorLog.GetDefault(null).Log(new Error(task.Exception));
                }, TaskContinuationOptions.OnlyOnFaulted);
        }
    }
}