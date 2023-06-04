using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using MyLeasing.Web.Data;
using MyLeasing.Web.Data.Entities;
using MyLeasing.Web.Helpers;

namespace MyLeasing.Web.Controllers
{
    public class LesseesController : Controller
    {

        private readonly ILesseeRepository _lesseeRepository;
        private readonly IUserHelper _userHelper;

        public LesseesController(ILesseeRepository lesseeRepository,
            IUserHelper userHelper)
        {
            _lesseeRepository = lesseeRepository;
            _userHelper = userHelper;
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
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lessees/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Lessee lessee)
        {
            var email = Request.Form["Email"].ToString();
            email = lessee.FullName.Replace(" ", "_") + "@Email.com";
            var password = Request.Form["Password"].ToString();
            password = "123456";

            if (ModelState.IsValid)
            {

                var user = await _userHelper.CreateUserAsync(lessee.FullName, email, password, lessee.CellPhone, lessee.Document, lessee.Address);
                lessee.user = await _userHelper.GetUserByEmailAsync(email);
                await _lesseeRepository.CreateAsync(lessee);
                return RedirectToAction(nameof(Index));
            }
            return View(lessee);
            
        }

        // GET: Lessees/Edit/5
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
        
            return View(lessee);
        }

        // POST: Lessees/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Lessee lessee)
        {
            if (id != lessee.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var editedLessee = await  _lesseeRepository.GetLesseeByIdWithUserAsync(id);
                    editedLessee.Document= lessee.Document;
                    editedLessee.FirstName= lessee.FirstName;
                    editedLessee.LastName= lessee.LastName;
                    editedLessee.FixedPhone= lessee.FixedPhone;
                    editedLessee.CellPhone= lessee.CellPhone;
                    editedLessee.Address= lessee.Address;
                  

                    await _lesseeRepository.UpdateAsync(editedLessee);
                    await _userHelper.UpdateUserAsync(editedLessee.user, editedLessee.FullName, editedLessee.Document, editedLessee.CellPhone, editedLessee.Address);
                   

                   
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (! await _lesseeRepository.ExistAsync(lessee))
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
            return View(lessee);
        }

        // GET: Lessees/Delete/5
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
            await _lesseeRepository.DeleteAsync(lessee);
            return RedirectToAction(nameof(Index));
        }

      
    }
}
