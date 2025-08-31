
using System;

namespace Benetti
{
    public class MainMenuHandler
    {
        public static void ButtonClickHandler(Action onClick = null)
        {
            AudioManager.instance.PlaySound("ButtonClick");
            onClick?.Invoke();
        }
    }
}

