using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;

namespace Goldlight.Blazor.VirtualServer.Components;

public class Loading : ComponentBase
{
  [Parameter] public bool IsLoading { get; set; }

  [Parameter] public RenderFragment? Loaded { get; set; }

  protected override void BuildRenderTree(RenderTreeBuilder builder)
  {
    base.BuildRenderTree(builder);
    if (IsLoading)
    {
      // Add MudProgressCircular as a child of this component
      builder.OpenComponent<MudBlazor.MudProgressCircular>(0);
      builder.AddAttribute(1, "Size", MudBlazor.Size.Small);
      builder.AddAttribute(2, "IsIndeterminate", true);
      builder.AddAttribute(3, "Class", "mb-8");
      builder.CloseComponent();
    }
    else
    {
      builder.AddContent(0, Loaded);
    }
  }
}