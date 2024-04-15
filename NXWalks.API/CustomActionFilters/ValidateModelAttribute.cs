using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace NXWalks.API.CustomActionFilters
{
    public class ValidateModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if(context.ModelState.IsValid == false)
            {
            //You can use This Custom Filter Just Below you Route [ValidateModel] that will be imported namespace from this class and you can remove the explicitely defined checks in the controllers of create and update
              context.Result = new BadRequestResult();
            }
        }
    }
}
