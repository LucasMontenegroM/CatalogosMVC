using System.ComponentModel.DataAnnotations;

namespace CatalogosMVC.Business.Enums;

public enum ReadingStatus
{
    [Display(Name = "Planejo Ler")]
    Lerei = 0,
    Lendo = 1,
    Lido = 2,
};
