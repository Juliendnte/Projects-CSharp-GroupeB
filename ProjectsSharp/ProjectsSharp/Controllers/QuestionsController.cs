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
        try
        {
            var questions = _questionService.GetRandomQuestion();
            return View(questions); 
        }
        catch (Exception)
        {
            return View();
        }
    }

    [HttpGet]
    public JsonResult GetScore()
    {
        return Json(new {score = _questionService.Score});
    }
    
    [HttpPost]
    public JsonResult SubmitOption([FromBody] AnswerModel answer)
    {
        var correct = _questionService.CheckIsCorrectAnswer(answer.QuestionId, answer.SelectedOption);
        return Json(new { success = true, correct = correct.Item1, correctAnswer = correct.Item2 });
    }

    public class AnswerModel
    {
        public int QuestionId { get; set; }
        public string SelectedOption { get; set; }
    }
    
    [HttpPost]
    public IActionResult ResetQuestions()
    {
        _questionService.ResetQuestions();
        _questionService.Score = 0;
        return Ok();
    }

}
