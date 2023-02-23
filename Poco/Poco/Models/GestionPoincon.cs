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
        public void PoinconnerEmploye(Employe pEmploye)
        {
            pEmploye.MesPoincons.Add(new Poincon(eTypePoincon.Entree));
        }

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
