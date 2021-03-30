using System.Collections.Generic;
using System.Linq;
using Pitz.Domain.Entities;

namespace Pitz.Infraestructure
{
   public class MockDB<T>
   {
      public static List<T> Items { get; set; } = new List<T>();

      public static void Add(T item)
      {
         var props = item.GetType().GetProperties();
         var id = props
            .FirstOrDefault(p => p.Name.Equals("id", System.StringComparison.OrdinalIgnoreCase));

         if (id != null)
         {
            id.SetValue(item, Items.Count + 1);
         }

         Items.Add(item);
      }
   }

   public class PeopleDB : MockDB<Person> { }
   public class OrganizationsDB : MockDB<Organization> { }
}
