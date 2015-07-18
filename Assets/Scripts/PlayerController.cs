using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerController : MonoBehaviour {

	public float Speed;
	public Text CountText;
	public Text WinText;
	public Text GoodScText;
	public Text BadScText;

	private Rigidbody RBody;
	private int Count;
	private Power[] PowersData;

	void Start ()
	{
		RBody = GetComponent<Rigidbody> ();

		// Inicializa a lista de powers
		this.PowersData = new Power[(int)Powers.MAX];

		for (int i = 0; i < (int)Powers.MAX; i++) {
			PowersData[i] = new Power();
		}

		// Limpa variaveis para iniciar
		Count = 0;
		SetCountText ();
		WinText.text = "";
	}

	/// <summary>
	/// Atualizaçoes de Fisca
	/// </summary>
	void FixedUpdate ()
	{
		// Recebe o valor dos eixos de movimento (WASD)
		float moveHorizontal = Input.GetAxis ("Horizontal");
		float moveVertical = Input.GetAxis ("Vertical");

		// Calcula o vetor para a força de movimento
		Vector3 movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

		// Aplica a força
		RBody.AddForce (movement * Speed);
	}

	/// <summary>
	/// Contato com triggers
	/// </summary>
	/// <param name="other">O outro objeto</param>
	void OnTriggerEnter (Collider other)
	{
		if (other.gameObject.CompareTag (Tags.PickUp)) { // Ativaçao para Pick Ups
			other.gameObject.SetActive (false);
			Count++;
			SetCountText ();
		} else if (other.gameObject.CompareTag (Tags.Power)) { // Ativacao para Powers
			PowerObject pObj = other.gameObject.GetComponent<PowerObject>();
			this.PowerStart(pObj.PowerData);
			other.gameObject.SetActive(false);
		}
	}

	// ============================
	// 		Sistema de Powers	
	// ============================

	/// <summary>
	/// Chamado para iniciar o efeito de um Power
	/// </summary>
	/// <param name="pObj">O poder ativado</param>
	private void PowerStart(Power pObj)
	{
		// Se o efeito ja esta em execucao, encerra
		if (this.PowersData[(int)pObj.Type].Routine != null)
			StopCoroutine(this.PowersData[(int)pObj.Type].Routine);
		
		// Atualiza a informaçao sobre o efeito
		this.PowersData [(int)pObj.Type] = pObj;
		
		// Inicia um novo timer
		this.PowersData[(int)pObj.Type].Routine =  StartCoroutine(PowerTimer(pObj.Type, pObj.Duration));
		
		// Mostra a mensagem do efeito
		if (pObj.IsGood) {
			GoodScText.text = pObj.Name;
			StartCoroutine(TimedDisplay(GoodScText.gameObject, 1f));
		} else {
			BadScText.text = pObj.Name;
			StartCoroutine(TimedDisplay(BadScText.gameObject, 1f));
		}
	}
	
	/// <summary>
	/// Coroutine para controlar o
	/// tempo de um Power
	/// </summary>
	/// <returns>The timer.</returns>
	/// <param name="powerType">Tipo do Power</param>
	/// <param name="duration">Duracao em segundos</param>
	private IEnumerator PowerTimer(Powers powerType, float duration)
	{
		yield return new WaitForSeconds (duration);
		PowerEnd (powerType);
	}

	/// <summary>
	/// Finaliza o efeito de um Power
	/// </summary>
	/// <param name="type">Type.</param>
	private void PowerEnd(Powers type)
	{
		this.PowersData [(int)type].Duration = 0;
	}

	/// <summary>
	/// Activates a GameObject for duration time
	/// and hides it again
	/// </summary>
	/// <returns>The timer.</returns>
	/// <param name="display">Display.</param>
	/// <param name="duration">Duration.</param>
	private IEnumerator TimedDisplay(GameObject display, float duration)
	{
		display.SetActive (true);
		yield return new WaitForSeconds (duration);
		display.SetActive (false);
	}

	// ============================
	// 		Atualizaçoes de UI
	// ============================
	void SetCountText()
	{
		CountText.text = "Count: " + Count.ToString();
		if (Count >= 11) {
			WinText.text = "You Win!";
		}
	}
}
