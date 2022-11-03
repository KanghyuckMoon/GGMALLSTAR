using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class EFFSO : ScriptableObject
{
	private Dictionary<string, AudioClip> audioDictionary = new Dictionary<string, AudioClip>();

	public AudioClip[] effaudioClips;


	//소리 가져오기
	public AudioClip GetEFFClip(string name)
	{
		AudioClip clip = null;
		if(audioDictionary.TryGetValue(name, out clip))
		{
			return clip;
		}

		Debug.LogError("없는 이름입니다");

		return null;
	}

	[ContextMenu("SetEFFClips")]
	private void SetEFFClips()
	{
		audioDictionary.Clear();
		foreach(var clip in  effaudioClips)
		{
			audioDictionary.Add(clip.name, clip);
		}
	}

}
