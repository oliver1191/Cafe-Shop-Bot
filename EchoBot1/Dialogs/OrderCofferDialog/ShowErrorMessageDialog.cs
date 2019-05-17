using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs.OrderCofferDialog
{
    public class ShowErrorMessageDialog:ComponentDialog
    {
        public ShowErrorMessageDialog() : base(nameof(ShowErrorMessageDialog))
        {

        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {            
            await dc.Context.SendActivityAsync($"{dc.Context.Activity.Text} is wrong commond.Please input 'order' to reorder");
            return await dc.EndDialogAsync();
        }
    }
}
