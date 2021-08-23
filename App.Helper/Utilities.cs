using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Helper
{
    public static class Utilities
    {
        private static Dictionary<string, List<string>> BuildListConstants<T>()
        {
            Dictionary<string, List<string>> listService = new Dictionary<string, List<string>>();
            var nestedTypes = typeof(T).GetNestedTypes(BindingFlags.Public);

            foreach (Type nestedType in nestedTypes)
            {

                var listConstants = new List<string>();
                FieldInfo[] constants = nestedType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static);
                constants.ToList().ForEach(constant =>
                {
                    listConstants.Add((string)constant.GetValue(null));
                });
                listService.Add(nestedType.Name, listConstants);
            }

            return listService;
        }

        public static string BuildQuery<T>(string serviceName, string methodName, string fields)
        {
            Dictionary<string, List<string>> listService = BuildListConstants<T>();
            if (listService.Count <= 0 || !listService.ContainsKey(serviceName)) return "{}";
            List<string> listMethod;
            listService.TryGetValue(serviceName, out listMethod);
            if (listMethod.Count <= 0 || !listMethod.Contains(methodName)) return "{}";
            return "{" + serviceName.ToLower() + " { " + methodName.ToLower() + " "  + fields.ToLower() + " " + " }}";
        }
    }
}
