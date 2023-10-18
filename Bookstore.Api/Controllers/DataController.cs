using Bookstore.DataAccess;
using Bookstore.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Bookstore.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DataController : ControllerBase
    {
        private BookstoreContext _context;

        public DataController(BookstoreContext context)
        {
            _context = context;
        }

        // POST api/<DataController>
        [HttpPost]
        public IActionResult Post()
        {
            Role role1 = new Role();
            role1.Name = "user";
            role1.IsDefault = true;
            role1.IsActive = true;
            _context.Roles.Add(role1);
            _context.SaveChanges();

            Role role2 = new Role();
            role2.Name = "admin";
            role2.IsDefault = false;
            role2.IsActive = true;
            _context.Roles.Add(role2);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin1 = new RoleUseCase();
            roleUseCaseAdmin1.RoleId = role2.Id;
            roleUseCaseAdmin1.UseCaseId = 1;
            _context.RoleUseCases.Add(roleUseCaseAdmin1);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin2 = new RoleUseCase();
            roleUseCaseAdmin2.RoleId = role2.Id;
            roleUseCaseAdmin2.UseCaseId = 2;
            _context.RoleUseCases.Add(roleUseCaseAdmin2);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin3 = new RoleUseCase();
            roleUseCaseAdmin3.RoleId = role2.Id;
            roleUseCaseAdmin3.UseCaseId = 3;
            _context.RoleUseCases.Add(roleUseCaseAdmin3);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin4 = new RoleUseCase();
            roleUseCaseAdmin4.RoleId = role2.Id;
            roleUseCaseAdmin4.UseCaseId = 4;
            _context.RoleUseCases.Add(roleUseCaseAdmin4);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin5 = new RoleUseCase();
            roleUseCaseAdmin5.RoleId = role2.Id;
            roleUseCaseAdmin5.UseCaseId = 5;
            _context.RoleUseCases.Add(roleUseCaseAdmin5);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin6 = new RoleUseCase();
            roleUseCaseAdmin6.RoleId = role2.Id;
            roleUseCaseAdmin6.UseCaseId = 6;
            _context.RoleUseCases.Add(roleUseCaseAdmin6);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin7 = new RoleUseCase();
            roleUseCaseAdmin7.RoleId = role2.Id;
            roleUseCaseAdmin7.UseCaseId = 7;
            _context.RoleUseCases.Add(roleUseCaseAdmin7);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin8 = new RoleUseCase();
            roleUseCaseAdmin8.RoleId = role2.Id;
            roleUseCaseAdmin8.UseCaseId = 8;
            _context.RoleUseCases.Add(roleUseCaseAdmin8);
            _context.SaveChanges();

          
            RoleUseCase roleUseCaseAdmin10 = new RoleUseCase();
            roleUseCaseAdmin10.RoleId = role2.Id;
            roleUseCaseAdmin10.UseCaseId = 10;
            _context.RoleUseCases.Add(roleUseCaseAdmin10);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin11 = new RoleUseCase();
            roleUseCaseAdmin11.RoleId = role2.Id;
            roleUseCaseAdmin11.UseCaseId = 11;
            _context.RoleUseCases.Add(roleUseCaseAdmin11);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin12 = new RoleUseCase();
            roleUseCaseAdmin12.RoleId = role2.Id;
            roleUseCaseAdmin12.UseCaseId = 12;
            _context.RoleUseCases.Add(roleUseCaseAdmin12);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin13 = new RoleUseCase();
            roleUseCaseAdmin13.RoleId = role1.Id;
            roleUseCaseAdmin13.UseCaseId = 16;
            _context.RoleUseCases.Add(roleUseCaseAdmin13);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin14 = new RoleUseCase();
            roleUseCaseAdmin14.RoleId = role1.Id;
            roleUseCaseAdmin14.UseCaseId = 14;
            _context.RoleUseCases.Add(roleUseCaseAdmin14);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin15 = new RoleUseCase();
            roleUseCaseAdmin15.RoleId = role2.Id;
            roleUseCaseAdmin15.UseCaseId = 18;
            _context.RoleUseCases.Add(roleUseCaseAdmin15);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin16 = new RoleUseCase();
            roleUseCaseAdmin16.RoleId = role2.Id;
            roleUseCaseAdmin16.UseCaseId = 19;
            _context.RoleUseCases.Add(roleUseCaseAdmin16);
            _context.SaveChanges();


            RoleUseCase roleUseCaseAdmin17 = new RoleUseCase();
            roleUseCaseAdmin17.RoleId = role2.Id;
            roleUseCaseAdmin17.UseCaseId = 20;
            _context.RoleUseCases.Add(roleUseCaseAdmin17);
            _context.SaveChanges();



            RoleUseCase roleUseCaseAdmin18 = new RoleUseCase();
            roleUseCaseAdmin18.RoleId = role2.Id;
            roleUseCaseAdmin18.UseCaseId = 25;
            _context.RoleUseCases.Add(roleUseCaseAdmin18);
            _context.SaveChanges();


        

            RoleUseCase roleUseCaseAdmin20 = new RoleUseCase();
            roleUseCaseAdmin20.RoleId = role2.Id;
            roleUseCaseAdmin20.UseCaseId = 21;
            _context.RoleUseCases.Add(roleUseCaseAdmin20);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin21 = new RoleUseCase();
            roleUseCaseAdmin21.RoleId = role2.Id;
            roleUseCaseAdmin21.UseCaseId = 24;
            _context.RoleUseCases.Add(roleUseCaseAdmin21);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin22 = new RoleUseCase();
            roleUseCaseAdmin22.RoleId = role2.Id;
            roleUseCaseAdmin22.UseCaseId = 22;
            _context.RoleUseCases.Add(roleUseCaseAdmin22);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin23 = new RoleUseCase();
            roleUseCaseAdmin23.RoleId = role2.Id;
            roleUseCaseAdmin23.UseCaseId = 23;
            _context.RoleUseCases.Add(roleUseCaseAdmin23);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin24 = new RoleUseCase();
            roleUseCaseAdmin24.RoleId = role2.Id;
            roleUseCaseAdmin24.UseCaseId = 24;
            _context.RoleUseCases.Add(roleUseCaseAdmin24);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin25 = new RoleUseCase();
            roleUseCaseAdmin25.RoleId = role2.Id;
            roleUseCaseAdmin25.UseCaseId = 26;
            _context.RoleUseCases.Add(roleUseCaseAdmin25);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin26 = new RoleUseCase();
            roleUseCaseAdmin26.RoleId = role2.Id;
            roleUseCaseAdmin26.UseCaseId = 27;
            _context.RoleUseCases.Add(roleUseCaseAdmin26);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin27 = new RoleUseCase();
            roleUseCaseAdmin27.RoleId = role2.Id;
            roleUseCaseAdmin27.UseCaseId = 28;
            _context.RoleUseCases.Add(roleUseCaseAdmin27);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin28 = new RoleUseCase();
            roleUseCaseAdmin28.RoleId = role2.Id;
            roleUseCaseAdmin28.UseCaseId = 29;
            _context.RoleUseCases.Add(roleUseCaseAdmin28);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin29 = new RoleUseCase();
            roleUseCaseAdmin29.RoleId = role2.Id;
            roleUseCaseAdmin29.UseCaseId = 30;
            _context.RoleUseCases.Add(roleUseCaseAdmin29);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin30 = new RoleUseCase();
            roleUseCaseAdmin30.RoleId = role1.Id;
            roleUseCaseAdmin30.UseCaseId = 30;
            _context.RoleUseCases.Add(roleUseCaseAdmin30);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin31 = new RoleUseCase();
            roleUseCaseAdmin31.RoleId = role1.Id;
            roleUseCaseAdmin31.UseCaseId = 31;
            _context.RoleUseCases.Add(roleUseCaseAdmin31);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin32 = new RoleUseCase();
            roleUseCaseAdmin32.RoleId = role1.Id;
            roleUseCaseAdmin32.UseCaseId = 32;
            _context.RoleUseCases.Add(roleUseCaseAdmin32);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin33 = new RoleUseCase();
            roleUseCaseAdmin33.RoleId = role2.Id;
            roleUseCaseAdmin33.UseCaseId = 33;
            _context.RoleUseCases.Add(roleUseCaseAdmin33);
            _context.SaveChanges();


            RoleUseCase roleUseCaseAdmin34 = new RoleUseCase();
            roleUseCaseAdmin34.RoleId = role2.Id;
            roleUseCaseAdmin34.UseCaseId = 34;
            _context.RoleUseCases.Add(roleUseCaseAdmin34);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin35 = new RoleUseCase();
            roleUseCaseAdmin35.RoleId = role2.Id;
            roleUseCaseAdmin35.UseCaseId = 35;
            _context.RoleUseCases.Add(roleUseCaseAdmin35);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin36 = new RoleUseCase();
            roleUseCaseAdmin36.RoleId = role2.Id;
            roleUseCaseAdmin36.UseCaseId = 36;
            _context.RoleUseCases.Add(roleUseCaseAdmin36);
            _context.SaveChanges();


            RoleUseCase roleUseCaseAdmin37 = new RoleUseCase();
            roleUseCaseAdmin37.RoleId = role2.Id;
            roleUseCaseAdmin37.UseCaseId = 37;
            _context.RoleUseCases.Add(roleUseCaseAdmin37);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin38 = new RoleUseCase();
            roleUseCaseAdmin38.RoleId = role2.Id;
            roleUseCaseAdmin38.UseCaseId = 38;
            _context.RoleUseCases.Add(roleUseCaseAdmin37);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin41 = new RoleUseCase();
            roleUseCaseAdmin41.RoleId = role1.Id;
            roleUseCaseAdmin41.UseCaseId = 38;
            _context.RoleUseCases.Add(roleUseCaseAdmin41);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin39 = new RoleUseCase();
            roleUseCaseAdmin39.RoleId = role2.Id;
            roleUseCaseAdmin39.UseCaseId = 39;
            _context.RoleUseCases.Add(roleUseCaseAdmin39);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin40 = new RoleUseCase();
            roleUseCaseAdmin40.RoleId = role2.Id;
            roleUseCaseAdmin40.UseCaseId = 40;
            _context.RoleUseCases.Add(roleUseCaseAdmin40);
            _context.SaveChanges();

            RoleUseCase roleUseCaseAdmin42 = new RoleUseCase();
            roleUseCaseAdmin42.RoleId = role2.Id;
            roleUseCaseAdmin42.UseCaseId = 41;
            _context.RoleUseCases.Add(roleUseCaseAdmin42);
            _context.SaveChanges();


            string pass1 = "sifra123";
            string passwordHash = BCrypt.Net.BCrypt.HashPassword(pass1);
       

            User user1 = new User();
            user1.FirstName = "Mika";
            user1.LastName = "Mikic";
            user1.Email = "mika.mikic@gmail.com";
            user1.Username = "mika_mikic";
            user1.Password = passwordHash;
            user1.RoleId = role1.Id;
            user1.IsActive = true;
            _context.Users.Add(user1);
            _context.SaveChanges();

            User user2 = new User();
            user2.FirstName = "Zika";
            user2.LastName = "Zikic";
            user2.Email = "zika.zikic@gmail.com";
            user2.Username = "zika_zikic";
            user2.Password = passwordHash;
            user2.RoleId = role2.Id;
            user2.IsActive = true;
            _context.Users.Add(user2);
            _context.SaveChanges();


            Publisher publisher1 = new Publisher();
            publisher1.Name = "EVRO GUINT";
            publisher1.Location = "Dimitrija Tucovića 41, Belgrade";
            _context.Publishers.Add(publisher1);
            _context.SaveChanges();

            Publisher publisher2 = new Publisher();
            publisher2.Name = "HODDER";
            publisher2.Location = "London, England";
            publisher2.Website = "https://www.hodder.co.uk";
            _context.Publishers.Add(publisher2);
            _context.SaveChanges();

            Publisher publisher3 = new Publisher();
            publisher3.Name = "SEZAM BOOK";
            publisher3.Location = "Cara Dušana 40, Belgrade";
            publisher3.Website = "https://www.sezambook.rs/";
            _context.Publishers.Add(publisher3);
            _context.SaveChanges();

            Publisher publisher4 = new Publisher();
            publisher4.Name = "LAGUNA";
            publisher4.Location = "Kralja Petra 45, Belgrade";
            publisher4.Website = "https://www.laguna.rs/";
            _context.Publishers.Add(publisher4);
            _context.SaveChanges();

            Publisher publisher5 = new Publisher();
            publisher5.Name = "FINESA";
            publisher5.Location = "Krunska 36, Belgrade";
            publisher5.Website = "https://www.finesa.edu.rs/";
            _context.Publishers.Add(publisher5);
            _context.SaveChanges();

            Author author1 = new Author();
            author1.FirstName = "George Raymond Richard";
            author1.LastName = "Martin";
            author1.Pseudonym = "GRRM";
            author1.DateOfBirth = new DateTime(1948,09,20);
            author1.Country = "U.S";
            _context.Authors.Add(author1);
            _context.SaveChanges();

            Author author2 = new Author();
            author2.FirstName = "Ivo";
            author2.LastName = "Andrić";
            author2.DateOfBirth = new DateTime(1892, 10, 09);
            author2.Country = "Serbia";
            _context.Authors.Add(author2);
            _context.SaveChanges();

            Author author3 = new Author();
            author3.FirstName = "Stephen Edwin";
            author3.LastName = "King";
            author1.Pseudonym = "King";
            author3.DateOfBirth = new DateTime(1947, 09, 21);
            author3.Country = "U.S";
            _context.Authors.Add(author3);
            _context.SaveChanges();

            Book book1 = new Book();
            book1.Name = "It";
            book1.Code = 1234567890;
            book1.Description = "To the adults, knowing better, Derry Maine was just their home town: familiar, well-ordered for the most part. A good place to live.It is the children who see - and feel - what makes the small town of Derry so horribly different. In the storm drains, in the sewers, IT lurks, taking on the shape of every nightmare, each one's deepest dread. Sometimes IT reaches up, seizing, tearing, killing ..";
            _context.Books.Add(book1);
            _context.SaveChanges();

            Book book2 = new Book();
            book2.Name = "A Clash of Kings";
            book2.Code = 1234567891;
            book2.Description = "A Clash of Kings depicts the Seven Kingdoms of Westeros in civil war, while the Night's Watch mounts a reconnaissance to investigate the mysterious people known as wildlings. Meanwhile, Daenerys Targaryen continues her plan to conquer the Seven Kingdoms.";
            _context.Books.Add(book2);
            _context.SaveChanges();

            Book book3 = new Book();
            book3.Name = "Prokleta Avlija";
            book3.Code = 1234567892;
            book3.Description = "Carigradski zatvor, nazvan Prokleta avlija, sa svojim simbolički šarenim svetom, spojio je jednog skromnog, poštenog i nedužno optuženog bosanskog fratra i razočaranog, životom otrovanog i fikcijom opsednutog turskog bogataša.";
            _context.Books.Add(book3);
            _context.SaveChanges();

            Genre genre1 = new Genre();
            genre1.Name = "Fantasy";
            _context.Genres.Add(genre1);
            _context.SaveChanges();

            Genre genre1sub1 = new Genre();
            genre1sub1.Name = "Dark Fantasy";
            genre1sub1.ParentId = genre1.Id;
            _context.Genres.Add(genre1sub1);
            _context.SaveChanges();

            Genre genre1sub2 = new Genre();
            genre1sub2.Name = "Fairy Tale";
            genre1sub2.ParentId = genre1.Id;
            _context.Genres.Add(genre1sub2);
            _context.SaveChanges();

            Genre genre2 = new Genre();
            genre2.Name = "Horror";
            _context.Genres.Add(genre2);
            _context.SaveChanges();

            Genre genre2sub1 = new Genre();
            genre2sub1.Name = "Paranorlam Horror";
            genre2sub1.ParentId = genre2.Id;
            _context.Genres.Add(genre2sub1);
            _context.SaveChanges();

            Genre genre2sub2 = new Genre();
            genre2sub2.Name = "Psychological Horror";
            genre2sub2.ParentId = genre2.Id;
            _context.Genres.Add(genre2sub2);
            _context.SaveChanges();

            Genre genre3 = new Genre();
            genre3.Name = "Fiction";
            _context.Genres.Add(genre3);
            _context.SaveChanges();

            Genre genre4 = new Genre();
            genre4.Name = "Thrller";
            _context.Genres.Add(genre4);
            _context.SaveChanges();

            Genre genre5 = new Genre();
            genre5.Name = "Satire";
            _context.Genres.Add(genre5);
            _context.SaveChanges();

            Genre genre6 = new Genre();
            genre6.Name = "Romance";
            _context.Genres.Add(genre6);
            _context.SaveChanges();

            Genre genre7 = new Genre();
            genre7.Name = "Poetry";
            _context.Genres.Add(genre7);
            _context.SaveChanges();

            BookGenre bookgenre1 = new BookGenre();
            bookgenre1.BookId = book1.Id;
            bookgenre1.GenreId = genre2.Id;
            _context.BookGenres.Add(bookgenre1);
            _context.SaveChanges();

            BookGenre bookgenre2 = new BookGenre();
            bookgenre2.BookId = book1.Id;
            bookgenre2.GenreId = genre3.Id;
            _context.BookGenres.Add(bookgenre2);
            _context.SaveChanges();

            BookGenre bookgenre3 = new BookGenre();
            bookgenre3.BookId = book2.Id;
            bookgenre3.GenreId = genre1.Id;
            _context.BookGenres.Add(bookgenre3);
            _context.SaveChanges();

            BookGenre bookgenre4 = new BookGenre();
            bookgenre4.BookId = book2.Id;
            bookgenre4.GenreId = genre3.Id;
            _context.BookGenres.Add(bookgenre4);
            _context.SaveChanges();

            BookAuthor bookauthor1 = new BookAuthor();
            bookauthor1.BookId = book1.Id;
            bookauthor1.AuthorId = author3.Id;
            _context.BookAuthors.Add(bookauthor1);
            _context.SaveChanges();

            BookAuthor bookauthor2 = new BookAuthor();
            bookauthor2.BookId = book2.Id;
            bookauthor2.AuthorId = author1.Id;
            _context.BookAuthors.Add(bookauthor2);
            _context.SaveChanges();

            BookAuthor bookauthor3 = new BookAuthor();
            bookauthor3.BookId = book3.Id;
            bookauthor3.AuthorId = author2.Id;
            _context.BookAuthors.Add(bookauthor3);
            _context.SaveChanges();

            BookPublisher bookP1 = new BookPublisher();
            bookP1.NumberOfPages = 1392;
            bookP1.BookCover = "Hardcover";
            bookP1.BookFormat = "12x18";
            bookP1.BookWritingSystem = "Latin Alphabet";
            bookP1.Year = 2011;
            bookP1.BookId = book1.Id;
            bookP1.PublisherId = publisher2.Id;
            _context.BookPublishers.Add(bookP1);
            _context.SaveChanges();

            Price price1 = new Price();
            price1.BookPublisherId = bookP1.Id;
            price1.BookPrice = 1480.00;
            _context.Prices.Add(price1);
            _context.SaveChanges();

            BookDiscount bookD = new BookDiscount();
            bookD.BookPublisherId = bookP1.Id;
            bookD.DiscountPercentage = 10;
            bookD.StartsFrom = new DateTime(2023, 06, 06);
            bookD.EndsAt = new DateTime(2023, 06, 09);
            _context.BookDiscounts.Add(bookD);
            _context.SaveChanges();

            File file1 = new File();
            file1.Size = 200;
            file1.BookPublisherId = bookP1.Id;
            file1.Path = "book1.png";
            _context.Files.Add(file1);
            _context.SaveChanges();

            BookPublisher bookP2 = new BookPublisher();
            bookP2.NumberOfPages = 756;
            bookP2.BookCover = "Softcover";
            bookP2.BookFormat = "14x20";
            bookP2.BookWritingSystem = "Latin Alphabet";
            bookP2.Year = 2017;
            bookP2.BookId = book2.Id;
            bookP2.PublisherId = publisher4.Id;
            _context.BookPublishers.Add(bookP2);
            _context.SaveChanges();

            Price price2 = new Price();
            price2.BookPublisherId = bookP2.Id;
            price2.BookPrice = 1400.00;
            _context.Prices.Add(price2);
            _context.SaveChanges();

            BookDiscount bookD2 = new BookDiscount();
            bookD2.BookPublisherId = bookP2.Id;
            bookD2.DiscountPercentage = 15;
            bookD2.StartsFrom = new DateTime(2023, 06, 06);
            bookD2.EndsAt = new DateTime(2023, 06, 12);
            _context.BookDiscounts.Add(bookD2);
            _context.SaveChanges();

            File file2 = new File();
            file2.Size = 200;
            file2.BookPublisherId = bookP2.Id;
            file2.Path = "book2.png";
            _context.Files.Add(file2);
            _context.SaveChanges();

            BookPublisher bookP3 = new BookPublisher();
            bookP3.NumberOfPages =121;
            bookP3.BookCover = "Softcover";
            bookP3.BookFormat = "14x20";
            bookP3.BookWritingSystem = "Latin Alphabet";
            bookP3.Year = 2011;
            bookP3.BookId = book3.Id;
            bookP3.PublisherId = publisher3.Id;
            _context.BookPublishers.Add(bookP3);
            _context.SaveChanges();

            Price price3 = new Price();
            price3.BookPublisherId = bookP3.Id;
            price3.BookPrice = 1782.00;
            _context.Prices.Add(price3);
            _context.SaveChanges();

            File file3 = new File();
            file3.Size = 100;
            file3.BookPublisherId = bookP3.Id;
            file3.Path = "book3.png";
            _context.Files.Add(file3);
            _context.SaveChanges();

            return StatusCode(201);
        }

        
    }
}
