namespace AnonymousType
{
    public class Book
    {
        public int BookId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            List<Book> books = new List<Book>();
            books.Add(new Book { BookId = 1, Name = "C#", Price = 210M });
            books.Add(new Book { BookId = 2, Name = "SQL", Price = 241M });
            books.Add(new Book { BookId = 3, Name = "Mongo", Price = 190M });
            books.Add(new Book { BookId = 4, Name = "Azure", Price = 214M });

            //This is Anonymous Type collection
            var result = (from book in books
                          where book.Price > 200
                          orderby book.Name
                          select new //Anonymous Type
                          {
                              Number = "#" + book.BookId,
                              Name = book.Name
                          });

            foreach(var item in result)
            {
                Console.WriteLine("Book Number: {0}, Name: {1}", item.Number, item.Name);
            }

            Console.ReadLine();
        }
    }
}
