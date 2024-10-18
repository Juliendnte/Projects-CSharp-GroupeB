namespace ProjectsSharp.Service;

using System.IO;
using System.Text.Json;
using Models;

public class QuestionService
{
    private const string JsonFilePath = "wwwroot/json/questions.json"; // Path to the json file
    private List<int> _Questions { get; } = new List<int>();
    public int Score { get; set; }
    /**
     * GetQuestions method reads the json file and deserializes it into a list of Question objects
     * @return List<Question> - List of Question objects
     */
    private List<Question> GetQuestions() 
    {
        var jsonString = File.ReadAllText(JsonFilePath);
        return JsonSerializer.Deserialize<List<Question>>(jsonString);
    }
    
    /**
     * GetRandomQuestion method returns a random question from the list of questions
     * @return Question - A random question
     */
    public Question GetRandomQuestion()
    {
        var questions = GetQuestions();

        if (questions == null || questions.Count == 0 || (_Questions != null && questions.Count == _Questions.Count))
            throw new InvalidOperationException("No questions available.");
    
        var question = questions[0];

        if (_Questions != null)
        {
            do
            {
                var random = new Random();

                question = questions[random.Next(questions.Count)];
            } while (_Questions.Contains(question.Id));
        }
        
        _Questions.Add(question.Id);
        
        return question;
    }
    
    /**
     * CheckIsCorrectAnswer method checks if the answer provided by the user is correct
     * @param int questionId - The id of the question
     * @param string answer - The answer provided by the user
     * @return bool - True if the answer is correct, false otherwise
     */
    public (bool, string) CheckIsCorrectAnswer(int questionId, string answer)
    {
        var questions = GetQuestions();
        var question = questions.FirstOrDefault(q => q.Id == questionId);
        bool win = question?.CorrectAnswer.Replace(" ", "") == answer;
        Score += win ? question.Niveau : -question.Niveau;
        return (win, question?.CorrectAnswer.Replace(" ", ""));
    }
    
    public void ResetQuestions()
    {
        _Questions.Clear();
    }
}
