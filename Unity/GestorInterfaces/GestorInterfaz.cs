using System;
using System.Collections;
using System.Collections.Generic;
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


    public GameObject mensajeInvalido;
    public GameObject mensajeDeEscaneo;
    public GameObject mensajeValido;
    

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
        urgencia.text = resultado.urgencia;
        fecha_nacimiento.text = resultado.fecha_nacimiento;
        sexo.text = resultado.sexo;
        numero_orden.text = resultado.numero_orden == 0 ? "" : resultado.numero_orden.ToString();
        muestra.text = resultado.codigoBarras;
        ColocarExamenes(resultado.examenes);
    }

    private void ColocarExamenes(List<InformacionAdicionalPacienteResultadoExamenes> examenes_muestra)
    {
        string txtMeshPro = "";
        if (examenes_muestra != null)
        {
            foreach (var examen in examenes_muestra)
            {
                // Concatenar cada examen con txtMeshPro
                txtMeshPro += "ID: " + examen.id + "\n";
                txtMeshPro += "Código: " + examen.codigo + "\n";
                txtMeshPro += "Nombre: " + examen.nombre + "\n";
                txtMeshPro += "Estado: " + examen.estado + "\n";
                txtMeshPro += "Valor: " + examen.valor + "\n\n";
            }
            // Asignar el resultado al objeto examenes.text
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
            examenes = null
        };
        ColocarDatosEnAplicacion(pacienteVacio);
    }
}
