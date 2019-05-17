using Microsoft.Bot.Builder.Dialogs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs.OrderCofferDialog
{
    public class OrderCofferDialogs:ComponentDialog
    {
        public OrderCofferDialogs() : base(nameof(OrderCofferDialogs))
        {
            var waterfallSteps=new WaterfallStep[]
            {               
                AskDrinkSubTypeAsync,
                AskDrinkSizeAsync,
                ShowOrderDrinkSizeAndTypeMessageAsync,
            };
            AddDialog(new WaterfallDialog("OrderCofferDialogs", waterfallSteps));
            AddDialog(new ShowOrderDrinkSizeAndTypeMessage());
            AddDialog(new AskDrinkSizeDialog());
            AddDialog(new ShowErrorMessageDialog());
            AddDialog(new AskDrinkTypeDialog());
            AddDialog(new AskDrinkSubTypeDialog());
        }

        private async Task<DialogTurnResult> AskDrinkSizeAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(AskDrinkSizeDialog));
            //DialogTurnResult dialogTurnResult;
            ////var drinkType = await stepContext.Context.GetConversationPropertyAsync<Drink>("Drink");
            //string drinkType = stepContext.Context.Activity.Text;
            //switch (drinkType)
            //{
            //    //"红茶", "绿茶", "猫屎咖啡", "黑糖玛奇朵咖啡", "酸奶", "纯牛奶" 
            //    case "红茶":
            //    case "绿茶":
            //    case "猫屎咖啡":
            //    case "黑糖玛奇朵咖啡":
            //    case "酸奶":
            //    case "纯牛奶":
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(AskDrinkSizeDialog));                    
            //        break;                
            //    default:
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowErrorMessageDialog));
            //        //await stepContext.Context.SendActivityAsync($"{stepContext.Context.Activity.Text} has not support now.");
            //        break;
            //}
            //return dialogTurnResult;
        }  
        
        private async Task<DialogTurnResult> ShowOrderDrinkSizeAndTypeMessageAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {
            return await stepContext.BeginDialogAsync(nameof(ShowOrderDrinkSizeAndTypeMessage));
            //DialogTurnResult dialogTurnResult;
            ////var drinkType = await stepContext.Context.GetConversationPropertyAsync<Drink>("Drink");
            //var drinkType = stepContext.Context.Activity.Text;
            //var messageBack = stepContext.Context.Activity.Value.ToString().Trim('{').Trim('}');
            //string[] message = messageBack.Split(':');
            //if(message!=null && message.Length==2 && !string.IsNullOrEmpty(message[0]) && !string.IsNullOrEmpty(message[1]))
            //{                
            //    dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowOrderDrinkSizeAndTypeMessage));
            //}
            //else
            //{
            //    dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowErrorMessageDialog));
            //}

            //switch (drinkType)
            //{
            //    case "Large":
            //    case "Middle":
            //    case "Small":
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowOrderDrinkSizeAndTypeMessage));
            //        break;
            //    default:
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowErrorMessageDialog));
            //        break;
            //}
            //return dialogTurnResult;
        }

        private async Task<DialogTurnResult> AskDrinkSubTypeAsync(WaterfallStepContext stepContext, CancellationToken cancellationToken)
        {        
            return await stepContext.BeginDialogAsync(nameof(AskDrinkSubTypeDialog));
            //DialogTurnResult dialogTurnResult;
            ////var drinkType = await stepContext.Context.GetConversationPropertyAsync<Drink>("Drink");
            //string drinkType = stepContext.Context.Activity.Text;
            //switch (drinkType)
            //{
            //    case "Tea":
            //    case "Coffer":
            //    case "Milk":
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(AskDrinkSubTypeDialog));
            //        break;                
            //    default:
            //        dialogTurnResult = await stepContext.BeginDialogAsync(nameof(ShowErrorMessageDialog));
            //        //await stepContext.Context.SendActivityAsync($"{stepContext.Context.Activity.Text} has not support now.");
            //        break;
            //}
            //return dialogTurnResult;
        }
    }   
}
