using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CactIFMS
{
	public class Differ {//numerical differentiator
		public float step = 1f;
		public float ppdist = 0f;
		public float pdist = 0f;

		public Differ(float tstep){
			step = tstep;
		}

		public float update(float dist, float tstep){
			step = tstep;
			float left, right;
			left = (pdist - ppdist) / (step * 2);
			right = (dist - pdist) / (step * 2);
			ppdist = pdist; 
			pdist = dist;
			return right + left;
		}
	}

	[KSPAddon(KSPAddon.Startup.Flight , false)]
	public class CactIFMS : MonoBehaviour
	{
		private Rect _windowRect = new Rect(0f, 0f, 400f, 400f);
		private Vector3 vescom, vesfgwp, vestrans, kscpos;
		private Differ vel1, vel2, vel3;
		void Start()
		{
			// initially set window in middle of screen
			_windowRect.x = Screen.width * 0.5f - _windowRect.width * 0.5f;
			_windowRect.y = Screen.height * 0.5f - _windowRect.height * 0.5f;
			Debug.Log("CactIFMS [" + this.GetInstanceID().ToString("X")
				+ "][" + Time.time.ToString("0.0000") + "]: Start");

		}

		void OnGUI()
		{
			_windowRect =
				KSPUtil.ClampRectToScreen(GUILayout.Window(GetInstanceID(), _windowRect, DrawWindow, "IFMS"));
		}
		
		private string stringifyVec(Vector3 v){
			return string.Format("<{0:###.##} {1:###.##} {2:###.##}>", v.x, v.y, v.z);
		}

		void DrawWindow(int winid)
		{
			GUILayout.BeginVertical();
			GUILayout.Label("KSC pos: " + stringifyVec(kscpos));
			GUILayout.Label("Vessel pos (CoM): " + stringifyVec(vescom));
			GUILayout.Label("rel.ves.pos (CoM): " + stringifyVec(vescom-kscpos));
			GUILayout.Label("rel.ves.dst (CoM): " + (vescom-kscpos).magnitude.ToString("0.00"));
			GUILayout.Label("rel.ves.dst (CoM): " + vel1.update((vescom-kscpos).magnitude, TimeWarp.fixedDeltaTime).ToString("0.00"));
			GUILayout.Label("Vessel pos (GWP): " + stringifyVec(vesfgwp));
			GUILayout.Label("rel.ves.pos (GWP): " + stringifyVec(vesfgwp-kscpos));
			GUILayout.Label("rel.ves.dst (GWP): " + (vesfgwp-kscpos).magnitude.ToString("0.00"));
			GUILayout.Label("rel.ves.dst (GWP): " + vel2.update((vesfgwp-kscpos).magnitude, TimeWarp.fixedDeltaTime).ToString("0.00"));
			GUILayout.Label("Vessel pos (transform): " + stringifyVec(vestrans));
			GUILayout.Label("rel.ves.pos (transform): " + stringifyVec(vestrans-kscpos));
			GUILayout.Label("rel.ves.dst (transform): " + (vestrans-kscpos).magnitude.ToString("0.00"));
			GUILayout.Label("rel.ves.dst (transform): " + vel3.update((vestrans-kscpos).magnitude, TimeWarp.fixedDeltaTime).ToString("0.00"));

			GUILayout.EndVertical();

			GUI.DragWindow(); // make window draggable
		}



	//code from TE's sample mod
		private float lastUpdate = 0.0f;
		private float lastFixedUpdate = 0.0f;
		private float logInterval = 5.0f;
		void Awake()
		{
			Debug.Log("CactIFMS [" + this.GetInstanceID().ToString("X")
				+ "][" + Time.time.ToString("0.0000") + "]: Awake: " + this.name);
		}
		/*
* Called every frame
*/
		void Update()
		{
			if ((Time.time - lastUpdate) > logInterval)
			{
				lastUpdate = Time.time;
				Debug.Log("CactIFMS [" + this.GetInstanceID().ToString("X")
					+ "][" + Time.time.ToString("0.0000") + "]: Update");
			}
		}
		/*
* Called at a fixed time interval determined by the physics time step.
*/
		void FixedUpdate()
		{
			if ((Time.time - lastFixedUpdate) > logInterval)
			{
				lastFixedUpdate = Time.time;
				Debug.Log("CactIFMS [" + this.GetInstanceID().ToString("X")
					+ "][" + Time.time.ToString("0.0000") + "]: FixedUpdate");
			}
			vescom = FlightGlobals.ActiveVessel.findWorldCenterOfMass();
			vesfgwp = FlightGlobals.ActiveVessel.GetWorldPos3D();
			vestrans = FlightGlobals.ActiveVessel.transform.position;
			kscpos = GameObject.Find("KSC").transform.position;
			//TimeWarp.fixedDeltaTime
		}
		/*
* Called when the game is leaving the scene (or exiting). Perform any clean up work here.
*/
		void OnDestroy()
		{
			Debug.Log("CactIFMS [" + this.GetInstanceID().ToString("X")
				+ "][" + Time.time.ToString("0.0000") + "]: OnDestroy");
		}
	}
}
