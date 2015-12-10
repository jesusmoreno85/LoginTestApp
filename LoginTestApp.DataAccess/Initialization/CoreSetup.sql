--This script is inteded to setup everything in regards of the Core functionality for the application

--This script is inteded to setup everything in regards of the Core functionality for the application

DECLARE @CreatedBy NVARCHAR(max) = 'SysAdmin'
DECLARE @CreatedDate DATETIME = GETDATE()

/* Roles Setup */
DECLARE @SysAdminRoleId INT;
DECLARE @AnonymousRoleId INT;

INSERT INTO Roles (Name, [Description], IsActive, CreatedBy, CreatedDate)
VALUES('System Administrator', 'System administrator full granted access to the all features', 1, @CreatedBy, @CreatedDate)
	
SET @SysAdminRoleId = Scope_Identity()

INSERT INTO Roles (Name, [Description], IsActive, CreatedBy, CreatedDate)
VALUES('Anonymous', 'Account with limited access to the system features', 1, @CreatedBy, @CreatedDate)

SET @AnonymousRoleId = Scope_Identity()

/* Users Setup */
INSERT INTO Users (Alias, FullName, [Password], Email, PhoneNumber, PasswordRecoveryClue, AccountType, RoleId, IsActive, CreatedBy, CreatedDate)
SELECT 'SysAdmin', 'System administration account', '3JbJxLOOmOqZVxcgcMUUlA==', 'jesusmoreno85@hotmail.com', '6671372531', 'WUkvch0aYy+gf4G8uIsCNg==', 1, @SysAdminRoleId, 1, @CreatedBy, @CreatedDate UNION ALL
SELECT 'Anonymous', 'Anonymous with limited access', '', '', NULL, NULL, 99, @SysAdminRoleId, 1, @CreatedBy, @CreatedDate

/* Modules Setup */
DECLARE @AccountModuleId INT;

INSERT INTO Modules (Name, [Description], IsActive, CreatedBy, CreatedDate)
VALUES('Account', 'Handles the account access features', 1, @CreatedBy, @CreatedDate);

SET @AccountModuleId = Scope_Identity()

/* Common Access Setup */

INSERT INTO ModuleActions (Id, ModuleId, GrantedRoles, IsActive, CreatedBy, CreatedDate)
SELECT '/Account/Index', @AccountModuleId, 'Everyone', 1, @CreatedBy, @CreatedDate UNION ALL
SELECT '/Account/Login', @AccountModuleId, 'Everyone', 1, @CreatedBy, @CreatedDate UNION ALL
SELECT '/Account/Logout', @AccountModuleId, 'Everyone', 1, @CreatedBy, @CreatedDate UNION ALL
SELECT '/Account/PasswordRecoveryRequest', @AccountModuleId, 'Everyone', 1, @CreatedBy, @CreatedDate UNION ALL
SELECT '/Account/PasswordRecovery', @AccountModuleId, 'Everyone', 1, @CreatedBy, @CreatedDate UNION ALL
SELECT '/Account/UpdateAccount', @AccountModuleId, 'Authenticated', 1, @CreatedBy, @CreatedDate;
	