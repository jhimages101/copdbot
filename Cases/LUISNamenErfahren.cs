using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class LUISNamenErfahren {

        public static string[] moeglicheAntworten = {
            "Ich bin Simon, ihr persönlicher Gesundheitsberater.",
            "Mein Name ist Simon. Ich kann Ihnen vielleicht helfen ihren Gesundheitszustand angenehmer zu machen.",
            "Ich bin Simon. Ich freue mich Sie zu in Gesundheitsfragen zu unterstützen."
        };

        public static string vorstellungsAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }
    }

}