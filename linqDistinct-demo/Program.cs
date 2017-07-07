using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace linqDistinct_demo
{
    class Program
    {
        static void Main(string[] args)
        {
            //Set up sample data
            Product[] products = { new Product { Name = "apple", Code = 9, Stock = 100 },
                       new Product { Name = "orange", Code = 4, Stock = 140 },
                       new Product { Name = "apple", Code = 9, Stock = 150 },
                       new Product { Name = "lemon", Code = 12, Stock = 110 },
                        new Product { Name = "lemon", Code = 12, Stock = 80 },
                        new Product { Name = "coconut", Code = 12, Stock = 90 }}; //test whether an entry with one thing the same but not both is considered equal

            //Use our custom comparer to create a new list with no duplicates
            IEnumerable<Product> noduplicates = products.Distinct(new ProductComparer());

            //Display results
            foreach (var product in noduplicates)
            {
                Console.WriteLine(product.Name + " " + product.Code);
            }               

            Console.ReadLine();
        }
    }

    public class Product
    {
        public string Name { get; set; }
        public int Code { get; set; }
        public int Stock { get; set; } //extra property that we are not comparing on
    }

    public class ProductComparer : IEqualityComparer<Product>
    {
        // Products are considered the same if their names and product numbers are equal.
        public bool Equals(Product x, Product y)
        {

            //Check whether the compared objects reference the actual same data.
            if (Object.ReferenceEquals(x, y)) return true;

            //Either null? If either are null then return false, always
            if (Object.ReferenceEquals(x, null) || Object.ReferenceEquals(y, null))
                return false;

            //Lastly perform our customer compare logic
            return x.Code == y.Code && x.Name == y.Name;
        }

        // If Equals() returns true for a pair of objects 
        // then GetHashCode() must return the same value for each one of these objects.
        // i.e. if Equals(x, y) returns true, then GetHashCode(x) and GetHashCode(y) should both return identical hash code
        public int GetHashCode(Product product)
        {
            //Check whether the object is null
            if (Object.ReferenceEquals(product, null)) return 0;

            //Get hash code for the Name field if it is not null.
            int hashProductName = product.Name == null ? 0 : product.Name.GetHashCode();

            //Get hash code for the Code field.
            int hashProductCode = product.Code.GetHashCode();

            //Calculate the hash code for the product (using a XOR to return the code for the hashProductName unless it is 0, in which
            //case return the hashProductCode
            return hashProductName ^ hashProductCode;
        }
    }
}
