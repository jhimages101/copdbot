using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class Tutorial {

        public static string tutorialBekommen() {
            string tutorialText = "" +
                "Ich bin IHR Assistent. Ich bin der ideale Gesprächspartner & Betreuer für Menschen wie Sie, die mit COPD zu kämpfen haben." +
                "\n Ich kannen Ihnen viele Tipps geben die Ihnen helfen Ihren Gesundheitsstatus zu verbessern. Schreiben Sie dazu einfach 'Tipps'."+
                "\n Auch kann ich Sie an Medikamenteinnahmen erinnern. Schreiben Sie 'Erinnerung hinzufügen' oder nur 'Erinnerungen' um neue Erinnerungen" +
                " hinzuzufügen, oder alte zu ändern." +
                "\n ..." +
                "";
            return tutorialText;
        }
    }
}