using AutoMapper;
using DinkToPdf.Contracts;
using Invoicing.BusinessLogic.Interfaces;
using Invoicing.Core.Database.Entities;
using Invoicing.Web.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NUnit.Framework;
using System.Web.Http.Results;

namespace Invoicing.Web.Tests
{
    public class InvoicesControllerTests
    {
        private InvoicesController controller;
        private Mock<IInvoiceService> mockInvoiceService;
        private Mock<IMapper> mockIMapper;
        private Mock<IFileService> mockIFileService;
        private Mock<IUserService> mockIUserService;
        private Mock<IConverter> mockIConverter;

        [SetUp]
        public void Setup()
        {
           mockIConverter = new Mock<IConverter>();
           mockIUserService = new Mock<IUserService>();
           mockIMapper = new Mock<IMapper>();
           mockIFileService = new Mock<IFileService>();
           mockInvoiceService = new Mock<IInvoiceService>();
           controller = new InvoicesController(mockIMapper.Object, mockInvoiceService.Object, mockIFileService.Object, mockIUserService.Object, mockIConverter.Object);
        }

        [Test]
        public void Get_ReturnsJsonResult()
        {
            //Arrange
            mockInvoiceService.Setup(p => p.GetById(It.IsInRange(1, int.MaxValue, Range.Inclusive)))
                .Returns(new Invoice() {Id = 20 });

            //Act
            var result = controller.Get(20);

            //Assert
            Assert.IsInstanceOf<JsonResult>(result);
        }
    }
}