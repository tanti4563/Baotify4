-- Ferry Booking Database Schema - PRODUCTION READY
-- This script creates the database tables to match the DataModel.edmx

-- Create database if it doesn't exist
IF NOT EXISTS (SELECT name FROM sys.databases WHERE name = N'ShipManage')
BEGIN
    CREATE DATABASE [ShipManage]
END
GO

USE [ShipManage]
GO

-- Drop existing tables if they exist (in correct order to avoid FK constraints)
IF OBJECT_ID('dbo.PaymentLogs', 'U') IS NOT NULL DROP TABLE dbo.PaymentLogs;
IF OBJECT_ID('dbo.Payments', 'U') IS NOT NULL DROP TABLE dbo.Payments;
IF OBJECT_ID('dbo.Tickets', 'U') IS NOT NULL DROP TABLE dbo.Tickets;
IF OBJECT_ID('dbo.Orders', 'U') IS NOT NULL DROP TABLE dbo.Orders;
IF OBJECT_ID('dbo.UserRoleAssignments', 'U') IS NOT NULL DROP TABLE dbo.UserRoleAssignments;
IF OBJECT_ID('dbo.UserRoles', 'U') IS NOT NULL DROP TABLE dbo.UserRoles;
IF OBJECT_ID('dbo.Users', 'U') IS NOT NULL DROP TABLE dbo.Users;

-- Create Users table
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
    [IsActive] bit NULL,
    [CreatedDate] datetime NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED ([UserId] ASC)
);

-- Create UserRoles table
CREATE TABLE [dbo].[UserRoles] (
    [RoleId] int IDENTITY(1,1) NOT NULL,
    [RoleName] nvarchar(50) NOT NULL,
    [Description] nvarchar(200) NULL,
    CONSTRAINT [PK_UserRoles] PRIMARY KEY CLUSTERED ([RoleId] ASC)
);

-- Create UserRoleAssignments table
CREATE TABLE [dbo].[UserRoleAssignments] (
    [UserId] int NOT NULL,
    [RoleId] int NOT NULL,
    [AssignedDate] datetime NULL,
    CONSTRAINT [PK_UserRoleAssignments] PRIMARY KEY CLUSTERED ([UserId] ASC, [RoleId] ASC)
);

-- Create Orders table
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
    [CreatedDate] datetime NULL,
    [UpdatedDate] datetime NULL,
    [ExpiryDate] datetime NULL,
    CONSTRAINT [PK_Orders] PRIMARY KEY CLUSTERED ([OrderId] ASC)
);

-- Create Tickets table
CREATE TABLE [dbo].[Tickets] (
    [TicketId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int NOT NULL,
    [TicketNumber] nvarchar(50) NOT NULL,
    [PassengerIdNumber] nvarchar(50) NOT NULL,
    [PassengerName] nvarchar(100) NOT NULL,
    [PassengerGender] int NOT NULL,
    [DateOfBirth] date NULL,
    [PlaceOfBirth] nvarchar(100) NULL,
    [NationId] int NOT NULL,
    [NationName] nvarchar(100) NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Email] nvarchar(100) NULL,
    [SeatId] int NOT NULL,
    [SeatNumber] nvarchar(10) NOT NULL,
    [SeatClass] nvarchar(50) NOT NULL,
    [PositionId] int NOT NULL,
    [IsVIP] bit NULL,
    [TicketTypeId] int NOT NULL,
    [TicketTypeName] nvarchar(50) NOT NULL,
    [TicketPriceId] int NOT NULL,
    [UnitPrice] decimal(10,2) NOT NULL,
    [SequenceNumber] int NOT NULL,
    [QRCode] nvarchar(500) NULL,
    [TicketStatus] nvarchar(20) NULL,
    [CheckInTime] datetime NULL,
    [CreatedDate] datetime NULL,
    CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([TicketId] ASC)
);

-- Create Payments table
CREATE TABLE [dbo].[Payments] (
    [PaymentId] int IDENTITY(1,1) NOT NULL,
    [OrderId] int NOT NULL,
    [PaymentMethod] nvarchar(50) NOT NULL,
    [TransactionId] nvarchar(100) NOT NULL,
    [BankTransactionNumber] nvarchar(100) NULL,
    [PaymentAmount] decimal(12,2) NOT NULL,
    [PaymentCurrency] nvarchar(10) NULL,
    [vnp_Amount] nvarchar(50) NULL,
    [vnp_BankCode] nvarchar(20) NULL,
    [vnp_BankTranNo] nvarchar(100) NULL,
    [vnp_CardType] nvarchar(20) NULL,
    [vnp_OrderInfo] nvarchar(100) NULL,
    [vnp_PayDate] nvarchar(20) NULL,
    [vnp_ResponseCode] nvarchar(10) NULL,
    [vnp_TransactionNo] nvarchar(100) NULL,
    [vnp_TxnRef] nvarchar(100) NULL,
    [vnp_SecureHash] nvarchar(500) NULL,
    [PaymentDate] datetime NULL,
    [PaymentStatus] nvarchar(20) NULL,
    [ResponseMessage] nvarchar(500) NULL,
    [IPNData] nvarchar(max) NULL,
    [CreatedDate] datetime NULL,
    [UpdatedDate] datetime NULL,
    CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([PaymentId] ASC)
);

-- Create PaymentLogs table
CREATE TABLE [dbo].[PaymentLogs] (
    [LogId] int IDENTITY(1,1) NOT NULL,
    [PaymentId] int NOT NULL,
    [LogType] nvarchar(50) NOT NULL,
    [LogMessage] nvarchar(max) NULL,
    [LogData] nvarchar(max) NULL,
    [CreatedDate] datetime NULL,
    CONSTRAINT [PK_PaymentLogs] PRIMARY KEY CLUSTERED ([LogId] ASC)
);

-- Add Foreign Key Constraints
ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [FK__Orders__UserId__06CD04F7] 
    FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId]);

ALTER TABLE [dbo].[Tickets] ADD CONSTRAINT [FK__Tickets__OrderId__0D7A0286] 
    FOREIGN KEY([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]);

ALTER TABLE [dbo].[Payments] ADD CONSTRAINT [FK__Payments__OrderI__151B244E] 
    FOREIGN KEY([OrderId]) REFERENCES [dbo].[Orders] ([OrderId]);

ALTER TABLE [dbo].[PaymentLogs] ADD CONSTRAINT [FK__PaymentLo__Payme__18EBB532] 
    FOREIGN KEY([PaymentId]) REFERENCES [dbo].[Payments] ([PaymentId]);

ALTER TABLE [dbo].[UserRoleAssignments] ADD CONSTRAINT [FK__UserRoleA__UserI__7A672E12] 
    FOREIGN KEY([UserId]) REFERENCES [dbo].[Users] ([UserId]);

ALTER TABLE [dbo].[UserRoleAssignments] ADD CONSTRAINT [FK__UserRoleA__RoleI__7B5B524B] 
    FOREIGN KEY([RoleId]) REFERENCES [dbo].[UserRoles] ([RoleId]);

PRINT 'Database schema created successfully!';
