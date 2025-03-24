using Microsoft.AspNetCore.Mvc.ModelBinding;
using PRM.API.ModelBinders;
using PRM.Application.Models;

namespace PRM.API.ModelBinderProviders;

public class CurrentUserModelBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context.Metadata.ModelType == typeof(User) && context.BindingInfo.BindingSource == BindingSource.Custom)
        {
            return new CurrentUserModelBinder();
        }
        return null;
    }
}