using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SwinGameSDK;

static class playerName
{
	private const int NAME_WIDTH = 10;
	private const int NAME_HORIZONTAL = 200;
	private const int NAME_VERTICAL = 200;

	public static void enterName ()
	{
		int x = 0;
		string name;
		x = NAME_HORIZONTAL + SwinGame.TextWidth (GameResources.GameFont ("Courier"), "Name: ");
		SwinGame.StartReadingText (Color.White, NAME_WIDTH, GameResources.GameFont ("Courier"), x, NAME_VERTICAL);

		//Read the text from the user
		while (SwinGame.ReadingText ()) {
			SwinGame.ProcessEvents ();
			UtilityFunctions.DrawBackground ();
			SwinGame.DrawText ("Name: ", Color.White, GameResources.GameFont ("Courier"), NAME_HORIZONTAL, NAME_VERTICAL);
			SwinGame.RefreshScreen ();
		}

		name = SwinGame.TextReadAsASCII ();

		string filename = null;
		filename = SwinGame.PathToResource ("name.txt");

		StreamWriter output = default (StreamWriter);
		output = new StreamWriter (filename);

		if (SwinGame.KeyTyped (KeyCode.vk_ESCAPE)) {
			GameController.EndCurrentState ();
		}

		if (SwinGame.KeyTyped (KeyCode.vk_RETURN)) {
			output.WriteLine (name);
			output.Close ();
			GameController.SwitchState (GameState.Deploying);
			SwinGame.RefreshScreen ();
		}
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================