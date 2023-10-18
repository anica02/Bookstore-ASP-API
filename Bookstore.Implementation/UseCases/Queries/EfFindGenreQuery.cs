using Bookstore.Application.Exceptions;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bookstore.Implementation.Extensions;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfFindGenreQuery:EfUseCase, IFindGenreQuery
    {
        public EfFindGenreQuery(BookstoreContext context):base(context)
        {

        }

        public int Id => 26;

        public string Name => "Find genre";

        public string Description => "";

        public ReadGenreDto Execute(int search)
        {
            var genre = Context.Genres.Include(x => x.Subgenres).FirstOrDefault(x => x.Id == search);

            if (genre == null || !genre.IsActive || genre.DeletedAt.HasValue)
            {
                throw new EntityNotFoundException(search, nameof(genre));
            }


            ReadGenreDto result = new ReadGenreDto
            {
                Id = genre.Id,
                Name = genre.Name,
                ParentId = genre.ParentId,
                Subgenres = genre.Subgenres.Select(s => new ReadGenreDto
                {
                    Id = s.Id,
                    Name = s.Name,
                    ParentId = s.ParentId

                }).ToList()
            };

            foreach (var sub in result.Subgenres)
            {

                HandleSubgenres(sub);
            }

            return result;
        }

        private void HandleSubgenres(ReadGenreDto dto)
        {
            var context = new BookstoreContext();

            var subgenres = context.Genres.Where(x => x.ParentId == dto.Id)
                                             .Select(x => new ReadGenreDto
                                             {
                                                 Id = x.Id,
                                                 Name = x.Name,
                                                 ParentId = x.ParentId,
                                                 Subgenres = x.Subgenres.Select(s => new ReadGenreDto
                                                 {
                                                     Id = s.Id,
                                                     Name = s.Name,
                                                     ParentId = s.ParentId
                                                 })
                                             }).ToList();

            dto.Subgenres = subgenres;

            foreach (var sub in subgenres)
            {
                HandleSubgenres(sub);
            }
        }

       
    }
}
