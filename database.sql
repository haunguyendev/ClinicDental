IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK__Roles__8AFACE1A1B65C4C2] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [Services] (
    [ServiceId] int NOT NULL IDENTITY,
    [ServiceName] nvarchar(100) NOT NULL,
    [Description] nvarchar(255) NULL,
    [ImageURL] nvarchar(max) NULL,
    CONSTRAINT [PK__Services__C51BB00A1639594A] PRIMARY KEY ([ServiceId])
);
GO

CREATE TABLE [Users] (
    [UserId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Username] nvarchar(50) NOT NULL,
    [Email] nvarchar(100) NOT NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Address] nvarchar(255) NULL,
    [PasswordHash] nvarchar(255) NOT NULL,
    [RoleId] int NOT NULL,
    CONSTRAINT [PK__Users__1788CC4C3AD45CF5] PRIMARY KEY ([UserId]),
    CONSTRAINT [FK_Users_Roles] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId])
);
GO

CREATE TABLE [Clinics] (
    [ClinicId] int NOT NULL IDENTITY,
    [Name] nvarchar(100) NOT NULL,
    [Address] nvarchar(255) NOT NULL,
    [ClinicOwnerId] int NOT NULL,
    [OpeningTime] time NOT NULL,
    [ClosingTime] time NOT NULL,
    [ImageURL] nvarchar(max) NOT NULL,
    CONSTRAINT [PK__Clinics__3347C2DD332B6F58] PRIMARY KEY ([ClinicId]),
    CONSTRAINT [FK_Clinics_Users] FOREIGN KEY ([ClinicOwnerId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [Appointments] (
    [AppointmentId] int NOT NULL IDENTITY,
    [CustomerId] int NOT NULL,
    [ServiceId] int NOT NULL,
    [ClinicId] int NOT NULL,
    [DentistId] int NOT NULL,
    [AppointmentTime] datetime NOT NULL,
    [PhoneNumber] nvarchar(20) NULL,
    [Notes] nvarchar(255) NULL,
    [Status] nvarchar(50) NULL DEFAULT N'Scheduled',
    CONSTRAINT [PK__Appointm__8ECDFCC280841F6C] PRIMARY KEY ([AppointmentId]),
    CONSTRAINT [FK_Appointments_Clinics] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([ClinicId]),
    CONSTRAINT [FK_Appointments_Services] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId]),
    CONSTRAINT [FK_Appointments_Users_Customer] FOREIGN KEY ([CustomerId]) REFERENCES [Users] ([UserId]),
    CONSTRAINT [FK_Appointments_Users_Dentist] FOREIGN KEY ([DentistId]) REFERENCES [Users] ([UserId])
);
GO

CREATE TABLE [ClinicServices] (
    [ClinicServiceId] int NOT NULL IDENTITY,
    [ClinicId] int NOT NULL,
    [ServiceId] int NOT NULL,
    [Price] decimal(10,2) NOT NULL,
    CONSTRAINT [PK__ClinicSe__32750C42B0F1B300] PRIMARY KEY ([ClinicServiceId]),
    CONSTRAINT [FK_ClinicServices_Clinics] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([ClinicId]),
    CONSTRAINT [FK_ClinicServices_Services] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId])
);
GO

CREATE TABLE [DentistDetails] (
    [DentistDetailId] int NOT NULL IDENTITY,
    [UserId] int NOT NULL,
    [ClinicId] int NULL,
    [Certificate] nvarchar(255) NULL,
    [Qualification] nvarchar(255) NULL,
    [Experience] nvarchar(255) NULL,
    CONSTRAINT [PK__DentistD__859D62F153740678] PRIMARY KEY ([DentistDetailId]),
    CONSTRAINT [FK_DentistDetails_Clinics] FOREIGN KEY ([ClinicId]) REFERENCES [Clinics] ([ClinicId]),
    CONSTRAINT [FK_DentistDetails_Users] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
);
GO

CREATE INDEX [IX_Appointments_ClinicId] ON [Appointments] ([ClinicId]);
GO

CREATE INDEX [IX_Appointments_CustomerId] ON [Appointments] ([CustomerId]);
GO

CREATE INDEX [IX_Appointments_DentistId] ON [Appointments] ([DentistId]);
GO

CREATE INDEX [IX_Appointments_ServiceId] ON [Appointments] ([ServiceId]);
GO

CREATE INDEX [IX_Clinics_ClinicOwnerId] ON [Clinics] ([ClinicOwnerId]);
GO

CREATE INDEX [IX_ClinicServices_ServiceId] ON [ClinicServices] ([ServiceId]);
GO

CREATE UNIQUE INDEX [UQ_ClinicService] ON [ClinicServices] ([ClinicId], [ServiceId]);
GO

CREATE INDEX [IX_DentistDetails_ClinicId] ON [DentistDetails] ([ClinicId]);
GO

CREATE UNIQUE INDEX [UQ__DentistD__1788CC4D6F77906B] ON [DentistDetails] ([UserId]);
GO

CREATE UNIQUE INDEX [UQ__Roles__8A2B61609ADB6310] ON [Roles] ([RoleName]);
GO

CREATE INDEX [IX_Users_RoleId] ON [Users] ([RoleId]);
GO

CREATE UNIQUE INDEX [UQ__Users__536C85E417F46290] ON [Users] ([Username]);
GO

CREATE UNIQUE INDEX [UQ__Users__A9D105347EAF180D] ON [Users] ([Email]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240625140020_V0', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [DentistService] (
    [DentistId] int NOT NULL,
    [ServiceId] int NOT NULL,
    CONSTRAINT [PK_DentistService] PRIMARY KEY ([DentistId], [ServiceId]),
    CONSTRAINT [FK_DentistService_DentistDetails_DentistId] FOREIGN KEY ([DentistId]) REFERENCES [DentistDetails] ([DentistDetailId]) ON DELETE CASCADE,
    CONSTRAINT [FK_DentistService_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId]) ON DELETE CASCADE
);
GO

CREATE INDEX [IX_DentistService_ServiceId] ON [DentistService] ([ServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240625142805_AddDentistService', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [DentistService] DROP CONSTRAINT [FK_DentistService_DentistDetails_DentistId];
GO

ALTER TABLE [DentistService] DROP CONSTRAINT [FK_DentistService_Services_ServiceId];
GO

ALTER TABLE [DentistService] DROP CONSTRAINT [PK_DentistService];
GO

DECLARE @var0 sysname;
SELECT @var0 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clinics]') AND [c].[name] = N'Address');
IF @var0 IS NOT NULL EXEC(N'ALTER TABLE [Clinics] DROP CONSTRAINT [' + @var0 + '];');
ALTER TABLE [Clinics] DROP COLUMN [Address];
GO

EXEC sp_rename N'[DentistService]', N'DentistServices';
GO

EXEC sp_rename N'[DentistServices].[IX_DentistService_ServiceId]', N'IX_DentistServices_ServiceId', N'INDEX';
GO

ALTER TABLE [DentistServices] ADD CONSTRAINT [PK_DentistServices] PRIMARY KEY ([DentistId], [ServiceId]);
GO

ALTER TABLE [DentistServices] ADD CONSTRAINT [FK_DentistServices_DentistDetails_DentistId] FOREIGN KEY ([DentistId]) REFERENCES [DentistDetails] ([DentistDetailId]) ON DELETE CASCADE;
GO

ALTER TABLE [DentistServices] ADD CONSTRAINT [FK_DentistServices_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626080230_RemoveAddressClinic', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Clinics] ADD [AddressId] int NOT NULL DEFAULT 0;
GO

CREATE TABLE [Addresses] (
    [AddressId] int NOT NULL IDENTITY,
    [StreetAddress] nvarchar(255) NOT NULL,
    [District] nvarchar(100) NOT NULL,
    CONSTRAINT [PK_Addresses] PRIMARY KEY ([AddressId])
);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626081238_AddAddress', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

CREATE UNIQUE INDEX [IX_Clinics_AddressId] ON [Clinics] ([AddressId]);
GO

ALTER TABLE [Clinics] ADD CONSTRAINT [FK_Clinic_Address] FOREIGN KEY ([AddressId]) REFERENCES [Addresses] ([AddressId]) ON DELETE CASCADE;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626081837_AddAddressForClinic', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Appointments] ADD [Slot] int NULL;
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626115751_AddSlotForAppointment', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var1 sysname;
SELECT @var1 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clinics]') AND [c].[name] = N'ClosingTime');
IF @var1 IS NOT NULL EXEC(N'ALTER TABLE [Clinics] DROP CONSTRAINT [' + @var1 + '];');
ALTER TABLE [Clinics] DROP COLUMN [ClosingTime];
GO

DECLARE @var2 sysname;
SELECT @var2 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clinics]') AND [c].[name] = N'OpeningTime');
IF @var2 IS NOT NULL EXEC(N'ALTER TABLE [Clinics] DROP CONSTRAINT [' + @var2 + '];');
ALTER TABLE [Clinics] DROP COLUMN [OpeningTime];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240626121451_AddDeleteOpentimeAndCloseTimeClinic', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

ALTER TABLE [Users] ADD [Status] nvarchar(max) NOT NULL DEFAULT N'';
GO

DECLARE @var3 sysname;
SELECT @var3 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Clinics]') AND [c].[name] = N'ImageURL');
IF @var3 IS NOT NULL EXEC(N'ALTER TABLE [Clinics] DROP CONSTRAINT [' + @var3 + '];');
ALTER TABLE [Clinics] ALTER COLUMN [ImageURL] nvarchar(max) NULL;
GO

DECLARE @var4 sysname;
SELECT @var4 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Appointments]') AND [c].[name] = N'Slot');
IF @var4 IS NOT NULL EXEC(N'ALTER TABLE [Appointments] DROP CONSTRAINT [' + @var4 + '];');
UPDATE [Appointments] SET [Slot] = 0 WHERE [Slot] IS NULL;
ALTER TABLE [Appointments] ALTER COLUMN [Slot] int NOT NULL;
ALTER TABLE [Appointments] ADD DEFAULT 0 FOR [Slot];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240710080754_addStatusForUser', N'8.0.6');
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

DECLARE @var5 sysname;
SELECT @var5 = [d].[name]
FROM [sys].[default_constraints] [d]
INNER JOIN [sys].[columns] [c] ON [d].[parent_column_id] = [c].[column_id] AND [d].[parent_object_id] = [c].[object_id]
WHERE ([d].[parent_object_id] = OBJECT_ID(N'[Users]') AND [c].[name] = N'Status');
IF @var5 IS NOT NULL EXEC(N'ALTER TABLE [Users] DROP CONSTRAINT [' + @var5 + '];');
ALTER TABLE [Users] DROP COLUMN [Status];
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240710081038_deleteStatusUser', N'8.0.6');
GO

COMMIT;
GO

