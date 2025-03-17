using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PrulariaAankoopUI;

public class CustomBinderProvider : IModelBinderProvider
{
    public IModelBinder GetBinder(ModelBinderProviderContext context)
    {
        if (context == null)
        {
            throw new ArgumentNullException(nameof(context));
        }
        if (context.Metadata.ModelType == typeof(decimal))
        {
            return new DecimalModelBinder();
        }
        return null;
    }
}
