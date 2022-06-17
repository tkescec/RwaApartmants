using Administrator.Models.ViewModels;
using Administrator.Services;
using Administrator.Views.Shared;
using DAL.Models;
using DAL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Administrator
{
    public partial class AddTag : MainPage
    {
        private IList<TagType> _allTagTypes;
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                txtTagName.Focus();

                if (!IsPostBack)
                {
                    GetData(typeof(TagType));
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

        protected void btnAddNewTag_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                try
                {
                    TagViewModel tagModel = new TagViewModel()
                    {
                        GUID = Guid.NewGuid(),
                        CreatedAt = DateTime.Now,
                        Name = txtTagName.Text,
                        NameEng = txtTagNameEng.Text,
                        TypeId = Int32.Parse(ddlTagType.SelectedValue)
                    };

                    if (Repositories.TagRepository.AddTag(tagModel))
                    {
                        AlertService.ShowAlert(Page, AlertService.AlertType.Success, new SweetAlertModel
                        {
                            Title = "Uspjeh!",
                            Text = "Uspješno ste dodali novi tag."
                        });

                        ResetForm();

                        return;
                    }

                    AlertService.ShowAlert(Page, AlertService.AlertType.Info, new SweetAlertModel
                    {
                        Title = "Info!",
                        Text = "Nismo uspjeli dodati novi tag!"
                    });
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
        }

        private void ResetForm()
        {
            txtTagName.Text = "";
            txtTagNameEng.Text = "";
            ddlTagType.SelectedIndex = 0;
        }

        private void GetData(Type type)
        {
            try
            {
                if (type == typeof(TagType))
                {
                    _allTagTypes = Repositories.TagRepository.GetTagTypes();
                    BindData(ddlTagType, _allTagTypes, "Name", "TypeID");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}