using UnityEngine;

[ExecuteInEditMode]
public class LightImageEffect : MonoBehaviour
{
    public Shader shader;
    public RenderTexture renderTexture;
    public float intensity = 1.0f;

    public Vector3 targetPos;

    private Material mMaterial;
    protected Material Material
    {
        get
        {
            if(mMaterial == null)
            {
                mMaterial = new Material(shader);
                mMaterial.hideFlags = HideFlags.HideAndDontSave;
            }
            return mMaterial;
        }
    }

    private void OnDisable()
    {
        if (mMaterial)
        {
            DestroyImmediate(mMaterial);
            mMaterial = null;
        }
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (renderTexture == null)
            return;
        Material.SetTexture("_LightsTex", renderTexture);
        Material.SetFloat("_MultiplicativeFactor", intensity);
        Graphics.Blit(source, destination, Material);
    }
}
