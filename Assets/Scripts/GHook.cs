using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace Assets.Scripts
{
	public class GHook : MonoBehaviour
	{
		public GameObject chainLink;
		public GameObject player;
		public int chainSize;

		void Start()
		{
			for (int x = 0; x < 10; x++)
				AddChainLink ();

			player.GetComponent<DistanceJoint2D> ().connectedBody = transform.GetChild (transform.childCount - 1).GetComponent<Rigidbody2D> ();
		}

		void Update()
		{
			if (Input.GetMouseButtonDown (0)) 
			{
				Vector3 toWorldCoordinates = Camera.main.ScreenToWorldPoint (Input.mousePosition);
				toWorldCoordinates.z = 0;
				StartCoroutine(MoveToPoint(toWorldCoordinates));
			}
		}

		void AddChainLink()
		{
			if (transform.childCount <= 0) {
				var newLink = Instantiate (chainLink);
				newLink.GetComponent<DistanceJoint2D> ().connectedBody = transform.GetComponent<Rigidbody2D> ();
				newLink.transform.parent = transform;
			} else 
			{
				var newLink = Instantiate (chainLink);
				newLink.GetComponent<DistanceJoint2D> ().connectedBody = transform.GetChild(transform.childCount-1).GetComponent<Rigidbody2D> ();
				newLink.transform.parent = transform;
			}
		}

		void RemoveAllLinks()
		{
			
		}

		IEnumerator MoveToPoint(Vector3 desiredPosition)
		{
			while (transform.position != desiredPosition) 
			{
				GetComponent<Rigidbody2D>().MovePosition( Vector3.MoveTowards (transform.position, desiredPosition, Time.deltaTime * 5f));
				yield return null;
			}

		}
	}
}