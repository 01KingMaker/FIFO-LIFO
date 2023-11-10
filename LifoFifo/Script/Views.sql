create or replace view v_entre as
select identree, quantite, date_entree from entree group by identree, quantite;

create or replace view v_mouvement as
select m.identree, sum(m.quantite) quantite from mouvement m group by m.identree;

drop view v_etat_stock_final

create or replace view v_etat_stock_final as
select ve.identree, (ve.quantite - coalesce(vm.quantite, 0)) quantite, ve.date_entree date_entree
from v_entre ve left join v_mouvement vm on ve.identree = vm.identree;

select *
from v_etat_stock_final;

CREATE VIEW v_mouvement AS
SELECT date_sortie, s.code, m.quantite, s.idmagasin, m.identree, s.idsortie
FROM mouvement m
         JOIN sortie s ON m.idsortie = s.idsortie;

CREATE OR REPLACE VIEW v_somme_mouvement as
SELECT identree, SUM(quantite) as quantite
FROM v_mouvement
GROUP BY identree;

CREATE OR REPLACE VIEW v_etat_stock AS
SELECT e.identree, e.date_entree, (e.quantite - COALESCE(m.quantite, 0)) as quantite, e.prix_unitaire, e.idmagasin, e.code
FROM v_somme_mouvement m
         RIGHT JOIN entree e ON m.identree = e.identree
WHERE (e.quantite - COALESCE(m.quantite, 0)) > 0;

select *
from v_etat_stock;