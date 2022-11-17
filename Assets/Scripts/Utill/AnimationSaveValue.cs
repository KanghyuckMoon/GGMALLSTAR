using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSaveValue : MonoBehaviour
{
	public Transform rootTransform;
	public Dictionary<int, AnimationValue> keyValuePairs = new Dictionary<int, AnimationValue>();
	private int index = 0;

	[ContextMenu("CopyValue")]
	public void CopyValue()
	{
		index = 100;
		keyValuePairs = new Dictionary<int, AnimationValue>();
		CopyChild(rootTransform);
	}

	private void CopyChild(Transform transform)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform transformChild = transform.GetChild(i);
			keyValuePairs.Add(index++, new AnimationValue(transformChild.position, transformChild.eulerAngles, transformChild.localScale));
			CopyChild(transformChild);
		}
	}

	[ContextMenu("PasteValue")]
	public void PasteValue()
	{
		index = 100;
		PasteChild(rootTransform);
	}

	private void PasteChild(Transform transform)
	{
		for (int i = 0; i < transform.childCount; ++i)
		{
			Transform transformChild = transform.GetChild(i);
			transformChild.position = keyValuePairs[index].pos;
			transformChild.eulerAngles = keyValuePairs[index].rot;
			transformChild.localScale = keyValuePairs[index].scale;
			index += 1;
			PasteChild(transformChild);
		}
	}
}

public class AnimationValue
{
	public Vector3 pos;
	public Vector3 rot;
	public Vector3 scale;

	public AnimationValue(Vector3 pos, Vector3 rot, Vector3 scale)
	{
		this.pos = pos;
		this.rot = rot;
		this.scale = scale;
	}

}
