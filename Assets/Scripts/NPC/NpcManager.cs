using UnityEngine;
using System.Collections.Generic;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject _virginPrefab;
    [SerializeField] private Vector2 _generationRange = new Vector3(200,200,0);

    private List<GameObject> _activeObjects;
    private List<GameObject> _inactiveObjects;


    void Awake()
    {
        _activeObjects = new List<GameObject>();
        _inactiveObjects = new List<GameObject>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            GameObject go = GetInstance();
        }
    }

    private GameObject GetInstance()
    {
        GameObject go = new GameObject();
        go = Instantiate(_virginPrefab, (_generationRange * Random.value),Quaternion.identity) as GameObject;

        go.SetActive(true);
        _activeObjects.Add(go);

        return go;
    }
}
