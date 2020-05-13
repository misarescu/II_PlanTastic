SELECT loginNume as user, cast(aes_decrypt(parola,'Mona Lisa') as char(45)) as parola FROM mydb.useri;
select * from useri;

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
