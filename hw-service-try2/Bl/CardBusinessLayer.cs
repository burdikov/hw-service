using hw_service_try2.Bl.Interfaces;
using hw_service_try2.Common;
using hw_service_try2.Dal.Interfaces;
using hw_service_try2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web;

namespace hw_service_try2.Bl
{
    public class CardBusinessLayer : ICardBusinessLayer
    {
        ICardRepository repository;

        public CardBusinessLayer(ICardRepository cardRepository)
        {
            repository = cardRepository;
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
            
            if (word == null || word == string.Empty || word.Length > 30) return false;
            if (Regex.Replace(word, pattern, string.Empty) != string.Empty) return false;

            return true;
        }

        public Card Add(string rus, string eng, int? groupId)
        {
            if (!IsValidWord(rus, Lang.Russian))
                throw new ArgumentException("Field rus is not a valid russian word.");
            if (!IsValidWord(eng, Lang.English))
                throw new ArgumentException("Field eng is not a valid english word.");

            return repository.Create(rus,eng,groupId);
        }

        public bool Update(Card card)
        {
            if (card == null) throw new ArgumentNullException("card");
            if (!IsValidWord(card.Rus, Lang.Russian))
                throw new ArgumentException("Field rus is not a valid russian word.");
            if (!IsValidWord(card.Eng, Lang.English))
                throw new ArgumentException("Field eng is not a valid english word.");

            if (repository.Update(card) == 1) return true; else return false;
        }

        public bool Delete(int id) => repository.Delete(id) == 1 ? true : false;

        public Card Get(int id) => repository.Read(id);

        public IEnumerable<Card> GetGroup(int groupId) => repository.ReadGroup(groupId);

        public IEnumerable<Card> GetAll() => repository.ReadAll();

        public IEnumerable<int> List() => repository.List();

        public IEnumerable<Card> Get(int[] ids) => repository.Read(ids);
    }
}