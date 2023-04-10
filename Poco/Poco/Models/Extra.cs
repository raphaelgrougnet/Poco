using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class Extra
    {
        public enum TypeExtra
        {
            Fromage,
            Viande
        }


        #region CONSTANTES
        public Dictionary<TypeExtra, decimal> DictExtraPrix = new Dictionary<TypeExtra, decimal>()
        {
            {TypeExtra.Fromage, 0.99m},
            {TypeExtra.Viande, 2.99m}
        };
        #endregion

        #region ATTRIBUTS
        private decimal _prix;
        private string _nom;
        #endregion

        #region PROPRIÉTÉS
        public decimal Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        #endregion

        #region CONSTRUCTEURS

        public Extra(TypeExtra pTypeExtra)
        {
            Prix = DictExtraPrix[pTypeExtra];
            Nom = pTypeExtra.ToString();
        }

        #endregion

        #region MÉTHODES

        #endregion

    }
}
