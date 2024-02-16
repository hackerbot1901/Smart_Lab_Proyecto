using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Vuforia;

public class LectorCodigoQr : MonoBehaviour
{
    private BarcodeBehaviour mBarcodeBehaviour;
    private readonly string url = "http://localhost:50352/api/smart-lab-ra-api/v1/configuracion";
    private string temporal = "";
    public ExitoCreacionConfiguracion configuracionActual;
    public GestorInterfazMensajes interfaz;

    void Start()
    {
        mBarcodeBehaviour = GetComponent<BarcodeBehaviour>();
    }

    void Update()
    {
        if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            string qrJson = mBarcodeBehaviour.InstanceData.Text;
            if (temporal != qrJson)
            {
                temporal = qrJson;
                StartCoroutine(ControlarMensajes(qrJson));
            }
        }
    }

    private IEnumerator ControlarMensajes(string qrJson)
    {
        ExitoCreacionConfiguracion configuracion = JsonUtility.FromJson<ExitoCreacionConfiguracion>(qrJson);
        if (EsConfiguracionValida(configuracion))
        {

            interfaz.MostrarMensajeValidoQr();
            configuracionActual = configuracion;
            yield return new WaitForSeconds(0.6f);
            interfaz.MostrarMensajeDeEscaneoQr();
            //GN
            //StartCoroutine(ConfigurarQr(qrJson, configuracionActual));
            //Sideralsoft
            APICliente.ConfigurarAPI(configuracionActual);
            SceneManager.LoadScene("LecturaCodigoBarras");
        }
        else
        {
            interfaz.MostrarMensajeInvalidoQr();
            yield return new WaitForSeconds(1.0f);
            interfaz.MostrarMensajeDeEscaneoQr();
        }

    }

    private bool EsConfiguracionValida(ExitoCreacionConfiguracion configuracion)
    {
        return configuracion != null &&
               !string.IsNullOrEmpty(configuracion.subdominio) &&
               !string.IsNullOrEmpty(configuracion.api_token) &&
               !string.IsNullOrEmpty(configuracion.nombre_usuario) &&
               !string.IsNullOrEmpty(configuracion.nombre_tenant);
    }

    IEnumerator ConfigurarQr(string JSONCodigoQr, ExitoCreacionConfiguracion codigoQrValido)
    {
        byte[] jsonBytes = System.Text.Encoding.UTF8.GetBytes(JSONCodigoQr);
        using UnityWebRequest www = new(url, "POST");
        www.uploadHandler = new UploadHandlerRaw(jsonBytes);
        www.downloadHandler = new DownloadHandlerBuffer();
        www.SetRequestHeader("Content-Type", "application/json");
        yield return www.SendWebRequest();

        try
        {
            if (www.result != UnityWebRequest.Result.Success)
            {
                // Error en la solicitud a la API
                Debug.LogError("Error de red: " + www.error);
            }
            else
            {
                Debug.Log("Solicitud exitosa: " + www.downloadHandler.text);
                ExitoCreacionConfiguracion responseAPI = JsonUtility.FromJson<ExitoCreacionConfiguracion>(www.downloadHandler.text);
                // Puedes utilizar los valores según tus necesidades
                Debug.Log("Mensaje: " + responseAPI.mensaje);
                Debug.Log("Subdominio: " + responseAPI.subdominio);
                Debug.Log("Token: " + responseAPI.api_token);
                responseAPI.nombre_usuario = codigoQrValido.nombre_usuario;
                responseAPI.nombre_tenant = codigoQrValido.nombre_tenant;
                SceneManager.LoadScene("LecturaCodigoBarras");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error en la solicitud: " + ex.Message);
        }
    }



}
