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
            list = Utils.ChargerDonnees("C:\\Users\\rapha\\Desktop\\Poco Projet Suicide\\poco\\Poco\\Poco\\Files\\Employes.csv");
            string env = Environment.CurrentDirectory;
            string path = Directory.GetParent(env).Parent.Parent.Parent.FullName+"\\Poco\\Files\\Employes.csv";
            list = Utils.ChargerDonnees(path);
            Plat plat = new Plat(TypePlat.Burrito);
            
        }
    }
}