CREATE DATABASE  IF NOT EXISTS `dj_match` /*!40100 DEFAULT CHARACTER SET utf8 */;
USE `dj_match`;
-- MySQL dump 10.13  Distrib 5.7.17, for Win64 (x86_64)
--
-- Host: localhost    Database: dj_match
-- ------------------------------------------------------
-- Server version	5.7.20-log

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40103 SET @OLD_TIME_ZONE=@@TIME_ZONE */;
/*!40103 SET TIME_ZONE='+00:00' */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Table structure for table `djs`
--

DROP TABLE IF EXISTS `djs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `djs` (
  `id` int(11) NOT NULL,
  `name` varchar(45) NOT NULL,
  `age` int(11) DEFAULT NULL,
  `experience_years` int(11) DEFAULT NULL,
  `price_min` int(11) DEFAULT NULL,
  `price_max` int(11) DEFAULT NULL,
  `genres` varchar(4000) DEFAULT NULL,
  `address` varchar(100) DEFAULT NULL,
  `phone_num` varchar(15) DEFAULT NULL,
  `website` varchar(256) DEFAULT NULL,
  `facebook_page` varchar(256) DEFAULT NULL,
  `instagram_profile` varchar(256) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `users`
--

DROP TABLE IF EXISTS `users`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `users` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(45) DEFAULT NULL,
  `email` varchar(256) NOT NULL,
  `phone_num` varchar(15) DEFAULT NULL,
  `password` varchar(45) NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  UNIQUE KEY `email_UNIQUE` (`email`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;
/*!40103 SET TIME_ZONE=@OLD_TIME_ZONE */;

/*!40101 SET SQL_MODE=@OLD_SQL_MODE */;
/*!40014 SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS */;
/*!40014 SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS */;
/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
/*!40111 SET SQL_NOTES=@OLD_SQL_NOTES */;

--
-- Table structure for table `playlists`
--

DROP TABLE IF EXISTS `playlists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `playlists` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dj_id` int(11) NOT NULL,
  `user_id` int(11) DEFAULT NULL,
  `name` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `playlist_dj_id_fk_idx` (`dj_id`),
  KEY `playlist_user_id_fk_idx` (`user_id`),
  CONSTRAINT `playlist_dj_id_fk` FOREIGN KEY (`dj_id`) REFERENCES `djs` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `playlist_user_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `reviews`
--

DROP TABLE IF EXISTS `reviews`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `reviews` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `dj_id` int(11) NOT NULL,
  `user_id` int(11) NOT NULL,
  `text` text,
  `score` float NOT NULL,
  `date` datetime NOT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`),
  KEY `dj_id_id_fk_idx` (`dj_id`),
  KEY `user_id_id_fk_idx` (`user_id`),
  CONSTRAINT `dj_id_id_fk` FOREIGN KEY (`dj_id`) REFERENCES `djs` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `user_id_id_fk` FOREIGN KEY (`user_id`) REFERENCES `users` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `songs`
--

DROP TABLE IF EXISTS `songs`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `songs` (
  `id` int(11) NOT NULL AUTO_INCREMENT,
  `name` varchar(256) NOT NULL,
  `artist` varchar(256) NOT NULL,
  `genre` varchar(45) DEFAULT NULL,
  PRIMARY KEY (`id`),
  UNIQUE KEY `id_UNIQUE` (`id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Table structure for table `songs_to_playlists`
--

DROP TABLE IF EXISTS `songs_to_playlists`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `songs_to_playlists` (
  `song_id` int(11) NOT NULL,
  `playlist_id` int(11) NOT NULL,
  `tags` varchar(2000) DEFAULT NULL,
  PRIMARY KEY (`song_id`,`playlist_id`),
  KEY `stp_playlist_id_fk_idx` (`playlist_id`),
  CONSTRAINT `stp_playlist_id_fk` FOREIGN KEY (`playlist_id`) REFERENCES `playlists` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `stp_song_id_fk` FOREIGN KEY (`song_id`) REFERENCES `songs` (`id`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=utf8;
/*!40101 SET character_set_client = @saved_cs_client */;



-- Dump completed on 2017-12-18  0:25:53
