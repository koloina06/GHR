CREATE DATABASE grh;
use grh;

CREATE Table Services (
    idService int PRIMARY KEY,
    nomService VARCHAR(255),
    Email VARCHAR(255),
    mdp VARCHAR(255)
);

create table Postes(
    idPoste int PRIMARY KEY,
    nomPoste VARCHAR(255),
    idService int,
    Foreign Key (idService) REFERENCES Services(idService)
);

create table RH(
    idRH int PRIMARY KEY,
    email VARCHAR(255),
    mdp VARCHAR(255)
);
insert into RH(idRH,email,mdp) values (1,"rh@gmail.com","rh123");

CREATE table Critere(
    idCritere int PRIMARY KEY,
    nomCritere VARCHAR(255)
);

CREATE table SousCritere(
    idSousCritere int PRIMARY KEY,
    idCritere int,
    nomSousCritere VARCHAR(255),
    Foreign Key (idCritere) REFERENCES Critere(idCritere)
);

create table Annonce (
    idAnnonce int PRIMARY key,
    idPoste int,
    descriptions VARCHAR(255),
    volumeTache  int,
    volumeJourHomme int,
    dateAnnonce datetime,
    etat int,
    Foreign Key (idPoste) REFERENCES Postes(idPoste) 
);

create table critereCoef(
    idCriCoef int PRIMARY key,
    idSousCritere int,
    idAnnonce int,
    Foreign Key (idSousCritere) REFERENCES SousCritere(idSousCritere),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce)
);

CREATE Table Question(
    idQuestion int PRIMARY KEY,
    nom VARCHAR(255),
    idService int,
    Foreign Key (idService) REFERENCES Services(idService)
);

CREATE table Reponse(
    idReponse int primary KEY,
    idQuestion int,
    nom VARCHAR(255),
    etat int,
    Foreign Key (idQuestion) REFERENCES Question(idQuestion)
);

create table QuestionAnnonce(
    idQuestionAnnonce int PRIMARY KEY,
    idQuestion int,
    idAnnonce int,
    Foreign Key (idQuestion) REFERENCES Question(idQuestion),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce)
);

create table Clients(
    idClient int PRIMARY KEY,
    nom VARCHAR(255),
    prenom VARCHAR(255),
    dtn DATE,
    email VARCHAR(255),
    mdp VARCHAR(255),
    addresse VARCHAR(255)
);
insert into clients(idClient,nom, prenom,dtn,email,mdp,addresse) values (1,"Jean","Rakoto","2001-05-15","Jean@gmail.com","jean123","102 Andoharanofotsy");

create table cv(
    idCv int PRIMARY KEY,
    idClient int,
    idSousCritere int,
    pdfDiplome VARCHAR(255),
    pdfAttestation VARCHAR(255),
    Foreign Key (idClient) REFERENCES Clients(idClient),
    Foreign Key (idSousCritere) REFERENCES SousCritere(idSousCritere)
);

CREATE table etatClient (
    idEtatClient int PRIMARY KEY,
    idClient int,
    etat int,
    idAnnonce int,
    dateDepot Datetime,
    dateTeste datetime,
    dateEntretien datetime,
    Foreign Key (idClient) REFERENCES Clients(idClient),
    Foreign Key (idAnnonce) REFERENCES Annonce(idAnnonce) 
);
