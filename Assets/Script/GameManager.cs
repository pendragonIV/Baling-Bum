using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }
    }

    public SceneChanger sceneChanger;
    public GameScene gameScene;
    public GameObject pedestalContainer;
    public GameObject player;
    public GameObject circleBorder;
    public GameObject destination;

    #region Game status
    [SerializeField]
    private Level currentLevelData;
    [SerializeField]
    private bool isGameWin = false;
    [SerializeField]
    private bool isLose = false;
    [SerializeField]
    private bool isInteractable = true;
    [SerializeField]
    public int achivement = 0;
    [SerializeField]
    private int bouncTime = 0;
    private const int MAX_ACHIVE = 3;
    #endregion

    private void Start()
    {
        currentLevelData = LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex);
        currentLevelData.pedestals.ForEach(obj =>
        {
            SetUpNewObj(obj);
        });

        player.transform.localPosition = currentLevelData.playerSpawnPosition;
        circleBorder.transform.localPosition = new Vector2(player.transform.localPosition.x, player.transform.localPosition.y );
        destination.transform.localPosition = currentLevelData.destinationPosition;

        Time.timeScale = 1;
    }

    private void SetUpNewObj(IngamePedestal obj)
    {
        GameObject pedestalObject = Instantiate(obj.pedestal);
        pedestalObject.transform.SetParent(pedestalContainer.transform);
        pedestalObject.transform.localScale = obj.scale;
        pedestalObject.transform.rotation = Quaternion.Euler(obj.rotation);
        pedestalObject.transform.GetChild(0).GetChild(0).GetComponent<Pedestal>().typeOfPedestal = obj.typeOfPedestal;
        pedestalObject.transform.localPosition = obj.position;
    }

    public void Win()
    {
        if (LevelManager.instance.levelData.GetLevels().Count > LevelManager.instance.currentLevelIndex + 1)
        {
            if (LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex + 1).isPlayable == false)
            {
                LevelManager.instance.levelData.SetLevelData(LevelManager.instance.currentLevelIndex + 1, true, false, 0);
            }
        }
        SetAchivement();
        if (achivement > LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex).achivement)
        {
            LevelManager.instance.levelData.SetLevelData(LevelManager.instance.currentLevelIndex, true, true, achivement);
        }
        else
        {
            LevelManager.instance.levelData.SetLevelData(LevelManager.instance.currentLevelIndex, true, true, LevelManager.instance.levelData.GetLevelAt(LevelManager.instance.currentLevelIndex).achivement);
        }
        isGameWin = true;

        StartCoroutine(SetupWin());
    }

    private IEnumerator SetupWin()
    {
        yield return new WaitForSecondsRealtime(.5f);
        gameScene.ShowWinPanel();
        Time.timeScale = 0;
        LevelManager.instance.levelData.SaveDataJSON();
    }

    public void Bounc()
    {
        bouncTime ++;
    }

    private void SetAchivement()
    {
        if(bouncTime <= currentLevelData.pedestals.Count + 1)
        {
            achivement = MAX_ACHIVE;
        }
        else
        {
            achivement = MAX_ACHIVE - (bouncTime - (currentLevelData.pedestals.Count + 1));
            if(achivement < 0)
            {   
                achivement = 0;
            }
        }
    }

    public void Lose()
    {
        isLose = true;
        gameScene.ShowLosePanel();
        Time.timeScale = 0;
    }

    public bool IsGameWin()
    {
        return isGameWin;
    }

    public bool isGameLose()
    {
        return isLose;
    }

    public void NonInteractable()
    {
        isInteractable = false;
    }

    public bool IsInteractable()
    {
        return isInteractable;
    }
}

