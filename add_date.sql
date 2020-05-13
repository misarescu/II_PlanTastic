CREATE DEFINER=`root`@`localhost` PROCEDURE `add_date`(in data_inceput date, in data_sfarsit date, in ora_inceput time, in ora_sfarsit time, in id_eveniment int)
BEGIN
insert into dataevenimente(dataInceput,dataSfarsit,oraInceput,oraSfarsit,idEveniment)
values(data_inceput,data_sfarsit,ora_inceput,ora_sfarsit,id_eveniment);
END