using Poco.Models;

namespace PocoTests
{
    public class UtilsTests
    {
        [Fact]
        public void Test1()
        {
            List<string[]> list = new List<string[]>();
            list = Utils.ChargerDonnees("C:\\Users\\rapha\\Desktop\\Poco Projet Suicide\\poco\\Poco\\Poco\\Files\\Factures.csv");

            Plat plat = new Plat(TypePlat.Burrito);
            
        }
    }
}