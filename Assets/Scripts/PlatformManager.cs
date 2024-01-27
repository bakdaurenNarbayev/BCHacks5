using UnityEngine;
using System.Collections.Generic;
using static GameManager;

public class PlatformManager : SingletonMonoBehaviour<PlatformManager>
{
    [SerializeField] private GameObject platformPrefab;
    [SerializeField] private int platformQuantity;
    public List<GameObject> comets, nonComets, platforms;
    private Vector3 spawnPosition, position;
    private StateType state;
    private float baseHeightMultiplier;

    private void Start()
    {
        baseHeightMultiplier = Random.value;
        spawnPosition = new Vector3(0, 0, 0);
    }
    public void GeneratePlatforms()
    {
        for (int i = 0; i < platformQuantity; i++)
        {
            spawnPosition.z += Random.Range(7, 9);
            spawnPosition = new Vector3(0, 0, spawnPosition.z);
            GameObject platform = Instantiate(platformPrefab, spawnPosition, Quaternion.identity, transform);
        }
    }

    public void HidePlatforms()
    {
        comets = new List<GameObject>(GameObject.FindGameObjectsWithTag("Comet"));
        nonComets = new List<GameObject>(GameObject.FindGameObjectsWithTag("NonComet"));

        foreach (GameObject comet in comets)
        {
            comet.gameObject.SetActive(false);
        }

        foreach (GameObject nonComet in nonComets)
        {
            nonComet.gameObject.SetActive(false);
        }
    }

    public void IdentifyTechnique()
    {
        state = GameManager.Instance.state;

        if (state == StateType.COMET_VISIBLE || state == StateType.COMET_INVISIBLE)
        {
            foreach (GameObject nonComet in nonComets)
            {
                nonComet.gameObject.SetActive(false);
            }
            platforms = comets;
        }
        else
        {
            foreach (GameObject comet in comets)
            {
                comet.gameObject.SetActive(false);
            }
            platforms = nonComets;
        }

        if (state == StateType.BUBBLE_INVISIBLE)
        {
            foreach (GameObject platform in platforms)
            {
                platform.transform.Find("Collider").gameObject.transform.GetComponent<SphereCollider>().radius = 14;
            }
        }
        else if (state == StateType.BUBBLE_VISIBLE)
        {
            foreach (GameObject platform in platforms)
            {
                platform.transform.Find("Collider").gameObject.transform.GetComponent<SphereCollider>().radius = 14;
                platform.transform.Find("Bubble").gameObject.transform.localScale = new Vector3(28, 28, 28);
            }
        }
    }

    public void UpdatePlatforms()
    {
        foreach (GameObject platform in platforms)
        {
            position = platform.transform.position;
            position.y = DatabaseManager.Instance.minPlatformHeight + baseHeightMultiplier * (DatabaseManager.Instance.maxPlatformHeight - DatabaseManager.Instance.minPlatformHeight);
            platform.transform.position = position;
        }
    }
}