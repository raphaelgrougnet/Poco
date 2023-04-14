using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poco.Models
{
    [Serializable]
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
        
        public Extra(TypeExtra TypeExtra)
        {
            Prix = DictExtraPrix[TypeExtra];
            Nom = TypeExtra.ToString();
        }

        
        public Extra()
        {
        }

        #endregion

        #region MÉTHODES

        #endregion

    }
}
