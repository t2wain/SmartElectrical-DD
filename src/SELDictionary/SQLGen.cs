using SELDictionary.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SELDictionary
{
    public static class SQLGen
    {
        public static string GetSQL(Entities entity)
        {
            var q = entity
                .EntityAttrs
                .Select(i => string.Format("{0}.{1}", entity.EntityName, i.Attribute.AttributeName))
                .ToArray();
            return string.Format("select {0} from {1}", string.Join(",", q), entity.EntityName);
        }
    }
}
