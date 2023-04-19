using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poco.Models
{
    

    [Serializable]
    public abstract class Garniture : IQuantite
    {

        #region CONSTANTES
        
        #endregion

        #region ATTRIBUTS
        private string _nom;
        private ushort _quantite;

        #endregion

        #region PROPRIÉTÉS
        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public ushort Quantite
        {
            get { return _quantite; }
            set
            {
                if (value < 0)
                    throw new QantiteGarniturePlusPetitQueZeroException(Nom);
                _quantite = value;
            }
        }
        #endregion

        #region CONSTRUCTEURS

        public Garniture(string Nom)
        {
            this.Nom = Nom;
        }
         
        
        public Garniture() { }
        #endregion

        #region MÉTHODES

        /// <summary>
        /// Ajouter une quantité aux nombres de garnitures
        /// </summary>
        /// <param name="valeur">Valeur a ajoutée</param>
        public void AjouterQuantite(ushort valeur)
        {
            Quantite += valeur;
        }

        /// <summary>
        /// Retirer une quantité aux nombres de garnitures
        /// </summary>
        /// <param name="valeur">Valeur a retirée</param>
        /// <exception cref="QantiteGarniturePlusPetitQueZeroException"></exception>
        public void RetirerQuantite(ushort valeur)
        {
            if ((Quantite -= valeur) >= 0)
                Quantite -= valeur;
            else
                throw new QantiteGarniturePlusPetitQueZeroException(Nom);
        }

        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>Nom de la garniture</returns>
        public override string ToString()
        {
            return Nom[0].ToString();
        }
        #endregion

    }

    /// <summary>
    /// Class de l'exception QantiteGarniturePlusPetitQueZeroException
    /// </summary>
    public class QantiteGarniturePlusPetitQueZeroException : Exception
    {
        /// <summary>
        /// Exception lancé si la quantité de la garniture est inférieur à 0
        /// </summary>
        /// <param name="nom">Nom de la garniture</param>
        public QantiteGarniturePlusPetitQueZeroException(string nom) : base($"La quantité de la garniture ({nom}) est insuffisante") { }
    }
}
