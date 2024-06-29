using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rocket : MonoBehaviour
{
	[SerializeField] GameObject _effects;
	GameObject _vectorHit = null;

	private void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Goal") {
			if (_effects != null)
				_effects.SetActive(true);

			other.GetComponent<Goal>().Hit();
			Invoke("DestroyRocket", 5f);
		}
	}

	private void DestroyRocket() => Destroy(gameObject);

	internal void Fly(GameObject vectorHit) => _vectorHit = vectorHit;

	float time;
	private void FixedUpdate()
	{
		if (_vectorHit != null)
		{
			transform.position = new Vector3(
				Mathf.Lerp(transform.position.x, _vectorHit.transform.position.x, time),
				Mathf.Lerp(transform.position.y, _vectorHit.transform.position.y, time),
				Mathf.Lerp(transform.position.z, _vectorHit.transform.position.z, time));

			time += 0.025f * Time.deltaTime;

			if(time > 0.06f)
				transform.LookAt(_vectorHit.transform.position);

			//if(time > 4)
			//Invoke("DestroyRocket", 0f);
		}
	}
}
