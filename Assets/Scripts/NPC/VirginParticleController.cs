using UnityEngine;
using System.Collections;

public class VirginParticleController : MonoBehaviour
{

    public Transform TransformToFollow;
    private ParticleSystem ps;

    void Awake()
    {
        ps = GetComponent<ParticleSystem>();
    }

    void FixedUpdate()
    {
        if (TransformToFollow != null)
            transform.position = TransformToFollow.position;

        if(!ps.isPlaying && !ps.IsAlive())
            Destroy(gameObject);
    }
}
