using UnityEngine;
using System.Collections;

public class Scaree : MonoBehaviour {
    [SerializeField]
    float _awareRadius = 5;
    [SerializeField]
    float _speedMultiplier = 5;
	
	// Update is called once per frame
	void Update () {
        Vector3 scareDir = Vector3.zero;
        var hits = Physics2D.CircleCastAll(transform.position, _awareRadius, Vector2.zero, 0,LayerMask.GetMask("Player"));
        foreach (var hit in hits)
        {
            Debug.DrawLine(transform.position, hit.transform.position);

            float scareMagnitude = 1 - (Vector3.Distance(transform.position, hit.transform.position) / _awareRadius);

            scareDir += (transform.position - hit.transform.position).normalized * scareMagnitude;
        }
        Debug.DrawRay(transform.position, scareDir* _speedMultiplier);
	}
}
