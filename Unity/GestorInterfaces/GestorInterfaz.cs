using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using TMPro;
using UnityEngine;

public class GestorInterfaz : MonoBehaviour
{
    public TextMeshProUGUI sucursal;
    public TextMeshProUGUI codigoBarras;
    public TextMeshProUGUI muestra;
    public TextMeshProUGUI nombres;
    public TextMeshProUGUI urgencia;
    public TextMeshProUGUI fecha_nacimiento;
    public TextMeshProUGUI numero_orden;
    public TextMeshProUGUI sexo;
    public TextMeshProUGUI examenes;
    public TextMeshProUGUI edad_interfaz;


    public GameObject mensajeInvalido;
    public GameObject mensajeDeEscaneo;
    public GameObject mensajeValido;
    public GameObject visualizacion_urgencia;

    readonly Dictionary<char, string> estado = new()
    {
            { 'G', "Generado" },
            { 'P', "En proceso" },
            { 'R', "Reportado" },
            { 'L', "Preliminar" },
            { 'V', "Validado" },
        };

    readonly Dictionary<char, string> letra_sexo = new()
        {
            { 'M', "Masculino" },
            { 'F', "Femenino" },

        };
    public void MostrarMensajeValido()
    {
        mensajeDeEscaneo.SetActive(false);
        mensajeInvalido.SetActive(false);
        mensajeValido.SetActive(true);
    }

    public void MostrarMensajeInvalido()
    {
        mensajeValido.SetActive(false);
        mensajeDeEscaneo.SetActive(false);
        mensajeInvalido.SetActive(true);
    }

    public void MostrarMensajeDeEscaneo()
    {
        mensajeValido.SetActive(false);
        mensajeInvalido.SetActive(false);
        mensajeDeEscaneo.SetActive(true);
    }


    internal void ColocarDatosEnAplicacion(InformacionAdicionalPacienteResultado resultado)
    {
        sucursal.text = resultado.sucursal;
        codigoBarras.text = resultado.codigoBarras;
        nombres.text = resultado.nombres + " " + resultado.apellidos;
        urgencia.text = !string.IsNullOrEmpty(resultado.urgencia) && estado.ContainsKey(resultado.urgencia[0]) ? estado[resultado.urgencia[0]] : "";
        fecha_nacimiento.text = resultado.fecha_nacimiento;
        sexo.text = !string.IsNullOrEmpty(resultado.sexo) && letra_sexo.ContainsKey(resultado.sexo[0]) ? letra_sexo[resultado.sexo[0]] : "";
        numero_orden.text = resultado.numero_orden == 0 ? "" : resultado.numero_orden.ToString();
        muestra.text = resultado.codigoBarras;
        ColocarExamenes(resultado.examenes);
        ColocarEdad(resultado.fecha_nacimiento);
        ColocarVisualizacionEmergencia(resultado.emergencia);
    }


    public void ColocarEdad(string fecha_nacimiento)
    {
        if (string.IsNullOrEmpty(fecha_nacimiento) || !DateTime.TryParseExact(fecha_nacimiento, "yyyy-MM-dd", CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fechaNacimiento))
        {
            edad_interfaz.text = "";
            return;
        }
        DateTime ahora = DateTime.Now;
        int años = ahora.Year - fechaNacimiento.Year;
        int meses = ahora.Month - fechaNacimiento.Month;
        if (ahora.Month < fechaNacimiento.Month || (ahora.Month == fechaNacimiento.Month && ahora.Day < fechaNacimiento.Day))
        {
            años--;
            meses += 12;
        }
        edad_interfaz.text = años.ToString() + "A\t" + meses.ToString() + "M\t";
    }



    private void ColocarVisualizacionEmergencia(bool emergencia)
    {
        Color colorEmergencia = new Color(0.6039f, 0f, 0f, 0.8392f);
        Color colorNoEmergencia = new(0.2078f, 0.196f, 0.196f, 170f / 255f);
        visualizacion_urgencia.GetComponent<UnityEngine.UI.Image>().color = emergencia ? colorEmergencia : colorNoEmergencia;
    }


    private void ColocarExamenes(List<InformacionAdicionalPacienteResultadoExamenes> examenes_muestra)
    {
        string txtMeshPro = "";
        if (examenes_muestra != null)
        {
            foreach (var examen in examenes_muestra)
            {
                txtMeshPro += "Código " + examen.codigo + "\n";
                string estadoExamen = estado.ContainsKey(examen.estado[0]) ? estado[examen.estado[0]] : "";
                txtMeshPro += examen.nombre + " - " + estadoExamen + "\n";
                txtMeshPro += "------------" + "\n";
            }
            examenes.text = txtMeshPro;
        }
        examenes.text = txtMeshPro;
    }


    public void LimpiarCampos()
    {
        InformacionAdicionalPacienteResultado pacienteVacio = new()
        {
            nombres = "",
            apellidos = "",
            sucursal = "",
            codigoBarras = "",
            sexo = "",
            urgencia = "",
            fecha_nacimiento = "",
            emergencia = false,
            examenes = null,
        };
        ColocarVisualizacionEmergencia(pacienteVacio.emergencia);
        ColocarDatosEnAplicacion(pacienteVacio);
    }
}
