using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CactIFMS
{
	[KSPAddon(KSPAddon.Startup.Flight , false)]
	public class CactIFMS : MonoBehaviour
	{
		private Rect _windowRect = new Rect(0f, 0f, 400f, 400f);

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

		void DrawWindow(int winid)
		{
			GUILayout.BeginHorizontal();
			GUILayout.Label("Since startup: " + Time.realtimeSinceStartup.ToString("F2"));
			GUILayout.EndHorizontal();

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
