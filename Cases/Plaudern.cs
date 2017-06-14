using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class Plaudern {
        public static string[] moeglicheAntworten = {
            "Ich mag es Ihnen zuzuhören. Glauben Sie mir, wenn ich nur den richtigen Hinweis von Ihnen bekommen, kann ich Ihnen wirklich weiterhelfen!",
            "Irgendwann werde ich die Fähigkeiten besitzen vernünftig mit Ihnen zu plaudern. :-D Versprochen."
        };

        public static string plaudern() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }

    }
}