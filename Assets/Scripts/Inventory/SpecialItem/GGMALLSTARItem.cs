using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GGMALLSTARItem : MonoBehaviour
{
	private void Update()
	{
        if (Input.GetMouseButtonDown(0))
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out hit))
            {
                // Check if hit.transform is door, 
                if (hit.collider.gameObject == gameObject)
				{
                    SceneManager.LoadScene("Credit");
				}
            }
        }
    }
}
