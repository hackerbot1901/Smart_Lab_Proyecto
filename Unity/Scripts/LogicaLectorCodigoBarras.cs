using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;


public class LogicaLectorCodigoBarras : MonoBehaviour
{

    BarcodeBehaviour mBarcodeBehaviour;
    public TextMeshProUGUI sucursal;
    public TextMeshProUGUI codigoBarras;
    public TextMeshProUGUI muestra;
    public TextMeshProUGUI nombres;
    public TextMeshProUGUI urgencia;
    public TextMeshProUGUI fecha_nacimiento;
    public TextMeshProUGUI numero_orden;
    public TextMeshProUGUI sexo;
    public TextMeshProUGUI examenes;
    public TextMeshProUGUI laboratorista;
    public TextMeshProUGUI tiempo_TMP;

    public GameObject mensajeInvalido;
    public GameObject mensajeDeEscaneo;
    public GameObject mensajeValido;

    private int tiempoRestante;
    public static ExitoCreacionConfiguracion _configuracionAPI;
    private bool puedeLeerCodigoBarras = true;
    private string codigoBarrasActual = "";

    void Start()
    {
        laboratorista.text = _configuracionAPI.nombre_usuario;
        mBarcodeBehaviour = GetComponent<BarcodeBehaviour>();        
    }

    void Update()
    {
        if (puedeLeerCodigoBarras && mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            string codigoBarras = mBarcodeBehaviour.InstanceData.Text;
            if (codigoBarras != codigoBarrasActual)
            {
                codigoBarrasActual = codigoBarras;
                StartCoroutine(LeerCodigoBarras());
            }            
        }
    }

    IEnumerator LeerCodigoBarras()
    {
        puedeLeerCodigoBarras = false;
        yield return ObtenerInformacionPaciente(codigoBarrasActual);
    }

    IEnumerator ObtenerInformacionPaciente(string codigo_barras)
    {
        using UnityWebRequest www = ConfigurarPeticiones(codigo_barras);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {            
            MostrarMensajeInvalido();
            LimpiarCampos();
            Debug.Log("Error de red: " + www.error);
        }
        else
        {
            try
            {
                MostrarMensajeValido();
                string jsonText = www.downloadHandler.text;
                var resultado = MapearJSONText(jsonText);
                tiempoRestante = 10;
                ColocarDatosEnAplicacion(resultado);
                InvokeRepeating(nameof(ColocarTiempo), 0f, 1f); // Invoca la función ActualizarTiempo cada segundo

            }

            catch (Exception e)
            {
                // Mostrar mensaje de código de barras inválido
                MostrarMensajeInvalido();
                LimpiarCampos();                
                Debug.Log("Error al procesar la respuesta: " + e.Message);
            }
        }
    }

    private void MostrarMensajeValido()
    {
        mensajeDeEscaneo.SetActive(false);
        mensajeInvalido.SetActive(false);
        mensajeValido.SetActive(true);

    }

    private void MostrarMensajeInvalido()
    {
        mensajeValido.SetActive(false);
        mensajeDeEscaneo.SetActive(false);
        mensajeInvalido.SetActive(true);
    }

    private void MostrarMensajeDeEscaneo()
    {
        mensajeValido.SetActive(false);
        mensajeInvalido.SetActive(false);
        mensajeDeEscaneo.SetActive(true);
    }

    private void ColocarTiempo()
    {
        tiempo_TMP.text = tiempoRestante.ToString();
        tiempoRestante--;

        if (tiempoRestante <= 0)
        {
            CancelInvoke(nameof(ColocarTiempo)); // Detiene la repetición cuando tiempoRestante llega a cero
            MostrarMensajeDeEscaneo();
            LimpiarCampos();
        }

    }

    
    public void ColocarDatosEnAplicacion(InformacionAdicionalPacienteResultado resultado)
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
        if(examenes_muestra != null)
        {
            foreach (var examen in examenes_muestra)
            {
                // Concatenar cada examen con txtMeshPro
                txtMeshPro += "Examen:\n";
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


    private UnityWebRequest ConfigurarPeticiones(string codigo_barras)
    {
        string url = ConstruirUrl(_configuracionAPI, codigo_barras);
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", "Bearer " + _configuracionAPI.api_token);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        return www;
    }

    private InformacionAdicionalPacienteResultado MapearJSONText(string jsonText)
    {
        //var resultado = JsonUtility.FromJson<InformacionAdicionalPacienteResultado>(jsonText);
        //Sideralsoft
        var resultado = APISideralsoft(jsonText);
        return resultado;
    }

    private static InformacionAdicionalPacienteResultado APISideralsoft(string jsonText)
    {
        JSONNode data = JSON.Parse(jsonText)["data"][0];
        List<InformacionAdicionalPacienteResultadoExamenes> examenes = new List<InformacionAdicionalPacienteResultadoExamenes>();
        JSONArray examenesArray = (JSONArray)data["examenes"];
        foreach (JSONNode examenNode in examenesArray.Children)
        {
            InformacionAdicionalPacienteResultadoExamenes examen = new InformacionAdicionalPacienteResultadoExamenes();
            examen.id = examenNode["id"];
            examen.codigo = examenNode["codigo"];
            examen.nombre = examenNode["nombre"];
            examen.fecha_creacion = examenNode["fecha_creacion"];
            examen.fecha_toma_muestra = examenNode["fecha_toma_muestra"];
            examen.fecha_reporte = examenNode["fecha_reporte"];
            examen.fecha_validacion = examenNode["fecha_validacion"];
            examen.usuario_validacion = examenNode["usuario_validacion"];
            examen.estado = examenNode["estado"];
            examen.comentario = examenNode["comentario"];
            examen.valor = examenNode["valor"];
            examen.resultados = examenNode["resultados"];
            examenes.Add(examen);
        }

        InformacionAdicionalPacienteResultado resultado = new()
        {
            sucursal = data["sucursal_id"],
            codigoBarras = data["numero_orden"],
            nombres = data["paciente"]["nombres"],
            apellidos = data["paciente"]["apellidos"],
            sexo = data["paciente"]["sexo"],
            fecha_nacimiento = data["paciente"]["fecha_nacimiento"],
            urgencia = data["estado"],
            numero_orden = data["numero_orden"],
            examenes = examenes
        };
        return resultado;
    }

    private string ConstruirUrl(ExitoCreacionConfiguracion configuracionAPI, string codigo_barras)
    {
        //string url = "http://localhost:50352/api/smart-lab-ra-api/v1/informacionPaciente/" + codigo_barras;
        //Sidersalsoft
        string url = "https://" + configuracionAPI.subdominio + ".orion-labs.com/api/v1/ordenes?filtrar[codigo_barras]=" + codigo_barras + "&incluir=examenes,paciente";
        return url;
    }

    public static void ConfigurarAPI(ExitoCreacionConfiguracion configuracion)
    {
        _configuracionAPI = configuracion;
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
        tiempo_TMP.text = "";
        tiempoRestante = 0;
        ColocarDatosEnAplicacion(pacienteVacio);        
    }
    
    
}

