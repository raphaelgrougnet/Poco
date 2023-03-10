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

        public const int LONGUEUR_MIN_NOM = 2;
        public const int LONGUEUR_MAX_NOM = 20;
        public const int LONGUEUR_MIN_PRENOM = 2;
        public const int LONGUEUR_MAX_PRENOM = 20;
        

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
            set
            {
                if (value.Length != 4)
                {
                    throw new ArgumentOutOfRangeException("Le code ne doit comprendre 4 chiffres");
                }
                _code = value;
            }
        }
        public string Nom
        {
            get { return _nom; }
            set 
            {
                if (value.Length < LONGUEUR_MIN_NOM || value.Length > LONGUEUR_MAX_NOM)
                {
                    throw new ArgumentOutOfRangeException("Le nom doit comprendre entre " + LONGUEUR_MIN_NOM + " et " + LONGUEUR_MAX_NOM + " caractères");
                }
                _nom = value; 
            }
        }

        public string Prenom
        {
            get { return _prenom; }
            set
            {
                if (value.Length < LONGUEUR_MIN_PRENOM || value.Length > LONGUEUR_MAX_PRENOM)
                {
                    throw new ArgumentOutOfRangeException("Le nom doit comprendre entre " + LONGUEUR_MIN_PRENOM + " et " + LONGUEUR_MAX_PRENOM + " caractères");
                }
                _prenom = value;
            }
        }

        public DateTime DateNaissance
        {
            get { return _dateNaissance; }
            set
            {
                if (DateNaissance > DateTime.Now || !DateTime.TryParse(value.ToString(), out DateTime resultat))
                {
                    throw new ArgumentOutOfRangeException("La date doit être inférieur à la date d'ajourd'hui, et doit respecter le format MM/JJ/AAAA");
                }
                _dateNaissance = value;
            }
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
        public Employe(string code, string nom, string prenom, DateTime dateNaissance)
        {
            Code = code;
            Nom = nom;
            Prenom = prenom;
            DateNaissance = dateNaissance;
            HeuresTravailles = new TimeSpan(0, 0, 0);
            MesPoincons = new List<Poincon>();
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>Nom et Prénom de l'employé</returns>
        public override string ToString()
        {
            return Prenom + " " + Nom;
        }

        public override bool Equals(object obj)
        {
            if (obj is null)
            {
                return false;
            }
            if (obj is not Employe)
            {
                return false;
            }
            return obj is Employe employe &&
                   Code == employe.Code;
            
        }
        #endregion

    }
}
