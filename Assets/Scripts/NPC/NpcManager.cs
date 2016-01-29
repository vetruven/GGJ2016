using UnityEngine;
using System.Collections.Generic;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject _virginPrefab;
    [SerializeField] private Vector2 _generationRange = new Vector3(200,200,0);

    void Update()
    {
        if (Input.GetKey(KeyCode.P))
        {
            GameObject go = GetInstance();
        }
    }


    private GameObject GetInstance()
    {
        Vector3 newPos = _generationRange;
        newPos.Scale(Random.onUnitSphere);
        var go = Instantiate(_virginPrefab, newPos, Quaternion.identity) as GameObject;
        return go;
    }
}
