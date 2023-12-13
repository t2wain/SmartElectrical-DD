using Microsoft.Extensions.DependencyInjection;
using SELDictionary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionaryTest
{
    public class ItemSQLTest
    {
        ISPELDRepository _repo = null;
        ItemSQL _itemSQL = null;

        [SetUp]
        public void Setup()
        {
            var provider = Program.InitProvider();
            _repo = provider.GetService<ISPELDRepository>();
            _itemSQL = provider.GetService<ItemSQL>();
        }

        [Test]
        public void Can_calc_Item_SQL()
        {
            var itemId = 161; // cable
            Assert.DoesNotThrow(() =>
            {
                var item = _repo.FindItem(itemId).Result;
                var res = _itemSQL.GetItemJoin(item, _repo).Result;
                res.PrintAllSQL();
            });
        }

        [Test]
        public void Can_calc_OjectJoin_SQL()
        {
            var itemId = 161; // cable
            Assert.DoesNotThrow(() =>
            {
                var res = _itemSQL.GetItemObjRelJoin(itemId, _repo);
                res.PrintAllSQL();
            });
        }
    }
}
