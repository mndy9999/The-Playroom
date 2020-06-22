using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class LightController : MonoBehaviour
{
	public Camera mainCamera;
	public LayerMask lightLayer;

	float rtSizeMultiplier = 1f;

	float previousCameraOrthoSize;

	[SerializeField]
	Camera lightCamera;

	RenderTexture renderTexture;

	int lastScreenW = -1;
	int lastScreenH = -1;

	LightImageEffect imageEffect;

	private void OnEnable()
	{
		imageEffect = mainCamera.GetComponent<LightImageEffect>();
		SetupCamera();
		UpdateRenderTexture();
		transform.localPosition = Vector3.zero;
	}

	private void OnDisable()
	{
		//cleanup textures
		if (lightCamera != null)
			lightCamera.targetTexture = null;
		if(renderTexture != null)
		{
			renderTexture.Release();
			DestroyImmediate(renderTexture);
		}
		imageEffect.renderTexture = null;

	}

	private void OnPreRender()
	{
		//update render texture if screen size changes
		if (mainCamera.orthographicSize != previousCameraOrthoSize || lastScreenH != Screen.height || lastScreenW != Screen.width)
		{
			lightCamera.orthographicSize = mainCamera.orthographicSize;
			previousCameraOrthoSize = mainCamera.orthographicSize;

			UpdateRenderTexture();
		}
	}

	private void SetupCamera()
	{
		if (lightCamera != null)
		{
			//previousCameraOrthoSize = mainCamera.orthographicSize;
			return;
		}

		lightCamera = GetComponent<Camera>();
		if (lightCamera == null)
		{
			lightCamera = gameObject.AddComponent<Camera>();
			lightCamera.backgroundColor = new Color(0.25f, 0.25f, 0.25f);
		}

		lightCamera.CopyFrom(mainCamera);
		mainCamera.cullingMask ^= lightLayer;

		// set our custom settings here
		lightCamera.cullingMask = lightLayer;
		lightCamera.clearFlags = CameraClearFlags.Color;
		lightCamera.useOcclusionCulling = false;
		lightCamera.targetTexture = null;

		// we need to render before the main camera
		lightCamera.depth = mainCamera.depth - 10;
	}

	private void UpdateRenderTexture()
	{
		if(renderTexture != null)
		{
			lightCamera.targetTexture = null;
			renderTexture.Release();
			DestroyImmediate(renderTexture);
		}

		lastScreenH = Screen.height;
		lastScreenW = Screen.width;

		// setup the width/height based on our multiplier
		var rtWidth = Mathf.RoundToInt(lightCamera.pixelWidth * rtSizeMultiplier);
		var rtHeight = Mathf.RoundToInt(lightCamera.pixelHeight * rtSizeMultiplier);

		if (rtWidth == 0 || rtHeight == 0)
		{
			Debug.LogWarning("RT height or width rounded to 0. Defaulting to the camera pixelWidth");
			rtWidth = lightCamera.pixelWidth;
			rtHeight = lightCamera.pixelWidth;
		}

		renderTexture = new RenderTexture(rtWidth, rtHeight, 0, RenderTextureFormat.Default);
		renderTexture.name = "LightRenderTexture";
		renderTexture.Create();
		renderTexture.filterMode = FilterMode.Point;
		lightCamera.targetTexture = renderTexture;

		imageEffect.renderTexture = renderTexture;
	}

}


