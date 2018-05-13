using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
   public static GameManager Instance;

   public Text ScoreValueText;
   public Button ResetButton;
   public Button QuitButton;

   private static int _score = 0;

   public void AddScore(int amount)
   {
      _score += amount;
   }

   private void Awake()
   {
      Instance = this;

      var resetEvent = new Button.ButtonClickedEvent();
      resetEvent.AddListener(() =>
      {
         SceneManager.LoadScene("main");
         Destroy(gameObject);
      });
      ResetButton.onClick = resetEvent;

      var quitEvent = new Button.ButtonClickedEvent();
      quitEvent.AddListener(() =>
      {
         Application.Quit();
      });
      QuitButton.onClick = quitEvent;
   }

   private void Start()
   {
      _score = 0;
   }

   private void Update()
   {
      ScoreValueText.text = _score.ToString();
   }
}