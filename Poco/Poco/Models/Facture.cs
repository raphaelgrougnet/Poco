using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class Facture
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private uint _noFacture;

        private DateTime _date;

        private List<Plat> _listePlats;

        private decimal _sousTotal;

        private decimal _prixTotal;

        


        #endregion

        #region PROPRIÉTÉS
        
        public uint NoFacture
        {
            get { return _noFacture; }
            set { _noFacture = value; }
        }

        public DateTime Date
        {
            get { return _date; }
            set { _date = value; }
        }

        public List<Plat> ListePlats
        {
            get { return _listePlats; }
            set { _listePlats = value; }
        }

        public decimal SousTotal
        {
            get
            {
                SousTotal = CalculerSousTotal();
                return _sousTotal;
            }
            private set { _sousTotal = value; }
        }

        public decimal PrixTotal
        {
            get 
            {
                PrixTotal = CalculerPrixTotal();
                return _prixTotal; 
            }
            private set { _prixTotal = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        public Facture(uint noFacture, DateTime date, List<Plat> listePlats, decimal sousTotal, decimal prixTotal)
        {
            NoFacture = noFacture;
            Date = date;
            ListePlats = listePlats;
            SousTotal = sousTotal;
            PrixTotal = prixTotal;
        }
        #endregion

        #region MÉTHODES

        /// <summary>
        /// Calcule le sous-total de la facture
        /// </summary>
        /// <returns>Sous total sans les taxes</returns>
        private decimal CalculerSousTotal()
        {
            decimal sousTotal = 0;
            foreach (Plat plat in ListePlats)
            {
                sousTotal += plat.Prix;
            }
            return sousTotal;
        }

        /// <summary>
        /// Calcule le prix total de la facture
        /// </summary>
        /// <returns>Total avec les taxes</returns>
        private decimal CalculerPrixTotal()
        {
            decimal prixTotal = SousTotal + (SousTotal * 0.15m);
            return prixTotal;
        }
        #endregion

    }
}
