using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs.OrderCofferDialog
{
    public class AskDrinkSubTypeDialog: Dialog
    {
        public AskDrinkSubTypeDialog() : base(nameof(AskDrinkSubTypeDialog))
        {

        }
        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            var message = Activity.CreateMessageActivity();
            message.Type = ActivityTypes.Message;
            //message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateChoiceCard(DrinkType.Tea), AdaptiveCardFactory.CreateChoiceCard(DrinkType.Coffer), AdaptiveCardFactory.CreateChoiceCard(DrinkType.Milk), };
            //message.Attachments = new List<Attachment> { Helper.CreateAdaptiveCardAttachment(new[] { ".", "Dialogs", "Welcome", "Resources", "orderFood.json" }) };            
            //message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateShowCard() };                        
            if(dc.Context.Activity.Text == "Tea" || dc.Context.Activity.Text == "Coffer" || dc.Context.Activity.Text == "Milk")
            {
                DrinkType text = (DrinkType)Enum.Parse(typeof(DrinkType), dc.Context.Activity.Text);
                message.Attachments = new List<Attachment> { AdaptiveCardFactory.ChooseDrinkSubTypeCard(text) };
                await dc.Context.SendActivityAsync(message);
                return Dialog.EndOfTurn;
            }  
            else if ("Order".Equals(dc.Context.Activity.Text,StringComparison.OrdinalIgnoreCase))
            {
                message.Attachments=new List<Attachment> { AdaptiveCardFactory.CreateDrinkTypeCard() };
                await dc.Context.SendActivityAsync(message);
                return await dc.EndDialogAsync();
            }
            else{

                await dc.Context.SendActivityAsync($"{dc.Context.Activity.Text} has not support now.");
                return await dc.EndDialogAsync();
            }
            //    switch (dc.Context.Activity.Text)
            //{
            //    case "Tea":
            //    case "Coffer":
            //    case "Milk":
            //        message.Attachments = new List<Attachment> { AdaptiveCardFactory.ChooseDrinkSubTypeCard(text) };
            //        await dc.Context.SendActivityAsync(message);
            //        break;
            //    default:                    
            //        await dc.Context.SendActivityAsync($"{dc.Context.Activity.Text} has not support now.");
            //        await dc.EndDialogAsync();
            //        break;
            //}
            ////message.Attachments = new List<Attachment> { AdaptiveCardFactory.ChooseDrinkSubTypeCard(text) };
            ////await dc.Context.SendActivityAsync(message);
            //return Dialog.EndOfTurn;

            //return await base.BeginDialogAsync(dc, new PromptOptions());
        }
    }
}
