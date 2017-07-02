/*
Navicat MySQL Data Transfer

Source Server         : localhost_3306
Source Server Version : 100108
Source Host           : localhost:3306
Source Database       : angelhack

Target Server Type    : MYSQL
Target Server Version : 100108
File Encoding         : 65001

Date: 2017-07-02 09:59:28
*/

SET FOREIGN_KEY_CHECKS=0;

-- ----------------------------
-- Table structure for requestbids
-- ----------------------------
DROP TABLE IF EXISTS `requestbids`;
CREATE TABLE `requestbids` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `UserID` int(11) NOT NULL,
  `RequestID` int(11) NOT NULL,
  `Price` decimal(10,2) NOT NULL DEFAULT '0.00',
  `IsAwarded` bit(1) DEFAULT b'0',
  `ShopName` varchar(255) DEFAULT NULL,
  `ShopAddress` varchar(255) DEFAULT NULL,
  `BidDate` datetime NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  KEY `RequestID` (`RequestID`),
  CONSTRAINT `requestbids_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON UPDATE CASCADE,
  CONSTRAINT `requestbids_ibfk_2` FOREIGN KEY (`RequestID`) REFERENCES `requests` (`ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=3 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of requestbids
-- ----------------------------
INSERT INTO `requestbids` VALUES ('1', '2', '56', '0.00', '\0', null, null, '2017-07-02 09:36:14');
INSERT INTO `requestbids` VALUES ('2', '2', '62', '0.00', '', null, null, '2017-07-02 09:36:16');

-- ----------------------------
-- Table structure for requestnotifications
-- ----------------------------
DROP TABLE IF EXISTS `requestnotifications`;
CREATE TABLE `requestnotifications` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `RequestID` int(11) NOT NULL,
  `UserID` int(11) NOT NULL,
  `IsAccepted` bit(1) NOT NULL DEFAULT b'0',
  `DateReceived` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateLastUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `RequestorID` int(11) NOT NULL,
  `IsDeclined` bit(1) NOT NULL DEFAULT b'0',
  PRIMARY KEY (`ID`),
  KEY `RequestID` (`RequestID`),
  KEY `UserID` (`UserID`),
  KEY `RequestorID` (`RequestorID`),
  CONSTRAINT `requestnotifications_ibfk_1` FOREIGN KEY (`RequestID`) REFERENCES `requests` (`ID`) ON UPDATE CASCADE,
  CONSTRAINT `requestnotifications_ibfk_2` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON UPDATE CASCADE,
  CONSTRAINT `requestnotifications_ibfk_3` FOREIGN KEY (`RequestorID`) REFERENCES `users` (`ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=36 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of requestnotifications
-- ----------------------------
INSERT INTO `requestnotifications` VALUES ('1', '32', '2', '\0', '2017-07-02 08:26:59', '2017-07-02 09:48:24', '2', '');
INSERT INTO `requestnotifications` VALUES ('2', '35', '2', '\0', '2017-07-02 08:28:26', '2017-07-02 09:48:23', '2', '');
INSERT INTO `requestnotifications` VALUES ('3', '36', '2', '\0', '2017-07-02 08:32:22', '2017-07-02 09:48:22', '2', '');
INSERT INTO `requestnotifications` VALUES ('4', '37', '2', '\0', '2017-07-02 08:33:08', '2017-07-02 09:48:21', '2', '');
INSERT INTO `requestnotifications` VALUES ('5', '38', '2', '\0', '2017-07-02 08:33:49', '2017-07-02 09:48:19', '2', '');
INSERT INTO `requestnotifications` VALUES ('6', '39', '2', '\0', '2017-07-02 08:35:30', '2017-07-02 09:48:27', '2', '');
INSERT INTO `requestnotifications` VALUES ('7', '40', '2', '\0', '2017-07-02 08:35:38', '2017-07-02 09:48:26', '2', '');
INSERT INTO `requestnotifications` VALUES ('8', '41', '2', '\0', '2017-07-02 08:38:09', '2017-07-02 09:48:21', '2', '');
INSERT INTO `requestnotifications` VALUES ('9', '42', '2', '\0', '2017-07-02 08:39:04', '2017-07-02 09:48:19', '2', '');
INSERT INTO `requestnotifications` VALUES ('10', '43', '2', '\0', '2017-07-02 08:51:08', '2017-07-02 09:48:18', '2', '');
INSERT INTO `requestnotifications` VALUES ('11', '44', '2', '\0', '2017-07-02 08:52:36', '2017-07-02 09:48:16', '2', '');
INSERT INTO `requestnotifications` VALUES ('12', '45', '2', '\0', '2017-07-02 08:53:18', '2017-07-02 09:48:15', '2', '');
INSERT INTO `requestnotifications` VALUES ('13', '46', '2', '', '2017-07-02 08:55:05', '2017-07-02 08:55:10', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('14', '47', '2', '', '2017-07-02 08:56:18', '2017-07-02 08:56:19', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('15', '48', '2', '', '2017-07-02 08:56:47', '2017-07-02 08:56:49', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('16', '49', '2', '', '2017-07-02 08:57:27', '2017-07-02 08:57:29', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('17', '50', '2', '', '2017-07-02 08:58:18', '2017-07-02 08:58:19', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('18', '51', '2', '\0', '2017-07-02 09:00:12', '2017-07-02 09:48:13', '2', '');
INSERT INTO `requestnotifications` VALUES ('19', '52', '2', '', '2017-07-02 09:02:06', '2017-07-02 09:02:09', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('20', '53', '2', '', '2017-07-02 09:03:44', '2017-07-02 09:03:45', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('21', '54', '2', '', '2017-07-02 09:07:37', '2017-07-02 09:07:39', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('22', '55', '2', '', '2017-07-02 09:12:15', '2017-07-02 09:12:16', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('23', '56', '2', '', '2017-07-02 09:20:48', '2017-07-02 09:20:50', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('24', '57', '2', '\0', '2017-07-02 09:26:01', '2017-07-02 09:48:12', '2', '');
INSERT INTO `requestnotifications` VALUES ('25', '58', '2', '\0', '2017-07-02 09:26:42', '2017-07-02 09:48:11', '2', '');
INSERT INTO `requestnotifications` VALUES ('26', '59', '2', '\0', '2017-07-02 09:29:43', '2017-07-02 09:48:09', '2', '');
INSERT INTO `requestnotifications` VALUES ('27', '60', '2', '\0', '2017-07-02 09:30:10', '2017-07-02 09:48:07', '2', '');
INSERT INTO `requestnotifications` VALUES ('28', '61', '2', '\0', '2017-07-02 09:31:01', '2017-07-02 09:48:05', '2', '');
INSERT INTO `requestnotifications` VALUES ('29', '62', '2', '', '2017-07-02 09:33:06', '2017-07-02 09:33:08', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('30', '63', '2', '', '2017-07-02 09:48:30', '2017-07-02 09:49:11', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('31', '64', '2', '', '2017-07-02 09:49:25', '2017-07-02 09:51:23', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('32', '65', '2', '\0', '2017-07-02 09:52:36', '2017-07-02 09:53:53', '2', '');
INSERT INTO `requestnotifications` VALUES ('33', '66', '2', '', '2017-07-02 09:53:27', '2017-07-02 09:53:33', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('34', '67', '2', '', '2017-07-02 09:54:55', '2017-07-02 09:55:01', '2', '\0');
INSERT INTO `requestnotifications` VALUES ('35', '68', '2', '', '2017-07-02 09:57:48', '2017-07-02 09:57:51', '2', '\0');

-- ----------------------------
-- Table structure for requests
-- ----------------------------
DROP TABLE IF EXISTS `requests`;
CREATE TABLE `requests` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(1000) NOT NULL,
  `CurrentLocation` varchar(255) NOT NULL,
  `StatusID` int(11) NOT NULL,
  `DateRequested` datetime NOT NULL DEFAULT CURRENT_TIMESTAMP,
  `DateLastUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  `DeliveryDate` datetime NOT NULL,
  `UserID` int(11) NOT NULL,
  PRIMARY KEY (`ID`),
  KEY `UserID` (`UserID`),
  CONSTRAINT `requests_ibfk_1` FOREIGN KEY (`UserID`) REFERENCES `users` (`ID`) ON UPDATE CASCADE
) ENGINE=InnoDB AUTO_INCREMENT=69 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of requests
-- ----------------------------
INSERT INTO `requests` VALUES ('2', 'pizza', 'aspace', '1', '2017-07-02 06:44:47', '2017-07-02 08:19:14', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('3', 'pizza', 'pizaa', '1', '2017-07-02 07:21:41', '2017-07-02 08:19:09', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('4', 'pizza', 'pizza', '1', '2017-07-02 07:24:44', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('5', 'pizza', 'pizza', '1', '2017-07-02 07:26:45', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('6', 'pizza', 'pizza', '1', '2017-07-02 07:27:36', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('7', 'pizza', 'pizza', '1', '2017-07-02 07:27:38', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('8', 'pizza', 'pizza', '1', '2017-07-02 07:27:39', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('9', 'pizza', 'pizza', '1', '2017-07-02 07:27:40', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('10', 'pizza', 'pizza', '1', '2017-07-02 07:27:41', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('11', 'pizza', 'pizza', '1', '2017-07-02 07:27:42', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('12', 'pizza', 'pizza', '1', '2017-07-02 07:27:43', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('13', 'pizza', 'pizza', '1', '2017-07-02 07:27:44', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('14', 'pizza', 'pizza', '1', '2017-07-02 07:27:45', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('15', 'pizza', 'pizza', '1', '2017-07-02 07:27:46', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('16', 'pizza', 'pizza', '1', '2017-07-02 07:27:49', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('17', 'pizza', 'pizza', '1', '2017-07-02 07:27:50', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('18', 'pizza', 'pizza', '1', '2017-07-02 07:28:43', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('19', 'pizza', 'pizza', '1', '2017-07-02 07:28:45', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('20', 'pizza', 'pizza', '1', '2017-07-02 07:28:46', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('21', 'pizza', 'pizza', '1', '2017-07-02 07:28:46', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('22', 'pizza', 'pizza', '1', '2017-07-02 07:28:47', '2017-07-02 08:19:15', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('23', 'pizza', 'pizza', '1', '2017-07-02 07:28:47', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('24', 'pizza', 'pizza', '1', '2017-07-02 07:28:48', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('25', 'pizza', 'pizza', '1', '2017-07-02 07:28:48', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('26', 'pizza', 'pizza', '1', '2017-07-02 07:28:49', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('27', 'pizza', 'pizza', '1', '2017-07-02 07:28:50', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('28', 'pizza', 'pizza', '1', '2017-07-02 07:28:51', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('29', 'pizza', 'pizza', '1', '2017-07-02 07:28:51', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('30', 'pizza', 'pizza', '1', '2017-07-02 08:15:14', '2017-07-02 08:19:16', '2017-07-02 08:19:07', '2');
INSERT INTO `requests` VALUES ('31', 'pizza', 'pizza', '1', '2017-07-02 08:24:19', '2017-07-02 08:24:19', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('32', 'pizza', 'pizza', '1', '2017-07-02 08:26:59', '2017-07-02 08:26:59', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('33', 'pizza', 'pizza', '1', '2017-07-02 08:27:17', '2017-07-02 08:27:17', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('34', 'pizza', 'pizza', '1', '2017-07-02 08:28:01', '2017-07-02 08:28:01', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('35', 'pizza', 'pizza', '1', '2017-07-02 08:28:25', '2017-07-02 08:28:25', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('36', 'pizza', 'pizza', '1', '2017-07-02 08:32:22', '2017-07-02 08:32:22', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('37', 'pizza', 'pizza', '1', '2017-07-02 08:33:08', '2017-07-02 08:33:08', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('38', 'pizza', 'pizza', '1', '2017-07-02 08:33:49', '2017-07-02 08:33:49', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('39', 'pizza', 'pizza', '1', '2017-07-02 08:35:30', '2017-07-02 08:35:30', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('40', 'pizza', 'pizza', '1', '2017-07-02 08:35:37', '2017-07-02 08:35:37', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('41', 'pizza', 'pizza', '1', '2017-07-02 08:38:09', '2017-07-02 08:38:09', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('42', 'pizza', 'pizza', '1', '2017-07-02 08:39:04', '2017-07-02 08:39:04', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('43', 'pizza', 'pizza', '1', '2017-07-02 08:51:07', '2017-07-02 08:51:07', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('44', 'pizza', 'pizza', '1', '2017-07-02 08:52:35', '2017-07-02 08:52:35', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('45', 'pizza', 'pizza', '1', '2017-07-02 08:53:18', '2017-07-02 08:53:18', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('46', 'pizza', 'pizza', '1', '2017-07-02 08:55:04', '2017-07-02 08:55:04', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('47', 'pizza', 'pizza', '1', '2017-07-02 08:56:17', '2017-07-02 08:56:17', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('48', 'pizza', 'pizza', '1', '2017-07-02 08:56:47', '2017-07-02 08:56:47', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('49', 'pizza', 'pizza', '1', '2017-07-02 08:57:27', '2017-07-02 08:57:27', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('50', 'pizza', 'pizza', '1', '2017-07-02 08:58:17', '2017-07-02 08:58:17', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('51', 'pizza', 'pizza', '1', '2017-07-02 09:00:11', '2017-07-02 09:00:11', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('52', 'pizza', 'pizza', '1', '2017-07-02 09:02:06', '2017-07-02 09:02:06', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('53', 'pizza', 'pizza', '1', '2017-07-02 09:03:43', '2017-07-02 09:03:43', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('54', 'pizza', 'pizza', '1', '2017-07-02 09:07:37', '2017-07-02 09:07:37', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('55', 'pizza', 'pizza', '1', '2017-07-02 09:12:14', '2017-07-02 09:12:14', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('56', 'pizza', 'pizza', '1', '2017-07-02 09:20:47', '2017-07-02 09:20:47', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('57', 'pizza', 'pizza', '1', '2017-07-02 09:26:01', '2017-07-02 09:26:01', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('58', 'pizza', 'pizza', '1', '2017-07-02 09:26:42', '2017-07-02 09:26:42', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('59', 'pizza', 'pizza', '1', '2017-07-02 09:29:42', '2017-07-02 09:29:42', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('60', 'pizza', 'pizza', '1', '2017-07-02 09:30:10', '2017-07-02 09:30:10', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('61', 'pizza', 'pizza', '1', '2017-07-02 09:31:00', '2017-07-02 09:31:00', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('62', 'pizza', 'pizza', '1', '2017-07-02 09:33:06', '2017-07-02 09:33:06', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('63', 'pizza', 'pizza', '1', '2017-07-02 09:48:30', '2017-07-02 09:48:30', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('64', 'pizza', 'pizza', '1', '2017-07-02 09:49:25', '2017-07-02 09:49:25', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('65', 'pizza', 'pizza', '1', '2017-07-02 09:52:36', '2017-07-02 09:52:36', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('66', 'pizza', 'pizza', '1', '2017-07-02 09:53:27', '2017-07-02 09:53:27', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('67', 'pizza', 'pizza', '1', '2017-07-02 09:54:54', '2017-07-02 09:54:54', '0001-01-01 00:00:00', '2');
INSERT INTO `requests` VALUES ('68', 'pizza', 'pizza', '1', '2017-07-02 09:57:48', '2017-07-02 09:57:48', '0001-01-01 00:00:00', '2');

-- ----------------------------
-- Table structure for requeststatus
-- ----------------------------
DROP TABLE IF EXISTS `requeststatus`;
CREATE TABLE `requeststatus` (
  `ID` int(11) NOT NULL,
  `Description` varchar(255) NOT NULL,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of requeststatus
-- ----------------------------

-- ----------------------------
-- Table structure for users
-- ----------------------------
DROP TABLE IF EXISTS `users`;
CREATE TABLE `users` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Fullname` varchar(255) NOT NULL,
  `Email` varchar(255) NOT NULL,
  `IsActive` bit(1) NOT NULL DEFAULT b'1',
  `IsActivated` bit(1) NOT NULL,
  `ImageUrl` varchar(255) NOT NULL,
  `UserTypeID` int(11) NOT NULL,
  `Password` varchar(255) DEFAULT NULL,
  PRIMARY KEY (`ID`),
  KEY `users_ibfk_1` (`UserTypeID`)
) ENGINE=InnoDB AUTO_INCREMENT=7 DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of users
-- ----------------------------
INSERT INTO `users` VALUES ('1', 'Nicomedes Bulusan', 'nbulusanjr@gmail.com', '', '', 'none', '1', 'P@ssw0rd');
INSERT INTO `users` VALUES ('2', 'Abner Belloga', 'abner@gmail.com', '', '', 'none', '2', 'P@ssw0rd');
INSERT INTO `users` VALUES ('3', 'Juan Dela Cruz', 'juandelacruz@gmail.com', '', '', 'none', '2', 'P@ssw0rd');
INSERT INTO `users` VALUES ('4', 'John Doe', 'johndoe@gmail.com', '', '', 'none', '1', 'P@ssw0rd');
INSERT INTO `users` VALUES ('5', 'Josie Rizal', 'josierizal@gmail.com', '', '', 'none', '2', 'P@ssw0rd');
INSERT INTO `users` VALUES ('6', 'Andre Bonifacio', 'andrebonifacio@gmail.com', '', '', 'none', '2', 'P@ssw0rd');

-- ----------------------------
-- Table structure for usertypes
-- ----------------------------
DROP TABLE IF EXISTS `usertypes`;
CREATE TABLE `usertypes` (
  `ID` int(11) NOT NULL AUTO_INCREMENT,
  `Description` varchar(255) NOT NULL,
  `DateCreated` datetime NOT NULL,
  `DateLastUpdated` timestamp NOT NULL DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP,
  PRIMARY KEY (`ID`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;

-- ----------------------------
-- Records of usertypes
-- ----------------------------
