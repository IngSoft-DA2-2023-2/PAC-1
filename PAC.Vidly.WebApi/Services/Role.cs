namespace PAC.Vidly.WebApi.Services;

public class Role
{
    public List<PermissionKey> Permissions { get; init; }

    public Role()
    {
        Permissions = new List<PermissionKey>();
    }

    public bool HasPermission(PermissionKey permission)
    {
        return Permissions.Contains(permission);
    }
}