using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class Begruessung {

        public static string[] moeglicheAntworten = {
            "Guten Tag. Schön von Ihnen zu hören!",
            "Hallo",
            "Moin, Moin"
        };

        public static string begruessungsAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }
    }
}