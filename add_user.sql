CREATE DEFINER=`root`@`localhost` PROCEDURE `add_user`(in userLogin varchar(45),in pass varchar(45),in email varchar(45),in Nume varchar(45),in Prenume varchar(45))
BEGIN

insert into mydb.useri (loginNume,parola,mail,showNume,showPrenume) 
values (userLogin,aes_encrypt(pass,'Mona Lisa'),email,Nume,Prenume);

END