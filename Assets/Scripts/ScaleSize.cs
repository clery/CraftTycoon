using UnityEngine;
using System.Collections;

public class ScaleSize : MonoBehaviour {

    [SerializeField]
    Camera _camera;

	// Use this for initialization
	void Start () {
        ResizeSpriteToScreen();
	}

    void ResizeSpriteToScreen()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
            return;

        transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = _camera.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if (width / height > worldScreenWidth / worldScreenHeight)
            transform.localScale = new Vector3(worldScreenHeight / height, worldScreenHeight / height, transform.localScale.z);
        else
            transform.localScale = new Vector3(worldScreenWidth / width, worldScreenWidth / width, transform.localScale.z);
        transform.localPosition = new Vector3((transform.localScale.x * width - worldScreenWidth) / 2f, 0, 0);
    }
}
