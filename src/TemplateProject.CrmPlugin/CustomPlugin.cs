using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmUtils.Extensions;
using XrmUtils.Extensions.Plugins;

namespace $safeprojectname$
{

    [Message("update")]
    [PrimaryEntity("account")]
    [Stage(PipelineStage.PostOperation)]
    [ExecutionMode(ExecutionMode.Synchronous)]
    public class CustomPlugin : PluginBase
    {

        protected override void Execute(LocalPluginContext localContext)
        {

            // set the appropriate class attributes then
            // replace this with your code:

            localContext.Trace("Retrieving target entity.");
            var target = localContext.GetTargetEntity();

        }

    }
}

