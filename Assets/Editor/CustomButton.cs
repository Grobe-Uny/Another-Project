using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CustomButton 
{
     [MenuItem("GameObject/UI/Custom Button", false, 10)]
     public static void CreateCustomButton(MenuCommand menuCommand)
     {
          //if clicked on some gameobject in hierarchy, it will be parent
          GameObject parent = menuCommand.context as GameObject;
          //check if canvas exists in scene
          Canvas canvas = Object.FindObjectOfType<Canvas>();
          if (canvas == null)
          {
               //if not, create one
               GameObject canvasGO = new GameObject("Canvas");
               canvas = canvasGO.AddComponent<Canvas>();
               canvas.renderMode = RenderMode.ScreenSpaceOverlay;
               canvasGO.AddComponent<CanvasScaler>();
               canvasGO.AddComponent<GraphicRaycaster>();
               
          }
          //set parent to canvas if no gameobject selected
          Transform parentTransform = parent != null ? parent.transform : canvas.transform;
          
          //create button
          GameObject buttonGO = new GameObject("CustomButton", typeof(RectTransform));
          GameObjectUtility.SetParentAndAlign(buttonGO, parentTransform.transform.gameObject);
          Button button = buttonGO.AddComponent<Button>();
          Image image = buttonGO.AddComponent<Image>();
          image.sprite = Resources.GetBuiltinResource<Sprite>("UI/BtnDefault.png");
          image.color = Color.white;
          
          //attach custom scripts
          buttonGO.AddComponent<ButtonExpandAnim>();
          
          //set button size and position
          RectTransform rectTransform = buttonGO.GetComponent<RectTransform>();
          rectTransform.sizeDelta = new Vector2(160, 40);
          rectTransform.anchoredPosition = Vector2.zero;
          
          //add text to button
          GameObject textGO = new GameObject("Text", typeof(RectTransform));
          GameObjectUtility.SetParentAndAlign(textGO, buttonGO);
          TextMeshProUGUI text = textGO.AddComponent<TextMeshProUGUI>();
          text.text = "Custom Button";
          text.alignment = TextAlignmentOptions.Center;
          text.color = Color.black;
          text.font = Resources.GetBuiltinResource<TMP_FontAsset>("Roboto_Medium_v2.asset");
          
          //Undo functionality
          Undo.RegisterCreatedObjectUndo(buttonGO, "CreateCustomButton");
          
          //select the button in hierarchy
          Selection.activeObject = buttonGO;
     }
}
