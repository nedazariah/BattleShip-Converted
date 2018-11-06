using Microsoft.VisualBasic;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using SwinGameSDK;
using System.Text;

/// <summary>
/// Controls displaying and collecting high score data.
/// </summary>
/// <remarks>
/// Data is saved to a file.
/// </remarks>
static class HighScoreController
{
	private const int NAME_WIDTH = 3;

	private const int SCORES_LEFT = 490;
	/// <summary>
	/// The score structure is used to keep the name and
	/// score of the top players together.
	/// </summary>
	private struct Score : IComparable
	{
		public string Name;

		public int Value;

		public uint Time;
		/// <summary>
		/// Allows scores to be compared to facilitate sorting
		/// </summary>
		/// <param name="obj">the object to compare to</param>
		/// <returns>a value that indicates the sort order</returns>
		public int CompareTo(object obj)
		{
			if (obj is Score) {
				Score other = (Score)obj;

				return other.Value - this.Value;
			} else {
				return 0;
			}
		}
	}


	private static List<Score> _Scores = new List<Score>();
	/// <summary>
	/// Loads the scores from the highscores text file.
	/// </summary>
	/// <remarks>
	/// The format is
	/// # of scores
	/// NNNSSS
	/// 
	/// Where NNN is the name and SSS is the score
	/// </remarks>
	private static void LoadScores()
	{
		string filename = null;
		filename = SwinGame.PathToResource("highscores.txt");

		StreamReader input = default(StreamReader);

		StreamReader input2 = default (StreamReader);	
		input = new StreamReader(filename);
		string filename2 = SwinGame.PathToResource ("time.txt");
		input2 = new StreamReader (filename2);
	
		//Read in the # of scores
		int numScores = 0;
		numScores = Convert.ToInt32(input.ReadLine());

		_Scores.Clear();

		int i = 0;

		for (i = 1; i <= numScores; i++) {
			Score s = default(Score);
			string line = null;
			string line2 = null;
			line = input.ReadLine();
			line2 = input2.ReadLine ();
			s.Name = line.Substring(0, NAME_WIDTH);
			s.Value = Convert.ToInt32(line.Substring(NAME_WIDTH));
			s.Time = Convert.ToUInt32 (line2);
			_Scores.Add(s);
		}
		input.Close();
	}

	/// <summary>
	/// Saves the scores back to the highscores text file.
	/// </summary>
	/// <remarks>
	/// The format is
	/// # of scores
	/// NNNSSS
	/// 
	/// Where NNN is the name and SSS is the score
	/// </remarks>
	private static void SaveScores()
	{
		string filename = null;
		filename = SwinGame.PathToResource("highscores.txt");

		StreamWriter output = default(StreamWriter);
		output = new StreamWriter(filename);

		output.WriteLine(_Scores.Count);

		foreach (Score s in _Scores) {
			output.WriteLine(s.Name + s.Value);
		}

		output.Close();

	}
	public static void SaveTime ()
	{
		string filename = null;
		filename = SwinGame.PathToResource ("time.txt");
		StreamWriter output = default (StreamWriter);
		output = new StreamWriter (filename);

		output.WriteLine(_Scores.Count);
		foreach (Score s in _Scores) {
			output.WriteLine (s.Time);
		}

		output.Close ();	}
	/// <summary>
	/// Draws the high scores to the screen.
	/// </summary>
	public static void DrawHighScores()
	{
		const int SCORES_HEADING = 40;
		const int SCORES_TOP = 80;
		const int SCORE_GAP = 30;

		SwinGame.DrawLine (Color.White, 480, 65, 659, 65);
		SwinGame.DrawLine (Color.White, 480, 100, 659, 100);
		SwinGame.DrawLine (Color.White, 480, 130, 659, 130);
		SwinGame.DrawLine (Color.White, 480, 160, 659, 160);
		SwinGame.DrawLine (Color.White, 480, 190, 659, 190);
		SwinGame.DrawLine (Color.White, 480, 220, 659, 220);
		SwinGame.DrawLine (Color.White, 480, 250, 659, 250);
		SwinGame.DrawLine (Color.White, 480, 280, 659, 280);
		SwinGame.DrawLine (Color.White, 480, 310, 659, 310);
		SwinGame.DrawLine (Color.White, 480, 340, 659, 340);

		SwinGame.DrawRectangle(Color.White, 480, 35, 180, 335);

		if (_Scores.Count == 0)
			LoadScores();

		SwinGame.DrawText("   High Scores   ", Color.BlueViolet, GameResources.GameFont("Courier"), 515, SCORES_HEADING);

		//For all of the scores
		int i = 0;
		for (i = 0; i <= _Scores.Count - 1; i++) {
			Score s = default(Score);

			s = _Scores[i];
			TimeSpan time = TimeSpan.FromSeconds (s.Time);
			//for scores 1 - 9 use 01 - 09
			if (i < 9) {
				SwinGame.DrawText (" " + (i + 1) + ":   " + s.Name, Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
				SwinGame.DrawText (s.Value.ToString(), Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT+80, SCORES_TOP + i * SCORE_GAP);
				SwinGame.DrawText (time.ToString(@"mm\:ss"), Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT+130, SCORES_TOP + i* SCORE_GAP);
			} else {
				SwinGame.DrawText (" " + (i + 1) + ":   " + s.Name, Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT, SCORES_TOP + i * SCORE_GAP);
				SwinGame.DrawText (s.Value.ToString(), Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT+80, SCORES_TOP + i* SCORE_GAP);
				SwinGame.DrawText (time.ToString(@"mm\:ss"), Color.SkyBlue, GameResources.GameFont ("Courier"), SCORES_LEFT+130, SCORES_TOP + i* SCORE_GAP);
			}
		}
	}

	/// <summary>
	/// Handles the user input during the top score screen.
	/// </summary>
	/// <remarks></remarks>
	public static void HandleHighScoreInput()
	{
		if (SwinGame.MouseClicked(MouseButton.LeftButton) || SwinGame.KeyTyped(KeyCode.vk_F1) || SwinGame.KeyTyped(KeyCode.vk_ESCAPE) || SwinGame.KeyTyped(KeyCode.vk_RETURN)) {
			GameController.EndCurrentState();
		}
	}

	/// <summary>
	/// Read the user's name for their highsSwinGame.
	/// </summary>
	/// <param name="value">the player's sSwinGame.</param>
	/// <remarks>
	/// This verifies if the score is a highsSwinGame.
	/// </remarks>
	public static void ReadHighScore (int value, uint time)
	{
		//const int ENTRY_TOP = 500;

		if (_Scores.Count == 0)
			LoadScores ();

		//is it a high score
		if (value > _Scores [_Scores.Count - 1].Value) {
			GameController.SwitchState (GameState.ViewingHighScores);
			Score s = new Score ();
			s.Value = value;
			if (playerName.getName ().Length == 3) {
				s.Name = playerName.getName ();
			} else if (playerName.getName ().Length == 2) {
				s.Name = playerName.getName () + " ";
			} else if (playerName.getName ().Length == 1) {
				s.Name = playerName.getName () + "  ";
			}
			s.Time = time;
			_Scores.RemoveAt (_Scores.Count - 1);
			_Scores.Add (s);
			_Scores.Sort ();
			SaveScores ();
            
		} else {
			GameController.EndCurrentState ();
		}	}
}

//=======================================================
//Service provided by Telerik (www.telerik.com)
//Conversion powered by NRefactory.
//Twitter: @telerik
//Facebook: facebook.com/telerik
//=======================================================
