﻿using Meowv.Blog.Dto.Blog;
using Meowv.Blog.Response;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Meowv.Blog.Blog.Impl
{
    public partial class BlogService
    {
        /// <summary>
        /// Get category list.
        /// </summary>
        /// <returns></returns>
        public async Task<BlogResponse<List<GetCategoryDto>>> GetCategoriesAsync()
        {
            return await _cache.GetCategoriesAsync(async () =>
            {
                var response = new BlogResponse<List<GetCategoryDto>>();

                var categories = await _categories.GetListAsync();

                var result = categories.Select(x => new GetCategoryDto
                {
                    Name = x.Name,
                    Alias = x.Alias,
                    Total = _posts.GetCountByCategoryAsync(x.Id).Result
                }).Where(x => x.Total > 0).ToList();

                response.Result = result;
                return response;
            });
        }
    }
}