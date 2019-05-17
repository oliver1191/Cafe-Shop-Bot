// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using EchoBot1.Dialogs;
using EchoBot1.Dialogs.Shared;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using Microsoft.Extensions.Logging;

namespace EchoBot1
{
    /// <summary>
    /// Represents a bot that processes incoming activities.
    /// For each user interaction, an instance of this class is created and the OnTurnAsync method is called.
    /// This is a Transient lifetime service.  Transient lifetime services are created
    /// each time they're requested. For each Activity received, a new instance of this
    /// class is created. Objects that are expensive to construct, or have a lifetime
    /// beyond the single turn, should be carefully managed.
    /// For example, the <see cref="MemoryStorage"/> object and associated
    /// <see cref="IStatePropertyAccessor{T}"/> object are created with a singleton lifetime.
    /// </summary>
    /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-2.1"/>
    public class EchoBot1Bot : IBot
    {
        private readonly EchoBot1Accessors _accessors;
        private readonly ILogger _logger;        
        private DialogSet dialogs;

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="accessors">A class containing <see cref="IStatePropertyAccessor{T}"/> used to manage state.</param>
        /// <param name="loggerFactory">A <see cref="ILoggerFactory"/> that is hooked to the Azure App Service provider.</param>
        /// <seealso cref="https://docs.microsoft.com/en-us/aspnet/core/fundamentals/logging/?view=aspnetcore-2.1#windows-eventlog-provider"/>
        public EchoBot1Bot(EchoBot1Accessors accessors, ILoggerFactory loggerFactory)
        {
            if (loggerFactory == null)
            {
                throw new System.ArgumentNullException(nameof(loggerFactory));
            }

            _logger = loggerFactory.CreateLogger<EchoBot1Bot>();
            _logger.LogTrace("Turn start.");
            _accessors = accessors ?? throw new System.ArgumentNullException(nameof(accessors));
            var state = _accessors.CreateConversationSPA<DialogState>("Oliver Cafe Shop Start");
            dialogs = new DialogSet(state);
            dialogs.Add(new MainDispatcherDialog());
        }

        /// <summary>
        /// Every conversation turn for our Echo Bot will call this method.
        /// There are no dialogs used, since it's "single turn" processing, meaning a single
        /// request and response.
        /// </summary>
        /// <param name="turnContext">A <see cref="ITurnContext"/> containing all the data needed
        /// for processing this conversation turn. </param>
        /// <param name="cancellationToken">(Optional) A <see cref="CancellationToken"/> that can be used by other objects
        /// or threads to receive notice of cancellation.</param>
        /// <returns>A <see cref="Task"/> that represents the work queued to execute.</returns>
        /// <seealso cref="BotStateSet"/>
        /// <seealso cref="ConversationState"/>
        /// <seealso cref="IMiddleware"/>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        {
            // Handle Message activity type, which is the main activity type for shown within a conversational interface
            // Message activities may contain text, speech, interactive cards, and binary or unknown attachments.
            // see https://aka.ms/about-bot-activity-message to learn more about the message and other activity types
            //if (turnContext.Activity.Type == ActivityTypes.Message)
            //{
            //    // Get the conversation state from the turn context.
            //    var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());

            //    // Bump the turn count for this conversation.
            //    state.TurnCount++;

            //    // Set the property using the accessor.
            //    await _accessors.CounterState.SetAsync(turnContext, state);

            //    // Save the new turn count into the conversation state.
            //    await _accessors.ConversationState.SaveChangesAsync(turnContext);

            //    // Echo back to the user whatever they typed.
            //    var responseMessage = $"Turn {state.TurnCount}: You sent '{turnContext.Activity.Text}'\n";
            //    await turnContext.SendActivityAsync(responseMessage);
            //}
            //else
            //{
            //    await turnContext.SendActivityAsync($"{turnContext.Activity.Type} event detected");
            //}
            switch (turnContext.Activity.Type)
            {
                case ActivityTypes.Message:
                    //var dc = await dialogs.CreateContextAsync(turnContext);
                    //var turnResult = await dc.ContinueDialogAsync();
                    //if (turnResult.Status == DialogTurnStatus.Empty && !dc.Context.Responded)
                    //{
                    //    await dc.BeginDialogAsync(nameof(MainDispatcherDialog));
                    //}
                    if (turnContext.Activity.Text == "Order")
                    {
                        await CreateDrinkTypeCardMessageAsync(turnContext);
                    }
                    //var state = await _accessors.CounterState.GetAsync(turnContext, () => new CounterState());
                    //if (!string.IsNullOrEmpty(state.LastTurnMessage) && !string.IsNullOrEmpty(turnContext.Activity.Text) && state.OrderStatus == 1)
                    //{
                    //    await turnContext.SendActivityAsync($"Thanks,get your order:{turnContext.Activity.Text} {state.LastTurnMessage}.");
                    //    state.LastTurnMessage = "";
                    //    state.OrderStatus = 0;
                    //    await _accessors.CounterState.SetAsync(turnContext, state);
                    //    await _accessors.ConversationState.SaveChangesAsync(turnContext);
                    //}
                    //else if (string.IsNullOrEmpty(state.LastTurnMessage) && turnContext.Activity.Text != "order" && state.OrderStatus == 0)
                    //{
                    //    state.LastTurnMessage = turnContext.Activity.Text;
                    //    state.OrderStatus = 1;
                    //    await _accessors.CounterState.SetAsync(turnContext, state);
                    //    await _accessors.ConversationState.SaveChangesAsync(turnContext);
                    //    await CreateSizeCardMessageAsync(turnContext);
                    //}                                                      
                    //something to do                                         
                    break;
                case ActivityTypes.ConversationUpdate:
                    if (turnContext.Activity.MembersAdded != null&& turnContext.Activity.Recipient.Id== turnContext.Activity.MembersAdded[0].Id)
                    {

                        await SendWelcomeMessageAsync(turnContext);
                    }
                    break;
                default:
                    await turnContext.SendActivityAsync($"{turnContext.Activity.Type} has not support now.");
                    break;
            }
            await _accessors.ConversationState.SaveChangesAsync(turnContext);
        }

        private async Task SendWelcomeMessageAsync(ITurnContext turnContext)
        {            
            var activity = turnContext.Activity.CreateReply();
            activity.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateWelcomeCard()}; 
            //activity.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateDrinkTypeCard() };
            await turnContext.SendActivityAsync(activity);
        }

        private async Task CreateDrinkTypeCardMessageAsync(ITurnContext turnContext)
        {
            var activity = turnContext.Activity.CreateReply();
            //activity.Attachments = new List<Attachment> {AdaptiveCardFactory.CreateDrinkTypeCard()};
            activity.Attachments = new List<Attachment> { AdaptiveCardFactory.CreateShowCard()};
            await turnContext.SendActivityAsync(activity);
        }

        private async Task CreateSizeCardMessageAsync(ITurnContext turnContext)
        {
            var activity = turnContext.Activity.CreateReply();            
            activity.Attachments = new List<Attachment> { Helper.CreateAdaptiveCardAttachment(new[] { ".", "Dialogs", "Welcome", "Resources", "chooseSizeCard.json" }), ChooseSizeCard(), };
            await turnContext.SendActivityAsync(activity);
        }
        public static Attachment CreateWhatCanYouDoCard()
        {
            HeroCard whatCanYouDoCard = new HeroCard();
            whatCanYouDoCard.Title = "Welcome to Oliver Cafe Shop!";
            whatCanYouDoCard.Subtitle = "Please Choose the type of drink you want.";
            //StringBuilder body = new StringBuilder();
            //body.AppendFormat("<b>{0}</b><br />{1}<br />", "1", "2");
            //body.Append("<ul>");
            //body.AppendFormat("<li>{0}</li>", "3");
            //body.AppendFormat("<li>{0}</li>", "4");
            //body.Append("</ul>");
            //body.AppendFormat("{0}<br />", "5");
            //body.Append("<ul>");
            //body.AppendFormat("<li>{0}</li>", "Tea");
            //body.AppendFormat("<li>{0}</li>", "Coffer");
            //body.AppendFormat("<li>{0}</li>", "Milk");
            //body.Append("</ul>");            
            //whatCanYouDoCard.Text = body.ToString();
            whatCanYouDoCard.Buttons = new List<CardAction>()
            {
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Tea",
                    Title = "Tea"
                },
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Coffer",
                    Title = "Coffer"
                },
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Milk",
                    Title = "Mill"
                }
            };

            return whatCanYouDoCard.ToAttachment();
        }

        public static Attachment ChooseSizeCard()
        {
            HeroCard whatCanYouDoCard = new HeroCard();
            whatCanYouDoCard.Title = "Please choose the size of drink you want.";
            whatCanYouDoCard.Buttons = new List<CardAction>()
            {
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Large",
                    Title = "Large"
                },
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Middle",
                    Title = "Middle"
                },
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Small",
                    Title = "Small"
                }
            };

            return whatCanYouDoCard.ToAttachment();
        }
        
    }
}
