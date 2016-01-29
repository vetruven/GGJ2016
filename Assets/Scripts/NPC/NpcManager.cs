using UnityEngine;
using System.Collections.Generic;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject _virginPrefab;
    [SerializeField] private Vector2 _generationRange = new Vector3(200,200,0);
    [SerializeField] private float _creationInterval = 1; 

    private bool _isCreating;
    private float _creationTimer;

    void Awake()
    {
        EventBus.StartGame.AddListener(StartGame);
        EventBus.EndGame.AddListener(EndGame);
    }

    private void EndGame()
    {
        _isCreating = false;
        foreach (var virgin in FindObjectsOfType<VirginController>())
        {
            Destroy(virgin.gameObject);
        }
    }

    private void StartGame()
    {
        _isCreating = true;
        for (int i = 0; i < 60; i++)
        {
            CreateVirgin();
        }
    }

    void Start()
    {
    }

    void Update()
    {
        if (_isCreating && _creationTimer <= Time.time)
        {
            CreateVirgin();
            _creationTimer = Time.time + _creationInterval;
        }
    }


    private void CreateVirgin()
    {
        Vector3 newPos = _generationRange;
        newPos.Scale(Random.onUnitSphere);
        var go = Instantiate(_virginPrefab, newPos, Quaternion.identity) as GameObject;
    }
}
