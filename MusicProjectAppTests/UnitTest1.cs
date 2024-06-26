//using Microsoft.EntityFrameworkCore;
//using Microsoft.Extensions.Configuration;
//using MusicProjectApp.Models;


//namespace PruebaMVCTest
//{
//    [TestClass]
//    public class UnitTest1
//    {
//        public static IConfiguration InitConfiguration()
//        {
//            var config = new ConfigurationBuilder()
//                .AddJsonFile("appsettings.test.json")
//                .AddEnvironmentVariables()
//                .Build();
//            return config;
//        }

//        [TestMethod()]
//        public void TestContexto()
//        {
//            IConfiguration config = InitConfiguration();
//            GrupoAContext contexto = new(config);
//            Assert.AreEqual(contexto.Albumes.Count(),5);
//            Assert.IsNotNull(contexto);


//        }
       
//    }
//}