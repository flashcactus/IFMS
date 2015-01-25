using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace CactIMFS
{
	[KSPAddon(KSPAddon.Startup.Flight , false)]
	class CactusGUIWindow : MonoBehaviour
	{
		private Rect _windowRect = new Rect(0f, 0f, 400f, 400f);

		void Start()
		{
			// initially set window in middle of screen
			_windowRect.x = Screen.width * 0.5f - _windowRect.width * 0.5f;
			_windowRect.y = Screen.height * 0.5f - _windowRect.height * 0.5f;

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
	}
}
