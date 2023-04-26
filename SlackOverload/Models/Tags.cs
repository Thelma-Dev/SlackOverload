namespace SlackOverload.Models
{
    public class Tags
    {
        public int Id { get; set; }

        public string TagName { get; set; }

        public virtual HashSet<QuestionTag> QuestionTags { get; set; } = new HashSet<QuestionTag>();

        public Tags() { }
    }
}
