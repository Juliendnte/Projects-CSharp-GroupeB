namespace QuizzApp.Controllers;

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
}
