using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class SpriteDatabase : Singleton<SpriteDatabase> {

    public Dictionary<int, Sprite> Sprites = new Dictionary<int, Sprite>();

    protected override void Init() {
        base.Init();
        LoadFromResources();
    }

    void LoadFromResources() {
        Sprite[] sprites = Resources.LoadAll<Sprite>("Sprites");
        for (int i = 0; i < sprites.Length; ++i) {
            if (Sprites.ContainsKey(int.Parse(sprites[i].name))) {
                Debug.LogError("SpriteDatabase conflict : two assets have the same ID " + sprites[i].name);
                break;
            }
            Sprites[int.Parse(sprites[i].name)] = sprites[i];
        }
    }
}
