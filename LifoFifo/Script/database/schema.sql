drop database if exists gestion;
create database gestion;
\c gestion;

-- Table

create sequence s_article start with 1 increment by 1;
create table article(
	idArticle varchar(7) not null primary key,
	name varchar(50) not null,
	code varchar(7) not null unique,
	fifo boolean default true
);

create sequence s_magasin start with 1 increment by 1;
create table magasin(
	idMagasin varchar(7) not null primary key,
	name varchar(50)
);

create sequence s_entree start with 1 increment by 1;
create table entree(

	idEntree varchar(7) not null primary key,
	idArticle varchar(7) not null,
	idMagasin varchar(7) not null,
	quantite double precision default 0,
	prixUnitaire double precision,
	dateEntree date,
	unite varchar(2),
	foreign key (idArticle) references article(idArticle),
	foreign key (idMagasin) references magasin(idMagasin)
);

create sequence s_sortie start with 1 increment by 1;
create table sortie(

	idSortie varchar(7) not null primary key,
	idArticle varchar(7) not null,
	idMagasin varchar(7) not null,
	quantite double precision,
	dateSortie date,
	foreign key( idArticle ) references article(idArticle),
	foreign key( idMagasin ) references magasin(idMagasin)

);

create table mouvement(
	idMouvement serial primary key,
	idEntree varchar(7) not null,
	idSortie varchar(7) not null,
	quantite double precision,
	foreign key(idEntree) references entree(idEntree),
	foreign key(idSortie) references sortie(idSortie)
);


create or replace view v_article_magasin
	as
		select 
			a.*,
			e.identree, e.quantite, e.idMagasin, e.prixUnitaire, e.dateEntree, e.unite
		from article as a
		join entree as e
		on a.idArticle = e.idArticle;

-- Atao distinct par magasin