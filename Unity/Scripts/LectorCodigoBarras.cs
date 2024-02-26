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


public class LectorCodigoBarras : MonoBehaviour
{
    BarcodeBehaviour mBarcodeBehaviour;
    public APICliente apiCliente;


    public static ExitoCreacionConfiguracion _configuracionAPI;
    private bool puedeLeerCodigoBarras = true;
    private string codigoBarrasActual = "";
    //


    void Start()
    {
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
                StartCoroutine(apiCliente.ObtenerInformacionPaciente(codigoBarrasActual));
            }
        }
    }
}
