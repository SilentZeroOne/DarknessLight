using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class CharacterCreation : MonoBehaviour
{
    public GameObject[] characterPrefabs;
    public InputField inputField;
    private GameObject[] characterGameObjects;
    public static int selectIndex=0;
    // Start is called before the first frame update
    void Start()
    {
        Init();
    }

    // Update is called once per frame
    void Update()
    {
 
    }

    private void Init()
    {
        characterGameObjects = new GameObject[characterPrefabs.Length];
        for (int i = 0; i < characterPrefabs.Length; i++)
        {
            characterGameObjects[i] = Instantiate(characterPrefabs[i], transform.position, transform.rotation);
        }
        UpdateCharacterDisplay();
    }

    void UpdateCharacterDisplay()
    {
        if (selectIndex < 0) selectIndex = 1;
        selectIndex = selectIndex % characterGameObjects.Length;
        characterGameObjects[selectIndex].SetActive(true);
        for (int i = 0; i < characterGameObjects.Length; i++)
        {
            if (i != selectIndex)
            {
                characterGameObjects[i].SetActive(false);
            }
        }
    }
    public void NextCharacter()
    {
        selectIndex++;
        UpdateCharacterDisplay();
    }
    public void PreCharacter()
    {
        selectIndex--;
        UpdateCharacterDisplay();
    }
    public void OnOkBtnCilck()
    {
        if (string.IsNullOrEmpty(inputField.text))
        {
            ErrorPanelControl.instance.ShowPanel();
            return;
        }
        Debug.Log(inputField.text);
        PlayerPrefs.SetInt("selectedCharacterIndex", selectIndex);
        PlayerPrefs.SetString("characterName", inputField.text);
        NextScene();
    }

    private void NextScene()
    {
        Invoke("LoadNextScene", 1f);
    }
    private void LoadNextScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
