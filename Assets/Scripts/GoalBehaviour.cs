using UnityEngine;
using System.Collections;

public class GoalBehaviour : MonoBehaviour {

	ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = gameObject.GetComponent<ParticleSystem> ();
	}

	void OnTriggerEnter(Collider other){
		if (ps.isPlaying) {
			print ("You win!");
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
