using DAL.Models;
using DAL.Models.ViewModels;
using Listeo.App_Code;
using Listeo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Listeo.Controllers
{
    public class ApartmentsController : BaseController
    {
        [HttpGet]
        public ActionResult ApartmentDetails(int id)
        {

            ApartmentListViewModel apartmentListViewModel = new ApartmentListViewModel();

            try
            {
                DAL.Models.ViewModels.ApartmentViewModel apartment = Repository.ApartmentRepository.GetApartment(id);

                if (apartment == null || apartment.StatusId != (int)Models.ApartmentStatus.Slobodno)
                {
                    return RedirectToAction("NotFound", "Errors");
                }

                IList<Review> apartmentReviews = Repository.ReviewRepository.GetReviews(id).Collection;

                FillApartmentListViewModel(apartmentListViewModel, apartment, apartmentReviews, id);

            }
            catch (Exception)
            {
                ViewBag.Error = "There was a problem. Please contact the site administrator.";
            }

            return View(apartmentListViewModel);
        }

        [HttpPost]
        public JsonResult Reservation(int ApartmentId, int? UserId, string UserName, string UserEmail, string UserPhone, string UserAddress, string Details)
        {
            object jsonData;
            ReservationViewModel reservationViewModel = new ReservationViewModel
            {
                GUID = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ApartmentID = ApartmentId,
                UserID = UserId,
                UserName = UserName,
                UserEmail = UserEmail,
                UserPhone = UserPhone,
                UserAddress = UserAddress,
                Details = Details,                 
            };

            try
            {
                if (Repository.ApartmentRepository.AddApartmentReservation(reservationViewModel))
                {
                    jsonData = new { success = true, message = "Successful booking" };
                } 
                else
                {
                    jsonData = new { success = false, message = "Booking faild. Please try again." };
                }

            }
            catch (Exception ex)
            {
                jsonData = new { success = false, message = ex.Message };
            }

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult Review(int ApartmentId, int UserId, string Details, int Stars)
        {
            object jsonData;
            ReviewViewModel reviewViewModel = new ReviewViewModel
            {
                GUID = Guid.NewGuid(),
                CreatedAt = DateTime.Now,
                ApartmentID = ApartmentId,
                UserID = UserId,
                Details = Details,
                Stars = Stars
            };

            try
            {
                if (Repository.ApartmentRepository.AddApartmentReview(reviewViewModel))
                {
                    jsonData = new { success = true, message = "Successful review" };
                }
                else
                {
                    jsonData = new { success = false, message = "Review faild. Please try again." };
                }

            }
            catch (Exception ex)
            {
                jsonData = new { success = false, message = ex.Message };
            }

            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }


        private void FillApartmentListViewModel(ApartmentListViewModel apartmentListViewModel, DAL.Models.ViewModels.ApartmentViewModel apartment, IList<Review> apartmentReviews, int id)
        {
            apartmentListViewModel.ApartmentId = id;
            apartmentListViewModel.Name = apartment.Name;
            apartmentListViewModel.Address = apartment.Address;
            apartmentListViewModel.City = apartment.City;
            apartmentListViewModel.Price = apartment.Price;
            apartmentListViewModel.Owner = apartment.Owner;
            apartmentListViewModel.TotalRooms = apartment.TotalRooms;
            apartmentListViewModel.MaxAdults = apartment.MaxAdults;
            apartmentListViewModel.MaxChildren = apartment.MaxChildren;
            apartmentListViewModel.BeachDistance = apartment.BeachDistance;
            apartmentListViewModel.Gallery = apartment.Pictures;
            apartmentListViewModel.Tags = apartment.Tags;
            apartmentListViewModel.Reviews = apartmentReviews;
            apartmentListViewModel.User = Sentinel.IsLoggedIn() ? (User)Session["User"] : null;
        }
    }
}