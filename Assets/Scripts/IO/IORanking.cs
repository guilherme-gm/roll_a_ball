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
				rd[level] = new RankingData();

				// Score Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScore].Name[i] = StringTool.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestScore].Score[i] = br.ReadSingle();
				}

				// Time Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
					rd[level].Ranks[(int)Ranking.BestTime].Score[i] = br.ReadSingle();
				}

				// ScoreTime Entries [MaxEntries]
				for (int i = 0; i < RankingData.MaxEntries; i++)
				{
					rd[level].Ranks[(int)Ranking.BestScoreTime].Name[i] = StringTool.GetString(br.ReadBytes(20));
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

	public static void UpdateRank(Constants.Levels level, float gameTime, int score)
	{
		Debug.Log (score);
		RankingData[] rd = Load ();
		bool needSave = false;
		int curRank = RankingData.MaxEntries - 1;

		RankingData.Rank bestScore = rd[(int)level].Ranks[(int)Ranking.BestScore];
		RankingData.Rank bestTime = rd[(int)level].Ranks[(int)Ranking.BestTime];
		RankingData.Rank bestScoreTime = rd[(int)level].Ranks[(int)Ranking.BestScoreTime];

		for (int i = 0; i < RankingData.MaxEntries; i++) {
			if (bestScore.Name[i] == "asd") {
				curRank = i;
				break;
			}
		}

		for (int i = curRank - 1; i >= 0; i--)
		{
			if (score > bestScore.Score [i])
			{
				needSave = true;
				if (i == RankingData.MaxEntries - 1) {
					rd [(int)level].Ranks [(int)Ranking.BestScore].Name [i] = "asd";
					rd [(int)level].Ranks [(int)Ranking.BestScore].Score [i] = (float)score;
				} else {
					string tName = rd [(int)level].Ranks [(int)Ranking.BestScore].Name [i];
					float tScore = rd [(int)level].Ranks [(int)Ranking.BestScore].Score [i];
					rd [(int)level].Ranks [(int)Ranking.BestScore].Name [i] = "asd";
					rd [(int)level].Ranks [(int)Ranking.BestScore].Score [i] = (float)score;
					// Desce em um a colocaçao do jogador
					if (!tName.Equals("asd"))
					{
						rd [(int)level].Ranks [(int)Ranking.BestScore].Name [i + 1] = tName;
						rd [(int)level].Ranks [(int)Ranking.BestScore].Score [i + 1] = tScore;
					}
				}
			}
		}

		curRank = RankingData.MaxEntries - 1;

		for (int i = 0; i < RankingData.MaxEntries; i++) {
			if (bestScore.Name[i] == "asd") {
				curRank = i;
				break;
			}
		}

		for (int i = curRank; i >= 0; i--) {
			if (gameTime < bestTime.Score [i] || bestTime.Score [i] < 0) {
				needSave = true;
				if (i == RankingData.MaxEntries - 1) {
					rd [(int)level].Ranks [(int)Ranking.BestTime].Name [i] = "asd";
					rd [(int)level].Ranks [(int)Ranking.BestTime].Score [i] = gameTime;
				} else {
					rd [(int)level].Ranks [(int)Ranking.BestTime].Name [i] = "asd";
					rd [(int)level].Ranks [(int)Ranking.BestTime].Score [i] = gameTime;
					// Desce em um a colocaçao do jogador
					rd [(int)level].Ranks [(int)Ranking.BestTime].Name [i + 1] = bestTime.Name [i];
					rd [(int)level].Ranks [(int)Ranking.BestTime].Score [i + 1] = bestTime.Score [i];
				}
			}
		}

		curRank = RankingData.MaxEntries - 1;

		for (int i = 0; i < RankingData.MaxEntries; i++) {
			if (bestScore.Name[i] == "asd") {
				curRank = i;
				break;
			}
		}
		
		for (int i = curRank; i >= 0; i--) {
			if ((score/gameTime) > bestScoreTime.Score[i]) {
				needSave = true;
				if (i == RankingData.MaxEntries -1) {
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Name[i] = "asd";
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Score[i] = score/gameTime;
				} else {
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Name[i] = "asd";
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Score[i] = score/gameTime;
					// Desce em um a colocaçao do jogador
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Name[i+1] = bestScoreTime.Name[i];
					rd[(int)level].Ranks[(int)Ranking.BestScoreTime].Score[i+1] = bestScoreTime.Score[i];
				}
			}
		}

		Debug.Log (needSave);
		if (needSave)
			Save (rd);
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
						bw.Write (-1f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (-1f);
					}

					for (int i = 0; i < RankingData.MaxEntries; i++) {
						bw.Write (new byte[20]);
						bw.Write (-1f);
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
						Debug.Log(data [level].Ranks [(int)IORanking.Ranking.BestScore].Score [i]);
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