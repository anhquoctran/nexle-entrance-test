CREATE DATABASE IF NOT EXISTS entrance_test;

USE entrance_test;

CREATE TABLE IF NOT EXISTS `__EFMigrationsHistory` (
    `MigrationId` varchar(150) CHARACTER SET utf8mb4 NOT NULL,
    `ProductVersion` varchar(32) CHARACTER SET utf8mb4 NOT NULL,
    CONSTRAINT `PK___EFMigrationsHistory` PRIMARY KEY (`MigrationId`)
) CHARACTER SET = utf8mb4;

START TRANSACTION;

ALTER DATABASE CHARACTER SET utf8mb4;

CREATE TABLE `roles` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `Name` varchar(256) CHARACTER SET utf8mb4 NULL,
    `NormalizedName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `ConcurrencyStamp` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_roles` PRIMARY KEY (`Id`)
) CHARACTER SET = utf8mb4;

CREATE TABLE `users` (
    `id` int NOT NULL AUTO_INCREMENT,
    `firstName` varchar(30) CHARACTER SET utf8mb4 NULL,
    `lastName` varchar(30) CHARACTER SET utf8mb4 NULL,
    `createdAt` datetime(6) NOT NULL,
    `updatedAt` datetime(6) NULL,
    `securityStamp` longtext CHARACTER SET utf8mb4 NULL,
    `userName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `normalizedUserName` varchar(256) CHARACTER SET utf8mb4 NULL,
    `email` varchar(250) CHARACTER SET utf8mb4 NULL,
    `normalizedEmail` varchar(256) CHARACTER SET utf8mb4 NULL,
    `password` varchar(250) CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_users` PRIMARY KEY (`id`)
) CHARACTER SET = utf8mb4;

CREATE TABLE `role_claims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `RoleId` int NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_role_claims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_role_claims_roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE TABLE `tokens` (
    `id` int NOT NULL AUTO_INCREMENT,
    `userId` int NOT NULL,
    `refreshToken` longtext CHARACTER SET utf8mb4 NOT NULL,
    `expireIn` longtext CHARACTER SET utf8mb4 NOT NULL,
    `expireInMs` bigint NOT NULL DEFAULT 0,
    `createdAt` datetime(6) NOT NULL,
    `updatedAt` datetime(6) NULL,
    `userId1` int NULL,
    CONSTRAINT `PK_tokens` PRIMARY KEY (`id`),
    CONSTRAINT `FK_tokens_users_userId1` FOREIGN KEY (`userId1`) REFERENCES `users` (`id`)
) CHARACTER SET = utf8mb4;

CREATE TABLE `user_claims` (
    `Id` int NOT NULL AUTO_INCREMENT,
    `UserId` int NOT NULL,
    `ClaimType` longtext CHARACTER SET utf8mb4 NULL,
    `ClaimValue` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_user_claims` PRIMARY KEY (`Id`),
    CONSTRAINT `FK_user_claims_users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE TABLE `user_logins` (
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderKey` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `ProviderDisplayName` longtext CHARACTER SET utf8mb4 NULL,
    `UserId` int NOT NULL,
    CONSTRAINT `PK_user_logins` PRIMARY KEY (`LoginProvider`, `ProviderKey`),
    CONSTRAINT `FK_user_logins_users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE TABLE `user_roles` (
    `UserId` int NOT NULL,
    `RoleId` int NOT NULL,
    CONSTRAINT `PK_user_roles` PRIMARY KEY (`UserId`, `RoleId`),
    CONSTRAINT `FK_user_roles_roles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `roles` (`Id`) ON DELETE CASCADE,
    CONSTRAINT `FK_user_roles_users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE TABLE `user_tokens` (
    `UserId` int NOT NULL,
    `LoginProvider` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Name` varchar(255) CHARACTER SET utf8mb4 NOT NULL,
    `Value` longtext CHARACTER SET utf8mb4 NULL,
    CONSTRAINT `PK_user_tokens` PRIMARY KEY (`UserId`, `LoginProvider`, `Name`),
    CONSTRAINT `FK_user_tokens_users_UserId` FOREIGN KEY (`UserId`) REFERENCES `users` (`id`) ON DELETE CASCADE
) CHARACTER SET = utf8mb4;

CREATE INDEX `IX_role_claims_RoleId` ON `role_claims` (`RoleId`);

CREATE UNIQUE INDEX `RoleNameIndex` ON `roles` (`NormalizedName`);

CREATE INDEX `IX_tokens_userId1` ON `tokens` (`userId1`);

CREATE INDEX `IX_user_claims_UserId` ON `user_claims` (`UserId`);

CREATE INDEX `IX_user_logins_UserId` ON `user_logins` (`UserId`);

CREATE INDEX `IX_user_roles_RoleId` ON `user_roles` (`RoleId`);

CREATE INDEX `EmailIndex` ON `users` (`normalizedEmail`);

CREATE UNIQUE INDEX `UserNameIndex` ON `users` (`normalizedUserName`);

INSERT INTO
    `__EFMigrationsHistory` (`MigrationId`, `ProductVersion`)
VALUES
    ('20230305161651_InitialCreate', '6.0.14');

COMMIT;
