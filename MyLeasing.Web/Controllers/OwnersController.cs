using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Commom.Data;
using MyLeasing.Web.Data.Entities;


namespace MyLeasing.Web.Controllers
{
    public class OwnersController : Controller
    {
        private readonly IRepository _repository;

        public OwnersController(IRepository repository)
        {
            _repository = repository;
        }

        // GET: Owners
        public IActionResult Index()
        {
            return View(_repository.GetOwners());
        }

        // GET: Owners/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = _repository.GetOwner(id.Value); //Passa também o valor nulo 
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
        public async Task<IActionResult> Create(Owner owner)
        {
            if (ModelState.IsValid)
            {

                _repository.AddOwner(owner);
                await _repository.SaveAllAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(owner);
        }

        // GET: Owners/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = _repository.GetOwner(id.Value);
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
                    _repository.UpdaterOwner(owner);
                    await _repository.SaveAllAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_repository.OwnerExists(owner.Id))
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
            return View(owner);
        }

        // GET: Owners/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var owner = _repository.GetOwner(id.Value);
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
            var owner = _repository.GetOwner(id);
            _repository.RemoveOwner(owner);
            await _repository.SaveAllAsync();
            return RedirectToAction(nameof(Index));
        }
    }
}
