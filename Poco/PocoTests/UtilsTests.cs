using Poco.Models;
using System;
using System.IO;

namespace PocoTests
{
    public class UtilsTests
    {
        [Fact]
        public void Test1()
        {
            List<string[]> list = new List<string[]>();
            list = Utils.ChargerDonnees("C:\\Users\\rapha\\Desktop\\Poco Projet Suicide\\poco\\Poco\\Poco\\Files\\Factures.csv");
            string env = Environment.CurrentDirectory;
            string path = Directory.GetParent(env).Parent.Parent.FullName;
            Plat plat = new Plat(TypePlat.Burrito);
            
        }
    }
}