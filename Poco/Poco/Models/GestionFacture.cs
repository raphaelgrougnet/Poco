using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class GestionFacture
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private List<Facture> _ListeFactures;
        
        #endregion

        #region PROPRIÉTÉS
        public List<Facture> ListeFactures
        {
            get { return _ListeFactures; }
            set { _ListeFactures = value; }
        }


        #endregion

        #region CONSTRUCTEURS
        public GestionFacture(List<Facture> listeFactures)
        {
            ListeFactures = listeFactures;
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Ajoute une facture à la liste de factures
        /// </summary>
        /// <param name="pFacture">Facture à ajouter</param>
        public void AjouterFacture(Facture pFacture)
        {
            ListeFactures.Add(pFacture);
        }

        /// <summary>
        /// Calcule le total de toutes les factures
        /// </summary>
        /// <returns>Le total de toutes les factures avec les taxes</returns>
        public decimal CalculerTotalFactures()
        {
            decimal total = 0;
            foreach (Facture facture in ListeFactures)
            {
                total += facture.PrixTotal;
            }
            return total;
        }
        #endregion

    }
}
