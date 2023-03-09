using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Poco.Models
{
    /// <summary>
    /// Type de poinçon
    /// </summary>
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
        /// <summary>
        /// Le type de poinçon
        /// </summary>
        private eTypePoincon _typePoincon;

        /// <summary>
        /// L'heure du poinçon
        /// </summary>
        private TimeSpan _heure;

        #endregion

        #region PROPRIÉTÉS
        /// <summary>
        /// Le type du poinçon
        /// </summary>
        public eTypePoincon TypePoincon
        {
            get { return _typePoincon; }
            set 
            {
                if (value == eTypePoincon.Entree || value == eTypePoincon.Sortie)
                    _typePoincon = value;
                else
                    throw new EnumPoiconNonValideException("Un poinçon ne peut être que un poinçon Entrée ou un poinçon Sortie");
                
            }
        }

        /// <summary>
        /// L'heure du poinçon
        /// </summary>
        public TimeSpan Heure
        {
            get { return _heure; }
            set 
            {
                if(value >= TimeSpan.Zero)
                    _heure = value;
                else
                    throw new HeureNonValideException("L'heure ne peut pas être vide");

            }
        }


        #endregion

        #region CONSTRUCTEURS
        /// <summary>
        /// Constructeur de poinçon
        /// </summary>
        /// <param name="typePoincon">Le type du poinçon</param>
        public Poincon(eTypePoincon typePoincon)
        {
            TypePoincon = typePoincon;
            Heure = DateTime.Now.TimeOfDay;
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>L'heure du poincon</returns>
        public override string ToString()
        {
            return Heure.ToString("hh\\:mm");
        }

        
        #endregion

    }

    /// <summary>
    /// Exception Personnalisée HeureNonValideException
    /// </summary>
    public class HeureNonValideException : Exception
    {
        /// <summary>
        /// Exception lancé si l'heure est zéro
        /// </summary>
        /// <param name="message">Un message d'erreur</param>
        public HeureNonValideException(string message) : base("L'heure ne pas être zéro") { }
    }

    /// <summary>
    /// Exception Personnalisée EnumPoinconNonValideException
    /// </summary>
    public class EnumPoiconNonValideException : Exception
    {
        /// <summary>
        /// Exception lancé si un poinçon n'a pas la bonne valeur d'enum
        /// </summary>
        /// <param name="message">Un message d'erreur</param>
        public EnumPoiconNonValideException(string message) : base("Un poinçon ne peut être que un poinçon Entrée ou un poinçon Sortie") { }
    }
}
