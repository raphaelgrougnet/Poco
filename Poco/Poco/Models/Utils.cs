using System;
using System.Collections.Generic;
using System.Diagnostics;
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



        static public List<string[]> ChargerDonnees(String pCheminFichier)
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

                    return ListLignes;

                }
            }

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
