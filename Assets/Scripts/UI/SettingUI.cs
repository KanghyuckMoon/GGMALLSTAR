using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingUI : MonoBehaviour
{
	public GameObject[] panels;
	//
	public void ActivePanel(int index)
	{
		foreach (var panel in panels)
		{
			panel.SetActive(false);
		}
		panels[index].SetActive(true);
	}
}
