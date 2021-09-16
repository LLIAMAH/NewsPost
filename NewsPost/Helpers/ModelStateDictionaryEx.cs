using System.Linq;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace NewsPost.Helpers
{
    public static class ModelStateDictionaryEx
    {
        public static string GetErrors(this ModelStateDictionary dictionary)
        {
            return dictionary.Keys.Aggregate(string.Empty, (current, key) => current + $"{key}: {dictionary[key]};");
        }
    }
}
