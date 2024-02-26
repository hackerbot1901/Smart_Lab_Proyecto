using SimpleJSON;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;
using Vuforia;

public class APICliente : MonoBehaviour
{
    public GestorInterfaz interfaz;
    public static ExitoCreacionConfiguracion configuracion;
    public TextMeshProUGUI laboratorista;
    public TextMeshProUGUI nombre_tenant;


    void Start()
    {
        laboratorista.text = configuracion.nombre_usuario;
        nombre_tenant.text = configuracion.nombre_tenant;
    }

    private InformacionAdicionalPacienteResultado MapearJSONText(string jsonText, string codigo_barras)
    {
        //var resultado = JsonUtility.FromJson<InformacionAdicionalPacienteResultado>(jsonText);
        //Sideralsoft
        var resultado = APISideralsoft(jsonText, codigo_barras);
        return resultado;
    }

    private static InformacionAdicionalPacienteResultado APISideralsoft(string jsonText, string codigo_barras)
    {
        JSONNode data = JSON.Parse(jsonText)["data"][0];
        List<InformacionAdicionalPacienteResultadoExamenes> examenes = new List<InformacionAdicionalPacienteResultadoExamenes>();
        JSONArray examenesArray = (JSONArray)data["examenes"];
        foreach (JSONNode examenNode in examenesArray.Children)
        {
            InformacionAdicionalPacienteResultadoExamenes examen = new()
            {
                id = examenNode["id"],
                codigo = examenNode["codigo"],
                nombre = examenNode["nombre"],
                fecha_creacion = examenNode["fecha_creacion"],
                fecha_toma_muestra = examenNode["fecha_toma_muestra"],
                fecha_reporte = examenNode["fecha_reporte"],
                fecha_validacion = examenNode["fecha_validacion"],
                usuario_validacion = examenNode["usuario_validacion"],
                estado = examenNode["estado"],
                comentario = examenNode["comentario"],
                valor = examenNode["valor"],
                resultados = examenNode["resultados"]
            };
            examenes.Add(examen);
        }

        InformacionAdicionalPacienteResultado resultado = new()
        {
            sucursal = data["sucursal"]["nombre"],
            codigoBarras = codigo_barras,
            nombres = data["paciente"]["nombres"],
            apellidos = data["paciente"]["apellidos"],
            sexo = data["paciente"]["sexo"],
            fecha_nacimiento = data["paciente"]["fecha_nacimiento"],
            urgencia = data["estado"],
            emergencia = data["tipo_atencion"]["urgente"],
            numero_orden = data["numero_orden"],
            examenes = examenes
        };
        return resultado;
    }


    private string ConstruirUrl(string codigo_barras)
    {
        //string url = "http://localhost:50352/api/smart-lab-ra-api/v1/informacionPaciente/" + codigo_barras;
        //Sidersalsoft
        string url = "https://" + configuracion.subdominio + ".orion-labs.com/api/v1/ordenes?filtrar[codigo_barras]=" + codigo_barras + "&incluir=examenes,paciente,sucursal,tipo_atencion,servicio";
        return url;
    }

    public IEnumerator ObtenerInformacionPaciente(string codigo_barras)
    {
        using UnityWebRequest www = ConfigurarPeticiones(codigo_barras);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            interfaz.MostrarMensajeInvalido();
            interfaz.LimpiarCampos();
            Debug.Log("Error de red: " + www.error);
        }
        else
        {
            try
            {
                interfaz.MostrarMensajeValido();
                string jsonText = www.downloadHandler.text;
                var resultado = MapearJSONText(jsonText, codigo_barras);
                interfaz.ColocarDatosEnAplicacion(resultado);
                //InvokeRepeating(nameof(ColocarTiempo), 0f, 1f); // Invoca la función ActualizarTiempo cada segundo

            }

            catch (Exception e)
            {
                // Mostrar mensaje de código de barras inválido
                interfaz.MostrarMensajeInvalido();
                interfaz.LimpiarCampos();
                Debug.Log("Error al procesar la respuesta: " + e.Message);
            }
        }
    }

    private UnityWebRequest ConfigurarPeticiones(string codigo_barras)
    {
        string url = ConstruirUrl(codigo_barras);
        UnityWebRequest www = UnityWebRequest.Get(url);
        www.SetRequestHeader("Authorization", "Bearer " + configuracion.api_token);
        www.SetRequestHeader("Content-Type", "application/json");
        www.SetRequestHeader("Accept", "application/json");
        return www;
    }

    internal static void ConfigurarAPI(ExitoCreacionConfiguracion configuracionActual)
    {
        configuracion = configuracionActual;
    }
}
