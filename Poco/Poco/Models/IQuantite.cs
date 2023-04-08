using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public interface IQuantite
    {
        ushort Quantite { get; set; }
        
        /// <summary>
        /// Ajouter une quantité
        /// </summary>
        /// <param name="valeur">La valeur a ajoutée</param>
        public void AjouterQuantite(ushort valeur);
        
        /// <summary>
        /// Retirer une quantité
        /// </summary>
        /// <param name="valeur">La valeur a retirée </param>
        public void RetirerQuantite(ushort valeur);
        
    }
}
