﻿using System.Collections.Generic;
using System.Linq;
using Tournaments.Domain.Entities;

namespace Tournaments.Infraestructure
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
}
