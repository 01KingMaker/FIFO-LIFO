-- Insert into article

insert into article values ( CONCAT('ART', nextval('s_article')), 'Riz Rouge', 'RR', false );
insert into article values ( CONCAT('ART', nextval('s_article')), 'Riz Blanc', 'RBS', true );

-- Insert into magasin
insert into magasin values
	( CONCAT('MAG', nextval('s_magasin')), 'Magasin Sarobidy' );
insert into magasin values
	( CONCAT('MAG', nextval('s_magasin')), 'Magasin Manoary' );

-- Insert into entree
insert into entree values
	( CONCAT('ENT', nextval('s_entree')), 'ART1', 'MAG1', 1000, 2000, '2020-09-01', 'kg');

insert into entree values
	( CONCAT('ENT', nextval('s_entree')), 'ART1', 'MAG1', 500, 2300, '2021-11-01', 'kg');

insert into entree values
	( CONCAT('ENT', nextval('s_entree')), 'ART1', 'MAG1', 200, 2500, '2021-12-03', 'kg');

insert into entree values
	( CONCAT('ENT', '5'), 'RBS', 'MG001', 500, 1500, '2021-11-01', 'kg');

insert into entree values
	( CONCAT('ENT', '6'), 'RBS', 'MG001', 1400, 1700, '2021-11-01', 'kg');

insert into entree values
	( CONCAT('ENT', '7'), 'RBS', 'MG001', 1000, 2000, '2021-11-01', 'kg');

-- Insert into sortie

insert into sortie values
	( CONCAT('SRT', nextval('s_sortie')), 'ART1', 'MAG1', 1200, '2021-12-02' );

insert into sortie values
	( CONCAT('SRT', nextval('s_sortie')), 'ART1', 'MAG1', 400, '2021-12-4' );