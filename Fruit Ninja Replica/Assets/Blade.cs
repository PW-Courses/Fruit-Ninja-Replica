using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.PlayerLoop;

public class Blade : MonoBehaviour
{
	private bool isCutting = false;
	public float minCuttingVelocity = .001f;

	private Vector2 previousPosition;
	
	private Rigidbody2D rb;
	private CircleCollider2D circleCollider;

	public GameObject bladeTrailPrefab;
	private GameObject currentBladeTrail;

	void Start()
	{
		circleCollider = GetComponent<CircleCollider2D>();
		rb = GetComponent<Rigidbody2D>();
	}

	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			StartCutting();
		} else if (Input.GetMouseButtonUp(0))
		{
			StopCutting();
		}

		if (isCutting)
		{
			UpdateCut();
		}
	}

	void UpdateCut()
	{
		Vector2 newPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		rb.position = newPosition;

		float velocity = (newPosition - previousPosition).magnitude / Time.deltaTime; // odejmujemy dwa vectory, magniutude zmienia to na długość float
		if (velocity > minCuttingVelocity)
		{
			circleCollider.enabled = true;
		}
		else
		{
			circleCollider.enabled = false;
		}

		previousPosition = newPosition;
	}

	void StartCutting()
	{
		isCutting = true;
		rb.position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		transform.position = rb.position;
		currentBladeTrail = Instantiate(bladeTrailPrefab, transform) as GameObject;
		previousPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		circleCollider.enabled = false;
	}

	void StopCutting()
	{
		isCutting = false;
		currentBladeTrail.transform.SetParent(null);
		Destroy(currentBladeTrail, 3f);
		circleCollider.enabled = false;
	}
}
