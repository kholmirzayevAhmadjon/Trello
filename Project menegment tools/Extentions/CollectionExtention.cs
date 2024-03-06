using Project_menegment_tools.Models.Commons;

namespace Project_menegment_tools.Extentions;

public static class CollectionExtention
{
        public static T Create<T>(this List<T> values, T model) where T : Auditable
        {
            var lastId = values.Count == 0 ? 1 : values.Last().Id + 1;
            model.Id = lastId;
            values.Add(model);
            return values.Last();
        }
}
