﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIAPS_Game.Map;
using SFML.System;

namespace PIAPS_Game.GameLogic
{


    internal class GameManager
    {
        private static GameManager _instance;
        public static GameManager Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new GameManager();
                }
                return _instance;
            }
        }

        private GameManager() { }

        const int ELITEPROC = 70;




        protected int _playerCardsReciveCount = 1;
        protected Map.DeckMap _deck = new Map.DeckMap();
        protected Map.GameMap _field = new Map.GameMap();
        protected List<Builder.Builder> _builders = new List<Builder.Builder>(); 
        protected CardCreator _creator = new CardCreator();
        
        private Random _random = new Random();

        public int PlayerCardsReciveCount
        {
            get { return _playerCardsReciveCount; }

            set
            {
                if (value > Deck.Size)
                    _playerCardsReciveCount = Deck.Size;
                else
                    _playerCardsReciveCount = value;
            }
        }

        public DeckMap Deck
        {
            get => _deck;
            protected set => _deck = value;
        }

        public GameMap Field
        {
            get => _field;
            protected set => _field = value;
        }

        private Card.AbstractCard reciveCard()
        {
            if (_builders.Count == 0)
            {
                _builders.Add(new Builder.CloseRangeBuilder());
                _builders.Add(new Builder.LongRangeBuilder());
                _builders.Add(new Builder.SplashBuilder());
            }

            Card.AbstractCard card;
            int randomBuilder = _random.Next(_builders.Count);
            bool randomEliteCard = _random.Next(100) > ELITEPROC;

            _creator.Builder = _builders[randomBuilder];
            if (randomEliteCard)
                card = _creator.CreateEliteCard();
            else
                card = _creator.CreateCard();
            Settings.Window.MouseButtonPressed += (sender, args) => card.MousePressed(args);
            Settings.Window.MouseButtonReleased += (sender, args) => card.MouseReleased(args); 
            Settings.Window.MouseMoved += (sender, args) => card.MouseMoved(args);   
            card.AddListener(Deck);
            card.AddListener(Field);
            return card;
        }

        public void StartGame()
        {
            
            for (int i = 0; i < Deck.Size; i++)
            {

                Deck.Cards.Add(reciveCard());
                
            }

        }

        public void PlayerReciveCards()
        {
            for (int i = 0; i < PlayerCardsReciveCount; i++)
            {
                Deck.Cards.Add(reciveCard());
            }
        }

        public void PlayersTurn() 
        {
            List<Card.AbstractCard> actors = Field.Cards.Where(c => !c.IsEnemy).ToList();
            actors.OrderBy(c => c.MapPosition.Y);
            foreach (var actor in actors)
            {
                actor.Go();
            }
        }

        public void EnemyReciveCards()
        {

        }
        
        public void EnemyTurn()
        {

        }

        #region TestingGround

        public void PlaceCard()
        {
            for (int i = 0; i < 3; i++)
            {
                Card.AbstractCard card = reciveCard();
                
                Field.Cards.Add(card);
            }
            
        }

        #endregion



    }
}
