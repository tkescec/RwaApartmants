using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Models;
using DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class AddApartment : MainPage
    {
        private IList<ApartmentOwner> _allApartmentOwners;
        private IList<City> _allCities;
        private IList<Tag> _allTags;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtApartmentName.Focus();

                if (!IsPostBack)
                {
                    GetData(typeof(ApartmentOwner));
                    GetData(typeof(City));
                    GetData(typeof(Tag));
                }

            }
            catch (Exception)
            {
                AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlertModel
                {
                    Title = "Greška!",
                    Text = "Došlo je do problema. Molimo kontaktirajte administratora stranice."
                });
            }
        }

        protected void btnAddNewApartment_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    Guid guid = Guid.NewGuid();
                    IList<ApartmentPictureViewModel> apartmentPictures = UploadImages(fileApartmentImages, guid);

                    ApartmentViewModel apartmentModel = new ApartmentViewModel()
                    {
                        GUID = guid,
                        CreatedAt = DateTime.Now,
                        OwnerId = Int32.Parse(ddlOwner.SelectedValue),
                        TypeId = 999,
                        StatusId = 3,
                        Name = txtApartmentName.Text,
                        NameEng = txtApartmentNameEng.Text,
                        CityId = Int32.Parse(ddlApartmentCity.SelectedValue),
                        Address = txtApartmentAddress.Text,
                        Pictures = apartmentPictures,
                        TotalRooms = TryIntParse(txtApartmentRooms.Text),
                        MaxAdults = TryIntParse(txtApartmentAdults.Text),
                        MaxChildren = TryIntParse(txtApartmentChilds.Text),
                        BeachDistance = TryIntParse(txtApartmentBeachDistance.Text),
                        Tags = GetSelectedTags(),
                        Price = ConvertCurrencyStringToDecimal(txtApartmentPrice.Text, ",", "€"),
                    };

                    if (Repositories.ApartmentRepository.AddApartment(apartmentModel))
                    {
                        AlertService.ShowAlert(Page, AlertService.AlertType.Success, new SweetAlertModel
                        {
                            Title = "Uspjeh!",
                            Text = "Uspješno ste dodali novi apartman."
                        });

                        ResetForm();

                        return;
                    }

                    AlertService.ShowAlert(Page, AlertService.AlertType.Info, new SweetAlertModel
                    {
                        Title = "Info!",
                        Text = "Nismo uspjeli dodati novi apartman!"
                    });
                }
                catch (Exception ex)
                {
                    AlertService.ShowAlert(Page, AlertService.AlertType.Error, new SweetAlertModel
                    {
                        Title = "Greška!",
                        Text = "Došlo je do problema. Molimo kontaktirajte administratora stranice."
                    });
                }
            }
        }

        private void GetData(Type type)
        {
            try
            {
                if (type == typeof(ApartmentOwner))
                {
                    _allApartmentOwners = Repositories.ApartmentRepository.GetApartmentOwners();
                    BindData(ddlOwner, _allApartmentOwners, "Name", "OwnerID");
                }
                else if (type == typeof(City))
                {
                    _allCities = Repositories.CityRepository.GetCities();
                    BindData(ddlApartmentCity, _allCities, "Name", "CityID");
                }
                else if (type == typeof(Tag))
                {
                    _allTags = Repositories.TagRepository.GetTags().Collection;
                    BindData(cblApartmentTags, _allTags, "Name", "TagID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private IList<ApartmentPictureViewModel> UploadImages(FileUpload fileUpload, Guid guid)
        {
            int count = 1;
            IList<ApartmentPictureViewModel> apartmentPictures = new List<ApartmentPictureViewModel>();

            if (fileUpload.HasFiles)
            {
                string apartmentImageDirectory = "~/Images/" + guid.ToString().ToUpper() + "/";

                if (!System.IO.Directory.Exists(Server.MapPath(apartmentImageDirectory)))
                {
                    System.IO.Directory.CreateDirectory(Server.MapPath(apartmentImageDirectory));
                }

                foreach (HttpPostedFile uploadedFile in fileUpload.PostedFiles)
                {
                    if (System.Web.MimeMapping.GetMimeMapping(uploadedFile.FileName).StartsWith("image/"))
                    {

                        string apartmentImagePath = System.IO.Path.Combine(Server.MapPath(apartmentImageDirectory), uploadedFile.FileName);

                        uploadedFile.SaveAs(apartmentImagePath);

                        using (System.Drawing.Image image = System.Drawing.Image.FromFile(apartmentImagePath))
                        {
                            using (MemoryStream m = new MemoryStream())
                            {
                                image.Save(m, image.RawFormat);
                                byte[] imageBytes = m.ToArray();
                                string base64String = Convert.ToBase64String(imageBytes);

                                ApartmentPictureViewModel apartmentPictureViewModel = new ApartmentPictureViewModel()
                                {
                                    GUID = Guid.NewGuid(),
                                    CreatedAt = DateTime.Now,
                                    Path = System.IO.Path.Combine(guid.ToString().ToUpper() + "/", uploadedFile.FileName),
                                    Base64Content = base64String,
                                    Name = "",
                                    IsRepresentative = count == 1
                                };

                                apartmentPictures.Add(apartmentPictureViewModel);
                            }
                        }

                        count++;
                    }
                }
            }

            return apartmentPictures;
        }

        private IList<int> GetSelectedTags()
        {
            IList<int> tags = new List<int>();

            foreach (ListItem listItem in cblApartmentTags.Items)
            {
                if (listItem.Selected)
                {
                    tags.Add(Int32.Parse(listItem.Value));
                }
            }

            return tags;
        }

        private void ResetForm()
        {
            ddlOwner.SelectedIndex = 0;
            ddlApartmentCity.SelectedIndex = 0;
            txtApartmentName.Text = "";
            txtApartmentNameEng.Text = "";
            txtApartmentAddress.Text = "";
            txtApartmentRooms.Text = "";
            txtApartmentAdults.Text = "";
            txtApartmentChilds.Text = "";
            txtApartmentBeachDistance.Text = "";
            txtApartmentPrice.Text = "";

            foreach (ListItem li in cblApartmentTags.Items)
            {
                li.Selected = false;
            }
        }
    }
}