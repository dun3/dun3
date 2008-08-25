using System;
using System.Linq;
using NUnit.Framework;
using NUnit.Framework.SyntaxHelpers;
using Rhino.Mocks;
using LogViewerTest.Service.Log.Mapper;
using LogViewerTest.Service.Log.DTO;
using System.Collections.Generic;

namespace LogViewerTest.Service.Log.Test
{
    [TestFixture]
    public class LogServiceTest
    {
        private LogService m_service;

        [SetUp]
        public void SetUp()
        {
            m_service = new LogService();
        }

        [TearDown]
        public void TearDown()
        {
            m_service = null;
        }

        [Test]
        public void FindLastLogIdTest()
        {
            ILogMapper stubbedLogMapper = MockRepository.GenerateStub<ILogMapper>();
            stubbedLogMapper.Stub(x => x.FindLastLogId()).Return(1).Repeat.Any();

            m_service.SetMapper(stubbedLogMapper);

            ILogId id = m_service.FindLastLogId();
            LogIdDTO serviceId = ((ILogServiceContract)m_service).FindLastLogId();

            Assert.That(id.Id, Is.EqualTo(1));
            Assert.That(serviceId.Id, Is.EqualTo(1));
        }

        [Test]
        public void FindLastLogIdByIncidentIdTest()
        {
            Guid incident1Guid = Guid.NewGuid();
            Guid incident2Guid = Guid.NewGuid();

            ILogMapper stubbedLogMapper = MockRepository.GenerateStub<ILogMapper>();
            stubbedLogMapper.Stub(x => x.FindLastLogIdByIncidentId(incident1Guid)).Return(1).Repeat.Any();
            stubbedLogMapper.Stub(x => x.FindLastLogIdByIncidentId(incident2Guid)).Return(2).Repeat.Any();

            m_service.SetMapper(stubbedLogMapper);

            ILogId id1 = m_service.FindLastLogIdByIncidentId(incident1Guid);
            ILogId id2 = m_service.FindLastLogIdByIncidentId(incident2Guid);

            LogIdDTO serviceId1 = ((ILogServiceContract)m_service).FindLastLogIdByIncidentId(incident1Guid);
            LogIdDTO serviceId2 = ((ILogServiceContract)m_service).FindLastLogIdByIncidentId(incident2Guid);

            Assert.That(id1.Id, Is.EqualTo(1));
            Assert.That(id2.Id, Is.EqualTo(2));
            Assert.That(serviceId1.Id, Is.EqualTo(1));
            Assert.That(serviceId2.Id, Is.EqualTo(2));
        }

        [Test]
        public void FindLogItem()
        {
            long id = 1;

            LogItem item = new LogItem();
            item.Id = id;
            item.Category = Guid.NewGuid();
            item.Event = Guid.NewGuid();
            item.Message = "message";
            item.Severity = Guid.NewGuid();
            item.Title = "title";

            ILogMapper stubbedLogMapper = MockRepository.GenerateStub<ILogMapper>();
            stubbedLogMapper.Stub(x => x.FindLogItem(id)).Return(item).Repeat.Any();

            m_service.SetMapper(stubbedLogMapper);

            ILogItem result = m_service.FindLogItem(new LogItem() { Id = id });
            LogItemDTO serviceResult = ((ILogServiceContract)m_service).FindLogItem(new LogIdDTO() { Id = id });

            Assert.That(result.Id, Is.EqualTo(item.Id));
            Assert.That(result.Category, Is.EqualTo(item.Category));
            Assert.That(result.Event, Is.EqualTo(item.Event));
            Assert.That(result.Message, Is.EqualTo(item.Message));
            Assert.That(result.Severity, Is.EqualTo(item.Severity));
            Assert.That(result.Title, Is.EqualTo(item.Title));

            Assert.That(serviceResult.Id, Is.EqualTo(item.Id));
            Assert.That(serviceResult.Category, Is.EqualTo(item.Category));
            Assert.That(serviceResult.Event, Is.EqualTo(item.Event));
            Assert.That(serviceResult.Message, Is.EqualTo(item.Message));
            Assert.That(serviceResult.Severity, Is.EqualTo(item.Severity));
            Assert.That(serviceResult.Title, Is.EqualTo(item.Title));
        }

        [Test]
        public void TestFakeMapper()
        {
            Guid incidentId = Guid.NewGuid();
            IList<ILogItem> itemzFirst = m_service.FindLogItemzByIncidentId(incidentId);
            IList<ILogItem> itemzSecond = m_service.FindLogItemzByIncidentId(incidentId);

            Assert.That(itemzFirst, Is.SubsetOf(itemzSecond.ToList()));
            Assert.That(itemzSecond, Has.All.Property("Incident").EqualTo(incidentId));

            Assert.That(itemzFirst.Count, Is.LessThanOrEqualTo(itemzSecond.Count));
        }
    }
}
