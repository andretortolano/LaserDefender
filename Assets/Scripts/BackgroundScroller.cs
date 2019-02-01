using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroller : MonoBehaviour
{

    [SerializeField] float scrollSpeed = 1f;

    Material mMaterial = null;
    Vector2 offset;

    // Start is called before the first frame update
    void Start()
    {
        mMaterial = GetComponent<Renderer>().material;
        offset = new Vector2(0, scrollSpeed);
    }

    // Update is called once per frame
    void Update()
    {
        mMaterial.mainTextureOffset += offset * Time.deltaTime;
    }
}
