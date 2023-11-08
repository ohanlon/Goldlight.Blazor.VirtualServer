namespace Goldlight.Blazor.VirtualServer.Extensions;

public class ResponseHandler
{
  private Action? createdAction;
  private Action? okAction;
  private Action? notFoundAction;
  private Action? serverFailureAction;
  private Action? conflictAction;
  private Action? unauthorizedAction;
  private Action? forbiddenAction;

  public ResponseHandler Forbidden(Action? action)
  {
    forbiddenAction = action;
    return this;
  }

  public ResponseHandler Unauthorized(Action? action)
  {
    unauthorizedAction = action;
    return this;
  }

  public ResponseHandler Created(Action? action)
  {
    createdAction = action;
    return this;
  }

  public ResponseHandler Ok(Action? action)
  {
    okAction = action;
    return this;
  }

  public ResponseHandler NotFound(Action? action)
  {
    notFoundAction = action;
    return this;
  }

  public ResponseHandler ServerFailure(Action? action)
  {
    serverFailureAction = action;
    return this;
  }

  public ResponseHandler Conflict(Action? action)
  {
    conflictAction = action;
    return this;
  }

  public void Ok() => okAction?.Invoke();
  public void NotFound() => notFoundAction?.Invoke();
  public void ServerFailure() => serverFailureAction?.Invoke();
  public void Created() => createdAction?.Invoke();
  public void Conflict() => conflictAction?.Invoke();
  public void Forbidden() => forbiddenAction?.Invoke();
  public void Unauthorized() => unauthorizedAction?.Invoke();
}