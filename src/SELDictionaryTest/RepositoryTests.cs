using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using SELDictionary;
using SELDictionary.Model;

namespace SELDictionaryTest
{
    public class RepositoryTests
    {
        ISPELDRepository _repo = null;
        ItemSQL _itemSQL = null;

        [SetUp]
        public void Setup()
        {
            var provider = Program.InitProvider();
            _repo = provider.GetService<ISPELDRepository>();
            _itemSQL = provider.GetService<ItemSQL>()!;
        }

        [Test]
        public void Can_get_Item()
        {
            var itemId = 161; // cable
            var item = _repo.FindItem(itemId).Result;
            Assert.NotNull(item);
        }

        [Test]
        public async Task Can_get_Entity()
        {
            var e = await _repo!.FindEntity(16);
            Assert.NotNull(e);
        }

        [Test]
        public async Task Can_get_ItemNames()
        {
            var e = await _repo!.GetAllItemNames();
            Assert.Greater(e.Count, 0);
        }

        [Test]
        public async Task Can_get_Relationship()
        {
            var rels = await _repo.FindRelationships(new int[] { });
            Assert.That(rels.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Can_get_Attribute()
        {
            var atts = await _repo.FindAttributes(new int[] { });
            Assert.That(atts.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Can_get_ItemRelations()
        {
            var itemId = 161; // cable
            var rels = await _repo.FindItemRelations(itemId);
            Assert.That(rels.Count(), Is.GreaterThan(0));

            rels = await _repo.FindItemRelationsFromPaths(new string[] { });
            Assert.That(rels.Count(), Is.EqualTo(0));
        }

        [Test]
        public async Task Can_get_Enumerations()
        {
            var enumId = 134; // Cable Category
            var enumItem = await _repo.FindEnumerations(enumId);
            Assert.NotNull(enumItem);
            Assert.That(enumItem.CodeItems?.Count, Is.GreaterThan(0));
        }

        [Test]
        public async Task Can_get_TableView()
        {
            var itemId = 161; // cable
            var v = await _repo.FindTableView(new int[] { itemId });
            Assert.NotNull(v);
        }
    }
}