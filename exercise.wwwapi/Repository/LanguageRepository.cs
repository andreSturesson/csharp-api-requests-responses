
using exercise.wwwapi.Data;
using exercise.wwwapi.Models;
using exercise.wwwapi.Models.Payload;


namespace exercise.wwwapi.Repository {

    public class LanguageRepository : ILanguageRepository
    {
        private readonly LanguageDb _languages;

        public LanguageRepository(LanguageDb languages) {
            _languages = languages;
        }

        public Language AddLanguage(string Name)
        {
            Language language = new Language(Name);
            _languages.Languages.Add(language);
            _languages.SaveChanges();
            return language;
        }

        public Language DeleteLanguage(string Name)
        {
            Language language = GetLanguage(Name);
            _languages.Languages.Remove(language);
            return language;
        }

        public List<Language> GetAllLanguages()
        {
            return _languages.Languages.ToList();
        }

        public Language GetLanguage(string Name)
        {
             var stud = _languages.Languages.FirstOrDefault(s => s.Name == Name);
             return stud;
        }

        public Language UpdateLanguage(string Name, LanguageUpdatePayload updateData)
        {
            var language = GetLanguage(Name);
            if (language == null)
            {
                return null;
            }

            bool hasUpdate = false;

            if(updateData.name != null)
            {
                language.Name = (string)updateData.name;
                hasUpdate = true;
            }

            if(!hasUpdate) throw new Exception("No update data provided");

            return language;
        }
    }
}