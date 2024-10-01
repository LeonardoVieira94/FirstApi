using APICatalog.Models;

namespace APICatalog.DTOs.Mappings;

public static class CategoryDTOMappingExtensions
{
    public static CategoryDTO? ToCategoryDTO(this Category category)
    {
        if (category is null)
        {
            return null;
        }

        return new CategoryDTO()
        {
            Name = category.Name,
            CategoryId = category.CategoryId,
            ImageUrl = category.ImageUrl
        };
    }

    public static Category? ToCategory(this CategoryDTO categoryDto)
    {
        if (categoryDto is null)
        {
            return null;
        }
        return new Category()
        {
            Name = categoryDto.Name,
            ImageUrl = categoryDto.ImageUrl,
            CategoryId = categoryDto.CategoryId
        };
    }
    public static IEnumerable<CategoryDTO> ToCategoryDTOList(this IEnumerable<Category> categories)
    {
        if (categories is null || !categories.Any())
        {
            return new List<CategoryDTO>();
        }

        return categories.Select(category => new CategoryDTO() 
        { Name = category.Name, 
            CategoryId = category.CategoryId, 
            ImageUrl = category.ImageUrl
        }).ToList();
    }
}
