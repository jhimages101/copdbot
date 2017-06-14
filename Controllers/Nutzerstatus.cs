using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace DAK.Controllers {
    public class Nutzerstatus {

       /* public static async Task<string> calculateUserStatus(StateClient stateClient, Activity activity, string nameOfDatafield) {
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            
            if (userData.GetProperty<string>(nameOfDatafield).Length != 0) {
                return userData.GetProperty<string>(nameOfDatafield);
            }


            return "";
        }*/



        public static async Task<bool> setStringUserDate(string nameOfDatafield, string valueOfUserData, StateClient stateClient, Activity activity) {
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);
            userData.SetProperty<string>(nameOfDatafield, valueOfUserData);
            await stateClient.BotState.SetUserDataAsync(activity.ChannelId, activity.From.Id, userData);
            return true;
        }
    }
}