namespace BookingSystem.Services;

public static class RoleMapper
{
    // Avtal i gruppa: 0 = User, 1 = Admin
    public static string ToRoleName(int role)
        => role == 1 ? "Admin" : "User";
}