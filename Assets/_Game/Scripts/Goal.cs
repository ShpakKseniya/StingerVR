using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
	[SerializeField] GameObject _effects;
	internal void Hit()
	{
		if (_effects != null)
			_effects.SetActive(true);

		Invoke("DestroyGoal", 5f);
	}

	private void DestroyGoal() => Destroy(gameObject);
}
