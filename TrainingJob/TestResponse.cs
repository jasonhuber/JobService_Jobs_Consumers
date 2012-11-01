using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;


namespace TrainingJob
{
    [Description("This is a test class that responds with Hey!")]
    public class TestResponse : Sage.Platform.Scheduling.SystemJobBase
    {
        protected override void OnExecute()
        {
            Context.Result = "Hey!";
            base.Phase = "Stun";
        }
    }

    [Description("This is a test class that responds with Oh!, but takes a long time.")]
    public class TestResponseSleepy : Sage.Platform.Scheduling.SystemJobBase
    {
        protected override void OnExecute()
        {
            for (int i = 0; i < 10; i++)
            {
                System.Threading.Thread.Sleep(60000);
                base.Phase = i.ToString() + " of 10";
            }
            base.Phase = "10 of 10";
            Context.Result = "Oh!";
        }
    }
}