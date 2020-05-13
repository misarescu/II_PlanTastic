CREATE DEFINER=`root`@`localhost` PROCEDURE `add_eveniment`(in tipEveniment varchar(45), in numeEveniment varchar(45), in descriereEveniment varchar(45), in numeLoginUser varchar(45))
BEGIN
select idUser 
into @id
from useri 
where loginNume = numeLoginUser;

insert into evenimente(tip,nume,descriere,idUser)
values(tipEveniment,numeEveniment,descriereEveniment, @id);

END