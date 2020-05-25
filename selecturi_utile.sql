SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri;

select idUser from useri where loginNume = 'Germania';

select loginNume as utilizator,evenimente.nume as nume_eveniment, descriere as descriere_eveniment
from mydb.useri
inner join mydb.evenimente
on useri.idUser = evenimente.idUser;

select * from mydb.dataevenimente;

select loginNume as utilizator, evenimente.nume as nume_eveniment, /*dataInceput, dataSfarsit,*/ sarcini.nume as nume_sarcina, sarcini.descriere as descriere_sarcina, sarcini.data as data_sarcina, sarcini.ora as ora_sarcina
from mydb.useri
inner join mydb.evenimente
on useri.idUser = evenimente.idUser
/*inner join mydb.dataevenimente
on evenimente.idEveniment = dataevenimente.idEveniment*/
inner join mydb.sarcini
on evenimente.idEveniment = sarcini.eveniment_idEveniment;

SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri WHERE loginNume = 'URSS';

SELECT showNume, showPrenume FROM mydb.useri WHERE loginNume = 'URSS';

SELECT evenimente.idEveniment, evenimente.nume, evenimente.tip, evenimente.descriere, dataInceput,oraInceput,dataSfarsit,oraSfarsit
FROM mydb.useri
INNER JOIN mydb.evenimente
ON useri.idUser = evenimente.idUser
INNER JOIN mydb.dataevenimente
ON evenimente.idEveniment = dataevenimente.idEveniment
/*WHERE LoginNume = 'URSS'*/
ORDER BY dataInceput ASC, oraInceput ASC, dataSfarsit ASC, oraSfarsit ASC;

select * from mydb.useri;
select * from mydb.evenimente;
select * from mydb.dataevenimente;

select loginNume from mydb.useri where loginNume = 'URSS';
select idUser from mydb.useri;

select idEveniment 
from mydb.evenimente
inner join mydb.useri 
on useri.idUser = evenimente.idUser 
where useri.loginNume = 'URSS' and evenimente.nume = 'batalia de la stalingrad';
