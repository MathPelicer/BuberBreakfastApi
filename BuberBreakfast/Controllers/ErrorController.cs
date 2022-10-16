using Microsoft.AspNetCore.Mvc;

namespace BuberBreakfast.Controllers;

public class ErrorController : ControllerBase
{
    [Route("/error")]
    [NonAction]
    public IActionResult Error()
    {
        return Problem();
    }
}

