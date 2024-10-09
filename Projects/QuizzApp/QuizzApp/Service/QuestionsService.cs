namespace QuizzApp.Service;

using System.IO;
using System.Text.Json;
using Models;

public class QuestionService
{
    private const string JsonFilePath = "wwwroot/json/questions.json"; // Path to the json file

    /**
     * GetQuestions method reads the json file and deserializes it into a list of Question objects
     * @return List<Question> - List of Question objects
     */
    public List<Question> GetQuestions() 
    {
        var jsonString = File.ReadAllText(JsonFilePath);
        return JsonSerializer.Deserialize<List<Question>>(jsonString);
    }
    
    /**
     * CheckIsCorrectAnswer method checks if the answer provided by the user is correct
     * @param int questionId - The id of the question
     * @param string answer - The answer provided by the user
     * @return bool - True if the answer is correct, false otherwise
     */
    public bool CheckIsCorrectAnswer(int questionId, string answer)
    {
        var questions = GetQuestions();
        var question = questions.FirstOrDefault(q => q.Id == questionId);
        return question?.CorrectAnswer == answer;
    }
}
