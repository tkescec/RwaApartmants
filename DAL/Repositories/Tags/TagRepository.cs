using DAL.Collection;
using DAL.Models;
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
                TotalRecords = 0
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetTags), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);

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
                    new Tag
                    {
                        TagID = (int)row[nameof(Tag.TagID)],
                        Name = row[nameof(Tag.Name)].ToString(),
                        NameEng = row[nameof(Tag.NameEng)].ToString(),
                        TypeName = row[nameof(Tag.TypeName)].ToString(),
                        TypeNameEng = row[nameof(Tag.TypeNameEng)].ToString(),
                        Picture = ImagesCollection.TAG_IMAGE,
                        ApartmentCount = (int)row[nameof(Tag.ApartmentCount)]
                    }
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
    }
}
