using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public enum eTypePoincon
    {
        Entree,
        Sortie
    }
    public class Poincon
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private eTypePoincon _typePoincon;

        private TimeSpan _heure;

        #endregion

        #region PROPRIÉTÉS
        public eTypePoincon TypePoincon
        {
            get { return _typePoincon; }
            set { _typePoincon = value; }
        }

        public TimeSpan Heure
        {
            get { return _heure; }
            set { _heure = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        public Poincon(eTypePoincon typePoincon)
        {
            TypePoincon = typePoincon;
            Heure = DateTime.Now.TimeOfDay;
        }
        #endregion

        #region MÉTHODES
        public override string ToString()
        {
            return Heure.ToString();
        }
        #endregion

    }
}
