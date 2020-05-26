CREATE 
    ALGORITHM = UNDEFINED 
    DEFINER = `root`@`localhost` 
    SQL SECURITY DEFINER
VIEW `mydb`.`evenimentele_useri` AS
    SELECT 
        `mydb`.`evenimente`.`idEveniment` AS `idEveniment`,
        `mydb`.`evenimente`.`nume` AS `nume`,
        `mydb`.`evenimente`.`tip` AS `tip`,
        `mydb`.`evenimente`.`descriere` AS `descriere`,
        ADDTIME(`mydb`.`dataevenimente`.`dataInceput`,
                `mydb`.`dataevenimente`.`oraInceput`) AS `inceput`,
        ADDTIME(`mydb`.`dataevenimente`.`dataSfarsit`,
                `mydb`.`dataevenimente`.`oraSfarsit`) AS `sfarsit`,
        `mydb`.`useri`.`loginNume` AS `loginNume`
    FROM
        ((`mydb`.`useri`
        JOIN `mydb`.`evenimente` ON ((`mydb`.`useri`.`idUser` = `mydb`.`evenimente`.`idUser`)))
        JOIN `mydb`.`dataevenimente` ON ((`mydb`.`evenimente`.`idEveniment` = `mydb`.`dataevenimente`.`idEveniment`)))
    ORDER BY `mydb`.`dataevenimente`.`dataInceput` , `mydb`.`dataevenimente`.`oraInceput` , `mydb`.`dataevenimente`.`dataSfarsit` , `mydb`.`dataevenimente`.`oraSfarsit`