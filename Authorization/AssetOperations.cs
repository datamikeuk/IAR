using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace IAR.Authorization
{
    public static class AssetOperations
    {
        public static OperationAuthorizationRequirement Create =   
          new OperationAuthorizationRequirement {Name=Constants.CreateOperationName};
        public static OperationAuthorizationRequirement Read = 
          new OperationAuthorizationRequirement {Name=Constants.ReadOperationName};  
        public static OperationAuthorizationRequirement Update = 
          new OperationAuthorizationRequirement {Name=Constants.UpdateOperationName}; 
        public static OperationAuthorizationRequirement Delete = 
          new OperationAuthorizationRequirement {Name=Constants.DeleteOperationName};
        public static OperationAuthorizationRequirement Review = 
          new OperationAuthorizationRequirement {Name=Constants.ApproveOperationName};
    }

    public class Constants
    {
        // Operations
        public static readonly string CreateOperationName = "Create";
        public static readonly string ReadOperationName = "Read";
        public static readonly string UpdateOperationName = "Update";
        public static readonly string DeleteOperationName = "Delete";
        public static readonly string ApproveOperationName = "Review";
        // Roles
        public static readonly string AssetAdministratorsRole = "Hive Users";
        public static readonly string AssetOwnersRole = "AssetOwners";
    }
}