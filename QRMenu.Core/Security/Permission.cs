using QRMenu.Core.Entities;
using QRMenu.Core.Enums;
using System.Xml.Linq;

namespace QRMenu.Core.Security;

public class Permission
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    public string Module { get; private set; }

    private Permission() { } // EF Core için

    private Permission(string name, string description, string module)
    {
        Name = name;
        Description = description;
        Module = module;
    }

    // Dealer permissions
    public static Permission DealerCreate = new("Dealer.Create", "Create dealer", "Dealer");
    public static Permission DealerRead = new("Dealer.Read", "Read dealer", "Dealer");
    public static Permission DealerUpdate = new("Dealer.Update", "Update dealer", "Dealer");
    public static Permission DealerDelete = new("Dealer.Delete", "Delete dealer", "Dealer");

    // Company permissions
    public static Permission CompanyCreate = new("Company.Create", "Create company", "Company");
    public static Permission CompanyRead = new("Company.Read", "Read company", "Company");
    public static Permission CompanyUpdate = new("Company.Update", "Update company", "Company");
    public static Permission CompanyDelete = new("Company.Delete", "Delete company", "Company");

    // Branch permissions
    public static Permission BranchCreate = new("Branch.Create", "Create branch", "Branch");
    public static Permission BranchRead = new("Branch.Read", "Read branch", "Branch");
    public static Permission BranchUpdate = new("Branch.Update", "Update branch", "Branch");
    public static Permission BranchDelete = new("Branch.Delete", "Delete branch", "Branch");

    public override string ToString()
    {
        return Name;
    }

    public static implicit operator string(Permission permission)
    {
        return permission.Name;
    }
}

