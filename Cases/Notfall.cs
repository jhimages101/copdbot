using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class Notfall {

        public static string[] moeglicheAntworten = {
            "Geht es Ihnen akut schlecht? Soll ich Ihnen erklären, was vielleicht helfen könnte? Wenn es zu schlimm ist: Rufen Sie einen Rettungswagen!"
        };

        public static string notfallAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }


    }
}