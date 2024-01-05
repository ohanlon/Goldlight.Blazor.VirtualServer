using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using Goldlight.Blazor.VirtualServer.Api;
using Goldlight.Blazor.VirtualServer.Models;

namespace Goldlight.Blazor.VirtualServer.State;

public class OrganizationProps : INotifyPropertyChanged
{
  private readonly OrganizationApi organizationApi;
  private Organization? selectedOrganization;
  private bool userHasEditCapabilities;

  public OrganizationProps(OrganizationApi organizationApi)
  {
    this.organizationApi = organizationApi;
  }

  public bool UserHasEditCapabilities
  {
    get => userHasEditCapabilities;
    private set => SetField(ref userHasEditCapabilities, value);
  }

  public Organization? SelectedOrganization
  {
    get => selectedOrganization;
    set
    {
      UserHasEditCapabilities = false;
      SetField(ref selectedOrganization, value);
    }
  }

  public ObservableCollection<Organization> Organizations { get; set; } = new();

  public async Task LoadOrganizationsAsync()
  {
    List<Organization>? organizations = await organizationApi.GetOrganizationsAsync();
    if (organizations is null || !organizations.Any()) return;
    organizations.ForEach(Organizations.Add);
    SelectedOrganization ??= Organizations[0];
    await CheckUserEditCapability();
  }

  public async Task CheckUserEditCapability()
  {
    UserHasEditCapabilities = await organizationApi.DoesUserHaveEditCapability(selectedOrganization!.Id);
  }

  public event PropertyChangedEventHandler? PropertyChanged;

  protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
  {
    PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
  }

  protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
  {
    if (EqualityComparer<T>.Default.Equals(field, value)) return false;
    field = value;
    OnPropertyChanged(propertyName);
    return true;
  }

  public override string ToString() => SelectedOrganization == null ? "<<unset>>" : SelectedOrganization!.FriendlyName;
}