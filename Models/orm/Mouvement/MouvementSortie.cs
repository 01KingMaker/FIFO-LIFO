using System.ComponentModel.DataAnnotations.Schema;

namespace FIFO_LIFO.Models.orm.Mouvement;

[Table("mouvement_sortie")]
public class MouvementSortie {
    
    [ForeignKey("id_mouvement_entre")]
    MouvementEntre IdMouvementEntre;
    [Column("id_mouvement_sortie")]
    string IdMouvementSortie;
    [Column("quantite")]
    double Quantite;
    [Column("date_mouvement_sortie")]
    DateTime DateMouvement;
    
}