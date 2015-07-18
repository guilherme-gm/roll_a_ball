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
public class Power
{
	// O tipo de power
	public Powers Type { get; set; }
	// O nome (para display)
	public string Name { get; set; }
	// Valores que auxiliares
	public int Val1 { get; set; }
	public int Val2 { get; set; }
	// Duraçao
	public float Duration { get; set; }
	// Up (true) / Down (False)
	public bool IsGood { get; set; }
	// Referencia a Coroutine para timer
	public Coroutine Routine { get; set; }

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
	public Power PowerData { get; set; }
}
