using Microsoft.AspNetCore.Components.Rendering;
using Microsoft.AspNetCore.Components;
using MudBlazor;

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
      builder.AddAttribute(2, "Indeterminate", true);
      builder.AddAttribute(3, "Color", MudBlazor.Color.Primary);
      builder.CloseComponent();
    }
    else
    {
      builder.AddContent(0, Loaded);
    }
  }
}