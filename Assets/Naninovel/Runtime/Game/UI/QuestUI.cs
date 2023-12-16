using Naninovel.UI;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Naninovel.Runtime.Game.UI
{
    public class QuestUI : CustomUI
    {
        [SerializeField] private TMP_Text logText;
        private readonly List<string> questLogs = new();
        private int counter;

        public void ShowLog(string text)
        {
            counter++;
            string showText = counter + ". " + text;
            questLogs.Add(showText);
            logText.text = showText;
        }

        public void ShowAllLogs()
        {
            foreach (string log in questLogs)
            {
            }
        }
    }
}