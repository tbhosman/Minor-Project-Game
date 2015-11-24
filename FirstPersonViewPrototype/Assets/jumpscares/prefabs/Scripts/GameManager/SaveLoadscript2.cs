using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using System.Collections.Generic;

public class SaveLoadscript2 : MonoBehaviour {
	
	public Transform Parent;
	public GameObject[] allObjects;
	public int AOGameObjects;
	
	
	void Start () {
		//initialize allobjectsarray
		allObjects = (GameObject[])Resources.FindObjectsOfTypeAll(typeof(GameObject)) ;
		AOGameObjects = allObjects.Length;
	}
	
	public void Save() {
		
		BinaryFormatter binary = new BinaryFormatter ();
		FileStream fStream = File.Create(Application.persistentDataPath + "/savefile.sav");
		
		
		SaveManager saver = new SaveManager ();
		
		//initialize arrays
		saver.xcoordinates = new float[AOGameObjects];
		saver.ycoordinates = new float[AOGameObjects];
		saver.zcoordinates = new float[AOGameObjects];
		saver.rotationx = new float[AOGameObjects];
		saver.rotationy = new float[AOGameObjects];
		saver.rotationz = new float[AOGameObjects];
		saver.rotationw = new float[AOGameObjects];
		saver.active = new bool[AOGameObjects];
		
		int i = 0;
		//Stuff to save!!!
		foreach (Transform child in Parent) {
			saver.xcoordinates [i] = child.position.x;
			saver.ycoordinates [i] = child.transform.position.y;
			saver.zcoordinates [i] = child.transform.position.z;
			saver.rotationx [i] = child.forward.x;
			saver.rotationy [i] = child.forward.y;
			saver.rotationz [i] = child.forward.z;
			saver.rotationw [i] = child.localRotation.w;;
			saver.active[i] = child.gameObject.activeInHierarchy;
			i++;
		}
		i = 0;
		
		binary.Serialize (fStream, saver);
		fStream.Close ();
		print ("game saved2");
	}
	
	public void Load()	{
		if (File.Exists (Application.persistentDataPath + "/savefile.sav")) {
			BinaryFormatter binary = new BinaryFormatter ();
			FileStream fStream = File.Open (Application.persistentDataPath + "/savefile.sav", FileMode.Open);
			SaveManager saver = (SaveManager)binary.Deserialize (fStream);
			fStream.Close ();
			int i = 0;
			//Stuff to load!!!
			foreach (Transform child in Parent) {
				child.position = new Vector3 (saver.xcoordinates [i], saver.ycoordinates [i], saver.zcoordinates [i]);
				child.gameObject.SetActive(saver.active[i]);
				i++;
			}
			i = 0;
			print ("game loaded2");
		} else {
			print ("No SaveFile Exists yet");
		}
	}
	
	public void RemoveSaveFile(){
		File.Delete (Application.persistentDataPath + "/savefile.sav");
		Application.LoadLevel(0);
		print ("savefile removed");
		
	}
	
	
	[Serializable]
	class SaveManager
	{
		public float[] xcoordinates;
		public float[] ycoordinates;
		public float[] zcoordinates;
		public float[] rotationx;
		public float[] rotationy;
		public float[] rotationz;
		public float[] rotationw;
		public bool[] active;
	}
}

