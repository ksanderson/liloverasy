//Code from https://youtu.be/PO5_aqapZXY
using UnityEngine;
using System.Collections;
//using UnityEditor;

public class ThirdPersonCamera : MonoBehaviour
{
    [SerializeField]
    private float distanceAway;
    [SerializeField]
    private float distanceUp;
    [SerializeField]
    private float smooth;
    [SerializeField]
    private Transform follow;
    private Vector3 targetPosition;

	void Start ()
	{
        follow = GameObject.FindWithTag("Player").transform;
	}
	
	void Update ()
	{
		
	}
	
	void OnDrawGizmos ()
	{	

	}
	
	void LateUpdate()
	{
        //Set target position with correct offsets
        targetPosition = follow.position + follow.up * distanceUp - follow.forward * distanceAway;
        Debug.DrawRay(follow.position, follow.up * distanceUp, Color.red);
        Debug.DrawRay(follow.position, -1f * follow.forward * distanceAway, Color.blue);
        Debug.DrawLine(follow.position, targetPosition, Color.magenta);

        //Make a smooth transition between where the camera is to where it should be
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smooth);

        //Make sure the camera looks at the player!
        //transform.LookAt(follow);
    }
}
