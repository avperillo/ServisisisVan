using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using Travel.Domain.AggregatesModel.CollectionAggregate;
using Travel.Domain.AggregatesModel.TravelerAggregate;
using Travel.Domain.Exceptions;
using Travel.DomainFake.AggregateTraveler;
using Travel.DomainFake.CollectionAggregate;
using Travel.Infrastructure.Services;

namespace Travel.DomainTest.Services
{
    [TestFixture]
    public class CollectionServiceUnitTest
    {
        private ICollectionService sut;

        private Mock<ICollectionRepository> mockCollectionRepository;
        private Mock<ITravelerRepository> mockTravelerRepository;
        
        [SetUp]
        public void SetupTest()
        {
            mockCollectionRepository = new Mock<ICollectionRepository>();
            mockTravelerRepository = new Mock<ITravelerRepository>();

            sut = new CollectionService(mockCollectionRepository.Object, mockTravelerRepository.Object);
        }

        [Test]
        public void GIVEN_close_collection_WHEN_close_THEN_obtain_exception()
        {
            Collection collection = CollectionFake.GetEmptyCollection();
            collection.CloseCollection();

            mockCollectionRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(collection);

            Assert.That(() => sut.CloseCollection(1), Throws.ArgumentException);
        }

        [Test]
        public void GIVEN_collection_not_travelers_pay_WHEN_close_THEN_obtain_exception()
        {
            Collection collection = CollectionFake.GetEmptyCollection();
            collection.AddEntry(1, DateTime.Now, 50M);
            collection.AddEntry(2, DateTime.Now, 50M);

            mockTravelerRepository.Setup(x => x.GetAllAsync()).ReturnsAsync(TravelerFake.GetTravelers(3));
            mockCollectionRepository.Setup(x => x.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(collection);

            Assert.That(() => sut.CloseCollection(1), Throws.Exception.TypeOf<AreMissingTravelersToPayException>());
        }

    }
}
