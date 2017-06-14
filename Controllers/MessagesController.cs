using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.Bot.Connector;
using Microsoft.Cognitive.LUIS;
using DAK.Controllers;
using System;
using DAK.Cases;
using System.Text.RegularExpressions;

namespace DAK {


    // Unsere wichtigste Klasse!

    [BotAuthentication]
    public class MessagesController : ApiController {


        // wichtige mögliche Zustände des Nutzers
        public string[] possibleUserStatus = {
            "ISTNUTZERNEU",
            "MACHTGERADEDIEEINFUEHRUNG",
        };


        public async Task<HttpResponseMessage> Post([FromBody]Activity activity) {

            // Zeigt uns die aktuelle Nutzer ID auf der Konsole an
            System.Diagnostics.Debug.WriteLine("Aktueller Nutzer hat die ID: " + activity.ChannelId);


            // NUTZER spezifische Daten abfragen
           /* StateClient stateClient = activity.GetStateClient();
            BotData userData = await stateClient.BotState.GetUserDataAsync(activity.ChannelId, activity.From.Id);


            foreach (string possibleStatus in possibleUserStatus) {
                string currentStatusValue = "";
                if (userData.GetProperty<string>(possibleStatus).Length != 0) {
                   currentStatusValue = userData.GetProperty<string>(possibleStatus);
                }
                System.Diagnostics.Debug.WriteLine("Nutzerstatus von " + possibleStatus + ":" + currentStatusValue);
            }


            await Nutzerstatus.setStringUserDate("ISTNUTZERNEU", "NEIN", stateClient, activity);
           

            */


            // Ertsmalige Initzialisierung von Datenbank und Co.
            if (!Main.isInitialized) {
                //System.Diagnostics.Debug.Write("Wird init.!");
                Main.init();
            }

            System.Diagnostics.Debug.Write("ID: " + activity.Id);


            if (activity.Type != ActivityTypes.Message) { // Für den Fall das der Nutzer keine Nachricht sondern etwas anderes schickt
                HandleSystemMessage(activity);

            } else { // Es muss eine Nachricht sein! :-D 


                ConnectorClient connector = new ConnectorClient(new Uri(activity.ServiceUrl));

                // Hier beginnt die Hauptverarbeitung der Nutzeranfrage

                // Bevor die Daten an LUIS geschickt werden sollten wir auf ein paar Standardfälle prüfen
                // z.B. ob das Wort Notfall enthalten ist.
                // oder ob es das erste Mal ist, dass ein Nutzer sich anmeldet

                string userInput = activity.Text;
                string returnString = "";
                bool keySwitch = false;

                // Datenbank Initializierung
                var patient = SimonDB.getPatientByChatID("HackathonDemoPatient");
                if(patient == null)
                {
                    patient = SimonDB.addPatient("Frank", "HackathonDemoPatient", 1955, false, false);
                }

                
                // Initzialer Status Check um ggf. in andere Bereiche der Kommunikation zu springen.
                switch (patient.ChatStatus)
                {
                    case "NewReminder":
                        SimonDB.addReminder(patient, userInput);
                        SimonDB.setChatStatus(patient.ID, "Initiated");
                        returnString = $"Ihr Termin '{userInput}' wurde erstellt";
                        keySwitch = true;
                        break;
                    case "Sauerstoff2":
                        keySwitch = true;
                        SimonDB.setChatStatus(patient.ID, "Initiated");
                        string druckangabe = Regex.Match(userInput, @"\d+").Value;
                        if (druckangabe == null || druckangabe.Length == 0) {
                            SimonDB.setChatStatus(patient.ID, "Sauerstoff2");
                            returnString =  "Ich habe leider nicht verstanden wie viel Sauerstoff sie durchgeben wollen.";
                        }
                        returnString = "Ich habe für jetzt einen Flaschendruck von " + druckangabe + " bar für ihre Sauerstoffflaschen ermittelt.";
                        break;
                    default:
                        break;
                }

                // Schlüsselwortabfrage (einfache Befehle = einfaches Ausführen)
                string einstellungenAntwortString = "Klicken Sie auf diesen Link um Ihre Einstellungen zu ändern: https://dak-simon-api.herokuapp.com/#/" + patient.ID;
                switch (userInput.ToLower()) {
                    case "termin erstellen":
                        break;
                    case "notfall":
                        keySwitch = true;
                        returnString = Notfall.notfallAntwort() + " without LUIS";
                        break;
                    case "sauerstoff":
                        //keySwitch = true;
                        //returnString = SauerstoffVorratMelden.sauerstoffAntwort(activity) + " without LUIS";
                        break;
                    case "einstellung":
                        keySwitch = true;
                        returnString = einstellungenAntwortString;
                        break;
                    case "einstellungen":
                        keySwitch = true;
                        returnString = einstellungenAntwortString;
                        break;
                    case "optionen":
                        keySwitch = true;
                        returnString = einstellungenAntwortString;
                        break;
                    default:
                        break;
                }


                if (!keySwitch) {
                    // Die Verbindung zu LUIS wird aufgebaut und sein Ergebnis entgegengenommen
                    var client = new LuisClient("7b4d2bd3-cc23-4668-97c7-d1a285ef7510", "30ba26b6b8df4690b097f893e489dc2f");
                    var result = await client.Predict(userInput); // Hier steckt die Auswertung von LUIS drinne. Damit können wir etwas anfangen

                    // Bevor das Result gecheckt wird sollte einmal gecheckt werden wie groß die Zustimmunsgrate ist.
                    // Wenn diese zu klein ist sollte entsprechend reagiert werden.
                    string mostImportantIntendName = result.Intents[0].Name;
                    double highestScore = result.Intents[0].Score;





                    if (highestScore * 100 < 20) {
                        returnString = "Ich bin mir leider nur zu " + highestScore * 100 + "% sicher, was Sie mir sagen wollten: " + mostImportantIntendName;
                    } else {
                        returnString = LUIS.calculateLUISResult(result, patient, activity);
                        System.Diagnostics.Debug.Write(mostImportantIntendName);
                    }

                } 



                // Es wird eine Antwort an den Nutzer zurückgeschickt
                Activity reply = activity.CreateReply(returnString);
                await connector.Conversations.ReplyToActivityAsync(reply);


            }



            // Der Antwortenteil
            var response = Request.CreateResponse(HttpStatusCode.OK);
            return response;
        }

        private Activity HandleSystemMessage(Activity message) {

            if (message.Type == ActivityTypes.DeleteUserData) {

                // Implement user deletion here
                // If we handle user deletion, return a real message

            } else if (message.Type == ActivityTypes.ConversationUpdate) {

                // Handle conversation state changes, like members being added and removed
                // Use Activity.MembersAdded and Activity.MembersRemoved and Activity.Action for info
                // Not available in all channels

            } else if (message.Type == ActivityTypes.ContactRelationUpdate) {

                // Handle add/remove from contact lists
                // Activity.From + Activity.Action represent what happened

            } else if (message.Type == ActivityTypes.Typing) {

                // Handle knowing tha the user is typing

            } else if (message.Type == ActivityTypes.Ping) {

            }

            return null;
        }
    }
}