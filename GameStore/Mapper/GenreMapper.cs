using GameStore.Dtos;
using GameStore.Entities;

namespace GameStore.Mapper;

public static class GenreMapper
{
    public static GenreDto ToDto(this Genre genre)
    {
        return new GenreDto(
            genre.Id,
            genre.Name
        );
    }

    public static Genre ToEntity(this GenreDto genreDto)
    {
        return new Genre
        {
            Id = genreDto.Id,
            Name = genreDto.Name
        };
    }
}
