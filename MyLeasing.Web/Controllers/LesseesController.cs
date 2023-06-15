using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;


namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {

        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;
        public LesseesController(ILesseeRepository lesseeRepository,
            IUserHelper userHelper,
            IBlobHelper blobHelper,
            IConverterHelper converterHelper)
        {
            _lesseeRepository = lesseeRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }

        // GET: Lessees
        public IActionResult Index()
        {
            return View(_lesseeRepository.GetAll().OrderBy(P => P.FirstName));

        }

        // GET: Lessees/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetLesseeByIdWithUserAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // GET: Lessees/Create
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(LesseeViewModel model)
        {
            var email = Request.Form["Email"].ToString();
            email = model.FullName.Replace(" ", "_") + "@Email.com";
            var password = Request.Form["Password"].ToString();
            password = "123456";

            if (ModelState.IsValid)
            {
                Guid imageId = Guid.NewGuid();

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "lessees");
                }


                var user = await _userHelper.CreateUserAsync(model.FullName, email, password, model.CellPhone, model.Document, model.Address);
                model.user = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var lessee = _converterHelper.ToLesse(model, imageId, true);
                await _lesseeRepository.CreateAsync(lessee);
                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }

        // GET: Lessees/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetLesseeByIdWithUserAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToLesseeViewModel(lessee);
            return View(model);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(LesseeViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;

                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "lessees");
                    }
                    var lessee = _converterHelper.ToLesse(model, imageId, true);


                    var editedLessee = await _lesseeRepository.GetLesseeByIdWithUserAsync(model.Id);

                    editedLessee.Document = lessee.Document;
                    editedLessee.FirstName = lessee.FirstName;
                    editedLessee.LastName = lessee.LastName;
                    editedLessee.FixedPhone = lessee.FixedPhone;
                    editedLessee.CellPhone = lessee.CellPhone;
                    editedLessee.Address = lessee.Address;
                    editedLessee.ImageId = lessee.ImageId;


                    await _lesseeRepository.UpdateAsync(editedLessee);

                    await _userHelper.UpdateUserAsync(editedLessee.user, editedLessee.FullName, editedLessee.Document, editedLessee.CellPhone, editedLessee.Address);



                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _lesseeRepository.ExistAsync(model))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(model);
        }

        // GET: Lessees/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lessee = await _lesseeRepository.GetLesseeByIdWithUserAsync(id.Value);
            if (lessee == null)
            {
                return NotFound();
            }

            return View(lessee);
        }

        // POST: Lessees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var lessee = await _lesseeRepository.GetLesseeByIdWithUserAsync(id);
            User user = lessee.user;
            await _blobHelper.DeleteImageAsync(lessee.ImageFullPath);
            await _lesseeRepository.DeleteAsync(lessee);
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }

    }
}
