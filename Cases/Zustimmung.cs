using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class Zustimmung {

        public static string zustimmendAntworten(Patient patient) {

            switch(patient.ChatStatus)
            {
                case "NewReminderQuestion":

                    SimonDB.setChatStatus(patient.ID, "NewReminder");
                    return "Woran wollen Sie erinnert werden?";

                default:
                    return "Sie sagen also ja.";
            }

        }
    }
}