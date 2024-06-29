using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Fly : MonoBehaviour
{
	float speed = 0.2f;   
	private float pos = 0.0f;
	private Vector3 [] path;
	float current;
	float desired;

	void Start()
	{
		current = 1;
		desired = current;
		GeneratePath();
	}

	void Update()
	{
		Move();
	}

	void Move()
	{
		current = Mathf.MoveTowards(current, desired, speed * Time.deltaTime);

		if (current == 0)
			desired = 1;

		// rotation
		if (desired == 1)
		{
			transform.LookAt(iTween.PointOnPath(path, Mathf.Clamp01(pos + 0.01f)));
		}
		else
		{
			Quaternion targetRotation = Quaternion.LookRotation(iTween.PointOnPath(path, Mathf.Clamp01(pos + .01f)) - transform.position);
			transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, speed * 2.5f * Time.deltaTime);
		}
		//

		// position
		iTween.PutOnPath(gameObject, path, pos);
		pos = pos + Time.deltaTime * speed / path.Length;

		if (pos >= 1.0)
		{
			desired = 0;
			pos -= 1.0f;
			GeneratePath();
		}
		//
	}

	private void GeneratePath()
	{
		speed = Random.Range(speed / 2, speed);
		path = new Vector3[10];
		path[0] = transform.position;
		for (int i = 1; i < path.Length; i++)
		{
			float value = Random.Range(200, 400);
			var array = new int[] { -1, -2, 1, 2 };
			path[i] = new Vector3(value * array[Random.Range(0, array.Length)], Random.Range(5, 200), value * array[Random.Range(0, array.Length)]);
		}
	}
}
