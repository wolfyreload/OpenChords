using System;
using Gtk;

namespace OpenChords
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			Application.Init ();
			EditorForm win = new EditorForm ();
			win.Show ();
			Application.Run ();
		}
	}
}
