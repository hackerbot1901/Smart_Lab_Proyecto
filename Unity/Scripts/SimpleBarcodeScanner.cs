using SimpleJSON;
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using Vuforia;

public class SimpleBarcodeScanner : MonoBehaviour
{
    BarcodeBehaviour mBarcodeBehaviour;
    public static string url = "http://localhost:50352/api/smart-lab-ra-api/v1/configuracion"; // Reemplaza esta URL con la URL de tu servidor
    public string temp = "";
    public int contador = 0;
    public GameObject mensajeInvalido;
    public GameObject mensajeDeEscaneo;
    public GameObject mensajeValido;
    public static ExitoCreacionConfiguracion _config;

    void Start()
    {
        mBarcodeBehaviour = GetComponent<BarcodeBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (mBarcodeBehaviour != null && mBarcodeBehaviour.InstanceData != null)
        {
            string qrJson = mBarcodeBehaviour.InstanceData.Text;
            if (temp != qrJson)
            {
                contador++;
                Debug.Log("Código QR #" + contador + " leído: " + qrJson);
                temp = qrJson;
                StartCoroutine(ControlarMensajes(qrJson));
            }            
        }
    }

   

    private IEnumerator ControlarMensajes(string qrJson)
    {
        ExitoCreacionConfiguracion configuracion = JsonUtility.FromJson<ExitoCreacionConfiguracion>(qrJson);
        if (configuracion != null && configuracion.subdominio != null && configuracion.api_token != null && configuracion.nombre_usuario != null)
        {
            mensajeDeEscaneo.SetActive(false);
            mensajeInvalido.SetActive(false);
            mensajeValido.SetActive(true);
            _config = configuracion;            
            yield return new WaitForSeconds(0.6f);
            //GN
            //StartCoroutine(Configuracion(qrJson, _configuracion));
            //Sideralsoft
            LogicaLectorCodigoBarras.ConfigurarAPI(_config);
            SceneManager.LoadScene("LecturaCodigoBarras");
        }
        else
        {
            mensajeDeEscaneo.SetActive(false);
            mensajeValido.SetActive(false);
            mensajeInvalido.SetActive(true);
            yield return new WaitForSeconds(1.0f); // Pausa de 0.8 segundos
            mensajeInvalido.SetActive(false);
            mensajeDeEscaneo.SetActive(true);
        }
    }

    IEnumerator Configuracion(string JSONCodigoQr, ExitoCreacionConfiguracion codigoQrValido)
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
                LogicaLectorCodigoBarras.ConfigurarAPI(responseAPI);
                SceneManager.LoadScene("LecturaCodigoBarras");
            }
        }
        catch (Exception ex)
        {
            Debug.LogError("Error en la solicitud: " + ex.Message);
        }
    }
    
}
