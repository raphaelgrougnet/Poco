using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class GestionEmploye
    {

        #region CONSTANTES
        static public string PATH_FILES = Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Files\\";
        CultureInfo ci = CultureInfo.InstalledUICulture;
        #endregion

        #region ATTRIBUTS
        private List<Employe> _listeEmployes;
        private List<Employe> _listeEmployesPresent;
        private Dictionary<string, Employe> _dictEmployesCodes;
        #endregion

        #region PROPRIÉTÉS
        public List<Employe> ListeEmployes
        {
            get { return _listeEmployes; }
            set { _listeEmployes = value; }
        }

        public List<Employe> ListeEmployesPresent
        {
            get { return _listeEmployesPresent; }
            set { _listeEmployesPresent = value; }
        }

        public Dictionary<string, Employe> DictEmployesCodes
        {
            get { return _dictEmployesCodes; }
            set { _dictEmployesCodes = value; }
        }

        #endregion

        #region CONSTRUCTEURS
        public GestionEmploye()
        {
            ListeEmployes = new List<Employe>();
            ListeEmployesPresent = new List<Employe>();
            DictEmployesCodes = new Dictionary<string, Employe>();
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Ajoute un employé à la liste
        /// </summary>
        /// <param name="employe">Employé à ajouter</param>
        public void AjouterEmploye(Employe employe)
        {
            DictEmployesCodes.Add(employe.Code, employe);
            ListeEmployes.Add(employe);
        }

        /// <summary>
        /// Supprime un employé de la liste
        /// </summary>
        /// <param name="employe">Employé à supprimer</param>
        public void SupprimerEmploye(Employe employe)
        {
            ListeEmployes.Remove(employe);
            if (ListeEmployesPresent.Contains(employe))
            {
                ListeEmployesPresent.Remove(employe);
            }
            DictEmployesCodes.Remove(employe.Code);
        }

        /// <summary>
        /// Valide les données d'un employé
        /// </summary>
        /// <param name="pCode">Code de l'employé</param>
        /// <param name="pNom">Nom de l'employé</param>
        /// <param name="pPrenom">Prénom de l'employé</param>
        /// <param name="pDateNaissance">Date de naissance de l'employé</param>
        /// <returns></returns>
        public string ValiderEmploye(string pCode, string pNom, string pPrenom, DateTime pDateNaissance)
        {
            string message = "";

            if (pCode == null || pCode == "" || pCode.Length != 4)
            {
                message += "Le code doit contenir 4 chiffres.\n";
            }
            if (pNom == null || pNom == "")
            {
                message += $"Le nom de famille ne peut pas être vide et doit contenir entre {Employe.LONGUEUR_MIN_NOM} et {Employe.LONGUEUR_MAX_NOM} caractères.\n";
            }
            if (pPrenom == null || pPrenom == "" || pPrenom.Length < Employe.LONGUEUR_MIN_PRENOM || pPrenom.Length > Employe.LONGUEUR_MAX_PRENOM)
            {
                message += $"Le prénom ne peut pas être vide et doit contenir entre {Employe.LONGUEUR_MIN_PRENOM} et {Employe.LONGUEUR_MAX_PRENOM} caractères.\n";
            }
            if (pDateNaissance < DateTime.Now.AddYears(-100) || pDateNaissance > DateTime.Now)
            {
                message += "La date de naissance est invalide.";
            }
            if (_dictEmployesCodes.ContainsKey(pCode))
            {
                message += "Le code de l'employé existe déjà.";
            }

            return message;
        }

        #endregion

    }
}
