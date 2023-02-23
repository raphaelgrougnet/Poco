using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class Employe
    {
        
        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private string _code;

        private string _nom;

        private string _prenom;

        private DateTime _dateNaissance;

        private TimeSpan _heuresTravailles;

        private List<Poincon> _mesPoincons;
        
        #endregion

        #region PROPRIÉTÉS
        public string Code
        {
            get { return _code; }
            set { _code = value; }
        }
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public string Prenom
        {
            get { return _prenom; }
            set { _prenom = value; }
        }

        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set { _dateNaissance = value; }
        }

        public TimeSpan HeuresTravailles
        {
            get { return _heuresTravailles; }
            set { _heuresTravailles = value; }
        }

        public List<Poincon> MesPoincons
        {
            get { return _mesPoincons; }
            set { _mesPoincons = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        public Employe(string code, string nom, string prenom, DateTime dateNaissance, TimeSpan heuresTravailles, List<Poincon> mesPoincons)
        {
            Code = code;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            HeuresTravailles = heuresTravailles;
            MesPoincons = mesPoincons;
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>Nom et Prénom de l'employé</returns>
        public override string ToString()
        {
            return Nom + " " + Prenom;
        }
        #endregion

    }
}
