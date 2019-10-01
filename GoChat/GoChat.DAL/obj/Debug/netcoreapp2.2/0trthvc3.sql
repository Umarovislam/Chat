  IF EXISTS(SELECT 1 FROM information_schema.tables 
  WHERE table_name = '
__EFMigrationsHistory' AND table_schema = DATABASE()) 
BEGIN
CREATE TABLE `gochatdb.__EFMigrationsHistory` (
    `MigrationId` varchar(150) NOT NULL,
    `ProductVersion` varchar(32) NOT NULL,
    PRIMARY KEY (`MigrationId`)
);

END;

CREATE TABLE `PreChat` (
    `Id` int NOT NULL AUTO_INCREMENT,
    PRIMARY KEY (`Id`)
);

CREATE TABLE `Chats` (
    `Id` varchar(767) NOT NULL,
    `UsersId` int NULL,
    `Messages` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Chats_PreChat_UsersId` FOREIGN KEY (`UsersId`) REFERENCES `PreChat` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Users` (
    `Id` varchar(767) NOT NULL,
    `UserName` text NULL,
    `NormalizedUserName` text NULL,
    `Email` text NULL,
    `NormalizedEmail` text NULL,
    `EmailConfirmed` bit NOT NULL,
    `PasswordHash` text NULL,
    `SecurityStamp` text NULL,
    `ConcurrencyStamp` text NULL,
    `PhoneNumber` text NULL,
    `PhoneNumberConfirmed` bit NOT NULL,
    `TwoFactorEnabled` bit NOT NULL,
    `LockoutEnd` timestamp NULL,
    `LockoutEnabled` bit NOT NULL,
    `AccessFailedCount` int NOT NULL,
    `ChatsId` int NULL,
    `PhotoLocation` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Users_PreChat_ChatsId` FOREIGN KEY (`ChatsId`) REFERENCES `PreChat` (`Id`) ON DELETE RESTRICT
);

CREATE TABLE `Friends` (
    `Id` varchar(767) NOT NULL,
    `UserId` varchar(767) NULL,
    `who` text NULL,
    PRIMARY KEY (`Id`),
    CONSTRAINT `FK_Friends_Users_UserId` FOREIGN KEY (`UserId`) REFERENCES `Users` (`Id`) ON DELETE RESTRICT
);

CREATE INDEX `IX_Chats_UsersId` ON `Chats` (`UsersId`);

CREATE INDEX `IX_Friends_UserId` ON `Friends` (`UserId`);

CREATE INDEX `IX_Users_ChatsId` ON `Users` (`ChatsId`);

INSERT INTO `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES ('20191001162601_Mig1', '2.2.6-servicing-10079');

