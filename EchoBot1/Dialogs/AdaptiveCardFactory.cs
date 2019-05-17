using AdaptiveCards;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EchoBot1.Dialogs
{
    public class AdaptiveCardFactory
    {
        static AdaptiveSchemaVersion schemaVersion = new AdaptiveSchemaVersion(1, 0);

        public static Attachment CreateChoiceCard(DrinkType drinkType)
        {
            var data = new List<string> { "红茶", "绿茶", "猫屎咖啡", "黑糖玛奇朵咖啡", "酸奶", "纯牛奶" };
            int drinkTypeNum = Convert.ToInt32(drinkType);
            string drinkTupeString = drinkType.ToString();
            AdaptiveCard drinkCard = new AdaptiveCard(schemaVersion);
            drinkCard.Body.Add
                (
                    new AdaptiveContainer()
                    {
                        Items = new List<AdaptiveElement>()
                        {
                            new AdaptiveTextBlock()
                            {
                                Color=AdaptiveTextColor.Attention,
                                Weight=AdaptiveTextWeight.Bolder,
                                Size=AdaptiveTextSize.Medium,
                                Text="Please choose the type of drink you want!",
                                HorizontalAlignment=AdaptiveHorizontalAlignment.Center,

                            },
                            new AdaptiveChoiceSetInput()
                            {
                                Id=drinkTupeString,
                                Style=AdaptiveChoiceInputStyle.Compact,                                
                                IsMultiSelect=false,
                                Value=drinkTupeString,
                                Choices=new List<AdaptiveChoice>()
                                {
                                    new AdaptiveChoice()
                                    {
                                        Title=data[drinkTypeNum],
                                        Value=data[drinkTypeNum],
                                    },
                                    new AdaptiveChoice()
                                    {
                                        Title=data[drinkTypeNum+1],
                                        Value=data[drinkTypeNum+1],
                                    }
                                }

                            }
                        }
                    }
                );
            return new Attachment()
            {
                Content = drinkCard,
                ContentType = AdaptiveCard.ContentType
            };
        }

        public static Attachment CreateDrinkTypeCard()
        {
            HeroCard heroCard = new HeroCard();
            heroCard.Title = "Welcome to Oliver Cafe Shop!";
            heroCard.Subtitle = "Please click the type of drink you want.";
            heroCard.Buttons = new List<CardAction>()
            {
                new CardAction
                {
                    Type=ActionTypes.ImBack,
                    Value="Tea",
                    Title="Tea",
                },
                new CardAction
                {
                    Type=ActionTypes.ImBack,
                    Value="Coffer",
                    Title="Coffer",
                },
                new CardAction
                {
                    Type=ActionTypes.ImBack,
                    Value="Milk",
                    Title="Milk",
                },

            };
            return heroCard.ToAttachment();
        }


        public static Attachment CreateWelcomeCard()
        {
            HeroCard welcomeCard = new HeroCard();
            welcomeCard.Title = "Welcome to Oliver Cafe Shop.";
            welcomeCard.Subtitle = "I am your virtual bot.";
            welcomeCard.Text = "Please click 'Order' button to Order your drink.";
            welcomeCard.Buttons = new List<CardAction>()
            {
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = "Order",
                    Title = "Order"
                },
                new CardAction
                {
                    Type = ActionTypes.OpenUrl,
                    Value = "http://baidu.com",
                    Title = "Link"
                },
                //new CardAction
                //{
                //    Type = ActionTypes.ImBack,
                //    Value = "Small",
                //    Title = "Small"
                //}
            };

            return welcomeCard.ToAttachment();
        }

        public static Attachment CreateShowCard()
        {
            AdaptiveCard adaptiveCard = new AdaptiveCard(schemaVersion);            
            adaptiveCard.Body.Add
                (
                new AdaptiveContainer()
                {
                    Items = new List<AdaptiveElement>()
                    {
                        new AdaptiveTextBlock()
                        {
                                Color=AdaptiveTextColor.Attention,
                                Weight=AdaptiveTextWeight.Bolder,
                                Size=AdaptiveTextSize.Medium,
                                Text="Please choose the type of drink you want!",
                                HorizontalAlignment=AdaptiveHorizontalAlignment.Center,
                        },
                    }
                }
            );
            adaptiveCard.Actions.Add
                (
                    new AdaptiveShowCardAction()
                    {
                        Title = "Tea",
                        Card = AdaptiveCardFactory.CreateDrinkCard(DrinkType.Tea),
                    }
                );
            adaptiveCard.Actions.Add
                (
                    new AdaptiveShowCardAction()
                    {
                        Title = "Coffer",
                        Card = AdaptiveCardFactory.CreateDrinkCard(DrinkType.Coffer),
                    }
                );
            adaptiveCard.Actions.Add
                (
                    new AdaptiveShowCardAction()
                    {
                        Title = "Milk",
                        Card = AdaptiveCardFactory.CreateDrinkCard(DrinkType.Milk),
                    }
                );                   
            return new Attachment()
            {
                Content = adaptiveCard,
                ContentType = AdaptiveCard.ContentType,
            };
        }

        public static AdaptiveCard CreateDrinkCard(DrinkType drinkType)
        {
            AdaptiveCard drinkCard = new AdaptiveCard(schemaVersion);
            var data = new List<string> { "红茶", "绿茶", "猫屎咖啡", "黑糖玛奇朵咖啡", "酸奶", "纯牛奶" };
            int drinkTypeNum = Convert.ToInt32(drinkType);
            string drinkTupeString = drinkType.ToString();
            drinkCard.Body.Add
                (
                        new AdaptiveTextBlock()
                        { 
                                Color = AdaptiveTextColor.Attention,
                                Weight = AdaptiveTextWeight.Bolder,
                                Size = AdaptiveTextSize.Medium,
                                Text = string.Format("Please choose the {0} type of drink you want!", drinkTupeString),
                                //HorizontalAlignment = AdaptiveHorizontalAlignment.Center,
                        }                           
                );
            drinkCard.Body.Add(
                new AdaptiveChoiceSetInput()
                {
                    Id = drinkTupeString,
                    Style = AdaptiveChoiceInputStyle.Compact,
                    //IsRequired=false,
                    IsMultiSelect = false,
                    Value = drinkTupeString,
                    Choices = new List<AdaptiveChoice>()
                                {
                                    new AdaptiveChoice()
                                    {
                                        Title=data[drinkTypeNum],
                                        Value=data[drinkTypeNum],
                                    },
                                    new AdaptiveChoice()
                                    {
                                        Title=data[drinkTypeNum+1],
                                        Value=data[drinkTypeNum+1],
                                    }
                                }

                }
                );
            drinkCard.Actions.Add
                (
                    new AdaptiveSubmitAction()
                    {
                        Title = string.Format("Order the \"{0}\" drink", drinkTupeString),                        
                    }
                );
            return drinkCard;
        }

        public static Attachment ChooseDrinkSubTypeCard(DrinkType drinkType)
        {
            HeroCard whatCanYouDoCard = new HeroCard();
            var data = new List<string> { "红茶", "绿茶", "猫屎咖啡", "黑糖玛奇朵咖啡", "酸奶", "纯牛奶" };
            int drinkTypeNum = Convert.ToInt32(drinkType);
            string drinkTupeString = drinkType.ToString();
            whatCanYouDoCard.Title = "Please choose the sub type of drink you want.";
            whatCanYouDoCard.Buttons = new List<CardAction>()
            {
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = data[drinkTypeNum],
                    Title = data[drinkTypeNum]
                },
                new CardAction
                {
                    Type = ActionTypes.ImBack,
                    Value = data[drinkTypeNum+1],
                    Title = data[drinkTypeNum+1]
                },                
            };

            return whatCanYouDoCard.ToAttachment();
        }

        public static Attachment CreateChooseSizeCard()
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

    public enum DrinkType
    {
        Tea = 0,
        Coffer = 2,
        Milk = 4,
    }
}
