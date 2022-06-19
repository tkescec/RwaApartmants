using DAL.Collection;
using DAL.Models;
using DAL.Models.ViewModels;
using Microsoft.ApplicationBlocks.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.Apartments
{
    internal class ApartmentRepository : IApartmentRepository
    {
        public string CS { get; }

        public ApartmentRepository(string cS)
        {
            CS = cS;
        }

        public PaginationCollection<Apartment> GetApartments()
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                TotalRecords = 0,
            };

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[0].Value);


            return pagination;
        }

        public PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize)
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Apartment>()
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

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[2].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateApartmentModel(row)
                );
            }

            return pagination;
        }

        public PaginationCollection<Apartment> GetApartments(int iPageIndex, int iPageSize, int iCityFilter, int iStatusFilter, string iSortFilter)
        {
            PaginationCollection<Apartment> pagination = new PaginationCollection<Apartment>
            {
                PageIndex = iPageIndex,
                PageSize = iPageSize,
                TotalRecords = 0,
                Collection = new List<Apartment>()
            };

            SqlParameter[] spParameter = new SqlParameter[7];

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

            if (iCityFilter != 0)
            {
                spParameter[2] = new SqlParameter("@FilterByCity", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = iCityFilter
                };
            }

            if (iStatusFilter != 0)
            {
                spParameter[3] = new SqlParameter("@FilterByStatus", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = iStatusFilter
                };
            }

            if (iSortFilter != "")
            {
                string sortColumn = GetSortColumn(iSortFilter);
                string sortDirection = GetSortDirection(iSortFilter);

                spParameter[4] = new SqlParameter("@SortColumn", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = sortColumn
                };
                spParameter[5] = new SqlParameter("@SortDirection", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = sortDirection
                };
            }

            spParameter[6] = new SqlParameter("@RecordCount", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartments), spParameter).Tables[0];

            pagination.TotalRecords = Convert.ToInt32(spParameter[6].Value);

            foreach (DataRow row in tblUsers.Rows)
            {
                pagination.Collection.Add(
                    CreateApartmentModel(row)
                );
            }

            return pagination;
        }

        public IList<ApartmentStatus> GetApartmentStatuses()
        {
            List<ApartmentStatus> apartmentStatuses = new List<ApartmentStatus>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartmentStatuses)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                apartmentStatuses.Add(
                    new ApartmentStatus
                    {
                        StatusID = (int)row[nameof(ApartmentStatus.StatusID)],
                        Name = row[nameof(ApartmentStatus.Name)].ToString()
                    }
                );
            }

            return apartmentStatuses;
        }

        public IList<ApartmentOwner> GetApartmentOwners()
        {
            List<ApartmentOwner> apartmentOwners = new List<ApartmentOwner>();

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartmentOwners)).Tables[0];

            foreach (DataRow row in tblUsers.Rows)
            {
                apartmentOwners.Add(
                    new ApartmentOwner
                    {
                        OwnerID = (int)row[nameof(ApartmentOwner.OwnerID)],
                        Name = row[nameof(ApartmentOwner.Name)].ToString()
                    }
                );
            }

            return apartmentOwners;
        }

        public IList<Tag> GetApartmentTags(int apartmentId)
        {

            IList<Tag> tags = new List<Tag>();

            using (SqlConnection connection = new SqlConnection(CS))
            {
                SqlCommand command = new SqlCommand(QueryStringCollection.APARTMENT_TAGS, connection);
                command.Parameters.AddWithValue("@apartmentId", apartmentId);

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        tags.Add(
                            new Tag
                            {
                                TagID = (int)row[nameof(Tag.TagID)],
                                Name = row[nameof(Tag.Name)].ToString(),
                                NameEng = row[nameof(Tag.NameEng)].ToString(),
                            }
                        );
                    }
                }

            }

            return tags;
        }

        public IList<ApartmentPicture> GetApartmentPictures(int apartmentId)
        {
            IList<ApartmentPicture> apartmentPictures = new List<ApartmentPicture>();

            using (SqlConnection connection = new SqlConnection(CS))
            {
                SqlCommand command = new SqlCommand(QueryStringCollection.APARTMENT_PICTURES, connection);
                command.Parameters.AddWithValue("@apartmentId", apartmentId);

                using (SqlDataAdapter da = new SqlDataAdapter(command))
                {
                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    foreach (DataRow row in dt.Rows)
                    {
                        apartmentPictures.Add(
                            new ApartmentPicture
                            {
                                Path = row[nameof(ApartmentPicture.Path)].ToString(),
                                Base64Content = row[nameof(ApartmentPicture.Base64Content)].ToString(),
                                Name = row[nameof(ApartmentPicture.Name)].ToString(),
                                IsRepresentative = (bool)row[nameof(ApartmentPicture.IsRepresentative)],
                            }
                        );
                    }
                }

            }

            return apartmentPictures;
        }

        public ApartmentViewModel GetApartment(int apartmentId)
        {
            ApartmentViewModel apartment = null;
            IList<Tag> tags = GetApartmentTags(apartmentId);
            IList<ApartmentPicture> pictures = GetApartmentPictures(apartmentId);

            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@ApartmentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartmentId
            };

            var tblUsers = SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(GetApartment), spParameter).Tables[0];


            if (tblUsers.Rows.Count > 0)
            {
                var row = tblUsers.Rows[0];

                apartment = new ApartmentViewModel
                {
                    OwnerId = (int)row[nameof(ApartmentViewModel.OwnerId)],
                    StatusId = (int)row[nameof(ApartmentViewModel.StatusId)],
                    CityId = (int)row[nameof(ApartmentViewModel.CityId)],
                    Name = row[nameof(ApartmentViewModel.Name)].ToString(),
                    NameEng = row[nameof(ApartmentViewModel.NameEng)].ToString(),
                    Price = (decimal)row[nameof(ApartmentViewModel.Price)],
                    MaxAdults = (int)row[nameof(ApartmentViewModel.MaxAdults)],
                    MaxChildren = (int)row[nameof(ApartmentViewModel.MaxChildren)],
                    TotalRooms = (int)row[nameof(ApartmentViewModel.TotalRooms)],
                    BeachDistance = (int)row[nameof(ApartmentViewModel.BeachDistance)],
                    Pictures = pictures,
                    Tags = tags
                };
            }

            return apartment;
        }

        public bool DeleteApartments(int apartmentId)
        {
            SqlParameter[] spParameter = new SqlParameter[1];

            spParameter[0] = new SqlParameter("@ApartmentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartmentId
            };

            // TODO provejriti rezultat i vratiti true or false
            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(DeleteApartments), spParameter);

            return true;
        }

        public bool AddApartment(ApartmentViewModel apartment)
        {
            if (apartment == null)
            {
                return false;
            }

            int? apartmentId = null;
            SqlParameter[] spParameter = new SqlParameter[15];

            spParameter[0] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.GUID
            };

            spParameter[1] = new SqlParameter("@CreatedAt", SqlDbType.DateTime2)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.CreatedAt
            };

            spParameter[2] = new SqlParameter("@Name", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.Name
            };

            spParameter[3] = new SqlParameter("@NameEng", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.NameEng
            };

            spParameter[4] = new SqlParameter("@OwnerId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.OwnerId
            };

            spParameter[5] = new SqlParameter("@TypeId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.TypeId
            };

            spParameter[6] = new SqlParameter("@StatusId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.StatusId
            };

            spParameter[7] = new SqlParameter("@Price", SqlDbType.Money)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.Price
            };

            spParameter[8] = new SqlParameter("@CityId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.CityId
            };

            spParameter[9] = new SqlParameter("@Address", SqlDbType.NVarChar)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.Address
            };

            spParameter[10] = new SqlParameter("@MaxAdults", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.MaxAdults
            };

            spParameter[11] = new SqlParameter("@MaxChildren", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.MaxChildren
            };

            spParameter[12] = new SqlParameter("@TotalRooms", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.TotalRooms
            };

            spParameter[13] = new SqlParameter("@BeachDistance", SqlDbType.Int)
            {
                Direction = ParameterDirection.Input,
                Value = apartment.BeachDistance
            };

            spParameter[14] = new SqlParameter("@ApartmentId", SqlDbType.Int)
            {
                Direction = ParameterDirection.Output,
                DbType = DbType.Int32
            };

            SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(AddApartment), spParameter);

            apartmentId = Convert.ToInt32(spParameter[14].Value);

            if (apartmentId != null)
            {

                AddApartmentTags(apartmentId, apartment.Tags);
                AddApartmentPictures(apartmentId, apartment.Pictures);

                return true;
            }

            return false;
        }

        public bool AddApartmentTags(int? apartmentId, IList<Tag> apartmentTags)
        {
            foreach (Tag tag in apartmentTags)
            {
                SqlParameter[] spParameter = new SqlParameter[2];

                spParameter[0] = new SqlParameter("@ApartmentId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentId
                };

                spParameter[1] = new SqlParameter("@TagId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = tag.TagID
                };

                SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(AddApartmentTags), spParameter);
            }

            return true;
        }

        public bool AddApartmentPictures(int? apartmentId, IList<ApartmentPicture> apartmentPictures)
        {
            foreach (ApartmentPicture apartmentPicture in apartmentPictures)
            {
                SqlParameter[] spParameter = new SqlParameter[7];

                spParameter[0] = new SqlParameter("@Guid", SqlDbType.UniqueIdentifier)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.GUID
                };

                spParameter[1] = new SqlParameter("@CreatedAt", SqlDbType.DateTime2)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.CreatedAt
                };

                spParameter[2] = new SqlParameter("@ApartmentId", SqlDbType.Int)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentId
                };

                spParameter[3] = new SqlParameter("@Path", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.Path
                };

                spParameter[4] = new SqlParameter("@Base64Content", SqlDbType.VarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.Base64Content
                };

                spParameter[5] = new SqlParameter("@Name", SqlDbType.NVarChar)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.Name
                };

                spParameter[6] = new SqlParameter("@IsRepresentative", SqlDbType.Bit)
                {
                    Direction = ParameterDirection.Input,
                    Value = apartmentPicture.IsRepresentative
                };

                SqlHelper.ExecuteDataset(CS, CommandType.StoredProcedure, nameof(AddApartmentPictures), spParameter);
            }

            return true;
        }


        #region Private Methods
        private Apartment CreateApartmentModel(DataRow row)
        {
            return new Apartment
            {
                ApartmentID = (int)row[nameof(Apartment.ApartmentID)],
                Owner = row[nameof(Apartment.Owner)].ToString(),
                Status = row[nameof(Apartment.Status)].ToString(),
                City = row[nameof(Apartment.City)].ToString(),
                Address = row[nameof(Apartment.Address)].ToString(),
                Name = row[nameof(Apartment.Name)].ToString(),
                NameEng = row[nameof(Apartment.NameEng)].ToString(),
                Price = (decimal)row[nameof(Apartment.Price)],
                MaxAdults = (int)row[nameof(Apartment.MaxAdults)],
                MaxChildren = (int)row[nameof(Apartment.MaxChildren)],
                TotalRooms = (int)row[nameof(Apartment.TotalRooms)],
                BeachDistance = (int)row[nameof(Apartment.BeachDistance)],
                Picture = GetFeaturedImage(row[nameof(Apartment.Picture)].ToString()),
                CreatedAt = (DateTime?)(row.IsNull(nameof(Apartment.CreatedAt)) ? null : row[nameof(Apartment.CreatedAt)]),
                DeletedAt = (DateTime?)(row.IsNull(nameof(Apartment.DeletedAt)) ? null : row[nameof(Apartment.DeletedAt)]),
            };
        }
        private Tag CreateTagModel(DataRow row)
        {
            return new Tag
            {
                TagID = (int)row[nameof(Tag.TagID)],
                Name = row[nameof(Tag.Name)].ToString(),
                NameEng = row[nameof(Tag.NameEng)].ToString(),
                TypeName = row[nameof(Tag.TypeName)].ToString(),
                TypeNameEng = row[nameof(Tag.TypeNameEng)].ToString(),
                Picture = row[nameof(Tag.Picture)].ToString(),
                ApartmentCount = (int)row[nameof(Tag.ApartmentCount)],
            };
        }
        private string GetSortDirection(string iSortFilter)
        {
            switch (iSortFilter)
            {
                case "price_asc":
                case "rooms_asc":
                case "cap_asc":
                    return "ASC";
                case "price_desc":
                case "rooms_desc":
                case "cap_desc":
                    return "DESC";
                default:
                    return "ASC";
            }
        }
        private string GetSortColumn(string iSortFilter)
        {
            switch (iSortFilter)
            {
                case "price_asc":
                case "price_desc":
                    return "Price";
                case "rooms_asc":
                case "rooms_desc":
                    return "TotalRooms";
                case "cap_asc":
                case "cap_desc":
                    return "MaxAdults";
                default:
                    return "Price";
            }
        }
        private string GetFeaturedImage(string path)
        {
            if (path == "")
            {
                return ImagesCollection.NO_IMAGE;
            }

            return System.IO.Path.Combine("~/Images/", path);
        }
        #endregion
    }
}
