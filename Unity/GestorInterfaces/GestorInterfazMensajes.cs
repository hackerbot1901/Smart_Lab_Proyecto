using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestorInterfazMensajes : MonoBehaviour
{
    public GameObject mensajeInvalidoQr;
    public GameObject mensajeDeEscaneoQr;
    public GameObject mensajeValidoQr;

    public void MostrarMensajeValidoQr()
    {
        mensajeDeEscaneoQr.SetActive(false);
        mensajeInvalidoQr.SetActive(false);
        mensajeValidoQr.SetActive(true);
    }

    public void MostrarMensajeInvalidoQr()
    {
        mensajeDeEscaneoQr.SetActive(false);
        mensajeValidoQr.SetActive(false);
        mensajeInvalidoQr.SetActive(true);
    }

    public void MostrarMensajeDeEscaneoQr()
    {
        mensajeInvalidoQr.SetActive(false);
        mensajeValidoQr.SetActive(false);
        mensajeDeEscaneoQr.SetActive(true);
    }
}
