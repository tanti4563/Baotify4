-- Add Ferry Booking Tables to Existing "test" Database
-- Run this in SQL Server Management Studio connected to LAPTOP-70K0HA84\KIN
-- Database: test

USE [test]
GO

PRINT 'Adding Ferry Booking tables to existing "test" database...'

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
    
    -- Add unique constraints
    ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ_Users_Username] UNIQUE ([Username]);
    ALTER TABLE [dbo].[Users] ADD CONSTRAINT [UQ_Users_Email] UNIQUE ([Email]);
    
    PRINT '✅ Users table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ Users table already exists'
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
    
    ALTER TABLE [dbo].[UserRoles] ADD CONSTRAINT [UQ_UserRoles_RoleName] UNIQUE ([RoleName]);
    
    PRINT '✅ UserRoles table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ UserRoles table already exists'
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
    PRINT '✅ UserRoleAssignments table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ UserRoleAssignments table already exists'
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
    
    ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [UQ_Orders_BookingCode] UNIQUE ([BookingCode]);
    ALTER TABLE [dbo].[Orders] ADD CONSTRAINT [UQ_Orders_OrderNumber] UNIQUE ([OrderNumber]);
    
    PRINT '✅ Orders table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ Orders table already exists'
END
GO

-- Create Tickets table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Tickets' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Tickets] (
        [TicketId] int IDENTITY(1,1) NOT NULL,
        [OrderId] int NOT NULL,
        [TicketNumber] nvarchar(50) NOT NULL,
        [PassengerIdNumber] nvarchar(50) NOT NULL,
        [PassengerName] nvarchar(100) NOT NULL,
        [PassengerGender] int NOT NULL,
        [DateOfBirth] datetime NULL,
        [PlaceOfBirth] nvarchar(100) NULL,
        [NationId] int NOT NULL,
        [NationName] nvarchar(100) NULL,
        [PhoneNumber] nvarchar(20) NULL,
        [Email] nvarchar(100) NULL,
        [SeatId] int NOT NULL,
        [SeatNumber] nvarchar(10) NOT NULL,
        [SeatClass] nvarchar(50) NOT NULL,
        [PositionId] int NOT NULL,
        [IsVIP] bit NOT NULL DEFAULT 0,
        [TicketTypeId] int NOT NULL,
        [TicketTypeName] nvarchar(50) NOT NULL,
        [TicketPriceId] int NOT NULL,
        [UnitPrice] decimal(10,2) NOT NULL,
        [SequenceNumber] int NOT NULL,
        [QRCode] nvarchar(500) NULL,
        [TicketStatus] nvarchar(20) NULL DEFAULT 'Valid',
        [CheckInTime] datetime NULL,
        [CreatedDate] datetime NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Tickets] PRIMARY KEY CLUSTERED ([TicketId] ASC),
        CONSTRAINT [FK_Tickets_Orders] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Orders] ([OrderId])
    );
    
    ALTER TABLE [dbo].[Tickets] ADD CONSTRAINT [UQ_Tickets_TicketNumber] UNIQUE ([TicketNumber]);
    
    PRINT '✅ Tickets table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ Tickets table already exists'
END
GO

-- Create Payments table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='Payments' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[Payments] (
        [PaymentId] int IDENTITY(1,1) NOT NULL,
        [OrderId] int NOT NULL,
        [PaymentMethod] nvarchar(50) NOT NULL,
        [TransactionId] nvarchar(100) NOT NULL,
        [BankTransactionNumber] nvarchar(100) NULL,
        [PaymentAmount] decimal(12,2) NOT NULL,
        [PaymentCurrency] nvarchar(10) NULL DEFAULT 'VND',
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
        [PaymentStatus] nvarchar(20) NULL DEFAULT 'Pending',
        [ResponseMessage] nvarchar(500) NULL,
        [IPNData] nvarchar(max) NULL,
        [CreatedDate] datetime NULL DEFAULT GETDATE(),
        [UpdatedDate] datetime NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_Payments] PRIMARY KEY CLUSTERED ([PaymentId] ASC),
        CONSTRAINT [FK_Payments_Orders] FOREIGN KEY([OrderId]) REFERENCES [dbo].[Orders] ([OrderId])
    );
    PRINT '✅ Payments table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ Payments table already exists'
END
GO

-- Create PaymentLogs table
IF NOT EXISTS (SELECT * FROM sysobjects WHERE name='PaymentLogs' AND xtype='U')
BEGIN
    CREATE TABLE [dbo].[PaymentLogs] (
        [LogId] int IDENTITY(1,1) NOT NULL,
        [PaymentId] int NOT NULL,
        [LogType] nvarchar(50) NOT NULL,
        [LogMessage] nvarchar(max) NULL,
        [LogData] nvarchar(max) NULL,
        [CreatedDate] datetime NULL DEFAULT GETDATE(),
        CONSTRAINT [PK_PaymentLogs] PRIMARY KEY CLUSTERED ([LogId] ASC),
        CONSTRAINT [FK_PaymentLogs_Payments] FOREIGN KEY([PaymentId]) REFERENCES [dbo].[Payments] ([PaymentId])
    );
    PRINT '✅ PaymentLogs table created successfully'
END
ELSE
BEGIN
    PRINT '⚠️ PaymentLogs table already exists'
END
GO

-- Insert default roles if they don't exist
IF NOT EXISTS (SELECT * FROM UserRoles WHERE RoleName = 'Admin')
BEGIN
    INSERT INTO UserRoles (RoleName, Description) VALUES 
    ('Admin', 'System Administrator'),
    ('Customer', 'Regular Customer'),
    ('Agent', 'Booking Agent');
    PRINT '✅ Default roles inserted successfully'
END
ELSE
BEGIN
    PRINT '⚠️ Default roles already exist'
END
GO

-- Create a test admin user if no users exist
IF NOT EXISTS (SELECT * FROM Users)
BEGIN
    INSERT INTO Users (Username, Password, Email, FullName, PhoneNumber, IsActive) VALUES 
    ('admin', 'e10adc3949ba59abbe56e057f20f883e', 'admin@ferrybook.com', 'System Administrator', '0123456789', 1);
    
    -- Assign admin role
    INSERT INTO UserRoleAssignments (UserId, RoleId) VALUES (1, 1);
    
    PRINT '✅ Test admin user created successfully'
    PRINT '   Username: admin'
    PRINT '   Password: 123456'
END
ELSE
BEGIN
    PRINT '⚠️ Users already exist'
END
GO

PRINT ''
PRINT '🎉 Ferry Booking tables added to "test" database successfully!'
PRINT ''
PRINT '📊 SUMMARY:'
PRINT '✅ Database: test'
PRINT '✅ Server: LAPTOP-70K0HA84\KIN'
PRINT '✅ Tables: Users, UserRoles, UserRoleAssignments, Orders, Tickets, Payments, PaymentLogs'
PRINT '✅ Test admin user: admin / 123456'
PRINT ''
PRINT '🚀 You can now test user registration and ferry booking!'
