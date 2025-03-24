using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PRM.API.Attributes;

[AttributeUsage(AttributeTargets.Parameter, AllowMultiple = false, Inherited = true)]
public class CurrentUserAttribute : Attribute, IBindingSourceMetadata
{
    public BindingSource BindingSource => BindingSource.Custom;
}