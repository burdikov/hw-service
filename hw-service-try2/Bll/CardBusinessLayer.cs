using hw_service_try2.Bll.Interfaces;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace hw_service_try2.Bll
{
    public class CardBusinessLayer : ICardBusinessLayer
    {
        ICardRepository repository;

        public CardBusinessLayer(ICardRepository cardRepository)
        {
            repository = cardRepository;
        }

        private enum Lang
        {
            Russian,
            English
        }

        private bool IsValidWord(string word, Lang lang)
        {
            string pattern = default;
            switch (lang)
            {
                case Lang.Russian:
                    pattern = "^[А-ЯЁа-яё]+(( |-)[А-ЯЁа-яё]+)*";
                    break;
                case Lang.English:
                    pattern = "^[A-Za-z]+(( |-)[A-Za-z]+)*";
                    break;
            }
            
            if (word == string.Empty || word.Length > 30) return false;
            if (Regex.Replace(word, pattern, string.Empty) != string.Empty) return false;

            return true;
        }

        public Card Add(string rus, string eng, int? groupId)
        {
            if (!IsValidWord(rus, Lang.Russian))
                throw new ArgumentException("Field rus is not a valid russian word");
            if (!IsValidWord(eng, Lang.English))
                throw new ArgumentException("Field eng is not a valid english word");

            return repository.Create(rus,eng,groupId);
        }

        public void Update(int id, Card card)
        {
            if (!IsValidWord(card.Rus, Lang.Russian))
                throw new ArgumentException("Field rus is not a valid russian word");
            if (!IsValidWord(card.Eng, Lang.English))
                throw new ArgumentException("Field eng is not a valid english word");

            repository.Update(id, card);
        }

        public void Delete(int id) => repository.Delete(id);

        public Card Get(int id) => repository.Read(id);

        public IEnumerable<Card> GetAll() => repository.ReadAll();
    }
}