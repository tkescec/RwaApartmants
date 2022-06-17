using DAL.Collection;
using DAL.Models;
using DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Tags
{
    public interface ITagRepository : IRepository<Tag>
    {
        PaginationCollection<Tag> GetTags();
        PaginationCollection<Tag> GetTags(int iPageIndex, int iPageSize);
        bool DeleteTags(int? tagId);
        IList<TagType> GetTagTypes();
        bool AddTag(TagViewModel tag);
    }
}
