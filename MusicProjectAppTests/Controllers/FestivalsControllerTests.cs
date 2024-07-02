
﻿using Microsoft.Extensions.Configuration;

 namespace MusicProjectApp.Controllers.Tests
{
    [TestClass()]
    public class FestivalsControllerTests
    {
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .AddEnvironmentVariables()
                .Build();
            return config;
        }
        [TestMethod()]
        public void IndexTest()
        {

        }

        [TestMethod()]
        public void DetailsTest()
        {

        }

        [TestMethod()]
        public void CreateTest()
        {

        }

        [TestMethod()]
        public void CreateTest1()
        {

        }

        [TestMethod()]
        public void EditTest()
        {

        }

        [TestMethod()]
        public void EditTest1()
        {

        }

        [TestMethod()]
        public void DeleteTest()
        {

        }

        [TestMethod()]
        public void DeleteConfirmedTest()
        {

        }
    }
}

