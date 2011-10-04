using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OrderManagement.Web.Command
{
    public interface ICommand
    {
        void Execute();
    }
}