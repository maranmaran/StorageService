using System;
using System.Text;

namespace StorageService.Domain
{
    /// <summary>
    /// Helps with managing hierarchy id
    /// EF Core officially has support but I couldn't cut it to work
    /// </summary>
    public class HierarchyHelper
    {
        private static HierarchyHelper _instance;

        private HierarchyHelper(string delimiter = "/")
        {
            Delimiter = delimiter;
        }

        public static HierarchyHelper GetInstance(string delimiter = "/")
        {
            return _instance ??= new HierarchyHelper(delimiter);
        }

        public static string Delimiter { get; private set; } = "/";

        public static string GetRoot()
        {
            return Delimiter;
        }

        public static string Parse(string input)
        {
            var nodes = input.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            var delimiterCount = input.Length - input.Replace(Delimiter, string.Empty).Length;

            if (nodes.Length + 1 != delimiterCount)
                throw new Exception("Hierarchy not valid");

            return input;
        }

        public static string[] GetParents(string input)
        {
            var nodes = input.Split(Delimiter, StringSplitOptions.RemoveEmptyEntries);

            return nodes.Length == 0 ? nodes : nodes[..^1];
        }

        public static string GetLevel(string input)
        {
            var sb = new StringBuilder(Delimiter);
            foreach (var node in GetParents(input))
            {
                sb.Append(node + Delimiter);
            }

            return sb.ToString();
        }

    }
}
