using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountDown : Timer {

  public float timeLeft = 10.0f;
  private BlinkingText blinkingText;
  private bool isBlinking;

  protected override void Start () {
    blinkingText = GetComponent<BlinkingText>();
    isBlinking = false;

    string minutes = (((int) timeLeft) / 60).ToString();
    float secondsFloat = (timeLeft % 60);
    string seconds = secondsFloat.ToString("f2");
    timerText.text = minutes + ". " + seconds;
  }

  // Update is called once per frame
  protected override void Update () {
    if(started) {
      float t = timeLeft - (Time.time - startTimer);

      string minutes = (((int) t) / 60).ToString();
      float secondsFloat = (t % 60);
      string seconds = secondsFloat.ToString("f2");

      if(secondsFloat < 0.0f) {
        timerText.text = minutes + ":  0.00";
        GameManager.instance.GameOver("timer_over");
        StopTimer();
        return;
      }

      if(secondsFloat < 5.0f && !isBlinking) {
        isBlinking = true;
        blinkingText.StartBlinking();
      }
      timerText.text = minutes + ". " + seconds;
    }
  }

  public void AddTime(float seconds) {
    timeLeft += seconds;
    if(seconds > 0) {
      GotBonus();
    } else {
      GotMalus();
    }
  }

  private void GotMalus() {
    timerText.color = Color.red;
    Invoke("WhiteText", 2.0f);
  }

  private void GotBonus() {
    timerText.color = Color.green;
    Invoke("WhiteText", 2.0f);
  }

  private void WhiteText() {
    timerText.color = Color.white;
  }
}
