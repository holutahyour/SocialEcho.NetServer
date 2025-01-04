using Microsoft.AspNetCore.Mvc.ModelBinding;
using SocialEcho.NetServer.Domain.Utilities;

namespace SocialEcho.NetServer.Domain.ModelBinders;

[AttributeUsage(AttributeTargets.Property | AttributeTargets.Parameter, AllowMultiple = false)]
public class SetDefaultAvatarAttribute : Attribute, IModelBinderProvider
{
    private readonly string _defaultValue;

    public SetDefaultAvatarAttribute(string defaultValue = null)
    {
        _defaultValue = defaultValue;
    }

    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null) throw new ArgumentNullException(nameof(context));
        if (context.Metadata.ModelType == typeof(string))
        {
            return new SetDefaultAvatarBinder(_defaultValue);
        }

        return null;
    }
}

public class SetDefaultAvatarBinder : IModelBinder
{
    private readonly string _defaultValue;

    public SetDefaultAvatarBinder(string defaultValue = null)
    {
        _defaultValue = defaultValue;
    }

    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        if (bindingContext == null) throw new ArgumentNullException(nameof(bindingContext));

        Console.WriteLine($"Binding field: {bindingContext.FieldName}");

        var valueProviderResult = bindingContext.ValueProvider.GetValue(bindingContext.FieldName);

        if (valueProviderResult != ValueProviderResult.None)
        {
            var value = valueProviderResult.FirstValue;

            Console.WriteLine($"Value from provider: {value}");

            // Example normalization: trim and convert to lowercase
            if (!string.IsNullOrWhiteSpace(value))
            {
                value = value.Trim().ToLowerInvariant();
            }
            else
            {
                value = string.IsNullOrWhiteSpace(_defaultValue) ? Defaults.Avatar : _defaultValue; // Set to default value if null or whitespace
            }

            bindingContext.Result = ModelBindingResult.Success(value);
        }
        else
        {
            Console.WriteLine("Value not provided by the client.");

            bindingContext.Result = ModelBindingResult.Success(_defaultValue); // Fallback if no value is provided
        }

        return Task.CompletedTask;
    }
}
