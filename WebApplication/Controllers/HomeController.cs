using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using WebApplication.Core.Entities;
using WebApplication.Core.Repositories;
using WebApplication.Models;
using WebApplication.Service;
using WebApplication.Service.DTOs;
using WebApplication.Service.Interfaces;

namespace WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly WiredService _wiredService;
        private readonly IQuestionService _questionService;
        private readonly IQuizRepository _quizRepository;
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(
            ILogger<HomeController> logger, 
            WiredService wiredService,
            IQuestionService questionService,
            IQuizRepository quizRepository,
            UserManager<ApplicationUser> userManager)
        {
            _logger = logger;
            _wiredService = wiredService;
            _questionService = questionService;
            _quizRepository = quizRepository;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {

            }

            return View();
        }

        [Authorize]
        public async Task<IActionResult> List()
        {
            var quizzes = await _quizRepository.GetAll();
            return View(quizzes);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public IActionResult QuizTrial()
        {

            return View();
        }

        [HttpPost]
        public IActionResult QuizTrial(string commaSeparateIdsInActionResult)
        {
            //QuizMock quizMock = new QuizMock();
            //List<QuizMock> quizzesMockFromDb = quizMock.QuizMocks();

            //QuestionMock questionMock = new QuestionMock();
            //List<QuestionMock> questionMockFromDb = questionMock.QuestionMocks();

            //foreach (var quiz in quizzesMockFromDb)
            //{
            //    foreach (var question in questionMockFromDb)
            //    {
            //        quiz.Questions.Add(question);

            //    }
            //}

            //QuizMock quizMock = quizzesMockFromDb.FirstOrDefault();
            ////SendCorrectAnswers sendCorrectAnswers = new SendCorrectAnswers();

            ////sendCorrectAnswers.CevapSoru1_DogruOlanCevap

            ////selected'leri class'ta tutmama gerek yok zaten client side'da var.
            ////yani sadece soru cevapları yollanmalı.

            //var gelenQuestion = questionMockFromDb.FirstOrDefault();

            //var sayi = 0;
            //// Lis
            //var list = new List<SendCorrectAnswers>();
            //list.Add(new SendCorrectAnswers()
            //{
            //    SoruSirasiSayiOlarak = ++sayi,
            //    KullanicininCevabi = "A",
            //    DogruCevap = gelenQuestion.Answer
                
            //});

            return View();
        }

        //[HttpPost]
        //public IActionResult QuizTrial(int objId)
        //{

        //    return View();
        //}


        [HttpGet]
        public IActionResult SolveQuiz()
        {
            var recent5Articles = _wiredService.DownloadRecentArticles(5);
            var chosenArticle = recent5Articles.FirstOrDefault();
            ViewBag.chosenArticle = chosenArticle;
            return View();
        }

        //[HttpPost]
        //public IActionResult SolveQuiz()
        //{
        //    return View();
        //}

        private void PrepareViewData()
        {
            List<WiredArticleDto> recent5Articles = _wiredService.DownloadRecentArticles(4);
            ViewBag.recent5Articles = recent5Articles;
        }

        [Authorize]
        public IActionResult CreateQuiz()
        {
            PrepareViewData();

            var quizViewModel = new QuizViewModel
            {
                QuestionViewModels = new List<QuestionViewModel>()
            };

            for (int i = 1; i <= 2; i++)
            {
                var question = new QuestionDto();
                question.AnswerDtos = new List<AnswerDto>
                {
                    new AnswerDto(),
                    new AnswerDto(),
                    new AnswerDto(),
                    new AnswerDto()
                };

                quizViewModel.QuestionViewModels.Add(new QuestionViewModel()
                {
                    Question = question,
                    CorrectAnswerIndex = 0
                });
            }

            return View(quizViewModel);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> CreateQuiz(QuizViewModel model)
        {
            if (ModelState.IsValid)
            {
                var questionDtos = new List<QuestionDto>();
                
                foreach (QuestionViewModel questionViewModel in model.QuestionViewModels)
                {
                    AnswerDto answer = questionViewModel.Question.AnswerDtos[questionViewModel.CorrectAnswerIndex];
                    answer.IsCorrectAnswer = true;

                    questionDtos.Add(questionViewModel.Question);
                }

                var loggedInUser = await _userManager.FindByNameAsync(User.Identity.Name);

                await _questionService.SaveQuizAsync(questionDtos, model.Title, model.Content, loggedInUser.Id);

                TempData["SUCCESS"] = "Sınav başarıyla oluşturuldu!";
                return RedirectToAction("List");
            }

            PrepareViewData();
            return View(model);
        }

        [HttpGet]
        public IActionResult GetArticleByTitle(string title)
        {
            var articles = _wiredService.DownloadRecentArticles(5);

            var article = articles.FirstOrDefault(x => x.Title == title);
            if (article == null)
                return NotFound();

            return Ok(article);
        }
    }
}
