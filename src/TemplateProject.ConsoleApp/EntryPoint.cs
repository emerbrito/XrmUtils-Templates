using Common.Logging;
using Microsoft.Xrm.Sdk;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace $safeprojectname$
{
    public class EntryPoint
    {

        private ILog log;
        private IOrganizationService service;

        public EntryPoint(IOrganizationService clientSvc, ILog logger)
        {

            log = logger ?? throw new ArgumentNullException(nameof(logger));
            service = clientSvc ?? throw new ArgumentNullException(nameof(clientSvc));

            log.Trace($"{nameof(EntryPoint)} instantiated.");

        }

        public void Execute()
        {
            // Your code here.
            // To access logging capabilities and IOrganizationService instance,
            // use local variables :
            // this.log and this.service
        }

    }
}
