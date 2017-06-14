using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;

namespace DAK
{

    public class SimonDB : DbContext
    {
        public SimonDB() : base("SimonDBConnection")
        {

        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Suggestion> Suggestions { get; set; }
        public DbSet<AssessmentQuestion> AssessmentQuestions { get; set; }
        public DbSet<AssessmentAnswer> AssessmentAnswers { get; set; }
        public DbSet<Reminder> Reminders { get; set; }
        public DbSet<OxygenMeasurement> OxygenMeasurements { get; set; }

        public static void FillEmptyDB()
        {
            addSuggestions();
            addAssessmentQuestions();
        }

        public static Patient addPatient(string name, string chatID, int birthYear, bool smoker, bool oxygen)
        {
            using (var db = new SimonDB())
            {
                var pat = new Patient();
                pat.Name = name;
                pat.ChatID = chatID;
                pat.YearOfBirth = birthYear;
                pat.Smoker = smoker;
                pat.Oxygen = oxygen;
                pat.CreationTime = DateTime.Now;

                pat.ChatStatus = "Initiated";
                pat.NextQuestionID = 1;

                db.Patients.Add(pat);
                db.SaveChanges();
                return pat;
            }
        }

        public static OxygenMeasurement addOxyMeasurement(Patient pat, int value)
        {
            using (var db = new SimonDB())
            {
                var om = new OxygenMeasurement();

                om.Patient = pat;
                om.Time = DateTime.Now;
                om.Value = value;

                db.OxygenMeasurements.Add(om);
                db.SaveChanges();
                return om;
            }
        }

        public static Reminder addReminder(Patient pat, string name)
        {
            using (var db = new SimonDB())
            {
                var r = new Reminder();
                r.Patient = pat;
                r.Time = DateTime.Now;
                r.Text = name;

                db.Reminders.Add(r);
                db.SaveChanges();
                return r;
            }
        }

        //public static Patient getCurrentPatient()
        //{
        //    using (var db = new SimonDB())
        //    {
        //        return db.Patients.FirstOrDefault();
        //    }
        //}

        public static Patient getPatientByChatID(string chatID)
        {
 
            using (var db = new SimonDB())
            {
                return db.Patients.FirstOrDefault(p => p.ChatID.Equals(chatID));
            }
        }

        public static void setSmoker(int patientID, bool smoker)
        {
            using (var db = new SimonDB())
            {
                var pat = db.Patients.Find(patientID);
                pat.Smoker = smoker;
                db.SaveChanges();
            }
        }

        public static void setChatStatus(int patientID, string chatStatus)
        {
            using (var db = new SimonDB())
            {
                var pat = db.Patients.Find(patientID);
                pat.ChatStatus = chatStatus;
                db.SaveChanges();
            }
        }

        public static void setOxygen(int patientID, bool oxygen)
        {
            using (var db = new SimonDB())
            {
                var pat = db.Patients.Find(patientID);
                pat.Oxygen = oxygen;
                db.SaveChanges();
            }
        }

        public static void addSuggestions()
        {
            using (var db = new SimonDB())
            {
                var sugs = new Suggestion[]
                    {
                        new Suggestion { Text="Geh spazieren" },
                        new Suggestion { Text="Triff dich mit Verwandten" },
                        new Suggestion { Text="Spiele ein Spiel" }
                    };

                db.Suggestions.AddRange(sugs);
                db.SaveChanges();
            }
        }

        public static void addAssessmentQuestions()
        {
            using (var db = new SimonDB())
            {
                var questions = new AssessmentQuestion[]
                    {
                        new AssessmentQuestion { Text="Wieviel husten Sie?" },
                        new AssessmentQuestion { Text="Wie verschleimt sind Ihre Atemwege?" },
                        new AssessmentQuestion { Text="Wie bewerten Sie das Engegefühl in der Brust?" }
                    };

                db.AssessmentQuestions.AddRange(questions);
                db.SaveChanges();
            }
        }

        public static void addAssessmentAnswer(Patient patient, AssessmentQuestion question, int value)
        {
            using (var db = new SimonDB())
            {
                var answer = new AssessmentAnswer();
                answer.Patient = patient;
                answer.Question = question;
                answer.Time = DateTime.Now;
                answer.Value = value;

                db.AssessmentAnswers.Add(answer);
                db.SaveChanges();
            }
        }
    }

    public class Patient
    {
        [Key]
        public int ID { get; set; }
        public string ChatID { get; set; }
        public string Name { get; set; }
        public int YearOfBirth { get; set; }
        public bool Smoker { get; set; }
        public bool Oxygen { get; set; }
        public DateTime CreationTime { get; set; }

        /// <summary>
        /// Initiated - Wenn Patient frisch erstellt wurde
        /// NewReminderQuestion - Wurde gefragt, ob neuer Termin erstellt werden soll
        /// NewReminder - Hat Bejaht, erwarte Antwort
        /// Sauerstoff0 - Der Sauerstoffvorrat soll abgecheckt werden
        /// Sauerstoff1 - Zahl entgegennehmen
        /// </summary>
        public string ChatStatus { get; set; }

        public int NextQuestionID { get; set; }

        public virtual List<Reminder> Reminder { get; set; }
        public virtual List<AssessmentAnswer> AssessmentAnswers { get; set; }

    }

    public class Reminder
    {
        [Key]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public string Text { get; set; }
        public virtual Patient Patient { get; set; }
    }

    public class OxygenMeasurement
    {
        [Key]
        public int ID { get; set; }
        public DateTime Time { get; set; }
        public int Value { get; set; }
        public virtual Patient Patient { get; set; }
    }

    public class Suggestion
    {
        [Key]
        public int ID { get; set; }
        public string Text { get; set; }
    }

    public class AssessmentQuestion
    {
        [Key]
        public int ID { get; set; }
        public string Text { get; set; }
    }

    public class AssessmentAnswer
    {
        [Key]
        public int ID { get; set; }
        public int Value { get; set; } // von 1-5?
        public DateTime Time { get; set; }

        public virtual AssessmentQuestion Question { get; set; }

        public virtual Patient Patient { get; set; }
    }

}