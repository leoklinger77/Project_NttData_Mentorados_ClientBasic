using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Valhalla.WebApp.Extension
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        
    }
}
