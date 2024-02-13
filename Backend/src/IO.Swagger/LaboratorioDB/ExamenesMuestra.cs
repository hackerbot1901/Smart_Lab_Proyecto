using System;
using System.Collections.Generic;

namespace IO.Swagger.LaboratorioDB;

public partial class ExamenesMuestra
{
    public int IdExamenMuestra { get; set; }

    public int? IdExterno { get; set; }

    public string Codigo { get; set; }

    public string Nombre { get; set; }

    public DateOnly? FechaCreacion { get; set; }

    public DateOnly? FechaTomaMuestra { get; set; }

    public DateOnly? FechaReporte { get; set; }

    public DateOnly? FechaValidacion { get; set; }

    public string UsuarioValidacion { get; set; }

    public string Estado { get; set; }

    public int Valor { get; set; }

    public int? MuestraId { get; set; }

    public virtual Muestra Muestra { get; set; }
}
