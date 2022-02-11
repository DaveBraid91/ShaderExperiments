using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DissolveExample : MonoBehaviour
{
    [Header("Dissolve")]
    [SerializeField] private float _dissolveSpeed = 1;
    [SerializeField] private float _dissolveWait;
    [Space]
    [SerializeField] private bool _useIndex;
    [SerializeField] private MeshRenderer _dissolveMeshRenderer;
    [SerializeField] private int _dissolveMeshIndex;

    [Header("References")]
    [SerializeField] private Material _dissolveMaterial;

    private bool isDissolving;
    private float _dissolveStartValue = 1;
    private float _dissolveEndValue = 0;
    private float _dissolveValue = 0;
    //Se convierte el string en una cadena de números enteros, más eficiente de comparar más abajo
    private int hash_Dissolve = Shader.PropertyToID("_DISSOLVE");

    private void Start()
    {
        if (_useIndex)
            _dissolveMaterial = _dissolveMeshRenderer.materials[_dissolveMeshIndex];
        _dissolveMaterial.SetFloat(hash_Dissolve, _dissolveStartValue);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && !isDissolving)
        {
            //TODO David: Ejecutar efecto
            StartCoroutine(MakeDissolve());
        }
    }

    IEnumerator MakeDissolve()
    {
        isDissolving = true;

        _dissolveValue = _dissolveStartValue;

        while(_dissolveValue > _dissolveEndValue)
        {
            _dissolveValue -= Time.deltaTime * _dissolveSpeed;
            _dissolveMaterial.SetFloat(hash_Dissolve, _dissolveValue);
            yield return null;
        }

        _dissolveValue = _dissolveEndValue;

        Debug.Log($"<color=green><b> CHANGE! </b></color>");

        yield return new WaitForSeconds(_dissolveWait);

        while (_dissolveValue < _dissolveStartValue)
        {
            _dissolveValue += Time.deltaTime * _dissolveSpeed;
            _dissolveMaterial.SetFloat(hash_Dissolve, _dissolveValue);
            yield return null;
        }

        isDissolving = false;

    }
}
