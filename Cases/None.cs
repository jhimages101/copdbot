using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class None {

        public static string getNoneAnwser(Patient patient) {
            //SimonDB.setChatStatus(activity.ChannelId,"Initiated");
            SimonDB.setChatStatus(patient.ID, "Initiated");
            return "Das habe ich leider nicht verstanden.";
        }
    }
}