CREATE DEFINER=`root`@`localhost` PROCEDURE `add_sarcina`(in nume_sarcina varchar(45), in descriere_sarcina varchar(45), in completitudine_sarcina boolean, in data_sarcinii date, in ora_sarcinii time, in id_eveniment int)
BEGIN
insert into sarcini(nume,descriere,complet,data,ora,eveniment_idEveniment)
values(nume_sarcina,descriere_sarcina,completitudine_sarcina,data_sarcinii,ora_sarcinii,id_eveniment);
END