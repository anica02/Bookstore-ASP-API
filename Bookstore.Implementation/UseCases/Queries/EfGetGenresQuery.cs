using Bookstore.Application.UseCase.Queries.Seraches;
using Bookstore.Application.UseCases.DTO;
using Bookstore.Application.UseCases.Queries;
using Bookstore.DataAccess;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bookstore.Implementation.UseCases.Queries
{
    public class EfGetGenresQuery : EfUseCase, IGetGenresQuery
    {
        public EfGetGenresQuery(BookstoreContext context) : base(context)
        {
        }

        public int Id => 37;

        public string Name => "Search genres";

        public string Description => "";

        public IEnumerable<ReadGenreDto> Execute(GenreSearch search)
        {


            var query = Context.Genres.AsQueryable();

            if (!string.IsNullOrEmpty(search.Name))
            {
                query = query.Where(x => x.Name.Contains(search.Name));
            }

            IEnumerable<ReadGenreDto> result = query.Select(x => new ReadGenreDto
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

            foreach (var genre in result)
            {

                foreach (var sub in genre.Subgenres)
                {
                    HandleSubgenres(sub);
                }
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
