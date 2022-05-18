using System;
using IKIMONO.Pet;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace IKIMONO.UI
{
    public class Tutorial : MonoBehaviour
    {
        private int _currStep = -1;
        [SerializeField] private Text _text;
        public static bool IsTutorial { get; private set; } = false;

        public static string[] TutorialText => new []
        {
            "Welcome to IKIMONO! Let's get you started with a reminder of what an Ikimono is.", 
            "The Ikimono is a small magical creature that lives in a world parallel to ours and can only be seen if you believe in their existence.",
            "The truth is, they have been here all along, but you have probably simply forgotten about them as you grew up.", 
            "Wait, you are able to see them? That's incredible! We have been searching for someone able to see them for years.", 
            $"This Ikimono is named {Player.Instance.Pet.Name} and we need someone able to take care of them.", 
            "Caring for an Ikimono is a lot like caring for a cat. You can feed it, play with it, and even take it on adventures.",
            "Are you up for the challenge? Let's go!",
            $"First of all, every living creature needs to be fed and an Ikimono is no exception. You see the drumstick? That's where you see {Player.Instance.Pet.Name} hunger.", 
            "You can feed them by clicking on the drumstick and then dragging the food onto them.", 
            $"It seems like talking about food is making {Player.Instance.Pet.Name} hungry. We should probably wrap up this tutorial and get to work.",
            $"You can also take {Player.Instance.Pet.Name} on adventures. You can find adventures by clicking on the dice icon.",
            "Oh I almost forgot, as a new owner you should name your pet!", 
            "Let's get that done, and then we can get to work!", 
        };
        
        private void Awake()
        {
            if (Player.Instance.TutorialStep == TutorialStep.End)    
                Close();

            // ReSharper disable once SwitchStatementMissingSomeEnumCasesNoDefault
            switch (Player.Instance.TutorialStep)
            {
                case TutorialStep.None:
                case TutorialStep.Intro:
                case TutorialStep.Feed:
                    Player.Instance.TutorialStep = TutorialStep.Intro;
                    NextStep();
                    IsTutorial = true;
                    break;
            }
            
            transform.localScale = Vector3.one;
        }

        private void Update()
        {
            if (Player.Instance.TutorialStep == TutorialStep.Feed && Player.Instance.Pet.Hunger.Value > 20)
                Player.Instance.Pet.Hunger.Decrease(Time.deltaTime * 3);
        }

        public void Close()
        {
            IsTutorial = false;
            Player.Instance.TutorialStep = TutorialStep.End;
            Player.Instance.Save();
            Destroy(gameObject);
        }

        /// <summary>
        /// Increases the tutorial step and runs the next step.
        /// </summary>
        public void NextStep()
        {
            if(TutorialText.Length > _currStep + 1)
                _text.text = TutorialText[++_currStep];

            Player.Instance.TutorialStep = _currStep switch
            {
                1 => TutorialStep.Intro,
                6 => TutorialStep.Feed,
                13 => TutorialStep.End,
                _ => Player.Instance.TutorialStep,
            };

            if (_currStep == 12)
            {
                Player.Instance.Inventory.AddItem(new Item("Cup of Coffee"),3);
                Player.Instance.Inventory.AddItem(new Item("Cherry Pie"),2);
                Player.Instance.Inventory.AddItem(new Item("Candy"),4);
                SetNamePanel.Open();
                Close();
            }
        }
    }

    public enum TutorialStep
    {
        None,
        Intro,
        Feed,
        Play,
        End,
    }
}