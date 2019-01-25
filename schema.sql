CREATE SCHEMA IF NOT EXISTS `cuberdb` DEFAULT CHARACTER SET utf8 ;
USE `cuberdb` ;

-- -----------------------------------------------------
-- Table `cuberdb`.`cuber`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cuberdb`.`cuber` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `name` VARCHAR(45) NOT NULL,
  PRIMARY KEY (`id`))
ENGINE = InnoDB
AUTO_INCREMENT = 4
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cuberdb`.`metadata`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cuberdb`.`metadata` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `mkey` VARCHAR(255) NOT NULL,
  `mvalue` VARCHAR(255) NOT NULL,
  `cuber_id` INT(11) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `metadata_fkey1_idx` (`cuber_id` ASC),
  CONSTRAINT `metadata_fkey1`
    FOREIGN KEY (`cuber_id`)
    REFERENCES `cuberdb`.`cuber` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
DEFAULT CHARACTER SET = utf8;


-- -----------------------------------------------------
-- Table `cuberdb`.`solve`
-- -----------------------------------------------------
CREATE TABLE IF NOT EXISTS `cuberdb`.`solve` (
  `id` INT(11) NOT NULL AUTO_INCREMENT,
  `time` FLOAT NOT NULL,
  `cuber_id` INT(11) NOT NULL,
  `event` VARCHAR(10) NOT NULL,
  PRIMARY KEY (`id`),
  INDEX `solve_fkey1_idx` (`cuber_id` ASC),
  CONSTRAINT `solve_fkey1`
    FOREIGN KEY (`cuber_id`)
    REFERENCES `cuberdb`.`cuber` (`id`)
    ON DELETE CASCADE
    ON UPDATE NO ACTION)
ENGINE = InnoDB
AUTO_INCREMENT = 57
DEFAULT CHARACTER SET = utf8;


