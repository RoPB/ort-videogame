using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMovement : MonoBehaviour
{
    [SerializeField]
    [Range(0, 1)]
    private float speed = 0.1f;

    private MeshRenderer _meshRenderer;

    void Start()
    {
        _meshRenderer = GetComponent<MeshRenderer>();
        if (_meshRenderer == null)
        {
            Debug.LogError("MeshRenderer not found");
        }
    }

    // Update is called once per frame
    void Update()
    {
        var offset = _meshRenderer.material.mainTextureOffset;

        offset.y = Mathf.Repeat(Time.time * speed, 1);

        _meshRenderer.material.mainTextureOffset = offset;
    }
}
