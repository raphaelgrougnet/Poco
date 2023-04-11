using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class Viande : Garniture
    {
        public enum TypeViande
        {
            Boeuf,
            Dinde,
            Poisson,
            Porc,
            Poulet,
            Végé
        }

        #region CONSTANTES
        public static Dictionary<string, decimal> DictViandePrix = new Dictionary<string, decimal>()
        {
            {"Boeuf", 1.99m},
            {"Dinde", 2.99m},
            {"Poisson", 3.99m},
            {"Porc", 1.50m},
            {"Poulet", 2.83m},
            {"Vege", 1m}
        };
        #endregion

        #region ATTRIBUTS
        private decimal _prix;

        #endregion

        #region PROPRIÉTÉS
        public decimal Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        
        public Viande(string Nom) : base(Nom)
        {
            
            Prix = DictViandePrix[Nom];
        }

        public Viande() { }
        #endregion

        #region MÉTHODES

        public override string ToString()
        {
            return Nom;
        }

        #endregion

    }
}
