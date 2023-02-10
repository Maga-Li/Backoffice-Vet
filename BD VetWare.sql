-- --------------------------------------------------------
-- Anfitrião:                    127.0.0.1
-- Versão do servidor:           10.10.2-MariaDB - mariadb.org binary distribution
-- SO do servidor:               Win64
-- HeidiSQL Versão:              11.3.0.6295
-- --------------------------------------------------------

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET NAMES utf8 */;
/*!50503 SET NAMES utf8mb4 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

-- A despejar estrutura para tabela formatiagop_vetware.animals
CREATE TABLE IF NOT EXISTS `animals` (
  `IdAnimal` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `BirthDate` datetime(6) NOT NULL,
  `Gender` int(11) NOT NULL,
  `IdClient` int(11) NOT NULL,
  `IdSpecie` int(11) NOT NULL,
  `IdBreed` int(11) NOT NULL,
  PRIMARY KEY (`IdAnimal`),
  KEY `IX_Animals_IdBreed` (`IdBreed`),
  KEY `IX_Animals_IdClient` (`IdClient`),
  KEY `IX_Animals_IdSpecie` (`IdSpecie`),
  CONSTRAINT `FK_Animals_Breeds_IdBreed` FOREIGN KEY (`IdBreed`) REFERENCES `breeds` (`IdBreed`) ON DELETE CASCADE,
  CONSTRAINT `FK_Animals_Clients_IdClient` FOREIGN KEY (`IdClient`) REFERENCES `clients` (`IdClient`) ON DELETE CASCADE,
  CONSTRAINT `FK_Animals_Species_IdSpecie` FOREIGN KEY (`IdSpecie`) REFERENCES `species` (`IdSpecie`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.animals: ~6 rows (aproximadamente)
/*!40000 ALTER TABLE `animals` DISABLE KEYS */;
INSERT INTO `animals` (`IdAnimal`, `Name`, `BirthDate`, `Gender`, `IdClient`, `IdSpecie`, `IdBreed`) VALUES
	(5, 'Billy Rodrigues', '2015-04-28 00:00:00.000000', 0, 1, 1, 22),
	(7, 'Buffy Rodrigues', '2018-04-02 00:00:00.000000', 1, 3, 1, 23),
	(8, 'Lassie', '2011-07-12 00:00:00.000000', 0, 10, 2, 29),
	(9, 'Garfield', '1960-11-01 00:00:00.000000', 0, 10, 1, 27),
	(10, 'Simba', '2018-04-25 00:00:00.000000', 0, 1, 1, 17),
	(11, 'Zazu', '1994-03-15 00:00:00.000000', 0, 11, 5, 88),
	(12, 'Belzebu', '2000-04-15 00:00:00.000000', 0, 1, 5, 88);
/*!40000 ALTER TABLE `animals` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.appointments
CREATE TABLE IF NOT EXISTS `appointments` (
  `IdAppointment` int(11) NOT NULL AUTO_INCREMENT,
  `WaitingTime` datetime(6) NOT NULL,
  `Schedule` datetime(6) NOT NULL,
  `Observations` longtext DEFAULT NULL,
  `Price` decimal(65,30) NOT NULL,
  `IdAnimal` int(11) NOT NULL,
  `IdVeterinarian` int(11) NOT NULL,
  `IdMotive` int(11) NOT NULL,
  `IdPriority` int(11) NOT NULL,
  PRIMARY KEY (`IdAppointment`),
  KEY `IX_Appointments_IdAnimal` (`IdAnimal`),
  KEY `IX_Appointments_IdMotive` (`IdMotive`),
  KEY `IX_Appointments_IdPriority` (`IdPriority`),
  KEY `IX_Appointments_IdVeterinarian` (`IdVeterinarian`),
  CONSTRAINT `FK_Appointments_Animals_IdAnimal` FOREIGN KEY (`IdAnimal`) REFERENCES `animals` (`IdAnimal`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_Motives_IdMotive` FOREIGN KEY (`IdMotive`) REFERENCES `motives` (`IdMotive`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_Priorities_IdPriority` FOREIGN KEY (`IdPriority`) REFERENCES `priorities` (`IdPriority`) ON DELETE CASCADE,
  CONSTRAINT `FK_Appointments_Veterinarians_IdVeterinarian` FOREIGN KEY (`IdVeterinarian`) REFERENCES `veterinarians` (`IdVeterinarian`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=297 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.appointments: ~7 rows (aproximadamente)
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` (`IdAppointment`, `WaitingTime`, `Schedule`, `Observations`, `Price`, `IdAnimal`, `IdVeterinarian`, `IdMotive`, `IdPriority`) VALUES
	(285, '0001-01-01 00:00:00.000000', '2023-02-06 19:30:00.000000', 'N/A', 25.500000000000000000000000000000, 7, 8, 5, 1),
	(286, '0001-01-01 00:00:00.000000', '2023-02-05 23:00:00.000000', 'N/A', 169.450000000000000000000000000000, 12, 10, 6, 1),
	(288, '0001-01-01 00:00:00.000000', '2023-02-07 23:00:00.000000', 'N/A', 359.450000000000000000000000000000, 9, 9, 7, 3),
	(291, '0001-01-01 00:00:00.000000', '2023-02-18 14:25:00.000000', 'N/A', 76.900000000000000000000000000000, 5, 9, 1, 1),
	(293, '0001-01-01 00:00:00.000000', '2023-02-07 22:45:00.000000', 'N/A', 115.250000000000000000000000000000, 11, 10, 4, 2),
	(294, '0001-01-01 00:00:00.000000', '2023-02-07 20:15:00.000000', 'N/A', 140.990000000000000000000000000000, 7, 9, 7, 3),
	(295, '0001-01-01 00:00:00.000000', '2023-02-08 17:15:00.000000', 'N/A', 255.890000000000000000000000000000, 10, 25, 6, 2),
	(296, '0001-01-01 00:00:00.000000', '2023-02-08 18:20:00.000000', 'N/A', 119.550000000000000000000000000000, 5, 8, 4, 1);
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetroleclaims
CREATE TABLE IF NOT EXISTS `aspnetroleclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `RoleId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetRoleClaims_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetRoleClaims_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetroleclaims: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetroleclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroleclaims` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetroles
CREATE TABLE IF NOT EXISTS `aspnetroles` (
  `Id` varchar(255) NOT NULL,
  `Name` varchar(256) DEFAULT NULL,
  `NormalizedName` varchar(256) DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `RoleNameIndex` (`NormalizedName`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetroles: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetroles` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetuserclaims
CREATE TABLE IF NOT EXISTS `aspnetuserclaims` (
  `Id` int(11) NOT NULL AUTO_INCREMENT,
  `UserId` varchar(255) NOT NULL,
  `ClaimType` longtext DEFAULT NULL,
  `ClaimValue` longtext DEFAULT NULL,
  PRIMARY KEY (`Id`),
  KEY `IX_AspNetUserClaims_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserClaims_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetuserclaims: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetuserclaims` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserclaims` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetuserlogins
CREATE TABLE IF NOT EXISTS `aspnetuserlogins` (
  `LoginProvider` varchar(128) NOT NULL,
  `ProviderKey` varchar(128) NOT NULL,
  `ProviderDisplayName` longtext DEFAULT NULL,
  `UserId` varchar(255) NOT NULL,
  PRIMARY KEY (`LoginProvider`,`ProviderKey`),
  KEY `IX_AspNetUserLogins_UserId` (`UserId`),
  CONSTRAINT `FK_AspNetUserLogins_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetuserlogins: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetuserlogins` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserlogins` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetuserroles
CREATE TABLE IF NOT EXISTS `aspnetuserroles` (
  `UserId` varchar(255) NOT NULL,
  `RoleId` varchar(255) NOT NULL,
  PRIMARY KEY (`UserId`,`RoleId`),
  KEY `IX_AspNetUserRoles_RoleId` (`RoleId`),
  CONSTRAINT `FK_AspNetUserRoles_AspNetRoles_RoleId` FOREIGN KEY (`RoleId`) REFERENCES `aspnetroles` (`Id`) ON DELETE CASCADE,
  CONSTRAINT `FK_AspNetUserRoles_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetuserroles: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetuserroles` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetuserroles` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetusers
CREATE TABLE IF NOT EXISTS `aspnetusers` (
  `Id` varchar(255) NOT NULL,
  `FirstName` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `LastName` varchar(255) CHARACTER SET utf8mb3 COLLATE utf8mb3_general_ci NOT NULL,
  `UserName` varchar(256) DEFAULT NULL,
  `NormalizedUserName` varchar(256) DEFAULT NULL,
  `Email` varchar(256) DEFAULT NULL,
  `NormalizedEmail` varchar(256) DEFAULT NULL,
  `EmailConfirmed` tinyint(1) NOT NULL,
  `PasswordHash` longtext DEFAULT NULL,
  `SecurityStamp` longtext DEFAULT NULL,
  `ConcurrencyStamp` longtext DEFAULT NULL,
  `PhoneNumber` longtext DEFAULT NULL,
  `PhoneNumberConfirmed` tinyint(1) NOT NULL,
  `TwoFactorEnabled` tinyint(1) NOT NULL,
  `LockoutEnd` datetime(6) DEFAULT NULL,
  `LockoutEnabled` tinyint(1) NOT NULL,
  `AccessFailedCount` int(11) NOT NULL,
  PRIMARY KEY (`Id`),
  UNIQUE KEY `UserNameIndex` (`NormalizedUserName`),
  KEY `EmailIndex` (`NormalizedEmail`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetusers: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetusers` DISABLE KEYS */;
INSERT INTO `aspnetusers` (`Id`, `FirstName`, `LastName`, `UserName`, `NormalizedUserName`, `Email`, `NormalizedEmail`, `EmailConfirmed`, `PasswordHash`, `SecurityStamp`, `ConcurrencyStamp`, `PhoneNumber`, `PhoneNumberConfirmed`, `TwoFactorEnabled`, `LockoutEnd`, `LockoutEnabled`, `AccessFailedCount`) VALUES
	('3cfb75c7-6db9-4545-aba0-d370db36d29b', 'Billy', 'Vanilly', 'billy@hotmail.com', 'BILLY@HOTMAIL.COM', 'billy@hotmail.com', 'BILLY@HOTMAIL.COM', 0, 'AQAAAAEAACcQAAAAEBthm2Ajx78MgnbW04pFh87BOk0IG7JVm+ojslrxVuxUP+Tlr9Jwd1/1XDHTlto/JQ==', 'VNSHP42U2SXY4WUCBJDDICO7KYWA2M2F', '793a7b5a-49c6-44c0-9722-31a41bac4e75', NULL, 0, 0, NULL, 1, 0),
	('57586931-8227-4fe3-bb18-db0282c11574', 'Liliana', 'Rodrigues', 'closing_soon@hotmail.com', 'CLOSING_SOON@HOTMAIL.COM', 'closing_soon@hotmail.com', 'CLOSING_SOON@HOTMAIL.COM', 0, 'AQAAAAEAACcQAAAAELzPwAgvic9B59ZCVBkMlcP483AFIL4Rn/RS1xEBL0G81sDy/jBTrPhfLbQY5uxGpA==', 'GTDAKFK5I3QK23MWSXLS44YFLOVKA2R3', '680e2011-d57c-4500-a6bc-cc63a7ed8b27', NULL, 0, 0, NULL, 1, 0);
/*!40000 ALTER TABLE `aspnetusers` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.aspnetusertokens
CREATE TABLE IF NOT EXISTS `aspnetusertokens` (
  `UserId` varchar(255) NOT NULL,
  `LoginProvider` varchar(128) NOT NULL,
  `Name` varchar(128) NOT NULL,
  `Value` longtext DEFAULT NULL,
  PRIMARY KEY (`UserId`,`LoginProvider`,`Name`),
  CONSTRAINT `FK_AspNetUserTokens_AspNetUsers_UserId` FOREIGN KEY (`UserId`) REFERENCES `aspnetusers` (`Id`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.aspnetusertokens: ~0 rows (aproximadamente)
/*!40000 ALTER TABLE `aspnetusertokens` DISABLE KEYS */;
/*!40000 ALTER TABLE `aspnetusertokens` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.breeds
CREATE TABLE IF NOT EXISTS `breeds` (
  `IdBreed` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `IdSpecie` int(11) NOT NULL,
  PRIMARY KEY (`IdBreed`),
  KEY `IX_Breeds_IdSpecie` (`IdSpecie`),
  CONSTRAINT `FK_Breeds_Species_IdSpecie` FOREIGN KEY (`IdSpecie`) REFERENCES `species` (`IdSpecie`) ON DELETE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=96 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.breeds: ~86 rows (aproximadamente)
/*!40000 ALTER TABLE `breeds` DISABLE KEYS */;
INSERT INTO `breeds` (`IdBreed`, `Name`, `IdSpecie`) VALUES
	(1, 'Abissínio', 1),
	(2, 'American Curl', 1),
	(3, 'American Shorthair', 1),
	(4, 'Angorá Turco', 1),
	(5, 'Balinês', 1),
	(6, 'Bengal', 1),
	(7, 'Bombain', 1),
	(8, 'Bosque da Noruega', 1),
	(9, 'British Shorthair', 1),
	(10, 'Burmês', 1),
	(11, 'Comum Europeu', 1),
	(12, 'Cornish Rex', 1),
	(13, 'Devon Rex', 1),
	(14, 'Exótico de pelo curto', 1),
	(15, 'German Rex', 1),
	(16, 'Maine Coon', 1),
	(17, 'Mau Egípcio', 1),
	(18, 'Munchkin', 1),
	(19, 'Persa', 1),
	(20, 'Ragdoll', 1),
	(21, 'Scottish Fold', 1),
	(22, 'Siamês', 1),
	(23, 'Snowshoe', 1),
	(24, 'Somali', 1),
	(25, 'Sphynx', 1),
	(26, 'Thai', 1),
	(27, 'Outro', 1),
	(28, 'Beagle', 2),
	(29, 'Border Collie', 2),
	(30, 'Boxer', 2),
	(31, 'Buldogue', 2),
	(32, 'Bull Terrier', 2),
	(33, 'Caniche', 2),
	(34, 'Chihuahua', 2),
	(35, 'Chow-chow', 2),
	(36, 'Cocker Spaniel', 2),
	(37, 'Dachshund', 2),
	(38, 'Dálmata', 2),
	(39, 'Doberman', 2),
	(40, 'Galgo', 2),
	(41, 'Golden Retriever', 2),
	(42, 'Husky', 2),
	(43, 'Labrador', 2),
	(44, 'Pastor Alemão', 2),
	(45, 'Pinscher', 2),
	(46, 'Pug', 2),
	(47, 'Rafeiro Alentejano', 2),
	(48, 'Rottweiler', 2),
	(49, 'Serra da Estrela', 2),
	(50, 'Shar-pei', 2),
	(51, 'Shih Tzu', 2),
	(52, 'Skye Terrier', 2),
	(53, 'Yorkshire Terrier', 2),
	(54, 'Outro', 2),
	(55, 'Chinchila', 3),
	(56, 'Coelho Azul de Viena', 3),
	(57, 'Coelho Angorá', 3),
	(58, 'Coelho-comum', 3),
	(59, 'Coelho Holandês', 3),
	(60, 'Coelho Rex', 3),
	(61, 'Coelho Teddy', 3),
	(62, 'Degu', 3),
	(63, 'Furão', 3),
	(64, 'Gerbo', 3),
	(65, 'Hamster-comum', 3),
	(66, 'Hamster-sírio', 3),
	(67, 'Lemingue', 3),
	(68, 'Ouriço Pigmeu', 3),
	(69, 'Porquinho-da-índia', 3),
	(70, 'Rato do campo', 3),
	(71, 'Outro', 3),
	(72, 'Agapornis', 4),
	(73, 'Arara', 4),
	(74, 'Canário', 4),
	(75, 'Caturra', 4),
	(76, 'Diamante-de-gould', 4),
	(77, 'Estrildídeo', 4),
	(78, 'Mandarim', 4),
	(79, 'Papagaio-cinzento', 4),
	(80, 'Periquito-catarina', 4),
	(81, 'Periquito-comum', 4),
	(82, 'Periquito-monge', 4),
	(83, 'Outro', 4),
	(84, 'Cágado de carapaça estriada', 5),
	(85, 'Camaleão', 5),
	(86, 'Cobra-do-milho', 5),
	(87, 'Cobra-liga', 5),
	(88, 'Dragão Barbudo', 5),
	(89, 'Dragão-d\'água-chinês', 5),
	(90, 'Gecko Leopardo', 5),
	(91, 'Iguana', 5),
	(92, 'Osga-comum', 5),
	(93, 'Tartaruga-comum', 5),
	(94, 'Tartaruga mediterrânea', 5),
	(95, 'Outro', 5);
/*!40000 ALTER TABLE `breeds` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.clients
CREATE TABLE IF NOT EXISTS `clients` (
  `IdClient` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  `Address` longtext DEFAULT NULL,
  `City` longtext DEFAULT NULL,
  `Phone` int(11) NOT NULL,
  `Email` longtext DEFAULT NULL,
  `TaxNumber` int(11) NOT NULL,
  PRIMARY KEY (`IdClient`)
) ENGINE=InnoDB AUTO_INCREMENT=13 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.clients: ~5 rows (aproximadamente)
/*!40000 ALTER TABLE `clients` DISABLE KEYS */;
INSERT INTO `clients` (`IdClient`, `Name`, `Address`, `City`, `Phone`, `Email`, `TaxNumber`) VALUES
	(1, 'Lila Rodrigues', 'Rua dos gatos', 'Lagoa', 912344536, 'liliana@gmail.com', 211433256),
	(3, 'João Figueiras', 'Rua das flores lindas', 'Portimão', 912234234, 'joaobatepe@sapo.pt', 209223442),
	(10, 'Guilherme Paiva', 'Travessa do paganismo', 'Faro', 932387884, 'guipaiva@gmail.com', 264453456),
	(11, 'Ana Paiva', 'Largo dos beijinhos', 'Lagoa', 936678365, 'anapaiva@sapo.pt', 208772667),
	(12, 'Lisa Simpson', 'Evergreen Terrace 742', 'Springfied', 934490309, 'lisasimpson@gmail.com', 253345336);
/*!40000 ALTER TABLE `clients` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.motives
CREATE TABLE IF NOT EXISTS `motives` (
  `IdMotive` int(11) NOT NULL AUTO_INCREMENT,
  `Description` longtext DEFAULT NULL,
  PRIMARY KEY (`IdMotive`)
) ENGINE=InnoDB AUTO_INCREMENT=9 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.motives: ~8 rows (aproximadamente)
/*!40000 ALTER TABLE `motives` DISABLE KEYS */;
INSERT INTO `motives` (`IdMotive`, `Description`) VALUES
	(1, 'Vacinação'),
	(2, 'Desparasitação interna'),
	(3, 'Desparasitação externa'),
	(4, 'Análises/Exames'),
	(5, 'Consulta geral'),
	(6, 'Tratamentos'),
	(7, 'Cirurgias'),
	(8, 'Outro');
/*!40000 ALTER TABLE `motives` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.priorities
CREATE TABLE IF NOT EXISTS `priorities` (
  `IdPriority` int(11) NOT NULL AUTO_INCREMENT,
  `Type` longtext DEFAULT NULL,
  PRIMARY KEY (`IdPriority`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.priorities: ~2 rows (aproximadamente)
/*!40000 ALTER TABLE `priorities` DISABLE KEYS */;
INSERT INTO `priorities` (`IdPriority`, `Type`) VALUES
	(1, 'Baixa'),
	(2, 'Média'),
	(3, 'Alta');
/*!40000 ALTER TABLE `priorities` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.species
CREATE TABLE IF NOT EXISTS `species` (
  `IdSpecie` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  PRIMARY KEY (`IdSpecie`)
) ENGINE=InnoDB AUTO_INCREMENT=6 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.species: ~4 rows (aproximadamente)
/*!40000 ALTER TABLE `species` DISABLE KEYS */;
INSERT INTO `species` (`IdSpecie`, `Name`) VALUES
	(1, 'Gatos'),
	(2, 'Cães'),
	(3, 'Roedores e Furões'),
	(4, 'Pássaros'),
	(5, 'Reptéis');
/*!40000 ALTER TABLE `species` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.veterinarians
CREATE TABLE IF NOT EXISTS `veterinarians` (
  `IdVeterinarian` int(11) NOT NULL AUTO_INCREMENT,
  `Name` longtext DEFAULT NULL,
  PRIMARY KEY (`IdVeterinarian`)
) ENGINE=InnoDB AUTO_INCREMENT=29 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.veterinarians: ~4 rows (aproximadamente)
/*!40000 ALTER TABLE `veterinarians` DISABLE KEYS */;
INSERT INTO `veterinarians` (`IdVeterinarian`, `Name`) VALUES
	(8, 'Gaia Terra'),
	(9, 'Hecaté'),
	(10, 'Fernando Pessoa'),
	(25, 'Edgar A. Poe');
/*!40000 ALTER TABLE `veterinarians` ENABLE KEYS */;

-- A despejar estrutura para tabela formatiagop_vetware.__efmigrationshistory
CREATE TABLE IF NOT EXISTS `__efmigrationshistory` (
  `MigrationId` varchar(150) NOT NULL,
  `ProductVersion` varchar(32) NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

-- A despejar dados para tabela formatiagop_vetware.__efmigrationshistory: ~5 rows (aproximadamente)
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` (`MigrationId`, `ProductVersion`) VALUES
	('20221215005248_UserVersion1', '6.0.12'),
	('20221218162159_UserVersion1', '6.0.12'),
	('20221218172037_UserVersion1', '6.0.12'),
	('20221218172508_UserVersion1', '6.0.12'),
	('20221218195037_UserVersion1', '6.0.12');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;

/*!40101 SET SQL_MODE=IFNULL(@OLD_SQL_MODE, '') */;
/*!40014 SET FOREIGN_KEY_CHECKS=IFNULL(@OLD_FOREIGN_KEY_CHECKS, 1) */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40111 SET SQL_NOTES=IFNULL(@OLD_SQL_NOTES, 1) */;
