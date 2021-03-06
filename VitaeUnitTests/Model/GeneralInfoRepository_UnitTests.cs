﻿using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Vitae.Model;
using Moq;
using Vitae.Services;
using System.Linq;

namespace VitaeUnitTests
{
    [TestClass]
    public class GeneralInfoRepository_UnitTests
    {
        [TestMethod]
        public void GeneralInfoRepository_ImplementsTheRightInterfaces() 
        {
            var mock = new Mock<IGeneralInfoXMLService>();
            var repos = new GeneralInfoRepository(mock.Object);

            Assert.IsTrue(repos is IGeneralInfoRepository);
            Assert.IsTrue(repos is IRepository<IGeneralInfoEntity>);
        }

        [TestMethod]
        public void GeneralInfoRepository_Add_Works() 
        {
            var originalGuid = Guid.NewGuid();
            var entity = new GeneralInfoEntity();
            entity.FullName = "test";
            var mock = new Mock<IGeneralInfoXMLService>();
            mock.Setup(T => T.Insert(entity)).Returns(originalGuid);
            var repos = new GeneralInfoRepository(mock.Object);

            var returnedGuid = repos.Add(entity);

            Assert.IsNotNull(returnedGuid);
            Assert.AreEqual(originalGuid, returnedGuid);
        }

        [TestMethod]
        public void GeneralInfoRepository_Get_Works() 
        {
            Guid guid = Guid.NewGuid();
            var originalItem = new GeneralInfoEntity();
            originalItem.FullName = "test";
            var mock = new Mock<IGeneralInfoXMLService>();
            mock.Setup(T => T.Get(guid)).Returns(originalItem);
            var repos = new GeneralInfoRepository(mock.Object);

            var item = repos.Get(guid);

            Assert.IsNotNull(item);
            Assert.AreEqual(originalItem.FullName, item.FullName);
        }

        [TestMethod]
        public void GeneralInfoRepository_GetAll_Works() 
        {
            GeneralInfoEntity gie = new GeneralInfoEntity
            {
                FullName = "caramel"
            };
            var mock = new Mock<IGeneralInfoXMLService>();
            mock.Setup(T => T.GetAll()).Returns(new List<IGeneralInfoEntity> { gie });

            var repos = new GeneralInfoRepository(mock.Object);

            var list = repos.GetAll();

            Assert.IsNotNull(list);
            Assert.AreEqual(1, list.Count);
            Assert.IsTrue(list.All(T => T.FullName == "caramel"));
        }

        [TestMethod]
        public void GeneralInfoRepository_Remove_Works() 
        {
            var guid = Guid.NewGuid();

            var mock = new Mock<IGeneralInfoXMLService>();
            mock.Setup(T => T.Delete(guid));
            var repos = new GeneralInfoRepository(mock.Object);

            repos.Remove(guid);

            Assert.IsTrue(true);
        }

        [TestMethod]
        public void GeneralInfoRepository_Update_Works() 
        {
            var guid = Guid.NewGuid();
            var gie = new GeneralInfoEntity();
            gie.FullName = "test";
            var mock = new Mock<IGeneralInfoXMLService>();
            mock.Setup(T => T.Update(guid, gie));
            var repos = new GeneralInfoRepository(mock.Object);

            repos.Update(guid, gie);

            Assert.IsTrue(true);
        }

    }
}
