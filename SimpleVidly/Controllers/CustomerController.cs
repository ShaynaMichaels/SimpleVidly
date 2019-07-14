using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using SimpleVidly.Models;
using SimpleVidly.ViewModels;

namespace SimpleVidly.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;

        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        public ViewResult Index()
        {
            var customers = _context.Customers.Include(c => c.Membertype).ToList();

            return View(customers);
        }

        public ActionResult Details(int id)
        {
            var customer = _context.Customers.Include(c => c.Membertype).SingleOrDefault(c => c.Id == id);

            if (customer == null)
                return HttpNotFound();

            return View(customer);
        }
        public ActionResult CustomerForm()
        {
            var membershipTypes = _context.MembershipTypes.ToList();
            var viewModel = new CustomerFormViewModel
            {
                MembershipTypes = membershipTypes
            };

            return View("CustomerForm", viewModel);
        }

        [HttpPost]
        //When we pass the customer here, Entity Framework checks that it's a valid customer
        //3 validation steps: add data annotations
        //use MOdelState.IsValid to check if valid and if not, return the same view
        //add validation messages to the form
        
        public ActionResult Save(Customer customer)
        {
            //if we want access to that validation:
            if (!ModelState.IsValid)
            {
                var viewModel = new CustomerFormViewModel
                {
                    Customer = customer,
                    MembershipTypes = _context.MembershipTypes.ToList()
                };

                //send the user back to the CustomerForm view
                return View("CustomerForm", viewModel);
            }

            //if it's not already in the database, add it
            if (_context.Customers.Find(customer.Id) == null){
                _context.Customers.Add(customer); }

            //otherwise, update it
            else
            {
                //get it from the database, modify, save it back to the db
                var customerInDb = _context.Customers.Single(c => c.Id == customer.Id); //if given customer is not found, an exception is thrown
                customerInDb.Name = customer.Name;
                customerInDb.Birthdate = customer.Birthdate;
                customerInDb.Membertype = customer.Membertype;
                customerInDb.IsSubscribedToNewsletter = customer.IsSubscribedToNewsletter;

                //also can do TryUpdateModel(customerInDb) properties are updated based on key/value pairs and request data
                //security holes - what if not all customers should modify all or some should not be modified
                //malicious user can add key/value pairs
                //Microsoft says to add new string[] {"Name, "Birthdate"...} to specify which to update, but then you have magic strings
            }
            //there are libraries to do this for you - AutoMapper  Mapper.Map(customer, customerInDB);
            //for security - make a customer object that only has some of the props

            _context.SaveChanges();

            return RedirectToAction("Index", "Customer");
        }

        public ActionResult Edit(int id)
        {
            var customer = _context.Customers.SingleOrDefault(c => c.Id == id);
            if(customer == null)
                return HttpNotFound();

            var viewModel = new CustomerFormViewModel
            {
                Customer = customer,
                MembershipTypes = _context.MembershipTypes.ToList()
            };

            return View("CustomerForm", viewModel);
        }

        
    }
}
