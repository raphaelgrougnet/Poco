using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public class GestionPoincon
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS
        private GestionEmploye _gestionEmployes;

        #endregion

        #region PROPRIÉTÉS

        #endregion

        #region CONSTRUCTEURS
        public GestionPoincon(GestionEmploye pGestionEmployes)
        {
            _gestionEmployes = pGestionEmployes;
        }
        #endregion

        #region MÉTHODES
        /// <summary>
        /// Poinçonne un employé
        /// </summary>
        /// <param name="pEmploye">Employé à poinconner</param>
        public void PoinconnerEmploye(Employe pEmploye)
        {
            pEmploye.MesPoincons.Add(new Poincon(eTypePoincon.Entree));
        }

        /// <summary>
        /// Dépoinçonne un employé
        /// </summary>
        /// <param name="pEmploye">Employé à dépoinconner</param>
        public void DepoinconnerEmploye(Employe pEmploye)
        {
            pEmploye.MesPoincons.Add(new Poincon(eTypePoincon.Sortie));
        }

        public TimeSpan CalculerTempsTotal()
        {
            TimeSpan tempsTotal = TimeSpan.Zero;
            foreach (Employe pEmployes in _gestionEmployes.ListeEmployes)
            {
                tempsTotal += pEmployes.HeuresTravailles;
            }

            return tempsTotal;
        }
        #endregion

    }
}
