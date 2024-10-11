namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Service;

public class QuestionsController : Controller
{
    private readonly QuestionService _questionService;

    public QuestionsController(QuestionService questionService)
    {
        _questionService = questionService;
    }

    public IActionResult Index()
    {
        var questions = _questionService.GetQuestions();
        return View(questions); 
    }
    
    [HttpPost]
    public JsonResult SubmitOption([FromBody] AnswerModel answer)
    {
        // Traiter la réponse
        bool isCorrect = _questionService.CheckIsCorrectAnswer(answer.QuestionId, answer.SelectedOption);

        // Retourner une réponse en JSON
        return Json(new { success = true, correct = isCorrect });
    }

    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; }
    }

}
