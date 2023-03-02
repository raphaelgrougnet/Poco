using System;
using System.Collections.Generic;
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
        #endregion

        #region ATTRIBUTS
        private List<Employe> _listeEmployes;

        #endregion

        #region PROPRIÉTÉS
        public List<Employe> ListeEmployes
        {
            get { return _listeEmployes; }
            set { _listeEmployes = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        public GestionEmploye()
        {
            ListeEmployes = new List<Employe>();
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Ajoute un employé à la liste
        /// </summary>
        /// <param name="employe">Employé à ajouter</param>
        public void AjouterEmploye(Employe employe)
        {
            ListeEmployes.Add(employe);
        }

        /// <summary>
        /// Supprime un employé de la liste
        /// </summary>
        /// <param name="employe">Employé à supprimer</param>
        public void SupprimerEmploye(Employe employe)
        {
            ListeEmployes.Remove(employe);
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
                message += "Le code doit contenir 4 chiffres\n";
            }
            if (pNom == null || pNom == "")
            {
                message += "Le nom de famille ne peut pas être vide\n";
            }
            if (pPrenom == null || pPrenom == "")
            {
                message += "Le prénom ne peut pas être vide\n";
            }
            if (pDateNaissance < DateTime.Now.AddYears(-100))
            {
                message += "La date de naissance est invalide";
            }

            return message;
        }

        #endregion

    }
}
