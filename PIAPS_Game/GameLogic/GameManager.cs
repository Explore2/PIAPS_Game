using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PIAPS_Game.GameLogic
{
    

    internal class GameManager
    {
        


        const int ELITEPROC = 70;


        protected Map.DeckMap _deck = new Map.DeckMap();
        protected Map.GameMap _field = new Map.GameMap();
        protected List<Builder.Builder> _builders = new List<Builder.Builder>(); 
        protected CardCreator _creator = new CardCreator();
        
        private Random _random = new Random();

        public void StartGame()
        {
            
            
            _builders.Add(new Builder.CloseRangeBuilder());
            _builders.Add(new Builder.LongRangeBuilder());
            _builders.Add(new Builder.SplashBuilder());

            for (int i = 0; i < _deck.Size; i++)
            {

                Card.AbstractCard card;
                int randomBuilder = _random.Next(_builders.Count);
                bool randomEliteCard = _random.Next(100) > ELITEPROC;

                _creator.Builder = _builders[randomBuilder];
                if (randomEliteCard)
                    card = _creator.CreateEliteCard();
                else
                    card = _creator.CreateCard();

                _deck.Cards.Add(card);
                card.AddListener(_deck);
                card.AddListener(_field);

            }



        }

    }
}
