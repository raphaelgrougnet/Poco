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

    public class Plat
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
        /// <returns></returns>
        public bool AjouterGarniture(Garniture pGarniture)
        {
            if (pGarniture is Garniture)
            {
                ListeGarniture.Add(pGarniture);
                return true;
            }
            return false;
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
}
