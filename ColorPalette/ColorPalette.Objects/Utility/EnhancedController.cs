using System;
using Microsoft.AspNetCore.Mvc;

namespace ColorPalette.Objects.Utility
{
    public class EnhancedController : ControllerBase
    {
        public IActionResult InternalError(Exception e)
        {
            return StatusCode(500, e);
        }
    }
}
