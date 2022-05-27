using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                if (value > _deck.Size)
                    _playerCardsReciveCount = _deck.Size;
                else
                    _playerCardsReciveCount = value;
            }
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

            card.AddListener(_deck);
            card.AddListener(_field);
            return card;
        }

        public void StartGame()
        {
            
            for (int i = 0; i < _deck.Size; i++)
            {

                _deck.Cards.Add(reciveCard());
                
            }

        }

        public void PlayerReciveCards()
        {
            for (int i = 0; i < PlayerCardsReciveCount; i++)
            {
                _deck.Cards.Add(reciveCard());
            }
        }

        public void PlayersTurn() 
        {
            List<Card.AbstractCard> actors = _field.Cards.Where(c => !c.IsEnemy).ToList();
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
                
                _field.Cards.Add(card);
            }
            
        }

        #endregion



    }
}
