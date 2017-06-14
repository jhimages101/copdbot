using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DAK.Cases {
    public class MitleidErhalten {

        public static string[] moeglicheAntworten = {
            "Das tut mir leid zu hören",
            "O nein. Wie kam es dazu?",
            "Das ist natürlich nicht so schön",
            "Das ist wirklich schade.",
        };

        public static string mitleidsAntwort() {
            Random random = new Random();
            int randomNumber = random.Next(0, moeglicheAntworten.Length);
            return moeglicheAntworten[randomNumber];
        }
    }
}