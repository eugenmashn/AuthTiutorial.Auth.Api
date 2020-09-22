using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AuthTutorial.Resource.Api.Models
{
    public class BookStore
    {
        public List<Book> Books => new List<Book>
        {
            new Book{Id =1, Auth = "J.K. Rowlong", Title = "Harry Potter and Philosopher`s Stone", Price = 10.45M},
            new Book{Id =2, Auth = "Herman Melville", Title = "Modby-Dick", Price = 8.52M},
            new Book{Id =3, Auth = "jULES vERNE", Title = "The Adventures of Pinoccgio", Price = 6.42M},
            new Book{Id =4, Auth = "Carlos Colode", Title = "The Mysterious island", Price = 7.11M}
        };

        public Dictionary<Guid, int[]> Orders => new Dictionary<Guid, int[]>
        {
            { Guid.Parse("7aa1eb39-9fcf-4bb9-be84-42a18fac65db"),new int[]{1,2,3 } },
            { Guid.Parse("6ed37231-7e84-478b-a490-aad1b8c7054b"),new int[]{2,3,4 } }
        };
    }
}
