using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrthoCamera : MonoBehaviour {

	public Transform player;
	public float smoothTime = 1f;
	public float adjustX;
	public float adjustY;


	public float mapMaxLimitX;
	public float mapMinLimitX;
	public float mapMaxLimitY;
	public float mapMinLimitY;

	void Start () {

		Camera thisCamera = Camera.main;

		player = GameObject.Find ("Hero").GetComponent<Transform>();
		Renderer map = GameObject.Find ("BasicMap").GetComponent<Renderer>();

		Vector3 mapPosition = map.transform.position;
		Vector3 mapDimensions = map.bounds.size;

		Vector3 p = thisCamera.ViewportToWorldPoint(new Vector3(1, 1, thisCamera.nearClipPlane));
		Vector3 q = thisCamera.ViewportToWorldPoint(new Vector3(0, 0, thisCamera.nearClipPlane));

		adjustX = (p.x - q.x) / 2;
		adjustY = (p.y - q.y) / 2;

		mapMaxLimitX = mapPosition.x + (mapDimensions.x/2.0f) - adjustX;
		mapMinLimitX = mapPosition.x - (mapDimensions.x/2.0f) + adjustX;
		mapMaxLimitY = mapPosition.y + (mapDimensions.y/2.0f) - adjustY;
		mapMinLimitY = mapPosition.y - (mapDimensions.y/2.0f) + adjustY;
	}


	private Vector3 velocity = Vector3.zero;

	void Update () {

		Vector3 goalPos = player.position;

		goalPos.z = transform.position.z;
		if (goalPos.y > mapMaxLimitY)
			goalPos.y = mapMaxLimitY;

		if (goalPos.y < mapMinLimitY)
			goalPos.y = mapMinLimitY;

		if (goalPos.x > mapMaxLimitX)
			goalPos.x = mapMaxLimitX;

		if (goalPos.x < mapMinLimitX)
			goalPos.x = mapMinLimitX;

		transform.position = Vector3.SmoothDamp (transform.position, goalPos, ref velocity, smoothTime);
	}
		
}
