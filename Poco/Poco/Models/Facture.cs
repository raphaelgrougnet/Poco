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
                return _sousTotal;
            }
            set { _sousTotal = value; }
        }

        public decimal PrixTotal
        {
            get 
            {
                return _prixTotal;
                
            }
            set { _prixTotal = value; }
        }

        

        #endregion

        #region CONSTRUCTEURS
        public Facture(uint noFacture, DateTime date, decimal sousTotal, decimal prixTotal)
        {
            NoFacture = noFacture;
            Date = date;
            ListePlats = new List<Plat>();
            SousTotal = sousTotal;
            PrixTotal = prixTotal;
        }
        public Facture(uint noFacture)
        {
            NoFacture = noFacture;
            Date = DateTime.Today;
            ListePlats = new List<Plat>();
            SousTotal = 0;
            PrixTotal = 0;
        }

        #endregion

        #region MÉTHODES

        /// <summary>
        /// Calcule le sous-total de la facture
        /// </summary>
        /// <returns>Sous total sans les taxes</returns>
        public decimal CalculerSousTotal()
        {
            decimal sousTotal = 0;
            foreach (Plat plat in ListePlats)
            {
                sousTotal += plat.Prix;
                if (plat.ListeExtras.Count > 0)
                {
                    foreach (Extra extra in plat.ListeExtras)
                    {
                        sousTotal += extra.Prix;
                    }
                }
                
            }

            
            return sousTotal;
        }

        /// <summary>
        /// Calcule le prix total de la facture
        /// </summary>
        /// <returns>Total avec les taxes</returns>
        public decimal CalculerPrixTotal()
        {
            decimal prixTotal = SousTotal + (SousTotal * 0.15m);
            return prixTotal;
        }

        public override string ToString()
        {
            string z = "";
            for (int i = 0; i < (4 - NoFacture.ToString().Length); i++)
            {
                z += "0";
            }
            return "Facture #" + z + NoFacture;
        }
        #endregion



    }
}
