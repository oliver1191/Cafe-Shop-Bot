using EchoBot1.Dialogs.Shared;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs.OrderCofferDialog
{
    public class AskDrinkSizeDialog : Dialog
    {
        public AskDrinkSizeDialog() : base(nameof(AskDrinkSizeDialog))
        {

        }

        //protected override async Task OnPromptAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, bool isRetry, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    var message = Activity.CreateMessageActivity();
        //    message.Type = ActivityTypes.Message;
        //    message.Attachments = new List<Attachment> { Helper.CreateAdaptiveCardAttachment(new[] { ".", "Dialogs", "Welcome", "Resources", "chooseSizeCard.json" }), EchoBot1Bot.ChooseSizeCard(), };
        //    await turnContext.SendActivityAsync(message);
        //}

        public override async Task<DialogTurnResult> BeginDialogAsync(DialogContext dc, object options, CancellationToken cancellationToken = default(CancellationToken))
        {
            //DialogTurnResult dialogTurnResult;
            //var drinkType = await stepContext.Context.GetConversationPropertyAsync<Drink>("Drink");
            string drinkType = dc.Context.Activity.Text;
            var message = Activity.CreateMessageActivity();
            message.Type = ActivityTypes.Message;
            switch (drinkType)
            {
                //"红茶", "绿茶", "猫屎咖啡", "黑糖玛奇朵咖啡", "酸奶", "纯牛奶" 
                case "红茶":
                case "绿茶":
                case "猫屎咖啡":
                case "黑糖玛奇朵咖啡":
                case "酸奶":
                case "纯牛奶":                   
                    //message.Attachments = new List<Attachment> { EchoBot1Bot.ChooseSizeCard(), };
                    message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateChooseSizeCard(), };
                    await dc.Context.SendActivityAsync(message);
                    return EndOfTurn;
                //break;
                case "order":
                case "Order":                                     
                    message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateDrinkTypeCard() };
                    await dc.Context.SendActivityAsync(message);
                    return await dc.EndDialogAsync();
                default:
                    await dc.Context.SendActivityAsync($"{dc.Context.Activity.Text} has not support now.");
                    return await dc.EndDialogAsync();
                    //break;
            }
            //var message = Activity.CreateMessageActivity();
            //message.Type = ActivityTypes.Message;
            ////message.Attachments = new List<Attachment> { EchoBot1Bot.ChooseSizeCard(), };
            //message.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateChooseSizeCard(), };
            //await dc.Context.SendActivityAsync(message);
            //return Dialog.EndOfTurn;

            //return await base.BeginDialogAsync(dc, new PromptOptions());
        }

        //protected override Task<PromptRecognizerResult<FoundChoice>> OnRecognizeAsync(ITurnContext turnContext, IDictionary<string, object> state, PromptOptions options, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    return base.OnRecognizeAsync(turnContext, state, options, cancellationToken);
        //}
    }
    //public class DrinkType
    //{
    //    public const string Tea = "Tea";
    //    public const string Coffer = "Coffer";
    //    public const string Milk = "Milk";
    //}

    public class DinkSize
    {
        public const string Large = "Large";
        public const string Middle = "Middle";
        public const string Small = "Small";
    }

    //public enum Drink
    //{
    //    None,
    //    Tea = 1,
    //    Coffer = 2,
    //    Milk = 4,
    //    Large = 8,
    //    Middle = 16,
    //    Small = 32,  
    //}
}
