-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema STT
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema STT
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `STT` DEFAULT CHARACTER SET utf8 ;
USE `STT` ;

-- -----------------------------------------------------
-- Table `STT`.`Competitions`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STT`.`Competitions` (
  `id` INT NOT NULL DEFAULT 1,
  `name` VARCHAR(60) NULL,
  `code` VARCHAR(45) NULL,
  `areaName` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `idCompetition_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `STT`.`Teams`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STT`.`Teams` (
  `id` INT NOT NULL DEFAULT 1,
  `name` VARCHAR(60) NULL,
  `tla` VARCHAR(15) NULL,
  `shortName` VARCHAR(45) NULL,
  `areaName` VARCHAR(45) NULL,
  `email` VARCHAR(45) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `STT`.`Players`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STT`.`Players` (
  `id` INT NOT NULL DEFAULT 1,
  `position` VARCHAR(45) NULL,
  `dateOfBirth` DATE NULL,
  `countryOfBirth` VARCHAR(60) NULL,
  `nationality` VARCHAR(60) NULL,
  PRIMARY KEY (`id`),
  UNIQUE INDEX `id_UNIQUE` (`id` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `STT`.`Competition_Teams`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STT`.`Competition_Teams` (
  `idCompetition_Teams` INT NOT NULL AUTO_INCREMENT,
  `Competition_id` INT NULL,
  `Team_id` INT NULL,
  PRIMARY KEY (`idCompetition_Teams`),
  UNIQUE INDEX `idCompetition_Teams_UNIQUE` (`idCompetition_Teams` ASC))
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `STT`.`TeamPlayers`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `STT`.`TeamPlayers` (
  `idTeamPlayers` INT NOT NULL AUTO_INCREMENT,
  `Team_id` INT NULL,
  `Player_id` INT NULL,
  PRIMARY KEY (`idTeamPlayers`),
  UNIQUE INDEX `idTeamPlayers_UNIQUE` (`idTeamPlayers` ASC))
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
