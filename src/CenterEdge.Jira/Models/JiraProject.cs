namespace CenterEdge.JiraLibrary.Models
{
    public class JiraProject
    {
        public string Name { get; set; }
        public string Key { get; set; }

        public override string ToString()
        {
            return $" Key:{Key}, Name:{Name}";
        }
    }
}
