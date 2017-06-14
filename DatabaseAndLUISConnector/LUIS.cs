using DAK.Cases;
using Microsoft.Bot.Connector;
using Microsoft.Cognitive.LUIS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Controllers {
    public class LUIS {

        // zu implementieren: Aktueller Systemstatus


        public static string calculateLUISResult(LuisResult result, Patient patient, Activity activity) {

            string mostImportantIntendName = result.Intents[0].Name;
            double score = result.Intents[0].Score * 100;
            System.Diagnostics.Debug.WriteLine("Ich bin mir zu " + score + "% sicher das Sie folgende Intention hatten: " + mostImportantIntendName);
            //return "Ich bin mir zu " + score + "% sicher das Sie folgende Intention hatten: " + mostImportantIntendName;


            
            switch (mostImportantIntendName) {
                case "None":
                    return None.getNoneAnwser(patient);
                case "LUISNamenErfahren":
                    return LUISNamenErfahren.vorstellungsAntwort();
                case "BegrüßungErhalten":
                    return Begruessung.begruessungsAntwort();
                case "MitleidErhalten":
                    return MitleidErhalten.mitleidsAntwort();
                case "Ablehnung":
                    return Ablehnung.ablehnendAntworten();
                case "Zustimmung":
                    return Zustimmung.zustimmendAntworten(patient);
                case "Plaudern":
                    return Plaudern.plaudern();
                case "Notfall":
                    return Notfall.notfallAntwort();
                case "TutorialErhalten":
                    return Tutorial.tutorialBekommen();
                case "SauerstoffVorratMelden":
                    return SauerstoffVorratMelden.sauerstoffAntwort(activity, patient);
                case "LobErhalten":
                    return LobErhalten.lobendeAntwort();
                case "ErinnerungHinzufügen":
                    return NeuerTermin.getNeuerTerminAnwser(patient);
                case "TippErhalten":
                    return GesundheitstippErhalten.tippAntwort();
                case "EinstellungenÄndern":


                default:
                    return mostImportantIntendName + "?"; 

            }

        }
    }
}