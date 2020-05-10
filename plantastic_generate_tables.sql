-- MySQL Workbench Forward Engineering

SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0;
SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0;
SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='ONLY_FULL_GROUP_BY,STRICT_TRANS_TABLES,NO_ZERO_IN_DATE,NO_ZERO_DATE,ERROR_FOR_DIVISION_BY_ZERO,NO_ENGINE_SUBSTITUTION';

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------

-- -----------------------------------------------------
-- Schema mydb
-- -----------------------------------------------------
CREATE SCHEMA IF NOT EXISTS `mydb` DEFAULT CHARACTER SET utf8 ;
USE `mydb` ;

-- -----------------------------------------------------
-- Table `mydb`.`useri`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`useri` ;

CREATE TABLE IF NOT EXISTS `mydb`.`useri` (
  `idUser` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `loginNume` VARCHAR(45) NOT NULL,
  `parola` VARCHAR(45) NOT NULL,
  `mail` VARCHAR(45) NOT NULL,
  `showNume` VARCHAR(45) NOT NULL,
  `showPrenume` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`idUser`),
  UNIQUE INDEX `iduser_UNIQUE` (`idUser` ASC) VISIBLE,
  UNIQUE INDEX `nume_UNIQUE` (`loginNume` ASC) VISIBLE,
  UNIQUE INDEX `mail_UNIQUE` (`mail` ASC) VISIBLE)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`evenimente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`evenimente` ;

CREATE TABLE IF NOT EXISTS `mydb`.`evenimente` (
  `idEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `tip` VARCHAR(45) NOT NULL,
  `nume` VARCHAR(45) NOT NULL,
  `descriere` VARCHAR(45) NULL,
  `idUser` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idEveniment`),
  UNIQUE INDEX `idEveniment_UNIQUE` (`idEveniment` ASC) VISIBLE,
  INDEX `fk_eveniment_user1_idx` (`idUser` ASC) VISIBLE,
  CONSTRAINT `fk_eveniment_user1`
    FOREIGN KEY (`idUser`)
    REFERENCES `mydb`.`useri` (`idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`dataEvenimente`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`dataEvenimente` ;

CREATE TABLE IF NOT EXISTS `mydb`.`dataEvenimente` (
  `idDataEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `dataInceput` DATE NOT NULL,
  `dataSfarsit` DATE NULL,
  `oraInceput` TIME NULL,
  `oraSfarsit` TIME NULL,
  `idEveniment` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idDataEveniment`, `idEveniment`),
  UNIQUE INDEX `idDataEveniment_UNIQUE` (`idDataEveniment` ASC) VISIBLE,
  INDEX `fk_dataEveniment_eveniment1_idx` (`idEveniment` ASC) VISIBLE,
  CONSTRAINT `fk_dataEveniment_eveniment1`
    FOREIGN KEY (`idEveniment`)
    REFERENCES `mydb`.`evenimente` (`idEveniment`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`multimediaEveniment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`multimediaEveniment` ;

CREATE TABLE IF NOT EXISTS `mydb`.`multimediaEveniment` (
  `idmultimediaEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `idEveniment` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idmultimediaEveniment`),
  INDEX `fk_multimediaEveniment_eveniment1_idx` (`idEveniment` ASC) VISIBLE,
  CONSTRAINT `fk_multimediaEveniment_eveniment1`
    FOREIGN KEY (`idEveniment`)
    REFERENCES `mydb`.`evenimente` (`idEveniment`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`pozeEveniment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`pozeEveniment` ;

CREATE TABLE IF NOT EXISTS `mydb`.`pozeEveniment` (
  `idPozeEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `poza` LONGBLOB NOT NULL,
  `multimediaId` INT UNSIGNED NOT NULL,
  `titlu` VARCHAR(45) NULL,
  PRIMARY KEY (`idPozeEveniment`),
  UNIQUE INDEX `idPozeEveniment_UNIQUE` (`idPozeEveniment` ASC) VISIBLE,
  INDEX `fk_pozeEveniment_multimediaEveniment_idx` (`multimediaId` ASC) VISIBLE,
  CONSTRAINT `fk_pozeEveniment_multimediaEveniment`
    FOREIGN KEY (`multimediaId`)
    REFERENCES `mydb`.`multimediaEveniment` (`idmultimediaEveniment`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`videoEveniment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`videoEveniment` ;

CREATE TABLE IF NOT EXISTS `mydb`.`videoEveniment` (
  `idvideoEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `video` LONGBLOB NOT NULL,
  `multimediaId` INT UNSIGNED NOT NULL,
  `titlu` VARCHAR(45) NULL,
  PRIMARY KEY (`idvideoEveniment`),
  UNIQUE INDEX `idvideoEveniment_UNIQUE` (`idvideoEveniment` ASC) VISIBLE,
  INDEX `fk_videoEveniment_multimediaEveniment1_idx` (`multimediaId` ASC) VISIBLE,
  CONSTRAINT `fk_videoEveniment_multimediaEveniment1`
    FOREIGN KEY (`multimediaId`)
    REFERENCES `mydb`.`multimediaEveniment` (`idmultimediaEveniment`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`audioEveniment`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`audioEveniment` ;

CREATE TABLE IF NOT EXISTS `mydb`.`audioEveniment` (
  `idAudioEveniment` INT UNSIGNED NOT NULL AUTO_INCREMENT,
  `multimediaId` INT UNSIGNED NOT NULL,
  `audio` LONGBLOB NULL,
  `titlu` VARCHAR(45) NULL,
  PRIMARY KEY (`idAudioEveniment`),
  UNIQUE INDEX `idaudioEveniment_UNIQUE` (`idAudioEveniment` ASC) VISIBLE,
  INDEX `fk_audioEveniment_multimediaEveniment1_idx` (`multimediaId` ASC) VISIBLE,
  CONSTRAINT `fk_audioEveniment_multimediaEveniment1`
    FOREIGN KEY (`multimediaId`)
    REFERENCES `mydb`.`multimediaEveniment` (`idmultimediaEveniment`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


-- -----------------------------------------------------
-- Table `mydb`.`sarcini`
-- -----------------------------------------------------
DROP TABLE IF EXISTS `mydb`.`sarcini` ;

CREATE TABLE IF NOT EXISTS `mydb`.`sarcini` (
  `idlistaTreburi` INT NOT NULL AUTO_INCREMENT,
  `nume` VARCHAR(45) NOT NULL,
  `descriere` VARCHAR(45) NULL,
  `complet` TINYINT NULL,
  `data` DATE NULL,
  `ora` TIME NULL,
  `listaTreburicol` VARCHAR(45) NULL,
  `eveniment_idEveniment` INT UNSIGNED NOT NULL,
  `eveniment_idUser` INT UNSIGNED NOT NULL,
  PRIMARY KEY (`idlistaTreburi`),
  UNIQUE INDEX `idlistaTreburi_UNIQUE` (`idlistaTreburi` ASC) VISIBLE,
  INDEX `fk_sarcini_eveniment1_idx` (`eveniment_idEveniment` ASC, `eveniment_idUser` ASC) VISIBLE,
  CONSTRAINT `fk_sarcini_eveniment1`
    FOREIGN KEY (`eveniment_idEveniment` , `eveniment_idUser`)
    REFERENCES `mydb`.`evenimente` (`idEveniment` , `idUser`)
    ON DELETE NO ACTION
    ON UPDATE NO ACTION)
ENGINE = InnoDB;


SET SQL_MODE=@OLD_SQL_MODE;
SET FOREIGN_KEY_CHECKS=@OLD_FOREIGN_KEY_CHECKS;
SET UNIQUE_CHECKS=@OLD_UNIQUE_CHECKS;
