using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Poco.Models
{
    public enum TypePlat
    {
        Burrito,
        Fajitas,
        Nachos,
        Quesadilla,
        Tacos
    }

    public class Plat : IQuantite
    {
        
        #region CONSTANTES
        private Dictionary<TypePlat, decimal> DictPlatPrix = new Dictionary<TypePlat, decimal>()
        {
            {TypePlat.Burrito, 5.99m},
            {TypePlat.Fajitas, 6.99m},
            {TypePlat.Nachos, 4.99m},
            {TypePlat.Quesadilla, 5.99m},
            {TypePlat.Tacos, 5.99m}
        };

        
        

        #endregion

        #region ATTRIBUTS
        private TypePlat _tPlat;
        private string _nom;
        private List<Garniture> _listeGarniture;
        private decimal _prix;
        private Viande _viandeP;
        private ushort _quantite;
        #endregion

        #region PROPRIÉTÉS
        public TypePlat TPlat
        {
            get { return _tPlat; }
            set { _tPlat = value; }
        }

        public string Nom
        {
            get { return _nom; }
            set { _nom = value; }
        }

        public List<Garniture> ListeGarniture
        {
            get { return _listeGarniture; }
            set { _listeGarniture = value; }
        }

        public decimal Prix
        {
            get { return _prix; }
            set { _prix = value; }
        }

        public Viande ViandeP
        {
            get { return _viandeP; }
            set { _viandeP = value; }
        }

        public ushort Quantite
        {
            get { return _quantite; }
            set 
            { 
                if(value < 0)
                    throw new QuantitePlatPlusPetitQueZeroException(Nom);
                _quantite = value;
            }
        }
        #endregion

        #region CONSTRUCTEURS
        public Plat(TypePlat pTPlat)
        {
            TPlat = pTPlat;
            Nom = pTPlat.ToString();
            ListeGarniture = new List<Garniture>();
            Prix = DictPlatPrix[pTPlat];

            
        }

        #endregion

        #region MÉTHODES
        /// <summary>
        /// Ajoute une garniture au plat
        /// </summary>
        /// <param name="pGarniture">Garniture à ajouter</param>
        /// <returns>True si la garniture a bien été ajoutée sinon retourne faux</returns>
        public bool AjouterGarniture(Garniture pGarniture)
        {
            if (pGarniture is Garniture)
            {
                if (pGarniture is Viande)
                {
                    ViandeP = ((Viande)pGarniture);
                    Prix += Viande.DictViandePrix[ViandeP.Nom];
                    return true;
                }
                ListeGarniture.Add(pGarniture);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Retirer une garniture du plat
        /// </summary>
        /// <param name="pGarniture">Une garniture</param>
        /// <returns>True si la garniture a bien été retirée sinon retourne faux</returns>
        public bool RetirerGarniture(Garniture pGarniture)
        {
            Garniture g = null;
            if (pGarniture is Garniture)
            {
                foreach (Legume garniture in ListeGarniture)
                {
                    if (garniture.Nom == pGarniture.Nom)
                    {
                        g = garniture;
                        
                        
                    }
                }
                ListeGarniture.Remove(g);
                return true;
            }
            return false;
        }

        /// <summary>
        /// Ajouter une quantité aux nombres de plat
        /// </summary>
        /// <param name="valeur">Valeur a ajoutée</param>
        public void AjouterQuantite(ushort valeur)
        {
            Quantite += valeur;
        }

        /// <summary>
        /// Retirer une quantité aux nombres de plats
        /// </summary>
        /// <param name="valeur">Valeur à retiter</param>
        /// <exception cref="QuantitePlatPlusPetitQueZeroException">Lancé si le résultat de la quantité du plat est plus petit que zéro</exception>
        public void RetirerQuantite(ushort valeur)
        {
            if((Quantite -= valeur) >= 0)
                Quantite -= valeur;
            else
                throw new QuantitePlatPlusPetitQueZeroException(Nom);
          
        }

        /// <summary>
        /// Override de la méthode ToString
        /// </summary>
        /// <returns>Nom du plat</returns>
        public override string ToString()
        {
            return Nom;
        }
        #endregion

    }

    /// <summary>
    /// Classe d'exception pour la quantité
    /// </summary>
    public class QuantitePlatPlusPetitQueZeroException : Exception
    {
        /// <summary>
        /// Exception lancé lorsque la quantité est plus petite que 0
        /// </summary>
        public QuantitePlatPlusPetitQueZeroException(string nom) : base($"La quantité du plat ({nom}) est insuffisante ") { }
        
    }
}
