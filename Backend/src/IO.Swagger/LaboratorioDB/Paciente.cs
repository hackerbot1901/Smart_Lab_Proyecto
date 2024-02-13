using System;
using System.Collections.Generic;

namespace IO.Swagger.LaboratorioDB;

public partial class Paciente
{
    public int IdPaciente { get; set; }

    public string NombrePaciente { get; set; }

    public string ApellidoPaciente { get; set; }

    public DateOnly? FechaNacimiento { get; set; }

    public string Genero { get; set; }

    public string Direccion { get; set; }

    public virtual ICollection<Muestra> Muestras { get; set; } = new List<Muestra>();
}
