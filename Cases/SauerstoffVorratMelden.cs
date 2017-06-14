using Microsoft.Bot.Connector;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace DAK.Cases {
    public class SauerstoffVorratMelden {

       

        public static string sauerstoffAntwort(Activity activity, Patient patient) {
            string text = activity.Text;

            //Patient patient = SimonDB.getPatientByChatID(activity.ChannelId);
            
            //Patient patient = SimonDB.getPatientByChatID("HackathonDemoPatient");
            string chatStatus = patient.ChatStatus;
            //SimonDB.setChatStatus(patient.ID, "");

            if (text.Contains("bar")) {
                string druckangabe = Regex.Match(text, @"\d+").Value;
                if (druckangabe == null || druckangabe.Length == 0) {
                    SimonDB.setChatStatus(patient.ID, "Sauerstoff2");
                    return "Ich habe leider nicht verstanden wie viel Sauerstoff sie durchgeben wollen.";
                }

                // Hier den Druck in die Datenbank schreiben!
                SimonDB.setChatStatus(patient.ID, "Initiated");
                return "Ich habe für jetzt einen Flaschendruck von " + druckangabe + " bar für ihre Sauerstoffflaschen ermittelt.";

            }
            SimonDB.setChatStatus(patient.ID, "Sauerstoff2");
            return "Wie viel Druck haben Sie noch auf ihrer Flasche?";
            /*
            switch (chatStatus) {
                    case "Sauerstoff1":
                   
                    default:
                        return None.getNoneAnwser();
                }

           return None.getNoneAnwser();*/
        }

       

    }
}