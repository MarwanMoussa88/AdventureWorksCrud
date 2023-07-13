using Microsoft.AspNetCore.Mvc;
using Scott.DataAccess.Data;
using Scott.DataAccess.Repository;
using Scott.DataAccess.Repository.IRepository;
using Scott.Models;
using ScottApi.Models;
using System.Diagnostics;

namespace ScottApi.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Index()
        {

            Address address = _unitOfWork.Address.GetOne(u => u.AddressId == 1);
            address.AddressLine2 = "Testing 123";
            _unitOfWork.Address.Update(address);
            _unitOfWork.Save();

            return View();
        }

        #region Api calls
        [HttpGet]
        public IActionResult GetOne(int id)
        {
            Address address = _unitOfWork.Address.GetOne(u => u.AddressId == id);
            return Json(new {data=address});
        }
        public IActionResult GetAll()
        {
            List<Address> address = _unitOfWork.Address.GetAll().ToList();
            return Json(new { data = address });
        }
        #endregion
    }
}