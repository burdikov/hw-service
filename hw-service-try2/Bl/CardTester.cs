using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Common;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace hw_service_try2.Bl
{
    public class CardTester : ICardTester
    {
        private ICardRepository repo;
        private Logger logger = LogManager.GetCurrentClassLogger();

        public CardTester(ICardRepository cardRepository)
        {
            repo = cardRepository;
        }

        public WordTest Create(int id, Lang originLang)
        {
            try
            {
                Random rnd = new Random(DateTime.Now.Millisecond);

                // get list of existing cards
                List<int> ids = repo.List().ToList();

                if (!ids.Contains(id)) throw new ArgumentException("Card with specified ID doesn't exist.");
                if (ids.Count() < 4) throw new ArgumentException("Not enough cards in the DB."); // not actually an argument ex

                // get requested card with a couple of random ones
                List<int> opts = new List<int>() { id };

                ids.Remove(id);
                for (int i = 0; i < 3; i++)
                {
                    var j = ids[rnd.Next(0, ids.Count())];
                    opts.Add(j);
                    ids.Remove(j);
                }

                var cards = repo.Read(opts.ToArray());

                // prepare WordTest object to return
                var test = new WordTest()
                {
                    CardId = id,
                    OriginLang = originLang
                };

                // find our card among all opts 
                var card = cards.Where(x => x.ID == id).ToList()[0];

                // set up Word property and fill Options
                switch (originLang)
                {
                    case Lang.Russian:
                        test.Word = card.Rus;
                        test.Options = cards.Select(x => x.Eng).ToList();
                        break;
                    case Lang.English:
                        test.Word = card.Eng;
                        test.Options = cards.Select(x => x.Rus).ToList();
                        break;
                }

                return test;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception e)
            {
                logger.Error(e);
                return null;
            }
        }

        public void Check(WordTest wordTest)
        {
            if (wordTest == null) throw new ArgumentNullException();
            if (wordTest.ChosenWord == null) throw new ArgumentException("Chosen word can not be null");

            Card card = repo.Read(wordTest.CardId);
            if (card == null)
            {
                wordTest.Result = WordTest.WordTestResult.NotChecked;
                return;
            }

            string s = null;
            switch (wordTest.OriginLang)
            {
                case Lang.English: s = card.Rus; break;
                case Lang.Russian: s = card.Eng; break;
            }
            if (wordTest.ChosenWord == s)
                wordTest.Result = WordTest.WordTestResult.Correct;
            else
                wordTest.Result = WordTest.WordTestResult.Incorrect;
        }
    }
}