using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;
using MyLeasing.Web.Models;


namespace MyLeasing.Web.Controllers
{

    public class OwnersController : Controller
    {

        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
        private readonly IBlobHelper _blobHelper;
        private readonly IConverterHelper _converterHelper;

        public OwnersController(IOwnerRepository ownerRepository,
            IUserHelper userHelper,
            IBlobHelper blobHelper,
            IConverterHelper converterHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
            _blobHelper = blobHelper;
            _converterHelper = converterHelper;
        }


        // GET: Owners
        public IActionResult Index()
        {
            return View(_ownerRepository.GetAll().OrderBy(p => p.OwnerName));

        }

        // GET: Owners/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value); //Passa também o valor nulo 

            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }


        // GET: Owners/Create

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OwnerViewModel model)
        {

            var email = Request.Form["Email"].ToString();
            email = model.OwnerName.Replace(" ", "_") + "@Email.com";
            var password = Request.Form["Password"].ToString();
            password = "123456";

            if (string.IsNullOrEmpty(model.OwnerName) || model.OwnerName.Split(' ').Length < 2)
            {
                ModelState.AddModelError("Name", "Please insert your full name!");
            }
            if (ModelState.IsValid)
            {
                Guid imageId = Guid.NewGuid();

                if (model.ImageFile != null && model.ImageFile.Length > 0)
                {
                    imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                }

                var user = await _userHelper.CreateUserAsync(model.OwnerName, email, password, model.Document, model.CellPhone, model.Address);
                model.User = await _userHelper.GetUserByEmailAsync(this.User.Identity.Name);
                var owner = _converterHelper.ToOwner(model, imageId, true);
                await _ownerRepository.CreateAsync(owner);

                return RedirectToAction(nameof(Index));
            }
            return View(model);

        }


        // GET: Owners/Edit/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetOwnerByIdWithUserAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }
            var model = _converterHelper.ToOwnerViewModel(owner);
            return View(model);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(OwnerViewModel model)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    Guid imageId = model.ImageId;
                    if (model.ImageFile != null && model.ImageFile.Length > 0)
                    {
                        imageId = await _blobHelper.UploadBlobAsync(model.ImageFile, "owners");
                    }

                    var owner = _converterHelper.ToOwner(model, imageId, false);


                    var editedOwner = await _ownerRepository.GetOwnerByIdWithUserAsync(model.Id);

                    editedOwner.Document = owner.Document;
                    editedOwner.OwnerName = owner.OwnerName;
                    editedOwner.Address = owner.Address;
                    editedOwner.CellPhone = owner.CellPhone;
                    editedOwner.ImageId = owner.ImageId;


                    await _ownerRepository.UpdateAsync(editedOwner);


                    await _userHelper.UpdateUserAsync(editedOwner.User, editedOwner.OwnerName, editedOwner.Address, editedOwner.CellPhone, editedOwner.Document);


                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(model))
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
        // GET: Owners/Delete/5
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetOwnerByIdWithUserAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }

            return View(owner);
        }


        // POST: Owners/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var owner = await _ownerRepository.GetOwnerByIdWithUserAsync(id);
            User user = owner.User;
            await _blobHelper.DeleteImageAsync(owner.ImageFullPath);
            await _ownerRepository.DeleteAsync(owner);
            await _userHelper.DeleteUserAsync(user);
            return RedirectToAction(nameof(Index));
        }
    }

}
