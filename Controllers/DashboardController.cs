﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

[Authorize] // Add this attribute to enforce authentication for all actions in this controller.
public class DashboardController : Controller
{
    private readonly IHubContext<HeartRateHub> _hubContext;
    private readonly ApplicationDbContext _context;

    public DashboardController(IHubContext<HeartRateHub> hubContext, ApplicationDbContext context)
    {
        _hubContext = hubContext;
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Retrieve the logged-in user.
        var username = User.Identity.Name;

        // Retrieve patient information data from the database for the logged-in userpatient information.
        //this is not fleshed out just here to show other things can be pulled in.
        var patientInformation = await _context.PatientInformation        
          .Where(p=> p.UserName == User.Identity.Name)
            .ToListAsync();

        return View(patientInformation);
    }

    // ...
}