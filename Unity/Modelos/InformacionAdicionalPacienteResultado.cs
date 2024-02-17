using System.Collections.Generic;
using System;

[Serializable]
public class InformacionAdicionalPacienteResultado
{
    public string sucursal;
    public string codigoBarras;
    public string nombres;
    public string apellidos;
    public string sexo;
    public string urgencia;
    public string fecha_nacimiento;
    public int numero_orden;
    public bool emergencia;
    public List<InformacionAdicionalPacienteResultadoExamenes> examenes;    
}