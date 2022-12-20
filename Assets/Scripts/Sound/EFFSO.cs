using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

/// <summary>
/// ȿ���� SO
/// </summary>
[CreateAssetMenu]
public class EFFSO : ScriptableObject
{
	[FormerlySerializedAs("effaudioClips")]
	public AudioClip[] _effaudioClips;
	[FormerlySerializedAs("audioDictionary")]
	private Dictionary<string, AudioClip> _audioDictionary = new Dictionary<string, AudioClip>();

	/// <summary>
	/// ȿ���� Ŭ���� �����´�
	/// </summary>
	/// <param name="name"></param>
	/// <returns></returns>
	public AudioClip GetEFFClip(string name)
	{
		AudioClip clip = null;
		
		if(_audioDictionary.TryGetValue(name, out clip))
		{
			return clip;
		}

		//���� ������ �Ҹ��� ������ �ʱ�ȭ�� �ٽ� �ѹ� �Ѵ�.
		SetEFFClips();
		
		if (_audioDictionary.TryGetValue(name, out clip))
		{
			return clip;
		}

		//ȿ���� ����
		Debug.LogError("���� �̸��Դϴ�");

		return null;
	}

	/// <summary>
	/// ���� ȿ���� Ŭ���� ��ųʸ��� ������
	/// </summary>
	[ContextMenu("SetEFFClips")]
	private void SetEFFClips()
	{
		_audioDictionary.Clear();
		foreach(var clip in  _effaudioClips)
		{
			_audioDictionary.Add(clip.name, clip);
		}
	}

}
