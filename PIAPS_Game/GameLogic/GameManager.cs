using System;
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
        #region 

        public static bool isCreatingEnemy = false;

        #endregion

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



        protected int _playerHP = 300;
        protected int _enemyHP = 300;
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
                if (value > Deck.Size.X)
                    _playerCardsReciveCount = Deck.Size.X;
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

        private Card.AbstractCard receiveCard()
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
            card.View.Scale =  new Vector2f(_deck.View.CellSize.Y / card.View.Size.Y,_deck.View.CellSize.Y / card.View.Size.Y);
            card.AddListener(Deck);
            card.AddListener(Field);
            
            var cards =  GameManager.Instance.Deck.GetCardsOnPosition(new Vector2i(-1, 0), new (Deck.Size.X, 0)).ConvertAll(x => x.MapPosition.X);
            for (int i = 0; i < Deck.Size.X; i++)
            {
                
                if (GameManager.Instance.Deck.GetCardOnPosition(new Vector2i(i, 0)) == null)
                {
                    card.MapPosition = new Vector2i(i, 0);
                    break;
                }
            }
            return card;
        }

        public void StartGame()
        {
            
            for (int i = 0; i < Deck.Size.X; i++)
            {

                Deck.Cards.Add(receiveCard());
                
            }

        }

        public void PlayerReciveCards()
        {
            for (int i = 0; i < PlayerCardsReciveCount; i++)
            {
                Deck.Cards.Add(receiveCard());
            }
        }

        public void PlayersTurn() 
        {
            List<Card.AbstractCard> actors = Field.Cards.Where(c => !c.IsEnemy).ToList();
            actors = actors.OrderBy(c => c.MapPosition.Y).ToList();
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
            List<Card.AbstractCard> actors = Field.Cards.Where(c => c.IsEnemy).ToList();
            actors = actors.OrderByDescending(c => c.MapPosition.Y).ToList();
            foreach (var actor in actors)
            {
                actor.Go();
            }
        }

        #region TestingGround

        public void PlaceCard()
        {
            for (int i = 0; i < 2; i++)
            {
                CardCreator creator = new CardCreator();
                creator.Builder = new Builder.CloseRangeBuilder();
                Card.AbstractCard card = creator.CreateCard();
                card.State = CardState.InMap;
                if (i == 3) {
                card.IsEnemy = true;
                    card.MapPosition = new Vector2i(0,1);
                }
                else
                    card.MapPosition = new Vector2i(0, 3 - i);
                Field.Cards.Add(card);
            }
            
        }

        #endregion



    }
}
