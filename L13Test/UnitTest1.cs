using L13;
using CarsLibrary;
using MyCollectionLibrary;
namespace L13Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void CollectionHandlerEventArgs_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string expectedChangeType = "Added";
            object expectedChangedItem = new { Id = 1, Name = "Test Item" };

            // Act
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(expectedChangeType, expectedChangedItem);

            // Assert
            Assert.AreEqual(expectedChangeType, args.ChangeType);
            Assert.AreEqual(expectedChangedItem, args.ChangedItem);
        }

        [TestMethod]
        public void CollectionHandlerEventArgs_ChangeType_Property_ShouldGetAndSet()
        {
            // Arrange
            string initialChangeType = "Added";
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs(initialChangeType, new { Id = 1, Name = "Test Item" });
            string newChangeType = "Removed";

            // Act
            args.ChangeType = newChangeType;

            // Assert
            Assert.AreEqual(newChangeType, args.ChangeType);
        }

        [TestMethod]
        public void CollectionHandlerEventArgs_ChangedItem_Property_ShouldGetAndSet()
        {
            // Arrange
            object initialChangedItem = new { Id = 1, Name = "Test Item" };
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs("Added", initialChangedItem);
            object newChangedItem = new { Id = 2, Name = "New Test Item" };

            // Act
            args.ChangedItem = newChangedItem;

            // Assert
            Assert.AreEqual(newChangedItem, args.ChangedItem);
        }
        [TestMethod]
        public void JournalEntry_Constructor_ShouldInitializeProperties()
        {
            // Arrange
            string expectedCollectionName = "TestCollection";
            string expectedChangeType = "Added";
            string expectedObjectData = "TestData";

            // Act
            JournalEntry entry = new JournalEntry(expectedCollectionName, expectedChangeType, expectedObjectData);

            // Assert
            Assert.AreEqual(expectedCollectionName, entry.CollectionName);
            Assert.AreEqual(expectedChangeType, entry.ChangeType);
            Assert.AreEqual(expectedObjectData, entry.ObjectData);
        }

        [TestMethod]
        public void JournalEntry_CollectionName_Property_ShouldGetAndSet()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("InitialCollection", "Added", "TestData");
            string newCollectionName = "NewCollection";

            // Act
            entry.CollectionName = newCollectionName;

            // Assert
            Assert.AreEqual(newCollectionName, entry.CollectionName);
        }

        [TestMethod]
        public void JournalEntry_ChangeType_Property_ShouldGetAndSet()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("TestCollection", "Added", "TestData");
            string newChangeType = "Removed";

            // Act
            entry.ChangeType = newChangeType;

            // Assert
            Assert.AreEqual(newChangeType, entry.ChangeType);
        }

        [TestMethod]
        public void JournalEntry_ObjectData_Property_ShouldGetAndSet()
        {
            // Arrange
            JournalEntry entry = new JournalEntry("TestCollection", "Added", "InitialData");
            string newObjectData = "NewData";

            // Act
            entry.ObjectData = newObjectData;

            // Assert
            Assert.AreEqual(newObjectData, entry.ObjectData);
        }

        [TestMethod]
        public void JournalEntry_ToString_ShouldReturnFormattedString()
        {
            // Arrange
            string collectionName = "TestCollection";
            string changeType = "Added";
            string objectData = "TestData";
            string expectedString = $"Коллекция: {collectionName}. {changeType}. Элемент: {objectData}.";
            JournalEntry entry = new JournalEntry(collectionName, changeType, objectData);

            // Act
            string result = entry.ToString();

            // Assert
            Assert.AreEqual(expectedString, result);
        }
        [TestMethod]
        public void Journal_WriteRecord_ShouldAddEntry()
        {
            // Arrange
            Journal journal = new Journal();
            object source = new { Name = "TestCollection" };
            CollectionHandlerEventArgs args = new CollectionHandlerEventArgs("Added", new { Id = 1, Name = "TestItem" });

            // Act
            journal.WriteRecord(source, args);

            // Assert
            Assert.AreEqual(1, journal.Count);
        }

        private MyObservableCollection<Car> collection;
        private bool eventCountChanged;
        private bool eventReferenceChanged;

        [TestInitialize]
        public void TestInitialize()
        {
            collection = new MyObservableCollection<Car>("TestCollection");
            eventCountChanged = false;
            eventReferenceChanged = false;
            collection.CollectionCountChanged += (s, e) => eventCountChanged = true;
            collection.CollectionReferenceChanged += (s, e) => eventReferenceChanged = true;
        }

        [TestMethod]
        public void MyObservableCollection_Constructor_ShouldInitializeName()
        {
            // Arrange
            string expectedName = "TestCollection";

            // Act
            MyObservableCollection<Car> col = new MyObservableCollection<Car>(expectedName);

            // Assert
            Assert.AreEqual(expectedName, col.Name);
        }

        [TestMethod]
        public void MyObservableCollection_Constructor_WithLength_ShouldInitializeNameAndLength()
        {
            // Arrange
            string expectedName = "TestCollection";
            int expectedLength = 5;

            // Act
            MyObservableCollection<Car> col = new MyObservableCollection<Car>(expectedName, expectedLength);

            // Assert
            Assert.AreEqual(expectedName, col.Name);
            Assert.AreEqual(expectedLength, col.Count);
        }

        [TestMethod]
        public void MyObservableCollection_Constructor_WithCollection_ShouldInitializeNameAndCopyCollection()
        {
            // Arrange
            string expectedName = "TestCollection";
            MyCollection<Car> baseCollection = new MyCollection<Car> { new Car(), new Car() };

            // Act
            MyObservableCollection<Car> col = new MyObservableCollection<Car>(expectedName, baseCollection);

            // Assert
            Assert.AreEqual(expectedName, col.Name);
            Assert.AreEqual(baseCollection.Count, col.Count);
        }

        [TestMethod]
        public void MyObservableCollection_Add_ShouldAddItemAndTriggerEvent()
        {
            // Arrange
            Car car = new Car();

            // Act
            collection.Add(car);

            // Assert
            Assert.IsTrue(eventCountChanged);
        }

        [TestMethod]
        public void MyObservableCollection_Remove_ShouldRemoveItemAndTriggerEvent()
        {
            // Arrange
            Car car = new Car();
            collection.Add(car);
            eventCountChanged = false;

            // Act
            bool removed = collection.Remove(car);

            // Assert
            Assert.IsTrue(removed);
            Assert.IsTrue(eventCountChanged);
        }

        [TestMethod]
        public void MyObservableCollection_Remove_ShouldReturnFalseIfItemNotFound()
        {
            // Arrange
            Car car = new Car();

            // Act
            bool removed = collection.Remove(car);

            // Assert
            Assert.IsFalse(removed);
        }

        [TestMethod]
        public void MyObservableCollection_Indexer_Get_ShouldReturnCorrectItem()
        {
            // Arrange
            Car car = new Car();
            collection.Add(car);

            // Act
            Car result = collection[car];

            // Assert
            Assert.AreEqual(car, result);
        }

        [TestMethod]
        public void MyObservableCollection_Indexer_Set_ShouldSetItemAndTriggerEvent()
        {
            // Arrange
            Car car1 = new Car();
            Car car2 = new Car();
            collection.Add(car1);

            // Act
            collection[car1] = car2;

            // Assert
            Assert.IsTrue(eventReferenceChanged);
        }

        [TestMethod]
        public void MyObservableCollection_ToString_ShouldReturnName()
        {
            // Arrange
            string expectedName = "TestCollection";

            // Act
            string name = collection.ToString();

            // Assert
            Assert.AreEqual(expectedName, name);
        }
    }
}