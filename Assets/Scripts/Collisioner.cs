using UnityEngine;
using UnityEngine.SceneManagement;


public class Collisioner : MonoBehaviour
{
    [SerializeField] float delayTime = 1f;
    [SerializeField] AudioClip levelFinish;
    [SerializeField] AudioClip crush;

    AudioSource RocketAudio;

    bool isTransitioning = false;

    private void Start()
    {
        RocketAudio = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(isTransitioning) { return; }
        switch(collision.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("nothing to do");
                break;
            case "Finish":
                StartSuccessSequence();
                break;
            case "Fuel":
                Debug.Log("add fuel");
                break;
            default:
                StartCrushSequence();
                break;
        }
    }

    void StartCrushSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        RocketAudio.PlayOneShot(crush);
        Invoke("ReloadLevel",delayTime);
    }
    void StartSuccessSequence()
    {
        isTransitioning = true;
        GetComponent<Movement>().enabled = false;
        RocketAudio.PlayOneShot(levelFinish);
        Invoke("LoadNextLevel", delayTime);
    }
    void ReloadLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentLevelIndex);
    }
    void LoadNextLevel()
    {
        int currentLevelIndex = SceneManager.GetActiveScene().buildIndex;
        int nextSceneIndex = currentLevelIndex + 1;
        if(nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            nextSceneIndex = 0;
        }       
        SceneManager.LoadScene(nextSceneIndex);
        
        
    }
}
