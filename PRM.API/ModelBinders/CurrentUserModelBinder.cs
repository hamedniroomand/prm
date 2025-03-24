using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using PRM.Application.Repositories.User;
using Task = System.Threading.Tasks.Task;

namespace PRM.API.ModelBinders;

public class CurrentUserModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null)
        {
            throw new ArgumentNullException(nameof(bindingContext));
        }

        var httpContext = bindingContext.HttpContext;
        var user = httpContext.User;

        var userRepository = httpContext.RequestServices.GetService<IUserRepository>();

        if (user?.Identity?.IsAuthenticated == true)
        {
            var userId = user.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var isValid = int.TryParse(userId, out var id);
            {
                var currentUser = userRepository.GetQuery().FirstOrDefault(u => u.Id == 1);
                bindingContext.Result = ModelBindingResult.Success(currentUser);
            }
            if (!isValid)
            {
                bindingContext.Result = ModelBindingResult.Failed();
            }
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Failed();
        }

        return Task.CompletedTask;
    }
}