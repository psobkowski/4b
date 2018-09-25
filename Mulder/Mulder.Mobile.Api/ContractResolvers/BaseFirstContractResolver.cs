using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Mulder.Mobile.Api.ContractResolvers
{
    public class BaseFirstContractResolver : DefaultContractResolver
    {
        static BaseFirstContractResolver _Instance;

        static BaseFirstContractResolver() { _Instance = new BaseFirstContractResolver(); }

        public static BaseFirstContractResolver Instance { get => _Instance; set => _Instance = value; }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            var properties = base.CreateProperties(type, memberSerialization);
            if (properties != null)
                return properties.OrderBy(p => p.DeclaringType.BaseTypesAndSelf().Count()).ToList();
            return properties;
        }
    }

    public static class TypeExtensions
    {
        public static IEnumerable<Type> BaseTypesAndSelf(this Type type)
        {
            while (type != null)
            {
                yield return type;
                type = type.BaseType;
            }
        }
    }
}
