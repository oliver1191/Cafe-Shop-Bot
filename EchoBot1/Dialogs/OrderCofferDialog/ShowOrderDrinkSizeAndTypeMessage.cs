using EchoBot1.Dialogs.Shared;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs.OrderCofferDialog
{
    public class ShowOrderDrinkSizeAndTypeMessage: Dialog
    {
        public ShowOrderDrinkSizeAndTypeMessage() : base(nameof(ShowOrderDrinkSizeAndTypeMessage))
        {

        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            //var message = Activity.CreateMessageActivity();
            //message.Type = ActivityTypes.Message;
            //message.Attachments = new List<Attachment> { Helper.CreateAdaptiveCardAttachment(new[] { ".", "Dialogs", "Welcome", "Resources", "chooseSizeCard.json" }), EchoBot1Bot.ChooseSizeCard(), };            
            var drinkSize = dc.Context.Activity.Text;
            if(drinkSize=="Large"|| drinkSize=="Middle" || drinkSize == "Small")
            {
                await dc.Context.SendActivityAsync("Sucessfully");
            }
            else if ("Order".Equals(dc.Context.Activity.Text, StringComparison.OrdinalIgnoreCase))
            {
                var message = Activity.CreateMessageActivity();
                message.Type = ActivityTypes.Message;
                message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateDrinkTypeCard() };
                await dc.Context.SendActivityAsync(message);
                return await dc.EndDialogAsync();
            }
            else
            {
                await dc.Context.SendActivityAsync("Order failed.Please input Order to reorder.");
            }
            return await dc.EndDialogAsync();
        }
    }
}
