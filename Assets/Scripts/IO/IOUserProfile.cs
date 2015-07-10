using UnityEngine;
using System.IO;
using System.Text;
using System.Collections;

public class UserProfile
{
	public string Name { get; set; }
	public int Points { get; set; }
	public byte[] LevelStatus { get; set; }

	public UserProfile()
	{
		this.LevelStatus = new byte[(int)Constants.Levels.Max];
		this.Points = 0;
	}
}

public static class IOUserProfile
{
	public static string LoadedProfile = "";
	public const int FileVersion = 1;

	public static void Init()
	{
		if (!Directory.Exists("Data/"))
		    Directory.CreateDirectory("Data/");

		if (!File.Exists ("Data/roll-a-ball.dat")) {
			File.WriteAllText("Data/roll-a-ball.dat", "");
		}

		string lastProf = File.ReadAllText ("Data/roll-a-ball.dat");
		if (lastProf == "" || !File.Exists("Data/"+lastProf))
			return;

		LoadedProfile = lastProf;
	}

	public static string[] GetProfiles()
	{
		string[] profiles = Directory.GetFiles ("Data/", "userprof-*", SearchOption.TopDirectoryOnly);

		return profiles;
	}

	public static UserProfile LoadProfile(string dir)
	{
		if (!File.Exists (dir))
			return null;

		UserProfile prof = new UserProfile ();
		using(BinaryReader br = new BinaryReader(File.OpenRead(dir)))
		{
			prof.Name = StringTool.GetString(br.ReadBytes(20));
			prof.Points = br.ReadInt32();

			for (int i = 0; i < (int)Constants.Levels.Max; i++) {
				prof.LevelStatus[i] = br.ReadByte();
			}
		}

		return prof;
	}

	public static void SaveProfile(UserProfile profile)
	{
		using(BinaryWriter bw = new BinaryWriter(File.Create("Data/userprof-"+profile.Name+".dat")))
		{
			string name = StringTool.Truncate(profile.Name, 20);
			bw.Write(name);
			bw.Write(new byte[20 - name.Length]);
			bw.Write(profile.Points);

			for (int i = 0; i < (int)Constants.Levels.Max; i++)
			{
				bw.Write(profile.LevelStatus[i]);
			}
		}
	}
}