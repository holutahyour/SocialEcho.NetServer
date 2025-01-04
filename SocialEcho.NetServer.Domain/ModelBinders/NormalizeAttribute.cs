using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace SocialEcho.NetServer.Domain.ModelBinders;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class NormalizeAttribute : Attribute, IModelBinderProvider
{
    private readonly string _defaultValue;

    public NormalizeAttribute(string defaultValue = null)
    {
        _defaultValue = defaultValue;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (context.Metadata.ModelType == typeof(string))
        {
            return new NormalizeBinder(_defaultValue);
        }

        return null;
    }
}

public class NormalizeBinder : IModelBinder
{
    private readonly string _defaultValue;

    public NormalizeBinder(string defaultValue = null)
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
            if (!string.IsNullOrEmpty(value))
            {
                value = value.Trim().ToLowerInvariant();
            }
            else
            {
                value = _defaultValue; // Set to default value if null or whitespace
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
