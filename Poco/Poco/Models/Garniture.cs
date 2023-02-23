using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public abstract class Garniture
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private string _nom;

        #endregion
        
        #region PROPRIÉTÉS
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }
        #endregion

        #region CONSTRUCTEURS
        public Garniture(string pNom)
        {
            Nom = pNom;
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>Nom de la garniture</returns>
        public override string ToString()
        {
            return Nom;
        }
        #endregion

    }
}
