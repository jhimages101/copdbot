using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class NeuerTermin {

        public static string getNeuerTerminAnwser(Patient patient) {

            SimonDB.setChatStatus(patient.ID, "NewReminderQuestion");

            return "Sie wollen einen neuen Termin vereinbaren?";
        }
    }
}