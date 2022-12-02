using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraType
{
	Normal,
	Shake,
	Zoom,
	AllStar,
}

public class CameraManager : MonoBehaviour
{
	[SerializeField] private List<CinemachineVirtualCamera> cinemachineVirtualCameras;
	private CinemachineBrain cinemachineBrains;
	private static Coroutine shakeCoroutine;

	public void Start()
	{
		cinemachineBrains = GetComponent<CinemachineBrain>();
		SetCamera(CameraType.Normal, true);
	}

	public void SetCamera(CameraType cameraType, bool isRoundNotMatter = false, bool isRoundSetting = true)
	{
		if(!isRoundNotMatter && isRoundSetting != RoundManager.ReturnIsSetting())
		{
			return;
		}

		foreach (var cam in cinemachineVirtualCameras)
		{
			cam.gameObject.SetActive(false);
		}
		cinemachineVirtualCameras[(int)cameraType].gameObject.SetActive(true);
	}

	public IEnumerator StartShake(float shakeTime, float shakePower)
	{

		CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCameras[(int)CameraType.Shake].GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		float totalShakeTime = shakeTime;
		cinemachineBasicMultiChannelPerlin.m_FrequencyGain = 5;

		SetCamera(CameraType.Shake, false, true);

		while (shakeTime > 0)
		{
			shakeTime -= Time.deltaTime;
			cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0f, shakePower, shakeTime / totalShakeTime);
			yield return new WaitForSeconds(Time.deltaTime);
		}

		SetCamera(CameraType.Normal, false, true);
	}

	public IEnumerator StartKO(Transform transform, float killTime)
	{

		cinemachineVirtualCameras[(int)CameraType.Zoom].Follow = transform;
		cinemachineVirtualCameras[(int)CameraType.Zoom].LookAt = transform;
		var transpoer = cinemachineVirtualCameras[(int)CameraType.Zoom].GetCinemachineComponent<CinemachineTransposer>();
		float killTotalTime = killTime;
		Vector3 startOffset = new Vector3(-3, 0.5f, -2.5f);
		Vector3 endOffset = new Vector3(1.3f, 0.5f, -2.5f);

		SetCamera(CameraType.Zoom, true);

		while (killTime > 0)
		{
			killTime -= Time.deltaTime;
			transpoer.m_FollowOffset = Vector3.Lerp(endOffset, startOffset, killTime / killTotalTime);
			yield return new WaitForEndOfFrame();
		}

		SetCamera(CameraType.Normal, true);
		shakeCoroutine = null;
	}

	public IEnumerator StartAllStar(Transform transform, float killTime)
	{

		cinemachineVirtualCameras[(int)CameraType.AllStar].Follow = transform;
		cinemachineVirtualCameras[(int)CameraType.AllStar].LookAt = transform;
		//var transpoer = cinemachineVirtualCameras[(int)CameraType.Zoom].GetCinemachineComponent<CinemachineTransposer>();
		//float killTotalTime = killTime;
		//Vector3 startOffset = new Vector3(-3, 0.5f, -2.5f);
		//Vector3 endOffset = new Vector3(1.3f, 0.5f, -2.5f);

		SetCamera(CameraType.AllStar, false, true);

		while (killTime > 0)
		{
			killTime -= Time.deltaTime;
			//transpoer.m_FollowOffset = Vector3.Lerp(endOffset, startOffset, killTime / killTotalTime);

			yield return new WaitForEndOfFrame();
		}

		SetCamera(CameraType.Normal, false, true);
		shakeCoroutine = null;
	}

	public static void SetShake(float shakeTime, float shakePower)
	{
		CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
		shakeCoroutine = cameraManager.StartCoroutine(cameraManager.StartShake(shakeTime, shakePower));
	}

	public static void SetKO(Transform transform, float killTime)
	{
		CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
		if (shakeCoroutine is not null)
		{
			cameraManager.StopCoroutine(shakeCoroutine);
		}
		cameraManager.StartCoroutine(cameraManager.StartKO(transform, killTime));
		
	}

	public static void SetAllStar(Transform transform)
	{
		CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
		if (shakeCoroutine is not null)
		{
			cameraManager.StopCoroutine(shakeCoroutine);
		}
		cameraManager.StartCoroutine(cameraManager.StartAllStar(transform, 1f));
	}

}
