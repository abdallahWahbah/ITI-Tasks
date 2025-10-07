using L2O___D09;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.Intrinsics.X86;
using System.Text.RegularExpressions;
using System.Xml.Linq;
using static System.Net.Mime.MediaTypeNames;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace _1_Lab
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var products = ListGenerators.ProductList;
            var customers = ListGenerators.CustomerList;

            #region LINQ - Restriction Operators

            string[] Arr1 = { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine" };

            // 1. Find all products that are out of stock.
            var res1 = products.Where(item => item.UnitsInStock == 0);

            // 2. Find all products that are in stock and cost more than 3.00 per unit.
            var res2 = products.Where(item => item.UnitsInStock > 0 && item.UnitPrice > 3);

            // 3. Returns digits whose name is shorter than their value.
            var res3 = Arr1.Where((item, index) => item.Length < index);
            
            #endregion




            #region LINQ - Element Operators
            
            int[] Arr2 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 1. Get first Product out of Stock 
            var res4 = products.FirstOrDefault(item => item.UnitsInStock == 0);

            // 2. Return the first product whose Price > 1000, unless there is no match, in which case null is returned.
            var res5 = products.FirstOrDefault(item => item.UnitPrice > 1000);

            // 3. Retrieve the second number greater than 5 
            var res6 = Arr2.Where(item => item > 5).Skip(1).FirstOrDefault();
            #endregion





            #region LINQ - Set Operators
            // 1. Find the unique Category names from Product List
            var res7 = products.Select(item => item.Category).Distinct();

            // 2. Produce a Sequence containing the unique first letter from both product and customer names.
            var res8 = products.Select(item => item.ProductName[0])
                                .Union(customers.Select(item => item.CompanyName[0]))
                                .Distinct();

            // 3. Create one sequence that contains the common first letter from both product and customer names.
            var res9 = products.Select(item => item.ProductName[0])
                                .Intersect(customers.Select(item => item.CompanyName[0]));

            // 4. Create one sequence that contains the first letters of product names that are not also first letters of customer names.
            var res10 = products.Select(item => item.ProductName[0])
                                .Except(customers.Select(item => item.CompanyName[0]));

            // 5. Create one sequence that contains the last Three Characters in each names of all customers and products, including any duplicates
            var res11 = products.Select(item => item.ProductName.Substring(item.ProductName.Length - 3))
                                .Concat(customers.Select(item => item.CompanyName.Substring(item.CompanyName.Length - 3)));
            #endregion




            #region LINQ - Aggregate Operators

            int[] Arr4 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 1. Uses Count to get the number of odd numbers in the array
            Console.WriteLine(Arr4.Count(item => item % 2 == 1));

            // 2. Return a list of customers and how many orders each has.
            var res12 = customers.Select(item => new { Customer = item.CompanyName, OrdersCount = item.Orders.Length });

            // 3. Return a list of categories and how many products each has
            var res13 = products.GroupBy(item => item.Category)
                                .Select(item => new { Categry = item.Key, CategoryCount = item.Count()});

            // 4. Get the total of the numbers in an array.
            var res14 = Arr4.Sum();

            // 5.Get the total number of characters of all words in dictionary_english.txt(Read dictionary_english.txt into Array of String First).
            string[] words = File.ReadAllLines("dictionary_english.txt");
            var res15 = words.Sum(item => item.Length);

            // 6. Get the total units in stock for each product category.
            var res16 = products.GroupBy(item => item.Category) // After GroupBy, each item is a group, not a product.
                                .Select(innerGroup => innerGroup.Sum(x => x.UnitsInStock));

            // 7. Get the length of the shortest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            var res17 = words.Min(item => item.Length);

            // 8. Get the cheapest price among each category's products
            var res18 = products.GroupBy(item => item.Category)
                                .Select(innerGroup => innerGroup.Min(x => x.UnitPrice));

            // 9. Get the products with the cheapest price in each category (Use Let)
            var res19 =from p in products
                group p by p.Category into g
                let minPrice = g.Min(x => x.UnitPrice)
                from p2 in g
                where p2.UnitPrice == minPrice
                select new { p2.ProductName, p2.Category, p2.UnitPrice };

            // 10. Get the length of the longest word in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            var res20 = words.Max(item => item.Length);

            // 11. Get the most expensive price among each category's products.
            var res21 = products.GroupBy(item => item.Category)
                                .Select(innerGroup => innerGroup.Max(x => x.UnitPrice));

            // 12. Get the products with the most expensive price in each category.
            var res22 = products.GroupBy(item => item.Category)
                                .Select(innerGroup =>
                                {
                                    var maxPrice = innerGroup.Max(x => x.UnitPrice);
                                    return new
                                    {
                                        Category = innerGroup.Key,
                                        Products = innerGroup.Where(item => item.UnitPrice == maxPrice)
                                                                        .Select(x => new { x.ProductName, x.UnitPrice })
                                    };
                                });

            // 13. Get the average length of the words in dictionary_english.txt (Read dictionary_english.txt into Array of String First).
            var wordsLength = words.Sum(item => item.Length);
            var wordsCount = words.Count();
            var wordsAverage = wordsLength / wordsCount;

            // 14. Get the average price of each category's products.
            var res23 = products.GroupBy(item => item.Category)
                                .Select(innerGroup =>
                                {
                                    var totalPrice = innerGroup.Sum(item => item.UnitPrice);
                                    var itemsCount = innerGroup.Count();
                                    return new { averagePrice = (totalPrice / itemsCount) };
                                });
            #endregion




            #region LINQ - Ordering Operators

            string[] Arr5 = { "aPPLE", "AbAcUs", "bRaNcH", "BlUeBeRrY", "ClOvEr", "cHeRry" };

            // 1. Sort a list of products by name
            var res24 = products.OrderBy(item => item.ProductName);

            // 2. Use a custom comparer to do a case-insensitive sort of the words in an array.
            var res25 = products.OrderBy(item => item.ProductName, new CustomComparer());

            // 3. Sort a list of products by units in stock from highest to lowest.
            var res26 = products.OrderByDescending(item => item.UnitsInStock);

            // 4. Sort a list of digits, first by length of their name, and then alphabetically by the name itself.
            var res27 = Arr5.OrderBy(item => item.Length).ThenBy(item => item);

            // 5. Sort first by word length and then by a case-insensitive sort of the words in an array.
            var res28 = Arr5.OrderBy(item => item.Length).ThenBy(item => new CustomComparer());
            //var res28 = Arr5.OrderBy(item => item.Length) // simpler (not custom class)
            //    .ThenBy(item => item, StringComparer.OrdinalIgnoreCase);

            // 6. Sort a list of products, first by category, and then by unit price, from highest to lowest.
            var res29 = products.OrderBy(item => item.Category).ThenByDescending(item => item.UnitPrice);

            // 7. Sort first by word length and then by a case-insensitive descending sort of the words in an array.
            var res30 = Arr5.OrderBy(item => item.Length)
                            .ThenByDescending(item => item, StringComparer.OrdinalIgnoreCase);

            // 8. Create a list of all digits in the array whose second letter is 'i' that is reversed from the order in the original array.
            var res31 = Arr5.Where(item => item[1] == 'i').Reverse();

            #endregion




            #region LINQ - Partitioning Operators

            int[] numbers6 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };

            // 1. Get the first 3 orders from customers in Washington
            var res32 = customers.Where(item => item.City == "Washington").SelectMany(x => x.Orders).Take(3);

            // 2. Get all but the first 2 orders from customers in Washington.
            var res33 = customers.Where(item => item.City == "Washington").SelectMany(x => x.Orders).Skip(2);

            // 3. Return elements starting from the beginning of the array until a number is hit that is less than its position in the array.
            var res34 = numbers6.TakeWhile((item, index) => item >= index);

            // 4. Get the elements of the array starting from the first element divisible by 3.
            var res35 = numbers6.SkipWhile((item) => item % 3 != 0);

            // 5. Get the elements of the array starting from the first element less than its position.
            var res36 = numbers6.SkipWhile((item, index) => item >= index);
            #endregion




            #region LINQ - Projection Operators

            string[] words7 = { "aPPLE", "BlUeBeRrY", "cHeRry" };
            int[] Arr7 = { 5, 4, 1, 3, 9, 8, 6, 7, 2, 0 };
            int[] numbersA = { 0, 2, 4, 5, 6, 8, 9 };
            int[] numbersB = { 1, 3, 5, 7, 8 };

            // 1. Return a sequence of just the names of a list of products.
            var res37 = products.Select(item => item.ProductName);

            // 2. Produce a sequence of the uppercase and lowercase versions of each word in the original array (Anonymous Types).
            var res38 = words7.Select(item => new { UpperCaseWords = item.ToUpper(), LowerCaseWords = item.ToLower() });

            // 3. Produce a sequence containing some properties of Products, including UnitPrice which is renamed to Price in the resulting type.
            var res39 = products.Select(item => new { Price = item.UnitPrice, Count = item.UnitsInStock });

            // 4. Determine if the value of ints in an array match their position in the array.
            var res40 = Arr7.Select((item, index) => new { Number = item, InPlace = item == index });
            foreach (var r in res40)
            {
                Console.WriteLine($"{r.Number}: {r.InPlace}");
            }

            // 5. Returns all pairs of numbers from both arrays such that the number from numbersA is less than the number from numbersB.
            var res41 = numbersA.SelectMany(a => numbersB.Where(b => a < b).Select(b => $"{a} is less than {b}"));
            foreach (var pair in res41)
            {
                Console.WriteLine(pair);
            }

            // 6. Select all orders where the order total is less than 500.00.
            var res42 = customers.Select(item => item.Orders.Where(x => x.Total < 500));

            // 7. Select all orders where the order was made in 1998 or later.
            var res43 = customers.Select(item => item.Orders.Where(x => x.OrderDate.Year >= 1998));

            #endregion




            #region LINQ - Quantifiers

            // 1. Determine if any of the words in dictionary_english.txt contain the substring 'ei'.
            var res44 = words.Any(item => item.Contains("ei"));

            // 2. Return a grouped a list of products only for categories that have at least one product that is out of stock.
            var res45 = products.GroupBy(item => item.Category)
                                .Where(x => x.Any(item => item.UnitsInStock == 0))
                                .Select(item => item);

            // 3. Return a grouped a list of products only for categories that have all of their products in stock.
            var res46 = products.GroupBy(item => item.Category)
                                .Where(x => x.All(item => item.UnitsInStock != 0));
            #endregion




            #region LINQ - Grouping Operators

            int[] numbers10 = { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14 };
            string[] Arr10 = { "from   ", " salt", " earn ", "  last   ", " near ", " form  " };

            // 1. Use group by to partition a list of numbers by their remainder when divided by 5
            var res47 = numbers10.GroupBy(item => item % 5); 

            foreach(var group in res47.OrderBy(g => g.Key)) // "g.Key" is the remainder (item % 5);
            {
                Console.WriteLine($"Numbers with a remainder of {group.Key} when divided by 5:");
                foreach (var number in group)
                {
                    Console.WriteLine(number);
                }
            }

            // 2. Uses group by to partition a list of words by their first letter Use dictionary_english.txt for Input
            var res48 = words.GroupBy(item => item[0]);

            //  Use Group By with a custom comparer that matches words that are consists of the same Characters Together
            var res49 = Arr10
                        .Select(item => item.Trim())
                        .GroupBy(word => word.Concat(word.OrderBy(x => x)));
            foreach (var group in res49)
            {
                Console.WriteLine("...");
                foreach (var word in group)
                {
                    Console.WriteLine(word);
                }
            }

            /* explaination for the last query
             * 
             word.OrderBy(c => c)
                - Takes each word and splits it into characters.
                - Sorts the characters alphabetically.
                - Example:
                - "from" → ['f','r','o','m'] → sorted → ['f','m','o','r']
                - "form" → ['f','o','r','m'] → sorted → ['f','m','o','r']
             
            String.Concat(...)
                - Joins the sorted characters back into a single string.
                - Example:
                - "from" → "fmor"
                - "form" → "fmor"

            GroupBy(...)
                - Groups words that have the same key together.
                - Result:
                - Group 1 (key = "fmor") → "from", "form"
                - Group 2 (key = "alst") → "salt", "last"
                - Group 3 (key = "aenr") → "earn", "near"
             */
            #endregion




            #region xxxxxxxxx
            #endregion




            #region xxxxxxxxx
            #endregion
        }
    }
    class CustomComparer: IComparer<string>
    {
        public int Compare(string x, string y)
        {
            return string.Compare(x, y);
        }
    }
}
