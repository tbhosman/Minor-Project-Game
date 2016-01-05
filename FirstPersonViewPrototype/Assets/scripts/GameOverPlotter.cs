using UnityEngine;
using System.Collections;

public class GameOverPlotter : MonoBehaviour {

	public Vector3[] GameOvers;

	// Use this for initialization
	void Start () {
		GameOvers = new Vector3[12];
	    GameOvers [0] = new Vector3 (-4.81843f, 0.98f, 77.9333f);    
	    GameOvers [1] = new Vector3 (-104.252f,0.979998f,57.9434f);    
	    GameOvers [2] = new Vector3 (-120.933f, 0.979995f, 28.8787f);    
	    GameOvers [3] = new Vector3 (-59.9078f, 0.979998f, 66.0973f);    
	    GameOvers [4] = new Vector3 (-157.579f, 0.98f, 60.4724f);    
	    GameOvers [5] = new Vector3 (-26.4675f, 12.98f, 28.3133f);    
	    GameOvers [6] = new Vector3 (-24.4751f, 0.98f, 70.267f);    
	    GameOvers [7] = new Vector3 (-98.9045f, 0.979998f, 51.6284f);    
	    GameOvers [8] = new Vector3 (-52.8633f, 12.98f, 30.0907f);    
	    GameOvers [9] = new Vector3 (-44.0032f, 0.98f, 85.4953f);
	    GameOvers [10] = new Vector3 (-62.5061f, 12.98f, 35.8828f);
	    GameOvers [11] = new Vector3 (-125.953f, 0.979996f, 37.7112f);
	}

	void OnDrawGizmos(){
		for (int i = 0; i < GameOvers.Length; i++) {
			Gizmos.DrawIcon(GameOvers[i],"GameOver.png", true);//Gizmos.DrawSphere (GameOvers [i], 2.0f);
		}
	}
}
