namespace ProjectsSharp.Controllers;

using Microsoft.AspNetCore.Mvc;
using Models;

public class QuestionsController : Controller
{
    private readonly QuestionModel _questionModel;

    public QuestionsController(QuestionModel questionModel)
    {
        _questionModel = questionModel;
    }

    public IActionResult Index()
    {
        try
        {
            var questions = _questionModel.GetRandomQuestion();
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
        return Json(new {score = _questionModel.Score});
    }
    
    [HttpPost]
    public JsonResult SubmitOption([FromBody] AnswerModel answer)
    {
        var correct = _questionModel.CheckIsCorrectAnswer(answer.QuestionId, answer.SelectedOption);
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
        _questionModel.ResetQuestions();
        _questionModel.Score = 0;
        return Ok();
    }

}
