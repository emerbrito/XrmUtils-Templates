using PowerArgs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class CommandArguments
    {

        [ArgShortcut("conn"), ArgDescription("CRM connection string. If not specified a modal connection dialog will be displayed.")]
        public string Connection { get; set; }

    }
}
