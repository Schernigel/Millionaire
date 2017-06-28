using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Millionaire.Domain.Concrete;
using Millionaire.WebUI.Models;
using System.Net.Http;
using Millionaire.Domain.Entities;
using System.Web.Security;

namespace Millionaire.WebUI.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly UnitOfWork _unitOfWork;
        

        public GameController(UnitOfWork unit)
        {
            _unitOfWork = unit;
        }

        public JsonResult TakeMoney(int money,int level)
        {

            var user = GetUser();
            var totalResult = _unitOfWork.TotalRepository.GetById(user.Id);

            if (totalResult == null)
            {
                totalResult = new TotalUserResult
                {
                    UserId = user.Id,
                    GameCount = 1,
                    Name = user.Name,
                    TotalPrize = +money
                };


                _unitOfWork.TotalRepository.Insert(totalResult);

            }
            else
            {
                totalResult.GameCount++;
                totalResult.TotalPrize += money;
                _unitOfWork.TotalRepository.Update(totalResult);
            }
            var result = new Results
            {
                Call = 0,
                Data = DateTime.Now,
                FiftyFifty = 0,
                Prize = money,
                QuestionNumber = level,
                UserId = user.Id
            };
            _unitOfWork.ResultsRepository.Insert(result);

            _unitOfWork.Save();

            Response.StatusCode=200;
            return Json(Response.Status);
        }

       

        public void CreateStatisticRecord(int answerNumber, int qustionId)
        {
            var email = Session["email"];
            var user = GetUser();
           
            UserStatistics statistics = new UserStatistics
            {
                AnswerNumber = answerNumber,
                Prompt = 0,
                QuestionId = qustionId,
                UserId = user.Id
            };
            _unitOfWork.UserStatRepository.Insert(statistics);
            _unitOfWork.Save();
        }

        // GET: Game
        public ViewResult StartGame()
        {
            var email = Session["email"];
            
            if (email != null)
            {
                var user = _unitOfWork.UserRepository.Get((u => u.Email == (string) email));
                ViewBag.Name = user.Name;
                ViewBag.Id = user.Id;
            }
            else
            {
                var user = GetUser();
                ViewBag.Name = user.Name;
                ViewBag.Id = user.Id;
            }
            return View();
        }

        public JsonResult GetQuestion(int level=1)
        {
            var currentquestion =
                _unitOfWork.GameQuestionRepository.GetAll()
                    .Where(q => q.Level == level)
                    .OrderBy(q => Guid.NewGuid())
                    .First();

            GameViewModel gameViewModel = new GameViewModel()
            {
                GameQuestion = currentquestion.Question,
                First = currentquestion.Answer.First,
                Second = currentquestion.Answer.Second,
                Third = currentquestion.Answer.Third,
                Fourth = currentquestion.Answer.Fourth,
                Id = currentquestion.Id
            };
            return Json(gameViewModel, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsAnswerCorrect(int answerNumber, int id)
        {
            var correctAnswer = _unitOfWork.GameQuestionRepository.GetAll().FirstOrDefault(q => q.Id == id);

            if (answerNumber == correctAnswer?.Answer.Сorrect)
            {
                return Json("true", JsonRequestBehavior.AllowGet);
            }
            return Json("false", JsonRequestBehavior.AllowGet);
        }
        
        public ViewResult ShowResult(string url)
        {
            var user = GetUser();
            ViewBag.Name = user.Name;
            ViewBag.Id = user.Id;
            var userResults = _unitOfWork.TotalRepository.GetAll().OrderByDescending(g => g.TotalPrize);
          
            return View(userResults);
        }

        private User GetUser()
        {
                HttpCookie authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];
            if (authCookie == null)
            {
                StartGame();
            }
                FormsAuthenticationTicket ticket = FormsAuthentication.Decrypt(authCookie.Value);
                string emai = ticket.Name;
                var user = _unitOfWork.UserRepository.Get((u => u.Email == (string)emai));
            
            return user;
        }

        public JsonResult GetFiftyFifty(int questionId)
        {
            var query = _unitOfWork.UserStatRepository.GetAll().Where(q=>q.QuestionId == questionId).GroupBy(que => que.AnswerNumber)
                                                              .Select(g => new {
                                                                  g.Key,
                                                                  ans = g.Count()
                                                              }).OrderByDescending(ac=>ac.ans);

        
            var correctAnswer = _unitOfWork.GameQuestionRepository.GetAll().FirstOrDefault(q => q.Id == questionId)?.Answer.Сorrect;
            int incorrect=0;
            foreach (var item in query)
            {
                if (item.Key != correctAnswer)
                {
                    incorrect = item.Key;
                    break;
                }
            }
            do
            {
                Random rand = new Random();
                incorrect = rand.Next(0, 4);
            }
            while (incorrect ==0 || incorrect==correctAnswer);
            var res = new[] { correctAnswer, incorrect };

            return Json(res, JsonRequestBehavior.AllowGet);

        }
    }
}