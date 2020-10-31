using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Assertions;

public class TreasureManager : MonoBehaviour
{
	public static TreasureManager Instance = null;
	private void Awake()
	{
		if (Instance == null)
			Instance = this;
		else if (Instance != this)
			Destroy(gameObject);

		Assert.IsNotNull(_treasure);
	}

	[SerializeField] private GameObject _treasure = null;
	[SerializeField] private int _treasureTimeBonus = 5;
	[SerializeField] private int _treasureCount = 0;
	[SerializeField] private List<GameObject> _treasurePool = new List<GameObject>();
	[SerializeField] private Vector2[] _testPosForTreasure = { new Vector2(1f, 2f), new Vector2(1f, 2f) };
	private int _testPosForTreasureIndex = 0;

	public void PlayerCollidedWithTreasure(GameObject treasure)
	{
		TreasureCollected(treasure);
		SpawnTreasure();
	}

	public void TreasureCollected(GameObject treasure)
	{
		treasure.SetActive(false);
		IncreaseTreasureCount();
		GameManager.Instance.AddToRoundTimer(_treasureTimeBonus);
	}

	public int TreasureCount
	{
		get { return _treasureCount; }
	}

	private void IncreaseTreasureCount()
	{
		_treasureCount++;
		GameManager.Instance.UpdateScore();
	}

	public void PopulateTreasurePool()
	{
		for (int i = 0; i < 2; i++)
		{
			AddTreasureToPool();
		}
	}

	public void SpawnTreasure()
	{
		GameObject newTreasure = GetPooledTreasure();
		Vector2 position = _testPosForTreasure[_testPosForTreasureIndex];
		// TODO: automated position for treasure spawn
		_testPosForTreasureIndex = _testPosForTreasureIndex == 0 ? 1 : 0;
		newTreasure.transform.position = position;
		newTreasure.SetActive(true);
	}

	private GameObject GetPooledTreasure()
	{
		// Get free treasure from the pool
		for (int i = 0; i < _treasurePool.Count; i++)
		{
			if (!_treasurePool[i].activeInHierarchy)
			{
				return _treasurePool[i];
			}
		}
		// If no item from pool is ready, instantiate new treasure and add it to the pool
		GameObject treasure = AddTreasureToPool();
		return treasure;
	}

	private GameObject AddTreasureToPool()
	{
		GameObject treasure = Instantiate(_treasure);
		treasure.SetActive(false);
		_treasurePool.Add(treasure);
		return treasure;
	}
}
