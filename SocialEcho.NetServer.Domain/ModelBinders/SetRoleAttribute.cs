using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SocialEcho.NetServer.Domain.ModelBinders;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class SetRoleAttribute : Attribute, IModelBinderProvider
{
    private readonly string _defaultValue;

    public SetRoleAttribute(string defaultValue = null)
    {
        _defaultValue = defaultValue;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (context.Metadata.ModelType == typeof(string))
        {
            return new SetRoleBinder(_defaultValue);
        }

        return null;
    }
}

public class SetRoleBinder : IModelBinder
{
    private readonly string _defaultValue;

    public SetRoleBinder(string defaultValue = null)
    {
        _defaultValue = defaultValue;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;

            // Example normalization: trim and convert to lowercase
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim().ToLowerInvariant();
            }
            else
            {
                value = string.IsNullOrWhiteSpace(_defaultValue) ? _defaultValue.Split('@')[1] == "mod.socialecho.com" ? "moderator" : "general" : _defaultValue;
            }

            bindingContext.Result = ModelBindingResult.Success(value);
        }
        else
        {
            bindingContext.Result = ModelBindingResult.Success(_defaultValue); // Fallback if no value is provided
        }

        return Task.CompletedTask;
    }
}
