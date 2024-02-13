using System;
using System.Collections.Generic;

namespace IO.Swagger.LaboratorioDB;

public partial class Laboratoristum
{
    public int IdLaboratorista { get; set; }

    public string Nombre { get; set; }

    public string Apellido { get; set; }

    public string Especialidad { get; set; }

    public DateOnly? FechaContratacion { get; set; }

    public int? SucursalId { get; set; }


    public virtual Sucursal Sucursal { get; set; }
}
