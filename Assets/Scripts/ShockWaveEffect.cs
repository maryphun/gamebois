using UnityEngine;
using System.Collections;

public class ShockWaveEffect : MonoBehaviour
{
    [SerializeField]
    [Range(0.0f, 1.0f)]
    private float timer = 0.5f;

    [Range(-1.5f, 1.5f)]
    public float posX = 0.5f;

    [Range(-1.5f, 1.5f)]
    public float posY = 0.5f;

    [Range(0f, 10f)]
    public float duration = 1f;

    [SerializeField]
    [Range(0f, 2f)]
    protected float shine = 0.5f;

    [SerializeField]
    [Range(0f, 1f)]
    protected float distortion = 0.1f;

    [SerializeField]
    [Range(0f, 10f)]
    protected float width = 0.5f;

    [SerializeField]
    protected Material material;

    void OnRenderImage(RenderTexture sourceTexture, RenderTexture destTexture)
    {
        material.SetFloat("_TimeOffset", (4.0f * timer - 2.0f)); // -2 〜 2 まで動けば画面端で切れることもなかろう
        material.SetFloat("_PosX", posX);
        material.SetFloat("_PosY", posY);
        material.SetFloat("_ShineMag", shine);
        material.SetFloat("_DistortionMag", distortion);
        material.SetFloat("_WidthRev", 1.0f / width);
        material.SetFloat("_AspectRatio", sourceTexture.width / (float)sourceTexture.height);
        Graphics.Blit(sourceTexture, destTexture, material);
    }
    void Update()
    {
        timer += Time.deltaTime * (1.0f / duration);
        if (timer > 1.0f)
        {
            enabled = false;
        }
    }

    public void StartEffect(Vector3 world_pos)
    {
        var camera = GetComponent<Camera>();
        var screen_pos = camera.WorldToScreenPoint(world_pos);
        enabled = true;
        timer = 0.0f;
        posX = (screen_pos.x) / camera.pixelWidth;
        posY = (screen_pos.y) / camera.pixelHeight;
    }

    [ContextMenu("startEffectTest")]
    private void startEffectTest()
    {
        StartEffect(Vector3.zero);
    }
}

