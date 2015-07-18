using UnityEngine;
using System.Collections;

/// <summary>
/// Contem uma lista de todos
/// os Powers Ups/Downs.
/// O ultimo elemento deve ser
/// MAX, para a criaçao de arrays
/// com tamanho fixo.
/// </summary>
public enum Powers {
	// Deve sempre ser o ultimo
	MAX
}

/// <summary>
/// Define os dados de um power
/// </summary>
[System.Serializable]
public class Power
{
	// O tipo de power
	public Powers Type;
	// O nome (para display)
	public string Name = "UNAMED";
	// Valores que auxiliares
	public float Val1 = 0f;
	public float Val2 = 0f;
	// Duraçao
	public float Duration = 0f;
	// Up (true) / Down (False)
	public bool IsGood = false;
	// Referencia a Coroutine para timer
	public Coroutine Routine = null;

	// Checa se o Power esta ativo ou nao
	public bool IsActive()
	{
		if (this.Duration > 0)
			return true;
		else
			return false;
	}
}

/// <summary>
/// Usado para armazenar informaçoes
/// do PowerUp relacionado a um objeto
/// </summary>
public class PowerObject : MonoBehaviour
{
	public Power PowerData;
}
