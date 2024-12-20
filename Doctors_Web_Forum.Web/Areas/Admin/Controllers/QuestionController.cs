using Doctors_Web_Forum.BLL.IServices;
using Doctors_Web_Forum.DAL.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Doctors_Web_Forum.Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class QuestionController : Controller
    {
        private readonly IQuestionService _questionService;
        private readonly ITopicService _topicService;
        private readonly UserManager<User> _userManager;

        public QuestionController(IQuestionService questionService, ITopicService topicService, UserManager<User> userManager)
        {
            _questionService = questionService;
            _topicService = topicService;
            _userManager = userManager;
        }

        // GET: Admin/Question/Index
        public async Task<IActionResult> Index()
        {
            var questions = await _questionService.GetAllQuestionsAsync();
            return View(questions);
        }

        // GET: Admin/Question/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi này.";
                return RedirectToAction(nameof(Index));
            }

            var user = await _userManager.FindByIdAsync(question.UserId.ToString());
            ViewBag.UserName = user?.UserName ?? "Người dùng không tồn tại";

            return View(question);
        }

        // GET: Admin/Question/Create
        public async Task<IActionResult> Create()
        {
            await PopulateTopicsViewBag();
            return View();
        }

        // POST: Admin/Question/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("UserId, TopicId, QuestionText, Description")] Question question)
        {
            if (!ModelState.IsValid)
            {
                await PopulateTopicsViewBag();
                return View(question);
            }

            await _questionService.CreateQuestionAsync(question);
            TempData["SuccessMessage"] = "Câu hỏi đã được tạo thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Question/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var question = await _questionService.GetQuestionByIdAsync(id);
            if (question == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi này để chỉnh sửa.";
                return RedirectToAction(nameof(Index));
            }

            await PopulateTopicsViewBag();
            return View(question);
        }

        // POST: Admin/Question/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id, UserId, TopicId, QuestionText, Description, Status")] Question question)
        {
            if (id != question.Id)
            {
                TempData["ErrorMessage"] = "Dữ liệu không hợp lệ!";
                return RedirectToAction(nameof(Index));
            }

            if (!ModelState.IsValid)
            {
                await PopulateTopicsViewBag();
                return View(question);
            }

            var updatedQuestion = await _questionService.UpdateQuestionAsync(id, question.QuestionText, question.Description, question.TopicId);
            if (updatedQuestion == null)
            {
                TempData["ErrorMessage"] = "Không tìm thấy câu hỏi này để cập nhật.";
                return RedirectToAction(nameof(Index));
            }

            TempData["SuccessMessage"] = "Câu hỏi đã được cập nhật thành công!";
            return RedirectToAction(nameof(Index));
        }

        // GET: Admin/Question/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _questionService.DeleteQuestionAsync(id);
            if (!result)
            {
                TempData["ErrorMessage"] = "Không thể xóa câu hỏi! Câu hỏi không tồn tại hoặc đã xảy ra lỗi.";
                return RedirectToAction("Index");
            }

            TempData["SuccessMessage"] = "Câu hỏi đã được xóa thành công!";
            return RedirectToAction("Index");
        }


        // Helper: Populate ViewBag with topics
        private async Task PopulateTopicsViewBag()
        {
            const int pageNumber = 1;
            const int pageSize = 5;
            var topics = await _topicService.GetAllTopicsAsync(pageNumber, pageSize, string.Empty);
            ViewBag.Topics = topics;
        }
    }
}
