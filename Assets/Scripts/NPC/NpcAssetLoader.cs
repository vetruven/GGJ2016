using System.Collections.Generic;
using UnityEngine;

public class NpcAssetLoader : MonoBehaviour
{
    private static Dictionary<NpcBodyPart, List<Sprite>> _npcResources;

    public static Sprite GetNpcBodypartSprite(NpcBodyPart bodypart)
    {
        if (_npcResources == null)
            LoadNpcResourcesSprites();

        return new Sprite();
    }

    private static void LoadNpcResourcesSprites()
    {
        _npcResources = new Dictionary<NpcBodyPart, List<Sprite>>();
        //Resources.LoadAll<Sprite>()
    }
}
