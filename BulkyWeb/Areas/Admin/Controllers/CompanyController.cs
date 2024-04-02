using Bulky.DataAccess.Repository.IRepository;
using Bulky.Models;
using Bulky.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace BulkyWeb.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = SD.Role_Admin)]
    public class CompanyController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public CompanyController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Company> objCompany = _unitOfWork.Company.GetAll().ToList();
            return View(objCompany);
        }

        public IActionResult Upsert(int? id)
        {

            if(id == null || id == 0)
            {
                //create
                return View(new Company());
            }
            else
            {
                //update
                Company companyObj = _unitOfWork.Company.Get(u=>u.Id==id);
                return View(companyObj);
            }
        }

        [HttpPost]
        public IActionResult Upsert(Company companyObj)
        {
            if (ModelState.IsValid)
            {

                if (companyObj.Id == 0) 
                { 
                    _unitOfWork.Company.Add(companyObj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company created successfully";
                    return RedirectToAction("Index");
                }
                else 
                {
                    _unitOfWork.Company.Update(companyObj);
                    _unitOfWork.Save();
                    TempData["success"] = "Company updated successfully";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                return View(companyObj);
            }
        }

        #region API CALLS

        [HttpGet]
        public IActionResult GetAll()
        {
            List<Company> objCompanyList = _unitOfWork.Company.GetAll().ToList();
            return Json(new { data = objCompanyList });
        }

        public IActionResult Delete(int? id)
        {
            var CompanyToBeDeleted = _unitOfWork.Company.Get(u=>u.Id == id);
            if(CompanyToBeDeleted == null)
            {
                return Json(new {success = false, message = "Error while deleting" });
            }

            _unitOfWork.Company.Delete(CompanyToBeDeleted);
            _unitOfWork.Save();

            return Json(new { success = false, message = "Delete Successfull" });
        }
        #endregion
    }
}
