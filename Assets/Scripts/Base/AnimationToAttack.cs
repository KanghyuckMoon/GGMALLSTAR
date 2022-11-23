using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationToAttack : MonoBehaviour
{
	private Character character;

	private void Start()
	{
		character = GetComponentInParent<Character>();
	}

	public void OnAttack(int hitboxIndex)
	{
		character.OnAttack(hitboxIndex);
	}
}
