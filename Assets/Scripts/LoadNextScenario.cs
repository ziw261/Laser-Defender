using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadNextScenario : MonoBehaviour {

    [SerializeField] Text textComponent;
    [SerializeField] SceneLoader sceneLoader;
    [TextArea(14, 10)] [SerializeField] string[] storyText;
    [TextArea(14, 10)] [SerializeField] string[] storyText2;
   

    int i = 0;
    bool chs_Lan = false;

    public void changeText () {

        if (chs_Lan) {
            if (i < storyText.Length)
            {
                textComponent.text = storyText[i];
                i++;
            }
            else
            {
                if (SceneManager.GetActiveScene().buildIndex == 9) {
                    SceneManager.LoadScene("Stuff_B");
                }
                else if (SceneManager.GetActiveScene().buildIndex == 25) {
                    SceneManager.LoadScene(0);
                }
                else {
                    sceneLoader.LoadNextScene();
                }
            }
        } else {
            if (i < storyText2.Length)
            {
                textComponent.text = storyText2[i];
                i++;
            } else {
                if (SceneManager.GetActiveScene().buildIndex == 9) {
                    SceneManager.LoadScene("Stuff_B");
                }
                else if (SceneManager.GetActiveScene().buildIndex == 25) {
                    SceneManager.LoadScene(0);
                }
                else {
                    sceneLoader.LoadNextScene();
                }
            }
        }
    }

    public void changeLanguage() {
        if(chs_Lan) {
            chs_Lan = false;
            if (i != 0) {
                textComponent.text = storyText2[i - 1];
            }
        }
        else {
            chs_Lan = true;
            if (i != 0) {
                textComponent.text = storyText[i - 1];
            }
        }
    }

}
