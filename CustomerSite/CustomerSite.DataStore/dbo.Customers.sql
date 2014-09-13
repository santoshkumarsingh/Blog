CREATE TABLE [dbo].[Table]
(
	[Id] INT NOT NULL PRIMARY KEY,
	[FirstName] NVARCHAR(50),
	[LastName] NVARCHAR(50),
	[Address] NVARCHAR(250),
	[Phone] NVARCHAR(250),
	[CreditLimit] Decimal(18,2),
	[CustomerSince] Decimal(18,2),
)
