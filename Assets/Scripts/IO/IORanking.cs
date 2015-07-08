using UnityEngine;
using System.Collections;
using System.Text;
using System.IO;

public static class IORanking
{
	/// <summary>
	/// Versao do formato de arquivo
	/// -> eh usado para determinar se
	///    o arquivo deve passar por 
	///    conversoes antes de ser
	///    carregado.
	/// </summary>
	public const int FileVersion = 1;

	public enum Ranking : int
	{
		BestScore = 0,
		BestTime = 1,
		BestScoreTime = 2,
		
		Max // Deve sempre ser o ultimo
	}

	/// <summary>
	/// Loads the rankings file (Data/ranking.dat)
	/// </summary>
	public static RankingData[] Load()
	{
		RankingData[] rd = new RankingData[(int)Constants.Levels.Max];

		using (BinaryReader br = new BinaryReader(File.OpenRead("Data/ranking.dat")))
		{
			int ver = br.ReadInt32();
			if (ver != FileVersion) {
				Debug.Log("Trying to load an outdated file version.");
				return null;
			}

			// levelRank[LevelCount]
			for (int level = 0; level < (int)Constants.Levels.Max; level++)
			{
				// Score Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScore].Name[i] = Encoding.ASCII.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestScore].Score[i] = br.ReadSingle();
				}

				// Time Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestTime].Name[i] = Encoding.ASCII.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestTime].Score[i] = br.ReadSingle();
				}

				// ScoreTime Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScoreTime].Name[i] = Encoding.ASCII.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestScoreTime].Score[i] = br.ReadSingle();
				}
			}

			br.Close();
		}

		return rd;
	}

	public static void Init()
	{
		// SE o arquivo de rankings nao existe, cria um novo
		if (!File.Exists ("Data/rankings.dat")) {
			Save(null);
		}

		int ver = 0;

		using (BinaryReader br = new BinaryReader(File.OpenRead("Data/ranking.dat"))) {
			ver = br.ReadInt32();
			br.Close();
		}

		if (ver != FileVersion)
			Convert(ver);
	}

	private static void Convert(int fromVersion)
	{
		Debug.Log("Converting Ranking Data. From " + fromVersion + " to " + FileVersion);
	}

	private static void Save(RankingData[] data)
	{
		if (data == null)
		{
			using (BinaryWriter bw = new BinaryWriter(File.Create("Data/ranking.dat")))
			{
				bw.Write (FileVersion);
				for (int level = 0; level < (int)Constants.Levels.Max; level++)
				{
					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (0f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (0f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (0f);
					}
				}

				bw.Close ();
			}
		}
		else
		{
			using (BinaryWriter bw = new BinaryWriter(File.Create("Data/ranking.dat")))
			{
				bw.Write (FileVersion);
				for (int level = 0; level < (int)Constants.Levels.Max; level++)
				{
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestScore].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestScore].Score [i]);
					}
					
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestTime].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestTime].Score [i]);
					}
					
					for (int i = 0; i < RankingData.MaxEntries; i++)
					{
						string name = StringTool.Truncate (data [level].Ranks [(int)IORanking.Ranking.BestScoreTime].Name [i], 20);
						bw.Write (Encoding.ASCII.GetBytes (name));
						bw.Write (new byte[20 - name.Length]);
						bw.Write (data [level].Ranks [(int)IORanking.Ranking.BestScoreTime].Score [i]);
					}
				}
				
				bw.Close ();
			}
		}

		return;
	}
}