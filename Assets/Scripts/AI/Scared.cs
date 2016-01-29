using UnityEngine;
using System.Collections;

public class Scared : MonoBehaviour {
    [SerializeField]
    float _demonAwareRadius = 5;
    [SerializeField]
    float _virginAwareRadius = 1;
    [SerializeField]
    bool _virginAwareness = true;
    [SerializeField]
    float _speedMultiplier = 5;


    [SerializeField]
    float _randomizeDirectionStep = 0.1f;
    [SerializeField]
    float _noDemondsRandomizeStep = 1.0f;
    [SerializeField]
    float _movementOffsetAngle = 30f;

    [SerializeField]
    float _PlayerWeight = 0.8f;

    Vector3 _enemyRunDirection = Vector3.zero;
    Vector3 _flockRunDirection = Vector3.zero;
    Vector3 _runDirection;
    Quaternion _runOffset;

    bool _seeDemons = false;

    void Start()
    {
        StartCoroutine(RandomizeRunDirection());
        StartCoroutine(NoDemonDirection());
    }

	// Update is called once per frame
	void Update () {

        DemonAwareness();
        VirginAwareness();

        ApplyMovement();
        
    }

    private void DemonAwareness()
    {
        Vector3 newRunDirection = CalculateRunDirection(_demonAwareRadius, "Player");
        _seeDemons = newRunDirection != Vector3.zero;

        if (newRunDirection != Vector3.zero)
        {
            _enemyRunDirection = newRunDirection;
        }
    }

    private void VirginAwareness()
    {
        if (_virginAwareness)
        {
            _flockRunDirection = CalculateRunDirection(_virginAwareRadius, "Virgin");
        }
    }

    private Vector3 CalculateRunDirection(float awareRadius , params string[] layers)
    {
        Vector3 scareDir = Vector3.zero;
        var hits = Physics2D.CircleCastAll(transform.position, awareRadius, Vector2.zero, 0, LayerMask.GetMask(layers));
        float maxDistance = 0;
        foreach (var hit in hits)
        {
            float dist = Vector3.Distance(hit.transform.position, transform.position);
            maxDistance = dist > maxDistance ? dist : maxDistance;
        }
        if (maxDistance == 0)
        {
            return Vector3.zero;
        }
        foreach (var hit in hits)
        {
            Debug.DrawLine(transform.position, hit.transform.position);

            float scareMagnitude = 1.3f - (Vector3.Distance(transform.position, hit.transform.position) / awareRadius) ;

            scareDir += (transform.position - hit.transform.position).normalized * scareMagnitude;
        }
        return scareDir * _speedMultiplier;
        
    }

    private void ApplyMovement()
    {
        _runDirection = _enemyRunDirection * _PlayerWeight + _flockRunDirection * (1 - _PlayerWeight);
        _runDirection = _runOffset * _runDirection;
        GetComponent<Rigidbody2D>().velocity = new Vector2(_runDirection.x, _runDirection.y);
        //Debug.DrawRay(transform.position, GetComponent<Rigidbody2D>().velocity * _speedMultiplier);
    }

    IEnumerator RandomizeRunDirection()
    {
        while (true)
        {

        yield return new WaitForSeconds(_randomizeDirectionStep);

        float angle = Random.Range(-1, 2);
        _runOffset = Quaternion.Euler(0, 0, _movementOffsetAngle*angle);
        }

    }

    IEnumerator NoDemonDirection()
    {
        while (true)
        {

            yield return new WaitForSeconds(_noDemondsRandomizeStep);
            if (!_seeDemons)
            {
                float x = Random.Range(-1f, 1f);
                float y = Random.Range(-1f, 1f);
                Vector3 dir = new Vector3(x, y);
                _enemyRunDirection = dir * _speedMultiplier;
            }
        }
    }
}
