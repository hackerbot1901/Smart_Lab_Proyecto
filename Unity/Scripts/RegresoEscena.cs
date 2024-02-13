using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegresoEscena : MonoBehaviour
{
    public GameObject mensajeInvalido;
    public GameObject mensajeDeEscaneo;
    public GameObject mensajeValido;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void RegresarEscena()
    {
        SimpleBarcodeScanner._config = null;
        SceneManager.LoadScene("SampleScene");

    }

    public void MostrarMensajeDeEscaneo()
    {
        mensajeValido.SetActive(false);
        mensajeInvalido.SetActive(false);
        mensajeDeEscaneo.SetActive(true);
    }
}

