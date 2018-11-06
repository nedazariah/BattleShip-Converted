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
			if (GameController.CurrentState == GameState.Quitting)
				SwinGame.StopTimer (a);
			GameController.HandleUserInput();

			GameController.DrawScreen();


			if (SwinGame.KeyTyped (KeyCode.vk_q)) {
				var myBit = SwinGame.LoadBitmap ("ExitConfirmation.png");
				SwinGame.DrawBitmap ("ExitConfirmation.png", 160, 150);
				SwinGame.RefreshScreen ();
				SwinGame.Delay (5000);
			}

			if (SwinGame.KeyTyped (KeyCode.vk_y))
				GameController.AddNewState(GameState.Quitting);

			if (SwinGame.KeyTyped (KeyCode.vk_n)) {
				SwinGame.StopMusic ();
			}
			if (SwinGame.KeyTyped (KeyCode.vk_m)) {
				SwinGame.PlayMusic (GameResources.GameMusic ("Background"));
			}
			if (SwinGame.KeyTyped (KeyCode.vk_l)) {
				SwinGame.PlayMusic (GameResources.GameMusic ("Background2"));
			}
			if (SwinGame.KeyTyped (KeyCode.vk_f)) {
				var myBit = SwinGame.LoadBitmap ("bitmap.jpg");
				SwinGame.DrawBitmap ("bitmap.jpg", 20, 20);
				SwinGame.RefreshScreen ();
				SwinGame.Delay (10000);
			}


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
