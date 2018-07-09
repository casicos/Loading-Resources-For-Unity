using System.Collections.Generic;
using UnityEngine;
using BackgroundMusic = SoundManager.BackgroundMusic;

/// <summary>
/// Loader, the data resource read manager from local/remote.
/// </summary>
public class Loader : Singleton<Loader>
{
	
	#region <Consts>

	private const string AnimationIndexFilePath = "ANIMATION_CSV/AnimationList/human_00";
	private const string AnimationFileSourcePath = "ANIMATION_CSV/RawAniData/HumanType/";
	public const string BackgroundMusicSourcePath = "Sounds/Musics/";

	#endregion </Consts>
	
	#region <Unity/Callbacks>

	private void Start()
	{
		SetTrigger();
	}

	#endregion </Unity/Callbacks>

	#region <Properties>
	
	#endregion </Properties>

	#region <Methods>

	public void SetTrigger()
	{		
		Logger.Write("SetTrigger");	
		
		var backgroundMusicIdGroup = new List<string> {"All_of_us", "Night_in_Katakum"};
		var backgroundMusicBpmGroup = new List<float> {120f, 115f};

		SoundManager.GetInstance.BackgroundMusicGroup = new List<BackgroundMusic>();

		for (var i = 0; i < backgroundMusicIdGroup.Count; ++i)
		{
			Logger.Write(backgroundMusicIdGroup[i]);
			SoundManager.GetInstance.BackgroundMusicGroup.Add(
				new BackgroundMusic(backgroundMusicIdGroup[i], backgroundMusicBpmGroup[i])
					);

			Logger.Write(backgroundMusicIdGroup[i]);
		}

		Logger.Write("SetTrigger");

		SoundManager.GetInstance.SetTrigger(0);
	}
	
	#endregion </Methods>

	#region <Classes>

	#endregion </Classes>
	
}
