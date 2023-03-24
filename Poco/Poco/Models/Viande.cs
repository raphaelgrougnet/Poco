using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class Viande : Garniture
    {

        #region CONSTANTES

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
        public Viande(string pNom, decimal pPrix) : base(pNom)
        {
            Prix = pPrix;
        }
        #endregion

        #region MÉTHODES

       

        #endregion

    }
}
