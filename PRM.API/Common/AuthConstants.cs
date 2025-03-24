namespace PRM.API.Common;

public static class AuthConstants
{
    public const string SuperAdminUserPolicyName = "SuperAdmin";
    public const string AdminUserPolicyName = "Admin";
    public const string EmployeePolicyName = "Employee";
    public const string AdminOrSuperAdminRoles = $"{SuperAdminUserPolicyName},{AdminUserPolicyName}";
}