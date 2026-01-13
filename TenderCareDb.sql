-- MySQL dump 10.13  Distrib 8.0.41, for Win64 (x86_64)
--
-- Host: localhost    Database: tendercaredb
-- ------------------------------------------------------
-- Server version	8.0.41

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!50503 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `__efmigrationshistory`
--

DROP TABLE IF EXISTS `__efmigrationshistory`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `__efmigrationshistory` (
  `MigrationId` varchar(150) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ProductVersion` varchar(32) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  PRIMARY KEY (`MigrationId`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `__efmigrationshistory`
--

LOCK TABLES `__efmigrationshistory` WRITE;
/*!40000 ALTER TABLE `__efmigrationshistory` DISABLE KEYS */;
INSERT INTO `__efmigrationshistory` VALUES ('20260112131340_InitialCreate','9.0.0'),('20260112142120_CompleteSystem','9.0.0'),('20260112143714_FinalFix','9.0.0');
/*!40000 ALTER TABLE `__efmigrationshistory` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `appointments`
--

DROP TABLE IF EXISTS `appointments`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `appointments` (
  `AppointmentID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PatientID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `AppointmentDate` datetime(6) DEFAULT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Notes` text,
  PRIMARY KEY (`AppointmentID`),
  KEY `IX_Appointments_PatientID` (`PatientID`),
  CONSTRAINT `FK_Appointments_Patients_PatientID` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `appointments`
--

LOCK TABLES `appointments` WRITE;
/*!40000 ALTER TABLE `appointments` DISABLE KEYS */;
INSERT INTO `appointments` VALUES ('A43874417','P41794451','Checkup','2026-01-29 00:00:00.000000','Scheduled','Buntis'),('A52770562','P51590564','Immunization','2026-02-11 00:00:00.000000','Scheduled',NULL),('A54319744','P51590564','Checkup','2026-01-17 00:00:00.000000','Completed',NULL),('A68881692','P67047572','Vaccination','2026-01-22 00:00:00.000000','Completed',NULL),('A79011815','P51590564','Immunization','2026-12-01 00:00:00.000000','Scheduled',NULL);
/*!40000 ALTER TABLE `appointments` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `immunization`
--

DROP TABLE IF EXISTS `immunization`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `immunization` (
  `ImmunizationID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PatientID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `VaccineName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DoseNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DateAdministered` datetime(6) DEFAULT NULL,
  `NextDueDate` datetime(6) DEFAULT NULL,
  `Status` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`ImmunizationID`),
  KEY `IX_Immunization_PatientID` (`PatientID`),
  CONSTRAINT `FK_Immunization_Patients_PatientID` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `immunization`
--

LOCK TABLES `immunization` WRITE;
/*!40000 ALTER TABLE `immunization` DISABLE KEYS */;
/*!40000 ALTER TABLE `immunization` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `medicalrecords`
--

DROP TABLE IF EXISTS `medicalrecords`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `medicalrecords` (
  `RecordID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PatientID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ServiceType` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Diagnosis` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Notes` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DoctorName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `DateRecorded` datetime(6) DEFAULT NULL,
  PRIMARY KEY (`RecordID`),
  KEY `IX_MedicalRecords_PatientID` (`PatientID`),
  CONSTRAINT `FK_MedicalRecords_Patients_PatientID` FOREIGN KEY (`PatientID`) REFERENCES `patients` (`PatientID`) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `medicalrecords`
--

LOCK TABLES `medicalrecords` WRITE;
/*!40000 ALTER TABLE `medicalrecords` DISABLE KEYS */;
/*!40000 ALTER TABLE `medicalrecords` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `patients`
--

DROP TABLE IF EXISTS `patients`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `patients` (
  `PatientID` varchar(255) CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `PatientName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `DateOfBirth` datetime(6) NOT NULL,
  `Gender` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `GuardianName` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `ContactNumber` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Address` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  PRIMARY KEY (`PatientID`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `patients`
--

LOCK TABLES `patients` WRITE;
/*!40000 ALTER TABLE `patients` DISABLE KEYS */;
INSERT INTO `patients` VALUES ('P15035572','Mary','2025-12-25 00:00:00.000000','Female','Cj','09999999999','cj@gmail.com','bahaybahayan'),('P20339747','sheunddd','2025-11-12 00:00:00.000000','Male','haha','09999999999','sheun@gmail.com','bahay na walang sigawan'),('P41794451','Maury','2024-03-14 00:00:00.000000','Female','Junjun','099384749382','junjun@gmail.com','house'),('P48687029','q','2025-12-18 00:00:00.000000','Male','dws','098080708087','oep@gmail.com','hwhw'),('P51590564','Geo','2025-12-17 00:00:00.000000','Male','Sheun','09953330490','Presheun5@gmail.com','house'),('P67047572','Christian James Eustaquio','2025-12-12 00:00:00.000000','Male','Harley Andrew Azana','09978695392','harleyazana@gmail.com','manila city');
/*!40000 ALTER TABLE `patients` ENABLE KEYS */;
UNLOCK TABLES;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!50503 SET character_set_client = utf8mb4 */;
CREATE TABLE `users` (
  `UserID` int NOT NULL AUTO_INCREMENT,
  `FirstName` varchar(255) DEFAULT NULL,
  `LastName` varchar(255) DEFAULT NULL,
  `Email` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Phone` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci,
  `Password` longtext CHARACTER SET utf8mb4 COLLATE utf8mb4_0900_ai_ci NOT NULL,
  `Role` varchar(50) DEFAULT 'User',
  PRIMARY KEY (`UserID`)
) ENGINE=InnoDB AUTO_INCREMENT=4 DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_0900_ai_ci;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `users`
--

LOCK TABLES `users` WRITE;
/*!40000 ALTER TABLE `users` DISABLE KEYS */;
INSERT INTO `users` VALUES (1,'Sheun','Pre','Presheun5@gmail.com','09953330490','Sheun','User'),(2,'Christian James','Eustaquio','cjames@gmail.com','09776437300','cjames','User'),(3,'Cj','Eustaquio','cj@gmail.com',NULL,'cjcj','User');
/*!40000 ALTER TABLE `users` ENABLE KEYS */;
UNLOCK TABLES;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

-- Dump completed on 2026-01-13 16:03:17
