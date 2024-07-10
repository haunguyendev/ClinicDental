IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO
INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240625140020_V0', N'8.0.6');
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



COMMIT;
GO

