using checkpoint.Models;
using checkpoint;
using Microsoft.AspNetCore.Mvc;

public class FormController : Controller
{
    private readonly AppDbContext _context;

    public FormController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Index()
    {
        return View();
    }

    [HttpPost]
    public IActionResult SubmitStudent(Student student)
    {
        if (ModelState.IsValid)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }

    [HttpPost]
    public IActionResult SubmitEmployee(Employee employee)
    {
        if (ModelState.IsValid)
        {
            _context.Employees.Add(employee);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }

    [HttpPost]
    public IActionResult SubmitVisitor(Visitor visitor)
    {
        if (ModelState.IsValid)
        {
            _context.Visitors.Add(visitor);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        return View("Index");
    }
}


