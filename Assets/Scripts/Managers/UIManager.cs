using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;

    [SerializeField] GameObject mainMenu;

    [SerializeField] GameObject gameMenu;

    [SerializeField] Slider healthSlider;

    [SerializeField] Text clipText;

    [SerializeField] GameObject endOfGame;

    [SerializeField] GameObject preGame;

    [SerializeField] GameObject mainButtons;

    [SerializeField] Text gameOverText;

    [SerializeField] Text winText;

    [SerializeField] GameObject level;

    [SerializeField] GameObject levelUp;

    [SerializeField] Text levelText;

    [SerializeField] Text levelUpText;

    [SerializeField] Transform panelOfAbilities;

    [SerializeField] GameObject ability_btn_1, ability_btn_2, ability_btn_3;

    [SerializeField] GameObject loadingScreen;

    AbilityManager abilityManager;

    static string levelUpString = "LEVEL UP! CHOOSE ABILITY";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        DontDestroyOnLoad(this);        
    }

    public void LoadMission(int lvl)
    {
        loadingScreen.SetActive(true);
        GameController.instance.SetLevel(lvl);
        //TurnOffAll();
        //SceneManager.LoadScene("BattleScene");
        StartCoroutine(LoadAsync());
        TurnOffAll();

    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainScene");
        TurnOffAll();
        mainMenu.SetActive(true);
        mainButtons.SetActive(true);
    }

    public void LoadPreGame()
    {
        TurnOffAll();
        mainMenu.SetActive(true);
        preGame.SetActive(true);
    }

    private void TurnOffAll()
    {
        mainMenu.SetActive(false);
        gameMenu.SetActive(false);
        endOfGame.SetActive(false);
        mainButtons.SetActive(false);
        preGame.SetActive(false);
        level.SetActive(false);
        //loadingScreen.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }

    public void HealthPlayerDisplay(float currentHealth)
    {
        healthSlider.value = currentHealth;
    }

    public void ClipDisplay(int maxAmmoCount, int currAmmoCount)
    {
        clipText.text = currAmmoCount + "/" + maxAmmoCount;
    }

    public void GameOver()
    {
        levelUp.SetActive(false);
        endOfGame.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        winText.gameObject.SetActive(false);
    }

    public void Win()
    {
        levelUp.SetActive(false);
        endOfGame.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        winText.gameObject.SetActive(true);
    }

    public void LevelUp (int lvl)
    {
        levelUp.SetActive(true);
        levelText.text = "Level: " + lvl;
        levelUpText.text = levelUpString;
        Time.timeScale = 0;
        abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        abilityManager.CreateListOfAbilitiesToChooseFrom();
        //CreateBtnsForAbilities(abilityManager.AbilitiesForLevel);
        DisplayAbilities(abilityManager.AbilitiesForLevel);
    }

    public void SelectAbility()
    {
        levelUp.SetActive(false);
    }

    public void DisplayAbilities(List<AbilityManager.AbilityPlayer> listOfAbilities)
    {
        ability_btn_1.transform.Find("Icon").GetComponent<Image>().sprite = listOfAbilities[0].ability.MyAbilityData.GetSprite;
        ability_btn_2.transform.Find("Icon").GetComponent<Image>().sprite = listOfAbilities[1].ability.MyAbilityData.GetSprite;
        ability_btn_3.transform.Find("Icon").GetComponent<Image>().sprite = listOfAbilities[2].ability.MyAbilityData.GetSprite;
    }

    //private void CreateBtnsForAbilities(List<AbilityManager.AbilityPlayer> listOfAbilities)
    //{
    //    List<GameObject> buttons = new List<GameObject>();
        

    //    for (int i = 0; i < listOfAbilities.Count; i++)
    //    {
    //        GameObject btn = Instantiate(ability_btn);

    //        btn.transform.SetParent(panelOfAbilities);
    //        btn.transform.Find("Icon").GetComponent<Image>().sprite = listOfAbilities[i].ability.MyAbilityData.GetSprite;
    //        btn.SetActive(true);
    //        buttons.Add(btn);
    //        btn.GetComponent<Button>().onClick.AddListener(delegate { OnClickAbility(listOfAbilities[i]); });
    //    }

    //    //Debug.Log("создаем кнопки");
    //    //for (int i = 0; i < listOfAbilities.Count; i++)
    //    //{
    //    //    GameObject btn = new GameObject("new btn", typeof(Image), typeof(Button), typeof(LayoutElement));
    //    //    btn.transform.SetParent(panelOfAbilities);
    //    //    btn.GetComponent<LayoutElement>().flexibleWidth = 80;
    //    //    btn.GetComponent<Image>().sprite = listOfAbilities[i].ability.MyAbilityData.GetSprite;
    //    //}
    //}

    public void OnClickAbility(int id)
    {
        abilityManager = GameObject.Find("AbilityManager").GetComponent<AbilityManager>();
        abilityManager.AbilitySelected(id);
        levelUp.SetActive(false);
        Time.timeScale = 1;
    }

    IEnumerator LoadAsync()
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync("BattleScene");
        Debug.Log("async вызвалась");

        while (!asyncLoad.isDone)
        {
            Debug.Log("async внутри while");
            yield return null;
        }
        if (asyncLoad.isDone)
        {
            loadingScreen.SetActive(false);
            gameMenu.SetActive(true);
            level.SetActive(true);
        }
 
    }
}
