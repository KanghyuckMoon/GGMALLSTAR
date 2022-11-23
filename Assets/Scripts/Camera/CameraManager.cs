using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public enum CameraType
{
	Normal,
	Shake,
}

public class CameraManager : MonoBehaviour
{
	[SerializeField] private List<CinemachineVirtualCamera> cinemachineVirtualCameras;
	private CinemachineBrain cinemachineBrains;

	public void Start()
	{
		cinemachineBrains = GetComponent<CinemachineBrain>();
		SetCamera(CameraType.Normal);
	}

	public void SetCamera(CameraType cameraType)
	{
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

		SetCamera(CameraType.Shake);

		while (shakeTime > 0)
		{
			shakeTime -= Time.deltaTime;
			cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(0f, shakePower, shakeTime / totalShakeTime);
			yield return new WaitForSeconds(Time.deltaTime);
		}

		SetCamera(CameraType.Normal);
	}

	public static void SetShake(float shakeTime, float shakePower)
	{
		CameraManager cameraManager = Camera.main.GetComponent<CameraManager>();
		cameraManager.StartCoroutine(cameraManager.StartShake(shakeTime, shakePower));
	}

}
