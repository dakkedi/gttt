using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class GameManager : MonoBehaviour
{
	public static GameManager Instance = null;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		Assert.IsNotNull(_player);
	}

	[SerializeField] private GameObject _player = null;
	[SerializeField] private float _roundStartTime = 12f;
	[SerializeField] private float _roundTimer = 0;
	[SerializeField] private int _score = 0;

	public float RoundTimer
	{
		get { return _roundTimer; }
	}

	private void Start()
	{
		_roundTimer = _roundStartTime;

		TreasureManager.Instance.PopulateTreasurePool();
		TreasureManager.Instance.SpawnTreasure();
	}

	private void Update()
	{
		GameTimer();
	}

	public void UpdateScore()
	{
		_score = TreasureManager.Instance.TreasureCount;
	}

	public void AddToRoundTimer(int timeBonus)
	{
		_roundTimer += timeBonus;
	}

	private void GameTimer()
	{
		if (_roundTimer > 0)
		{
			_roundTimer -= Time.deltaTime;
		}
		else
		{
			Debug.Log("Timer ran out!");
		}
	}
}
