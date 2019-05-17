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
    public class AskDrinkTypeDialog:Dialog
    {
        public AskDrinkTypeDialog() : base(nameof(AskDrinkTypeDialog))
        {

        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = Activity.CreateMessageActivity();
            message.Type = ActivityTypes.Message;
            //message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateChoiceCard(DrinkType.Tea), AdaptiveCardFactory.CreateChoiceCard(DrinkType.Coffer), AdaptiveCardFactory.CreateChoiceCard(DrinkType.Milk), };
            //message.Attachments = new List<Attachment> { Helper.CreateAdaptiveCardAttachment(new[] { ".", "Dialogs", "Welcome", "Resources", "orderFood.json" }) };            
            message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateShowCard()};
            await dc.Context.SendActivityAsync(message);
            return Dialog.EndOfTurn;

            //return await base.BeginDialogAsync(dc, new PromptOptions());
        }
    }
}
