using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poco.Models
{
    public enum TypeLegume
    {
        Avocat,
        Jalapeno,
        Mais,
        Oignon,
        OignonF,
        Olive,
        Poivron,
        Riz,
        Salade,
        Tomate
    }

    [Serializable]
    public class Legume : Garniture
    {

        #region CONSTANTES

        #endregion

        #region ATTRIBUTS

        #endregion

        #region PROPRIÉTÉS

        #endregion

        #region CONSTRUCTEURS
        [JsonConstructor]
        public Legume(string Nom) : base(Nom) {}

        #endregion

        #region MÉTHODES

        #endregion

    }
}
