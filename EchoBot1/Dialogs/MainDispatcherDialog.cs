using EchoBot1.Dialogs.OrderCofferDialog;
using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs
{
    public class MainDispatcherDialog:ComponentDialog
    {
        public MainDispatcherDialog() : base(nameof(MainDispatcherDialog))
        {
            AddDialog(new OrderCofferDialogs());
            //AddDialog(new AskDrinkTypeDialog());
        }

        protected override async Task<DialogTurnResult> OnBeginDialogAsync(DialogContext innerDc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var turnContext = innerDc.Context;
            ////var onTurnProperty= await innerDc.Context.GetConversationPropertyAsync<OnTurnProperty>
            //var dialogTurnResult = await innerDc.ContinueDialogAsync();
            //if (!turnContext.Responded && dialogTurnResult != null && dialogTurnResult.Status != DialogTurnStatus.Complete)
            //{
            //    dialogTurnResult= await innerDc.BeginDialogAsync(nameof(OrderCofferDialogs));
            //}
            //if (dialogTurnResult == null)
            //{
            //    return await innerDc.EndDialogAsync();
            //}
            //if (dialogTurnResult.Status == DialogTurnStatus.Cancelled)
            //{
            //    await innerDc.CancelAllDialogsAsync();
            //}
            return await innerDc.BeginDialogAsync(nameof(OrderCofferDialogs)); ;
        }
    }
}
