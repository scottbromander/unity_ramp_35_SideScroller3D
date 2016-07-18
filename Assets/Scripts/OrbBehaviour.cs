using UnityEngine;
using System.Collections;

public class OrbBehaviour : MonoBehaviour {

	void OnTriggerEnter(Collider other){
		Destroy (this.gameObject);
	}
}
