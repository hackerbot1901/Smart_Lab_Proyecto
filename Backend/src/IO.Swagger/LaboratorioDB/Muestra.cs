using System;
using System.Collections.Generic;

namespace IO.Swagger.LaboratorioDB;

public partial class Muestra
{
    public int IdMuestra { get; set; }

    public DateOnly? FechaRecepcion { get; set; }

    public int? PacienteId { get; set; }

    public int? SucursalId { get; set; }

    public string CodigoBarras { get; set; }

    public string? Urgencia { get; set; }

    public int? NumeroOrden { get; set; }

    public string Estado { get; set; }

    public virtual ICollection<ExamenesMuestra> ExamenesMuestras { get; set; } = new List<ExamenesMuestra>();

    public virtual Paciente Paciente { get; set; }

    public virtual Sucursal Sucursal { get; set; }
}
