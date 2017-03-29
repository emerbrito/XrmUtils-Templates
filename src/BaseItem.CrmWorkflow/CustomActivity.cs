using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk.Workflow;
using System;
using System.Activities;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using XrmUtils.Extensions.Plugins;

namespace XrmUtils.BaseProject
{
    public class CustomActivity : WorkflowActivityBase
    {

        // form more information on input and output parameters visit:
        // https://msdn.microsoft.com/en-us/library/gg327842.aspx

        [Input("Full Name")]
        [RequiredArgument]
        public InArgument<string> FullName { get; set; }


        [Output("Output value")]
        public OutArgument<string> Result { get; set; }

        protected override void Execute(LocalWorkflowContext localContext)
        {

            // set the appropriate input and output parameters then
            // replace this with your code:

            var name = FullName.Get(localContext.ExecutionContext);
            var greetings = $"Hello {name}!";

            Result.Set(localContext.ExecutionContext, greetings);

        }

    }
}
