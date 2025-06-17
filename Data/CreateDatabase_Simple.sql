-- Simple Database Creation Script for Ferry Booking System
-- Run this in SQL Server Management Studio connected to (localdb)\MSSQLLocalDB

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ShipManage')
BEGIN
    CREATE DATABASE [ShipManage]
    PRINT 'Database ShipManage created successfully'
END
ELSE
BEGIN
    PRINT 'Database ShipManage already exists'
END
GO

USE [ShipManage]
GO

-- Create Users table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Users' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Users] (
        [UserId] int IDENTITY(1,1) NOT NULL,
        [Username] nvarchar(50) NOT NULL,
        [Password] nvarchar(255) NOT NULL,
        [Email] nvarchar(100) NOT NULL,
        [FullName] nvarchar(100) NOT NULL,
        [PhoneNumber] nvarchar(20) NULL,
        [DateOfBirth] date NULL,
        [Gender] int NULL,
        [Address] nvarchar(500) NULL,
        [IsActive] bit NULL DEFAULT 1,
        [CreatedDate] datetime NULL DEFAULT GETDATE(),
        [UpdatedDate] datetime NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
    );
    PRINT 'Users table created successfully'
END
ELSE
BEGIN
    PRINT 'Users table already exists'
END
GO

-- Create UserRoles table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRoles' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[UserRoles] (
        [RoleId] int IDENTITY(1,1) NOT NULL,
        [RoleName] nvarchar(50) NOT NULL,
        [Description] nvarchar(200) NULL,
        CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
    );
    PRINT 'UserRoles table created successfully'
END
GO

-- Create UserRoleAssignments table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='UserRoleAssignments' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[UserRoleAssignments] (
        [UserId] int NOT NULL,
        [RoleId] int NOT NULL,
        [AssignedDate] datetime NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_UserRoleAssignments] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC),
        CONSTRAINT [FK_UserRoleAssignments_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId]),
        CONSTRAINT [FK_UserRoleAssignments_UserRoles] FOREIGN KEY([RoleId]) REFERENCES [dbo].[UserRoles] ([RoleId])
    );
    PRINT 'UserRoleAssignments table created successfully'
END
GO

-- Create Orders table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Orders' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Orders] (
        [OrderId] int IDENTITY(1,1) NOT NULL,
        [BookingCode] nvarchar(50) NOT NULL,
        [OrderNumber] nvarchar(50) NOT NULL,
        [UserId] int NULL,
        [BookerName] nvarchar(100) NOT NULL,
        [ContactPhone] nvarchar(20) NOT NULL,
        [ContactEmail] nvarchar(100) NOT NULL,
        [BuyerName] nvarchar(100) NULL,
        [CompanyName] nvarchar(200) NULL,
        [CompanyAddress] nvarchar(500) NULL,
        [TaxCode] nvarchar(50) NULL,
        [RouteId] int NOT NULL,
        [RouteName] nvarchar(100) NULL,
        [VoyageId] int NOT NULL,
        [BoatId] int NOT NULL,
        [BoatName] nvarchar(100) NULL,
        [ScheduleId] int NOT NULL,
        [DepartureDate] date NOT NULL,
        [DepartureTime] time(7) NOT NULL,
        [DeparturePort] nvarchar(100) NULL,
        [ArrivalPort] nvarchar(100) NULL,
        [TotalPassengers] int NOT NULL,
        [SubTotal] decimal(12,2) NOT NULL,
        [AdditionalFees] decimal(12,2) NULL,
        [TotalAmount] decimal(12,2) NOT NULL,
        [PaidAmount] decimal(12,2) NULL,
        [IsRoundTrip] bit NULL,
        [OrderStatus] nvarchar(20) NULL,
        [PaymentStatus] nvarchar(20) NULL,
        [CreatedDate] datetime NULL DEFAULT GETDATE(),
        [UpdatedDate] datetime NULL DEFAULT GETDATE(),
        [ExpiryDate] datetime NULL,
        CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC),
        CONSTRAINT [FK_Orders_Users] FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId])
    );
    PRINT 'Orders table created successfully'
END
GO

-- Insert default roles if they don't exist
IF NOT EXISTS (SELECT * FROM UserRoles WHERE RoleName = 'Admin')
BEGIN
    INSERT INTO UserRoles (RoleName, Description) VALUES 
    ('Admin', 'System Administrator'),
    ('Customer', 'Regular Customer'),
    ('Agent', 'Booking Agent');
    PRINT 'Default roles inserted successfully'
END
GO

-- Create a test user if no users exist
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO Users (Username, Password, Email, FullName, PhoneNumber, IsActive) VALUES 
    ('admin', 'e10adc3949ba59abbe56e057f20f883e', 'admin@ferrybook.com', 'System Administrator', '0123456789', 1);
    
    -- Assign admin role
    INSERT INTO UserRoleAssignments (UserId, RoleId) VALUES (1, 1);
    
    PRINT 'Test admin user created successfully (username: admin, password: 123456)'
END
GO

PRINT 'Database setup completed successfully!'
PRINT 'You can now test user registration and login.'
