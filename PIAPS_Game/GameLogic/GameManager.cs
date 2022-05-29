using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PIAPS_Game.Map;
using PIAPS_Game.View;
using SFML.Graphics;
using SFML.System;

namespace PIAPS_Game.GameLogic
{
    

    internal class GameManager
    {
        #region . 

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


        public GameStatus GameState = GameStatus.Win;
        public EndGamePopup EndGamePopup = new EndGamePopup();
        protected int _playerHP = 300;
        protected int _enemyHP = 300;
        protected int _castleDamage = 20;
        protected int _playerCardsReciveCount = 1;
        protected Map.DeckMap _deck = new Map.DeckMap();
        protected Map.GameMap _field = new Map.GameMap();
        protected List<Builder.Builder> _builders = new List<Builder.Builder>(); 
        protected CardCreator _creator = new CardCreator();

        public CardView EnemyCastle;
        public Text PlayerCastle;
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


        public int PlayerHP { get { return _playerHP; } set 
            {
                _playerHP = value;
                PlayerCastle.DisplayedString = value.ToString();
                if (_playerHP <= 0)
                {
                    GameState = GameStatus.Lose;
                }
            }
        }
        public int EnemyHP { get { return _enemyHP; } set 
            {
                _enemyHP = value;
                EnemyCastle.HpText.DisplayedString = value.ToString();
                if (_enemyHP <= 0)
                {
                    GameState = GameStatus.Win;
                }
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
            Settings.Window.MouseButtonPressed += (sender, args) => card.MousePressed(args);
            Settings.Window.MouseButtonReleased += (sender, args) => card.MouseReleased(args);
            Settings.Window.MouseMoved += (sender, args) => card.MouseMoved(args);
            if(!isCreatingEnemy)
                card.View.Scale = new Vector2f(_deck.View.CellSize.Y / card.View.Size.Y,
                    _deck.View.CellSize.Y / card.View.Size.Y);
            card.AddListener(Deck);
            card.AddListener(Field);

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

        public void ShowWinLose(GameStatus status)
        {
            
        }

        public void StartGame()
        {
            GameState = GameStatus.Play;
            _playerHP = 300;
            _enemyHP = 300;
            Deck = new DeckMap();
            Field = new GameMap();
            PlayerCastle = new Text();
            PlayerCastle.Font = new Font($"{Settings.ResourcesPath}/Fonts/Arial.ttf");
            PlayerCastle.DisplayedString = PlayerHP.ToString();
            PlayerCastle.CharacterSize = 100;
            PlayerCastle.FillColor = Color.Black;
            PlayerCastle.Position = new Vector2f(Settings.Window.Size.X / 2 - PlayerCastle.GetGlobalBounds().Width / 2,
                Settings.Window.Size.Y - Deck.View.Size.Y - PlayerCastle.GetGlobalBounds().Height - 50);
            EnemyCastle = new CardView(new Vector2f(100, 150),
                new Image($"{Settings.ResourcesPath}/backcard.png"),
                new Image($"{Settings.ResourcesPath}/BaseCardIcons/castle.png"), _enemyHP, _castleDamage, 100);
            EnemyCastle.Position = new Vector2f(Settings.Window.Size.X / 2 - EnemyCastle.Size.X / 2,
                0);

            for (int i = 0; i < Deck.Size.X; i++)
            {

                Deck.Cards.Add(reciveCard());
                
            }

        }

        public void PlayerReciveCards()
        {
            for (int i = 0; i < PlayerCardsReciveCount; i++)
            {
                if (Deck.Cards.Count >= Deck.Size.X)
                    break;
                Deck.Cards.Add(reciveCard());

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

            #region. 
            isCreatingEnemy = true;
            
            #endregion
            for (int i = 0; i < Field.Size.X; i++)
            {
                if (_random.Next(2) == 1)
                    continue;

                var position = new Vector2i(i, 0);
                var card = reciveCard();
                card.State = CardState.InMap;
                if (Field.GetCardOnPosition(position) is null)
                    card.MapPosition = position;
                else
                    continue;
                
                Field.Cards.Add(card);
            }
            #region .
            isCreatingEnemy = false;
            #endregion
           

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


        public void CastleDamage()
        {
            List<Card.AbstractCard> cards = Field.GetCardsOnPosition(new Vector2i(0, 0), new Vector2i((int)Field.Size.X, 0));
            foreach(var card in cards.Where(c=> !c.IsEnemy))
            {
                card.ReceiveDamage(20);
            }


            cards = Field.GetCardsOnPosition(new Vector2i(0, (int)Field.Size.Y-1), new Vector2i((int)Field.Size.X, (int)Field.Size.Y-1));
            foreach(var card in cards.Where(c => c.IsEnemy))
            {
                card.ReceiveDamage(20);
            }
        }


        #region TestingGround

        public void PlaceCard()
        {
            for (int i = 0; i < 5; i++)
            {
                Card.AbstractCard card;
                if (i == 2)
                {
                    CardCreator creator = new CardCreator();
                    creator.Builder = new Builder.SplashBuilder();
                    card = creator.CreateCard();
                    card.State = CardState.InMap;
                }
                else
                {
                    CardCreator creator = new CardCreator();
                    creator.Builder = new Builder.CloseRangeBuilder();
                    card = creator.CreateCard();
                    card.State = CardState.InMap;
                }
               


                if (i >= 3) 
                {
                    card.IsEnemy = true;

                    card.MapPosition = new Vector2i(i-3,0);
                }
                else
                    card.MapPosition = new Vector2i(0, 3 - i);
                Field.Cards.Add(card);
            }
            
        }

        #endregion



    }
}
