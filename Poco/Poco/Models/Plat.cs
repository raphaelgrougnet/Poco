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
        Fajita,
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
            {TypePlat.Fajita, 6.99m},
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
        public bool AjouterGarniture(Garniture pGarniture)
        {
            if (pGarniture is Garniture)
            {
                ListeGarniture.Add(pGarniture);
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return Nom;
        }
        #endregion

    }
}
