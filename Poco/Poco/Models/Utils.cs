﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Poco.Models
{
    public static class Utils
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS

        #endregion

        #region PROPRIÉTÉS

        #endregion

        #region CONSTRUCTEURS

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Initialiser le fichier de trace
        /// </summary>
        static public void InitialiserFichierTrace()
        {
            Stream leFichier = File.Create("FichierTrace.txt");
            TextWriterTraceListener leListener = new TextWriterTraceListener(leFichier);
            Trace.AutoFlush = true;
            Trace.Listeners.Add(leListener);
        }


        /// <summary>
        /// Tracer un message dans le fichier de trace
        /// </summary>
        /// <param name="pMsg">Message à écrire</param>
        static public void Tracer(string pMsg)
        {
            Trace.WriteLine($"{DateTime.Now.Hour}:{DateTime.Now.Minute}> {pMsg}");
        }

        /// <summary>
        /// Fonction Générale pour parse un string en enum
        /// </summary>
        /// <typeparam name="T">Param générique</typeparam>
        /// <param name="value">un string</param>
        /// <returns></returns>
        public static T ParseEnum<T>(string valeur)
        {
            return (T)Enum.Parse(typeof(T), valeur, true);
        }

        static public List<Facture> ChargerListeFacture(string pCheminFichier)
        {
            if (File.Exists(pCheminFichier))
            {
                StreamReader fluxLecture = new StreamReader(pCheminFichier);
                string fichierTexte = fluxLecture.ReadToEnd();
                fluxLecture.Close();
                fichierTexte = fichierTexte.Replace("\r", "");

                // Création d'un vecteur de chaînes de caractères contenant chaque ligne individuellement.
                string[] vectLignes = fichierTexte.Split('\n');


                // Nombre de lignes non vides dans le fichier.
                int nbLignes;

                if (vectLignes[vectLignes.Length - 1] == "")
                    nbLignes = vectLignes.Length - 1; // On ne considère pas la dernière ligne si elle est vide.
                else
                    nbLignes = vectLignes.Length;

                if (nbLignes > 0)
                {
                    // Création du vecteur contenant les données du fichier; la taille est déterminée par le nombre de lignes (non vides) du fichier.
                    List<string[]> ListLignes = new List<string[]>();

                    for (int i = 0; i < nbLignes; i++)
                    {
                        ListLignes.Add(vectLignes[i].Split(";"));
                    }

                    // On retourne le vecteur contenant les données créé.
                    ListLignes.RemoveAt(0);

                    List<Facture> ListFactures = new List<Facture>();

                    foreach (string[] ligne in ListLignes)
                    {
                        uint noFacture = uint.Parse(ligne[0]);
                        DateTime date = DateTime.Parse(ligne[1], FormPrincipal.cultureinfo);
                        decimal stt = decimal.Parse(ligne[2]);
                        decimal tt = decimal.Parse(ligne[3]);
                        ListFactures.Add(new Facture(noFacture, date, stt, tt));
                    }

                    return ListFactures;
                }


            }
            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
            fluxEcriture.Write("NoFacture;SousTotal;Total\n");
            fluxEcriture.Close();
            return new List<Facture>();

        }

        //static public List<Employe> ChargerListeEmployes(String pCheminFichier)
        //{
        //    if (File.Exists(pCheminFichier))
        //    {
        //        StreamReader fluxLecture = new StreamReader(pCheminFichier);
        //        String fichierTexte = fluxLecture.ReadToEnd();
        //        fluxLecture.Close();
        //        fichierTexte = fichierTexte.Replace("\r", "");

        //        // Création d'un vecteur de chaînes de caractères contenant chaque ligne individuellement.
        //        String[] vectLignes = fichierTexte.Split('\n');


        //        // Nombre de lignes non vides dans le fichier.
        //        int nbLignes;

        //        if (vectLignes[vectLignes.Length - 1] == "")
        //            nbLignes = vectLignes.Length - 1; // On ne considère pas la dernière ligne si elle est vide.
        //        else
        //            nbLignes = vectLignes.Length;

        //        if (nbLignes > 0)
        //        {
        //            // Création du vecteur contenant les données du fichier; la taille est déterminée par le nombre de lignes (non vides) du fichier.
        //            List<string[]> ListLignes = new List<string[]>();

        //            for (int i = 0; i < nbLignes; i++)
        //            {
        //                ListLignes.Add(vectLignes[i].Split(";"));
        //            }

        //            // On retourne le vecteur contenant les données créé.
        //            ListLignes.RemoveAt(0);

        //            List<Employe> ListEmployes = new List<Employe>();

        //            foreach (string[] ligne in ListLignes)
        //            {
        //                string code = ligne[0];
        //                string nom = ligne[1];
        //                string prenom = ligne[2];
        //                DateTime dob = DateTime.Parse(ligne[3], FormPrincipal.cultureinfo);

        //                ListEmployes.Add(new Employe(code, nom, prenom, dob));
        //            }

        //            return ListEmployes;

        //        }
        //    }

        //    //Création du fichier
        //    StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
        //    fluxEcriture.Write("Code;Nom;Prenom;DOB\n");
        //    fluxEcriture.Close();
        //    return new List<Employe>();



        //}

        public static void EnregistrerDonnees(GestionEmploye ge, GestionFacture gf, Dictionary<TypeLegume, int> dicoQuant)
        {
            using StreamWriter sw1 = new StreamWriter("Files/Employes.json");
            {
                sw1.Write(JsonSerializer.Serialize(ge.ListeEmployes, typeof(List<Employe>)));
            }
            using StreamWriter sw2 = new StreamWriter("Files/Quantites.json");
            {
                sw2.Write(JsonSerializer.Serialize(dicoQuant, typeof(Dictionary<TypeLegume, int>)));
            }
            string fichierTexte = "NoFacture;Date;SousTotal;Total\n";
            foreach (Facture f in gf.ListeFactures)
            {
                fichierTexte += f.NoFacture + ";" + f.Date.ToString("dd/MM/yyyy") + ";" + f.SousTotal + ";" + f.PrixTotal + "\n";
            }
            EnregistrerDonnees("Files/Factures.csv", fichierTexte, false);


        }

        public static Dictionary<TypeLegume, int> ChargerDonnees(GestionEmploye ge, GestionFacture gf)
        {

            if (File.Exists("Files/Employes.json"))
            {
                using StreamReader sr1 = new StreamReader("Files/Employes.json");
                {
                    ge.ListeEmployes = JsonSerializer.Deserialize(sr1.ReadToEnd(), typeof(List<Employe>)) as List<Employe>;
                }
            }
            else
            {
                ge.ListeEmployes = new List<Employe>();
            }
            if (File.Exists("Files/Factures.csv"))
            {
                gf.ListeFactures = ChargerListeFacture("Files/Factures.csv");
            }
            else
            {
                gf.ListeFactures = new List<Facture>();
            }
            if (File.Exists("Files/Quantites.json"))
            {
                using StreamReader sr3 = new StreamReader("Files/Quantites.json");
                {
                    return JsonSerializer.Deserialize(sr3.ReadToEnd(), typeof(Dictionary<TypeLegume, int>)) as Dictionary<TypeLegume, int>;
                }
            }
            
            return new Dictionary<TypeLegume, int>() 
            {
                {TypeLegume.Avocat , 0},
                {TypeLegume.Jalapeno , 0},
                {TypeLegume.Mais , 0},
                {TypeLegume.Oignon , 0},
                {TypeLegume.OignonF , 0},
                {TypeLegume.Olive , 0},
                {TypeLegume.Poivron , 0},
                {TypeLegume.Riz , 0},
                {TypeLegume.Salade , 0},
                {TypeLegume.Tomate , 0}
            };
            


        }



        public static void EnregistrerDonnees(string pCheminFichier, string pDonneesSerialises, bool pAjouterALaSuite)
        {

            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, pAjouterALaSuite);
            fluxEcriture.Write(pDonneesSerialises);
            fluxEcriture.Close();

        }



        #endregion

    }
}
