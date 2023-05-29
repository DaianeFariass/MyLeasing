using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly IUserHelper _userHelper;
  
        public OwnersController(IOwnerRepository ownerRepository, IUserHelper userHelper)
        {
            _ownerRepository = ownerRepository;
            _userHelper = userHelper;
         
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Owners/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Document,OwnerName,FixedPhone,CellPhone,Address")] Owner owner)
        {
            var email = Request.Form["Email"].ToString();         
            email = owner.OwnerName.Replace(" ", "_") + "@Email.com";
            var password = Request.Form["Password"].ToString();
            password = "123456";

            if (string.IsNullOrEmpty(owner.OwnerName) || owner.OwnerName.Split(' ').Length < 2)
            {
                ModelState.AddModelError("Name", "Please enter a full name with at least two names.");
            }
            if (ModelState.IsValid)
            {
                var user = await _userHelper.CreateUserAsync(owner.OwnerName, email, password, owner.CellPhone, owner.Document);
                owner.User = await _userHelper.GetUserByEmailAsync(email);
                await _ownerRepository.CreateAsync(owner);
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        
        }

        // GET: Owners/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
            if (owner == null)
            {
                return NotFound();
            }
            return View(owner);
        }

        // POST: Owners/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Owner owner)
        {
            if (id != owner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                   
                    var editedOwner = await _ownerRepository.GetByIdAsync(id);
                    editedOwner.User = await _userHelper.GetUserByIdAsync(editedOwner.UserId);
                    editedOwner.Document = owner.Document;
                    editedOwner.OwnerName = owner.OwnerName;
                    editedOwner.CellPhone= owner.CellPhone;
                    editedOwner.Address = owner.Address;

                                    
                    await _ownerRepository.UpdateAsync(editedOwner);

                    await _userHelper.UpdateUserAsync(editedOwner.User, owner.OwnerName, owner.Address, owner.CellPhone,owner.Document);

                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!await _ownerRepository.ExistAsync(owner))
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
            var own = await _ownerRepository.GetByIdAsync(id);
            own.User = await _userHelper.GetUserByIdAsync(owner.UserId);
            return View(owner);
        }
        // GET: Owners/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = await _ownerRepository.GetByIdAsync(id.Value);
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
            var owner = await _ownerRepository.GetByIdAsync(id); 
            owner.User = await _userHelper.GetUserByIdAsync(owner.UserId);
            await _ownerRepository.DeleteAsync(owner);
            await _userHelper.DeleteUserAsync(owner.User);


            return RedirectToAction(nameof(Index));
        }
    }
}
