using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class GesundheitstippErhalten {

        public static string[] moeglicheAntworten = {
            "Wann haben Sie das letzte Mal mit ihren Verwandten gesprochen? Mein Tipp: Reden Sie öfters mit Ihnen. Ob Sie es glauben,"+
            "oder nicht: Ein guter Kontakt wird Ihnen hilf gesünder zu bleiben.",
            "Sie führen ein Rauchertagebuch bei mir. Leider haben Sie noch nicht komplett mit dem Rauchen aufgehört. Aber zusammen können wir das schaffen!",

        };

        public static string tippAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }

    }
}