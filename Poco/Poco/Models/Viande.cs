using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private Dictionary<string, decimal> DictViandePrix = new Dictionary<string, decimal>()
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
        public Viande(string pNom) : base(pNom)
        {
            
            Prix = DictViandePrix[pNom];
        }
        #endregion

        #region MÉTHODES

       

        #endregion

    }
}
