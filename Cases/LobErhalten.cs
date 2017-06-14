using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class LobErhalten {

        public static string[] moeglicheAntworten = {
            "Das finde ich wirklich gut. Ich hoffe so geht es weiter.",
            "Das freut mich für Sie.",
            "Das finde ich klasse. Ich hoffe Sie erzählen auch anderen Menschen davon."
        };

        public static string lobendeAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }

    }
}