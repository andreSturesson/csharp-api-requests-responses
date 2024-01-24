namespace exercise.wwwapi.Models
{
    public class Language
    {
        public int Id {get; set;}
        public String Name {get; set;}

        public Language(String name)
        {
            this.Name = name;
        }
    }
}
