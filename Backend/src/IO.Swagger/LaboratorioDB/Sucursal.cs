using System;
using System.Collections.Generic;

namespace IO.Swagger.LaboratorioDB;

public partial class Sucursal
{
    public int IdSucursal { get; set; }

    public string NombreSucursal { get; set; }

    public string subdominio { get; set; }

    public string Telefono { get; set; }

    public string Encargado { get; set; }

    public DateOnly? FechaApertura { get; set; }

    public virtual ICollection<Laboratoristum> Laboratorista { get; set; } = new List<Laboratoristum>();

    public virtual ICollection<Muestra> Muestras { get; set; } = new List<Muestra>();
}
