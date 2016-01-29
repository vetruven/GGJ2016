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

    private void Awake()
    {
        AssignRandomBodyparts();
        var birthParticle = Instantiate(_birthParticles);
        birthParticle.TransformToFollow = transform;
    }

    void OnDestroy()
    {
        //Instantiate(_deathParticles, transform.position, Quaternion.identity);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            AssignRandomBodyparts();
        }
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
}