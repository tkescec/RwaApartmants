using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Collection
{
    public static class QueryStringCollection
    {
        public const string APARTMENT_TAGS = "SELECT T.id as TagId, T.Name, T.NameEng, TT.Name as TypeName, TT.NameEng as TypeNameEng FROM dbo.Tag AS T LEFT JOIN dbo.TagType AS TT ON TT.Id = T.TypeId LEFT JOIN dbo.TaggedApartment AS TA ON TA.TagId = T.Id WHERE TA.ApartmentId = @apartmentId";
        public const string APARTMENT_PICTURES = "SELECT Path, Base64Content, Name, IsRepresentative FROM dbo.ApartmentPicture WHERE ApartmentId = @apartmentId";
    }
}
