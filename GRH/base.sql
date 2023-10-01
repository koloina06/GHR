/* sqlcmd -S KOLOINA\SQLEXPRESS -E */

CREATE DATABASE grh 
use grh

CREATE Table Services (
    idService int PRIMARY KEY,
    nomService VARCHAR(255),
    Email VARCHAR(255),
    mdp VARCHAR(255)
)

insert into services (idService,nomService,Email,mdp) values (1,'Informatique','info@gmail.com','info')

create table Postes(
    idPoste int PRIMARY KEY,
    nomPoste VARCHAR(255),
    idService int,
    Foreign Key (idService) REFERENCES Services(idService)
)

insert into Postes(idPoste,nomPoste,idService) values (1,'Developeur',1)

create table RH(
    idRH int PRIMARY KEY,
    email VARCHAR(255),
    mdp VARCHAR(255)
)
insert into RH(idRH,email,mdp) values (1,"rh@gmail.com","rh123")

CREATE table Critere(
    idCritere int PRIMARY KEY,
    nomCritere VARCHAR(255)
)
insert into Critere(idCritere,nomCritere) values (1,'diplome'),(2,'experience'),(3,'sexe'),(4,'situation matrimoniale'),(5,'nationalite')

CREATE table SousCritere(
    idSousCritere int PRIMARY KEY,
    idCritere int,
    nomSousCritere VARCHAR(255),
    Foreign Key (idCritere) REFERENCES Critere(idCritere)
)
insert into SousCritere(idSousCritere,idCritere,nomSousCritere) values (1,1,'bepc'),(2,1,'bacc'),(3,1,'Bacc+3'),(4,1,'Bacc+4'),(5,1,'Bacc+5')
insert into SousCritere(idSousCritere,idCritere,nomSousCritere) values (6,2,'1 a 3 ans'),(7,2,'4 a 6 ans'),(8,2,'7 ans et plus')
insert into SousCritere(idSousCritere,idCritere,nomSousCritere) values (9,3,'homme'),(10,3,'femme')
insert into SousCritere(idSousCritere,idCritere,nomSousCritere) values (11,4,'celibataire'),(12,4,'marie(e)'),(13,4,'veuf(ve)')
insert into SousCritere(idSousCritere,idCritere,nomSousCritere) values (14,5,'malgache'),(15,5,'etranger')

create table Annonce (
    idAnnonce int PRIMARY key,
    idPoste int,
    descriptions VARCHAR(255),
    volumeTache  int,
    volumeJourHomme int,
    dateAnnonce datetime,
    etat int,
    Foreign Key (idPoste) REFERENCES Postes(idPoste) 
)

insert into annonce (idAnnonce,idPoste,descriptions,volumeTache,volumeJourHomme,dateAnnonce,etat) values (1,1,'Recrutement pour le poste de developpeur Senior Java',800,8,'2023-09-01',0)


create table critereCoef(
    idCriCoef int PRIMARY key,
    idSousCritere int,
    coef int,
    idAnnonce int,
    Foreign Key (idSousCritere) REFERENCES SousCritere(idSousCritere),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce)
)

insert into critereCoef(idCriCoef,idSousCritere,coef,idAnnonce) values (1,1,0,1),(2,2,1,1),(3,3,2,1),(4,4,3,1),(5,5,4,1)
insert into critereCoef(idCriCoef,idSousCritere,coef,idAnnonce) values (6,6,1,1),(7,7,2,1),(8,8,3,1)
insert into critereCoef(idCriCoef,idSousCritere,coef,idAnnonce) values (9,9,1,1),(10,10,2,1)
insert into critereCoef(idCriCoef,idSousCritere,coef,idAnnonce) values (11,11,1,1),(12,12,3,1),(13,13,2,1)
insert into critereCoef(idCriCoef,idSousCritere,coef,idAnnonce) values (14,14,2,1),(15,15,1,1)

CREATE Table Question(
    idQuestion int PRIMARY KEY,
    nom VARCHAR(255),
    idService int,
    Foreign Key (idService) REFERENCES Services(idService)
)

CREATE table Reponse(
    idReponse int primary KEY,
    idQuestion int,
    nom VARCHAR(255),
    etat int,
    Foreign Key (idQuestion) REFERENCES Question(idQuestion)
)

create table QuestionAnnonce(
    idQuestionAnnonce int PRIMARY KEY,
    idQuestion int,
    idAnnonce int,
    Foreign Key (idQuestion) REFERENCES Question(idQuestion),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce)
)

create table Clients(
    idClient int PRIMARY KEY,
    nom VARCHAR(255),
    prenom VARCHAR(255),
    dtn DATE,
    email VARCHAR(255),
    mdp VARCHAR(255),
    addresse VARCHAR(255),
    sexe int,
    note int
)
insert into clients(idClient,nom, prenom,dtn,email,mdp,addresse,sexe,note) values (1,"Jean","Rakoto","2001-05-15","Jean@gmail.com","jean123","102 Andoharanofotsy",0,0);
insert into clients(idClient,nom, prenom,dtn,email,mdp,addresse,sexe,note) values (2,"Julie","Rasoa","1998-06-20","Julie@gmail.com","julie","101 Antanimena",1,0);
insert into clients(idClient,nom, prenom,dtn,email,mdp,addresse,sexe,note) values (3,"Rabe","Paul","1980-12-02","Paul@gmail.com","paul","101 Ivato",0,0);
insert into clients(idClient,nom, prenom,dtn,email,mdp,addresse,sexe,note) values (4,"Rak","Jeanne","1995-04-07","Jeanne@gmail.com","jeanne","101 Alasora",1,0);


create table cv(
    idCv int PRIMARY KEY,
    idAnnonce int,
    idClient int,
    pdfDiplome VARCHAR(255),
    pdfAttestation VARCHAR(255),
    Foreign Key (idClient) REFERENCES Clients(idClient),
    foreign key (idAnnonce) references Annonce(idAnnonce)
)

insert into cv(idCv,idAnnonce,idClient,pdfDiplome,pdfAttestation) values (1,1,1,'pdf diplome','pdf Attestation'),(2,1,2,'pdf diplome','pdf Attestation'),(3,1,3,'pdf diplome','pdf Attestation'),(4,1,4,'pdf diplome','pdf Attestation')

create table DetailsCv(
    idCv int,
    idSousCritere int,
    Foreign Key (idSousCritere) REFERENCES SousCritere(idSousCritere),
    foreign key (idCv) references cv(idCv)
)

insert into DetailsCv(idCv,idSousCritere) values (1,3),(1,6),(1,11),(1,14)
insert into DetailsCv(idCv,idSousCritere) values (2,4),(2,7),(2,11),(2,14)
insert into DetailsCv(idCv,idSousCritere) values (3,5),(3,6),(3,13),(3,14)
insert into DetailsCv(idCv,idSousCritere) values (4,3),(4,8),(4,12),(4,14)

CREATE table etatClient (
    idEtatClient int PRIMARY KEY,
    idClient int,
    etat int,
    idAnnonce int,
    dateDepot datetime,
    dateTeste datetime,
    dateEntretien datetime,
    Foreign Key (idClient) REFERENCES Clients(idClient),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce) 
)                

insert into etatClient (idEtatClient,idClient,etat,idAnnonce,dateDepot,dateTeste,dateEntretien) values (1,1,0,1,"2023-09-05 08:00:00",null,null)
insert into etatClient (idEtatClient,idClient,etat,idAnnonce,dateDepot,dateTeste,dateEntretien) values (2,2,0,1,"2023-09-08 15:10:20",null,null)
insert into etatClient (idEtatClient,idClient,etat,idAnnonce,dateDepot,dateTeste,dateEntretien) values (3,3,0,1,"2023-09-10 08:00:00",null,null)
insert into etatClient (idEtatClient,idClient,etat,idAnnonce,dateDepot,dateTeste,dateEntretien) values (4,4,0,1,"2023-09-12 08:00:00",null,null)

create view v_clientAnnonce as
    select idCv,idAnnonce,c.idClient,nom,prenom,dtn,email,mdp,addresse,sexe,note from cv join clients c on cv.idClient=c.idClient

create view v_detailcv as 
    select cv.idAnnonce,dc.idcv,dc.idSousCritere,nomSousCritere,c.idCritere,nomCritere from detailscv dc join souscritere sc on dc.idSouscritere=sc.idSouscritere join critere c on sc.idCritere=c.idCritere join cv on dc.idcv=cv.idcv

create view v_noteCv as
    select sum(coef) somme,idcv from detailscv dc join criterecoef cc on dc.idSouscritere=cc.idSouscritere group by idcv

create view v_notesexe as
    select coef,nomSousCritere,idAnnonce from criterecoef cc join souscritere sc on cc.idSousCritere=sc.idSousCritere