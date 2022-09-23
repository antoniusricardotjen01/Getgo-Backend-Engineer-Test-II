using Microsoft.VisualStudio.TestTools.UnitTesting;
using CarSharingBooking.Controllers;
using System;
using System.Collections.Generic;
using System.Text;
using CarSharingBooking.Repositories.BusinessService;
using Moq;
using CarSharingBooking.Repositories.DBContext;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using CarSharingBooking.DTOModels;

namespace CarSharingBooking.Controllers.Tests
{
    [TestClass()]
    public class CarBookingControllerTests
    {
        private Mock<ICarBusinessFacade> carBusinessFacade;

        [TestInitialize]
        public void SetupTests()
        {
            List<Car> result = new List<Car>()
            {
                new Car() {CarId = 1, CarStatus = 1, CarCoordinateX = 1, CarCoordinateY = 1},
                new Car() {CarId = 2, CarStatus = 1, CarCoordinateX = 10, CarCoordinateY = 25},
                new Car() {CarId = 3, CarStatus = 1, CarCoordinateX = 100, CarCoordinateY = 125}
            };
            carBusinessFacade = new Mock<ICarBusinessFacade>();
            carBusinessFacade.Setup(t => t.GetAllCarDetails()).Returns(result);
            carBusinessFacade.Setup(t => t.SearchCar(It.IsAny<int>(), It.IsAny<int>())).Returns(result);
            carBusinessFacade.Setup(t => t.BookCar(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>())).Returns(true);
            carBusinessFacade.Setup(t => t.ReachCar(It.IsAny<int>())).Returns(true);
        }

        [TestMethod()]
        public void GetCarDetailsSuccess()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.GetCarDetails();
            Assert.IsNotNull(car.Value);
        }

        [TestMethod()]
        public void SearchCarReturnsResult()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.SearchCar(new APIParameter.SearchCar() { CoordinateX = 1, CoordinateY = 1 });
            Assert.IsNotNull(car.Value);
        }

        [TestMethod()]
        public void SearchCarReturnsNoResult()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.SearchCar(new APIParameter.SearchCar() { CoordinateX = 130, CoordinateY = 120 });
            Assert.IsNotNull(car.Value);
        }

        [TestMethod()]
        public void BookCarSuccess()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.BookCar(new APIParameter.BookCar() { CoordinateX = 1, CoordinateY = 1 , Username = "user1" });
            Assert.IsTrue(car.Value);
        }

        [TestMethod()]
        public void BookCarFail()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.BookCar(new APIParameter.BookCar() { CoordinateX = 1, CoordinateY = 1, Username = "user2" });
            Assert.IsTrue(!car.Value);
        }

        [TestMethod()]
        public void ReachCarSuccess()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.ReachCar(new APIParameter.ReachCar() { CarID = 1 });
            Assert.IsTrue(car.Value);
        }

        [TestMethod()]
        public void ReachCarFail()
        {
            CarBookingController controller = new CarBookingController(carBusinessFacade.Object);
            var car = controller.ReachCar(new APIParameter.ReachCar() { CarID = 2 });
            Assert.IsTrue(!car.Value);
        }
    }
}