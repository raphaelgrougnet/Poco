﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
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

        static public List<Facture> ChargerListeFacture(String pCheminFichier)
        {
            if (File.Exists(pCheminFichier))
            {
                StreamReader fluxLecture = new StreamReader(pCheminFichier);
                String fichierTexte = fluxLecture.ReadToEnd();
                fluxLecture.Close();
                fichierTexte = fichierTexte.Replace("\r", "");

                // Création d'un vecteur de chaînes de caractères contenant chaque ligne individuellement.
                String[] vectLignes = fichierTexte.Split('\n');


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
                        decimal stt = decimal.Parse(ligne[1]);
                        decimal tt = decimal.Parse(ligne[2]);
                        ListFactures.Add(new Facture(noFacture, DateTime.Today, stt, tt ));
                    }

                    return ListFactures;
                }

               
            }
            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
            fluxEcriture.Write("NoFacture;SousTotal;Total\n");
            fluxEcriture.Close();
            return null;

        }

        static public List<Employe> ChargerListeEmployes(String pCheminFichier)
        {
            if (File.Exists(pCheminFichier))
            {
                StreamReader fluxLecture = new StreamReader(pCheminFichier);
                String fichierTexte = fluxLecture.ReadToEnd();
                fluxLecture.Close();
                fichierTexte = fichierTexte.Replace("\r", "");

                // Création d'un vecteur de chaînes de caractères contenant chaque ligne individuellement.
                String[] vectLignes = fichierTexte.Split('\n');


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

                    List<Employe> ListEmployes = new List<Employe>();

                    foreach (string[] ligne in ListLignes)
                    {
                        string code = ligne[0];
                        string nom = ligne[1];
                        string prenom = ligne[2];
                        DateTime dob = DateTime.Parse(ligne[3], FormPrincipal.cultureinfo);

                        ListEmployes.Add(new Employe(code, nom, prenom, dob));
                    }

                    return ListEmployes;

                }
            }
            
            //Création du fichier
            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
            fluxEcriture.Write("Code;Nom;Prenom;DOB\n");
            fluxEcriture.Close();
            return null;



        }

        public static void EnregistrerDonneesAppend(String pCheminFichier, string pDonneesSerialises)
        {

            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, true);
            fluxEcriture.Write(pDonneesSerialises+'\n');
            fluxEcriture.Close();

        }

        public static void EnregistrerDonneesCrush(String pCheminFichier, string pDonneesSerialises)
        {

            StreamWriter fluxEcriture = new StreamWriter(pCheminFichier, false);
            fluxEcriture.Write(pDonneesSerialises);
            fluxEcriture.Close();

        }

        #endregion

    }
}
