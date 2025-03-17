using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace PrulariaAankoopUI;

public class DecimalModelBinder : IModelBinder
{
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
        var valueProviderResult = bindingContext.ValueProvider.GetValue(
        bindingContext.ModelName);
        var value = valueProviderResult.FirstValue;
        if (string.IsNullOrEmpty(value))
            return Task.CompletedTask;
        decimal myValue = 0;
        if (!decimal.TryParse(value, out myValue))
        {
            bindingContext.ModelState.TryAddModelError(
            bindingContext.ModelName,

            "Incorrect value, please try again.");

            return Task.CompletedTask;
        }
        bindingContext.Result = ModelBindingResult.Success(myValue);
        return Task.CompletedTask;
    }
}
