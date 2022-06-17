using DAL.Collection;
using DAL.Models;
using DAL.Models.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL.Repositories.Tags
{
    public class TagRepository : ITagRepository
    {
        public string CS { get; }

        public TagRepository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Tag> GetTags()
        {
            PaginationCollection<Tag> pagination = new PaginationCollection<Tag>
            {
                TotalRecords = 0,
                Collection = new List<Tag>()
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetTags), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateTagModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Tag> GetTags(int iPageIndex, int iPageSize)
        {
            PaginationCollection<Tag> pagination = new PaginationCollection<Tag>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Tag>()
            };

            SqlParameter[] spParameter = new SqlParameter[3];

            spParameter[0] = new SqlParameter("@PageIndex", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageIndex
            };

            spParameter[1] = new SqlParameter("@PageSize", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = iPageSize
            };

            spParameter[2] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetTags), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateTagModel(row)
                );
            }

            return pagination;
        }

        public bool DeleteTags(int? tagId)
        {
            SqlParameter[] spParameter = new SqlParameter[1];

            if (tagId != null)
            {
                spParameter[0] = new SqlParameter("@TagId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = tagId
                };
            }

            // TODO provejriti rezultat i vratiti true or false
            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(DeleteTags), spParameter);

            return true;
        }

        public IList<TagType> GetTagTypes()
        {
            List<TagType> tagTypes = new List<TagType>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetTagTypes)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                tagTypes.Add(
                    new TagType
                    {
                        TypeID = (int)row[nameof(TagType.TypeID)],
                        Name = row[nameof(TagType.Name)].ToString(),
                        NameEng = row[nameof(TagType.NameEng)].ToString()
                    }
                );
            }

            return tagTypes;
        }

        public bool AddTag(TagViewModel tag)
        {

            if (tag == null)
            {
                return false;
            }

            SqlParameter[] spParameter = new SqlParameter[5];

            spParameter[0] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Input,
                Value = tag.GUID
            };

            spParameter[1] = new SqlParameter("@CreatedAt", SqlDbType.DateTime2)
            {
                Direction = ParameterDirection.Input,
                Value = tag.CreatedAt
            };

            spParameter[2] = new SqlParameter("@Name", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = tag.Name
            };

            spParameter[3] = new SqlParameter("@NameEng", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = tag.NameEng
            };

            spParameter[4] = new SqlParameter("@TypeId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = tag.TypeId
            };

            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(AddTag), spParameter);

            return true;
        }

        #region Private Methods
        private Tag CreateTagModel(DataRow row)
        {
            return new Tag
            {
                TagID = (int)row[nameof(Tag.TagID)],
                Name = row[nameof(Tag.Name)].ToString(),
                NameEng = row[nameof(Tag.NameEng)].ToString(),
                TypeName = row[nameof(Tag.TypeName)].ToString(),
                TypeNameEng = row[nameof(Tag.TypeNameEng)].ToString(),
                Picture = ImagesCollection.TAG_IMAGE,
                ApartmentCount = (int)row[nameof(Tag.ApartmentCount)]
            };
        }
        #endregion
    }
}
