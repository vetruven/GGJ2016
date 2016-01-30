using UnityEngine;

public class VirginController : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _headNode;
    [SerializeField] private SpriteRenderer _torsoNode;
    [SerializeField] private SpriteRenderer _leftLegNode;
    [SerializeField] private SpriteRenderer _rightLegNode;
    [SerializeField] private SpriteRenderer _rightHandNode;
    [SerializeField] private SpriteRenderer _leftHandNode;
    [SerializeField] private SpriteRenderer _boobsNode;
    [SerializeField] private SpriteRenderer _genetaliaNode;

    [SerializeField]
    private VirginParticleController _birthParticles;
    [SerializeField]
    private VirginParticleController _deathParticles;

    private Transform _transform;

    public void Awake()
    {
        GetComponent<Scared>().enabled = false;
        _transform = GetComponent<Transform>();
    }

    public void Start()
    {
        EventBus.TheHandIsDown.AddListener(CheckDeath);
    }

    void OnDestroy()
    {
        EventBus.TheHandIsDown.RemoveListener(CheckDeath);

    }


    public void Setup(Vector3 positionToFallTo)
    {
        AssignRandomBodyparts();
        GetComponent<Scared>().enabled = true;
        LeanTween.move(gameObject, positionToFallTo, 1).onComplete += () =>
        {
            Instantiate(_birthParticles, transform.position, Quaternion.identity);
        };
    }

    private void AssignRandomBodyparts()
    {
        _headNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.Head);
        _torsoNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.Torso);
        _leftLegNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.LeftLeg);
        _rightLegNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.RightLeg);
        _rightHandNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.RightHand);
        _leftHandNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.LeftHand);
        _boobsNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.Boobies);
        _genetaliaNode.sprite = NpcAssetLoader.GetRandomNpcBodypartSprite(NpcBodyPart.Genetalia);
    }

    private void CheckDeath(Vector3 pos, float radius)
    {
        if( Vector3.Distance(_transform.position, pos) < radius)
        {
            EventBus.VirginDied.Dispatch();
            EventBus.TheHandIsDown.RemoveListener(CheckDeath);
            Instantiate(_deathParticles, transform.position, Quaternion.identity);

            Destroy(gameObject);
        }

    }

    
}