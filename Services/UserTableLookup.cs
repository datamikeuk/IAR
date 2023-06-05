using IAR.Data;
using IAR.Models;

public class UserTableLookup
{
    private readonly RegisterContext _context;

    public UserTableLookup(RegisterContext context)
    {
        _context = context;
    }

    public User UserLookup { get; set; } = default!;

    // Lookup Display Name from users table for a given account name
    public string GetDisplayName(string accountName)
    {
        var displayName = "*" + accountName + "*";
        UserLookup = _context.Users.FirstOrDefault(u => u.AccountName == accountName) ?? new User();
        if (UserLookup != null && UserLookup.DisplayName != null) {
            displayName = UserLookup.DisplayName;
        }
        return displayName;
    }
}