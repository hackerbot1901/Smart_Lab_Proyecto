using System.Collections;
using System.Collections.Generic;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegresoEscena : MonoBehaviour
{

    public void RegresarEscena()
    {
        APICliente.configuracion = null;
        SceneManager.LoadScene("SampleScene");

    }    
}

