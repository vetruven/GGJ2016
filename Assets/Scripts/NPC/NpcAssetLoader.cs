using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class NpcAssetLoader : MonoBehaviour
{
    private static Dictionary<NpcBodyPart, List<Sprite>> _npcResources;

    public static Sprite GetRandomNpcBodypartSprite(NpcBodyPart bodypart)
    {
        if (_npcResources == null)
            LoadNpcResourcesSprites();

        return _npcResources[bodypart].GetRandom();
    }

    private static void LoadNpcResourcesSprites()
    {
        _npcResources = new Dictionary<NpcBodyPart, List<Sprite>>();

        _npcResources[NpcBodyPart.Boobies] = Resources.LoadAll<Sprite>("Virgins/Boobies").ToList();
        _npcResources[NpcBodyPart.Genetalia] = Resources.LoadAll<Sprite>("Virgins/Genetalia").ToList();
        _npcResources[NpcBodyPart.Head] = Resources.LoadAll<Sprite>("Virgins/Head").ToList();
        _npcResources[NpcBodyPart.LeftHand] = Resources.LoadAll<Sprite>("Virgins/LeftHand").ToList();
        _npcResources[NpcBodyPart.LeftLeg] = Resources.LoadAll<Sprite>("Virgins/LeftLeg").ToList();
        _npcResources[NpcBodyPart.RightHand] = Resources.LoadAll<Sprite>("Virgins/RightHand").ToList();
        _npcResources[NpcBodyPart.RightLeg] = Resources.LoadAll<Sprite>("Virgins/RightLeg").ToList();
        _npcResources[NpcBodyPart.Torso] = Resources.LoadAll<Sprite>("Virgins/Torso").ToList();
    }
}
