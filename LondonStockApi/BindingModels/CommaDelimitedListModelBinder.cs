using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LondonStockApi.BindingModels
{
    public class CommaDelimitedListModelBinder : IModelBinder
    {
        public Task BindModelAsync(ModelBindingContext bindingContext)
        {
            var key = bindingContext.ModelName;
            var val = bindingContext.ValueProvider.GetValue(key);

            if (val == ValueProviderResult.None)
            {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue(key, val);

            if (string.IsNullOrEmpty(val.ToString()))
            {
                return Task.CompletedTask;
            }

            bindingContext.Model = val.ToString().Split(',').ToList();
            bindingContext.Result = ModelBindingResult.Success(bindingContext.Model);
            return Task.CompletedTask;
        }
    }
}
