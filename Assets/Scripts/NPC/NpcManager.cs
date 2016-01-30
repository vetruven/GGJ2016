using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class NpcManager : MonoBehaviour
{
    [SerializeField] private GameObject _virginPrefab;
    [SerializeField] private Vector2 _generationRange = new Vector3(200,200,0);
    [SerializeField] private float _creationInterval = 1;
    [SerializeField] private int _targetVirginCount = 80;

    [SerializeField]
    private int _virginsCount;

    private bool _isCreating;
    private float _creationTimer;

    private Transform _transfrom;

    private int _virginEnumerator = 0;

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
        _virginsCount = 0;
        EventBus.VirginDied.RemoveListener(VirginDied);
    }

    private void StartGame()
    {
        _isCreating = true;
        StartCoroutine(CreateMany(60));
        EventBus.VirginDied.AddListener(VirginDied);
    }

    void VirginDied()
    {
        _virginsCount--;
    }

    IEnumerator CreateMany(int howMany)
    {
        for (int i = 0; i < howMany; i++)
        {
            CreateVirgin();
            yield return null;
        }
    }

    void Update()
    {
        if (_virginsCount < _targetVirginCount && _isCreating && _creationTimer <= Time.time)
        {
            StartCoroutine(CreateMany(_targetVirginCount - _virginsCount));
            _creationTimer = Time.time + _creationInterval;
        }
    }

    void Start()
    {
        _transfrom = GetComponent<Transform>();
    }

    private void CreateVirgin()
    {
        _virginsCount++;
        Vector3 newPos = _generationRange;
        newPos.Scale(Random.onUnitSphere);
        var go = Instantiate(_virginPrefab, newPos, Quaternion.identity) as GameObject;
        go.GetComponent<VirginController>().Setup(newPos);
        go.transform.position += Vector3.up*1000;
        go.transform.parent = _transfrom;
        go.name = "Virgin " + _virginEnumerator++;
    }
}
