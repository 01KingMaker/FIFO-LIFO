-- requete pour avoir l'etat de stock 
select
    e.id_produit,
    e.id_magasin,
    sum(e.quantite) quantite,
    sum(e.quantite - s.quantite) reste,
    sum(s.quantite * e.prix_unitaire) total 
from mouvement_entre e
join mouvement_sortie s on
e.id_mouvement = s.id_mouvement_entre
group by e.id_magasin, e.id_produit
where 