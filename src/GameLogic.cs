using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using SwinGameSDK;
using System.Diagnostics;

static class GameLogic
{
	public static void Main()
	{
		//Opens a new Graphics Window
		SwinGame.OpenGraphicsWindow("Battle Ships", 800, 600);

		//Load Resources
		GameResources.LoadResources();

		SwinGame.PlayMusic(GameResources.GameMusic("Background"));
		Timer a = SwinGame.CreateTimer ();
		SwinGame.StartTimer (a);
		//Game Loop
		do {
			uint time = SwinGame.TimerTicks (a) / 1000;
			GameController.time = time;
			GameController.HandleUserInput(time);

			GameController.DrawScreen();
			if (GameController.CurrentState == GameState.Quitting)
				SwinGame.StopTimer (a);
		} while (!(SwinGame.WindowCloseRequested() == true | GameController.CurrentState == GameState.Quitting));

		SwinGame.StopMusic();

		//Free Resources and Close Audio, to end the program.
		GameResources.FreeResources();
	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
