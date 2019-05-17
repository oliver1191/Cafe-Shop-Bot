// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;

namespace EchoBot1
{
    /// <summary>
    /// This class is created as a Singleton and passed into the IBot-derived constructor.
    ///  - See <see cref="EchoWithCounterBot"/> constructor for how that is injected.
    ///  - See the Startup.cs file for more details on creating the Singleton that gets
    ///    injected into the constructor.
    /// </summary>
    public class EchoBot1Accessors
    {
        /// <summary>
        /// Initializes a new instance of the class.
        /// Contains the <see cref="ConversationState"/> and associated <see cref="IStatePropertyAccessor{T}"/>.
        /// </summary>
        /// <param name="conversationState">The state object that stores the counter.</param>
        public EchoBot1Accessors(ConversationState conversationState)
        {
            ConversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
        }

        /// <summary>
        /// Gets the <see cref="IStatePropertyAccessor{T}"/> name used for the <see cref="CounterState"/> accessor.
        /// </summary>
        /// <remarks>Accessors require a unique name.</remarks>
        /// <value>The accessor name for the counter accessor.</value>
        public static string CounterStateName { get; } = $"{nameof(EchoBot1Accessors)}.CounterState";

        /// <summary>
        /// Gets or sets the <see cref="IStatePropertyAccessor{T}"/> for CounterState.
        /// </summary>
        /// <value>
        /// The accessor stores the turn count for the conversation.
        /// </value>
        public IStatePropertyAccessor<CounterState> CounterState { get; set; }

        /// <summary>
        /// Gets the <see cref="ConversationState"/> object for the conversation.
        /// </summary>
        /// <value>The <see cref="ConversationState"/> object.</value>
        public ConversationState ConversationState { get; }

        //public async Task<T> GetConversationPropertyAsync<T>(ITurnContext turnContext, string key, Func<T> defaultValueFactory = null)
        //{
        //    var conversationSPA = ConversationState.CreateProperty<T>(key);
        //    return await conversationSPA.GetAsync(turnContext, defaultValueFactory);
        //}

        //public async Task SetConversationPropertyAsync<T>(ITurnContext turnContext, string key, T value)
        //{
        //    var conversationSPA = ConversationState.CreateProperty<T>(key);
        //    await conversationSPA.SetAsync(turnContext, value);
        //}

        //public async Task RemoveConversationPropertyAsync<T>(ITurnContext turnContext, string key)
        //{
        //    var conversationSPA = ConversationState.CreateProperty<T>(key);
        //    await conversationSPA.DeleteAsync(turnContext);
        //}

        //public async Task ClearConversationStateAsync(ITurnContext turnContext, CancellationToken cancellationToken = default(CancellationToken))
        //{
        //    await ConversationState.ClearStateAsync(turnContext, cancellationToken);
        //}

        public IStatePropertyAccessor<T> CreateConversationSPA<T>(string key)
        {
            return ConversationState.CreateProperty<T>(key);
        }
    }
}
